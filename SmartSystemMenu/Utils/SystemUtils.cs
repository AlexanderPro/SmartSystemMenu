using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Management;
using Microsoft.Win32;
using SmartSystemMenu.Native;
using SmartSystemMenu.Extensions;
using static SmartSystemMenu.Native.NativeMethods;
using static SmartSystemMenu.Native.NativeConstants;

namespace SmartSystemMenu
{
    static class SystemUtils
    {
        public static bool IsWow64Process(int processId)
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) || Environment.OSVersion.Version.Major >= 6)
            {
                var process = GetProcessByIdSafely(processId);
                if (process != null)
                {
                    bool retVal;
                    if (!NativeMethods.IsWow64Process(process.GetHandle(), out retVal))
                    {
                        return false;
                    }
                    return retVal;
                }
            }
            return false;
        }

        public static WmiProcessInfo GetWmiProcessInfo(int processId)
        {
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Process WHERE ProcessId = " + processId))
            using (var objects = searcher.Get())
            {
                var processInfo = new WmiProcessInfo();
                foreach (ManagementObject obj in objects)
                {
                    var argList = new string[] { string.Empty, string.Empty };
                    var returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
                    if (returnVal == 0)
                    {
                        // return DOMAIN\user
                        processInfo.Owner = argList[1] + "\\" + argList[0];
                        break;
                    }
                }

                var baseObject = objects.Cast<ManagementBaseObject>().FirstOrDefault();
                if (baseObject != null)
                {
                    processInfo.CommandLine = baseObject["CommandLine"] != null ? baseObject["CommandLine"].ToString() : "";
                    processInfo.HandleCount = baseObject["HandleCount"] != null ? (uint)baseObject["HandleCount"] : 0;
                    processInfo.ThreadCount = baseObject["ThreadCount"] != null ? (uint)baseObject["ThreadCount"] : 0;
                    processInfo.VirtualSize = baseObject["VirtualSize"] != null ? (ulong)baseObject["VirtualSize"] : 0;
                    processInfo.WorkingSetSize = baseObject["WorkingSetSize"] != null ? (ulong)baseObject["WorkingSetSize"] : 0;
                }

                return processInfo;
            }
        }

        public static IList<IntPtr> GetMonitors()
        {
            var monitors = new List<IntPtr>();
            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, (IntPtr hMonitor, IntPtr hdcMonitor, ref Rect rect, IntPtr data) =>
            {
                monitors.Add(hMonitor);
                return true;
            }, IntPtr.Zero);
            return monitors;
        }

        public static Process GetProcessByIdSafely(int processId)
        {
            try
            {
                return Process.GetProcessById(processId);
            }
            catch
            {
                return null;
            }
        }

        public static void RunAs(string fileName, string arguments, bool showWindow, UserType userType)
        {
            if (userType == UserType.Normal)
            {
                RunAsDesktopUser(fileName, arguments, showWindow);
            }
            else
            {
                foreach (var fullFileName in GetFullPaths(fileName))
                {
                    var process = new Process();
                    process.StartInfo.FileName = fullFileName;
                    process.StartInfo.Arguments = arguments;
                    process.StartInfo.WorkingDirectory = Path.GetDirectoryName(fullFileName);
                    if (!showWindow)
                    {
                        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        process.StartInfo.CreateNoWindow = true;
                    }
                    process.Start();
                }
            }
        }


        private static void RunAsDesktopUser(string fileName, string arguments, bool showWindow)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(fileName));
            }

            // To start process as shell user you will need to carry out these steps:
            // 1. Enable the SeIncreaseQuotaPrivilege in your current token
            // 2. Get an HWND representing the desktop shell (GetShellWindow)
            // 3. Get the Process ID(PID) of the process associated with that window(GetWindowThreadProcessId)
            // 4. Open that process(OpenProcess)
            // 5. Get the access token from that process (OpenProcessToken)
            // 6. Make a primary token with that token(DuplicateTokenEx)
            // 7. Start the new process with that primary token(CreateProcessWithTokenW)

            var hProcessToken = IntPtr.Zero;
            // Enable SeIncreaseQuotaPrivilege in this process.  (This won't work if current process is not elevated.)
            try
            {
                var process = GetCurrentProcess();
                if (!OpenProcessToken(process, 0x0020, ref hProcessToken))
                {
                    return;
                }

                var tkp = new TOKEN_PRIVILEGES
                {
                    PrivilegeCount = 1,
                    Privileges = new LUID_AND_ATTRIBUTES[1]
                };

                if (!LookupPrivilegeValue(null, "SeIncreaseQuotaPrivilege", ref tkp.Privileges[0].Luid))
                {
                    return;
                }

                tkp.Privileges[0].Attributes = 0x00000002;

                if (!AdjustTokenPrivileges(hProcessToken, false, ref tkp, 0, IntPtr.Zero, IntPtr.Zero))
                {
                    return;
                }
            }
            finally
            {
                CloseHandle(hProcessToken);
            }

            // Get an HWND representing the desktop shell.
            // CAVEATS:  This will fail if the shell is not running (crashed or terminated), or the default shell has been
            // replaced with a custom shell.  This also won't return what you probably want if Explorer has been terminated and
            // restarted elevated.
            var hwnd = GetShellWindow();
            if (hwnd == IntPtr.Zero)
            {
                return;
            }

            var hShellProcess = IntPtr.Zero;
            var hShellProcessToken = IntPtr.Zero;
            var hPrimaryToken = IntPtr.Zero;
            try
            {
                // Get the PID of the desktop shell process.
                int dwPID;
                if (GetWindowThreadProcessId(hwnd, out dwPID) == 0)
                {
                    return;
                }

                // Open the desktop shell process in order to query it (get the token)
                hShellProcess = OpenProcess(ProcessAccessFlags.QueryInformation, false, dwPID);
                if (hShellProcess == IntPtr.Zero)
                {
                    return;
                }

                // Get the process token of the desktop shell.
                if (!OpenProcessToken(hShellProcess, 0x0002, ref hShellProcessToken))
                {
                    return;
                }

                var dwTokenRights = 395U;

                // Duplicate the shell's process token to get a primary token.
                // Based on experimentation, this is the minimal set of rights required for CreateProcessWithTokenW (contrary to current documentation).
                if (!DuplicateTokenEx(hShellProcessToken, dwTokenRights, IntPtr.Zero, SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation, TOKEN_TYPE.TokenPrimary, out hPrimaryToken))
                {
                    return;
                }

                // Start the target process with the new token.
                var si = new STARTUPINFO();
                var pi = new PROCESS_INFORMATION();

                foreach (var fullFileName in GetFullPaths(fileName))
                {
                    var commandLine = string.Format("\"{0}\" {1}", fullFileName, arguments);
                    if (!showWindow)
                    {
                        si.wShowWindow = SW_HIDE;
                        si.dwFlags = STARTF_USESHOWWINDOW;
                    }
                    if (CreateProcessWithTokenW(hPrimaryToken, 0, fullFileName, commandLine, showWindow ? 0 : CREATE_NO_WINDOW, IntPtr.Zero, Path.GetDirectoryName(fullFileName), ref si, out pi))
                    {
                        break;
                    }
                }
            }
            finally
            {
                CloseHandle(hShellProcessToken);
                CloseHandle(hPrimaryToken);
                CloseHandle(hShellProcess);
            }
        }

        public static string GetDefaultBrowserModuleName()
        {
            var browserName = "iexplore.exe";
            using (var userChoiceKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\http\\UserChoice"))
            {
                if (userChoiceKey == null)
                {
                    return browserName;
                }

                var progIdValue = userChoiceKey.GetValue("Progid");
                if (progIdValue == null)
                {
                    return browserName;
                }

                var progId = progIdValue.ToString();
                var path = progId + "\\shell\\open\\command";
                using (var pathKey = Registry.ClassesRoot.OpenSubKey(path))
                {
                    if (pathKey == null)
                    {
                        return browserName;
                    }

                    try
                    {
                        path = pathKey.GetValue(null).ToString().ToLower().Replace("\"", "");
                        const string exeSuffix = ".exe";
                        if (!path.EndsWith(exeSuffix))
                        {
                            path = path.Substring(0, path.LastIndexOf(exeSuffix, StringComparison.Ordinal) + exeSuffix.Length);
                        }
                        return path;
                    }
                    catch
                    {
                        return browserName;
                    }
                }
            }
        }

        private static List<string> GetFullPaths(string fileName)
        {
            if (File.Exists(fileName))
            {
                return new List<string> { Path.GetFullPath(fileName) };
            }

            var fullPaths = new List<string>();
            var values = Environment.GetEnvironmentVariable("PATH");
            foreach (var path in values.Split(Path.PathSeparator))
            {
                var fullPath = Path.Combine(path, fileName);
                if (File.Exists(fullPath))
                {
                    fullPaths.Add(fullPath);
                }
            }
            return fullPaths;
        }

        public static bool TerminateProcess(int processId, uint exitCode)
        {
            var hProcess = OpenProcess(PROCESS_TERMINATE, false, processId);
            if (hProcess != IntPtr.Zero)
            {
                try
                {
                    return NativeMethods.TerminateProcess(hProcess, exitCode);
                }
                catch
                {
                    CloseHandle(hProcess);
                }
            }
            return false;
        }

        public static void EnableHighDPISupport()
        {
            if (Environment.OSVersion.Version >= new Version(6, 3, 0)) // win 8.1 added support for per monitor dpi
            {
                if (Environment.OSVersion.Version >= new Version(10, 0, 15063)) // win 10 creators update added support for per monitor v2
                {
                    SetProcessDpiAwarenessContext((int)DPI_AWARENESS_CONTEXT.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
                }
                else
                {
                    SetProcessDpiAwareness(PROCESS_DPI_AWARENESS.Process_Per_Monitor_DPI_Aware);
                }
            }
            else
            {
                SetProcessDPIAware();
            }
        }
    }
}