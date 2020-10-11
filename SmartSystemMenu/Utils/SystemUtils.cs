using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using SmartSystemMenu.Native;
using SmartSystemMenu.Extensions;

namespace SmartSystemMenu
{
    static class SystemUtils
    {
        public static bool IsWow64Process(int pId)
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) || Environment.OSVersion.Version.Major >= 6)
            {
                var process = GetProcessByIdSafely(pId);
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

        public static IList<IntPtr> GetMonitors()
        {
            var monitors = new List<IntPtr>();
            NativeMethods.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, (IntPtr hMonitor, IntPtr hdcMonitor, ref Rect rect, IntPtr data) =>
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

        public static void RunAsDesktopUser(string fileName, string arguments)
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
                var process = NativeMethods.GetCurrentProcess();
                if (!NativeMethods.OpenProcessToken(process, 0x0020, ref hProcessToken))
                {
                    return;
                }

                var tkp = new TOKEN_PRIVILEGES
                {
                    PrivilegeCount = 1,
                    Privileges = new LUID_AND_ATTRIBUTES[1]
                };

                if (!NativeMethods.LookupPrivilegeValue(null, "SeIncreaseQuotaPrivilege", ref tkp.Privileges[0].Luid))
                {
                    return;
                }

                tkp.Privileges[0].Attributes = 0x00000002;

                if (!NativeMethods.AdjustTokenPrivileges(hProcessToken, false, ref tkp, 0, IntPtr.Zero, IntPtr.Zero))
                {
                    return;
                }
            }
            finally
            {
                NativeMethods.CloseHandle(hProcessToken);
            }

            // Get an HWND representing the desktop shell.
            // CAVEATS:  This will fail if the shell is not running (crashed or terminated), or the default shell has been
            // replaced with a custom shell.  This also won't return what you probably want if Explorer has been terminated and
            // restarted elevated.
            var hwnd = NativeMethods.GetShellWindow();
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
                if (NativeMethods.GetWindowThreadProcessId(hwnd, out dwPID) == 0)
                {
                    return;
                }

                // Open the desktop shell process in order to query it (get the token)
                hShellProcess = NativeMethods.OpenProcess(ProcessAccessFlags.QueryInformation, false, dwPID);
                if (hShellProcess == IntPtr.Zero)
                {
                    return;
                }

                // Get the process token of the desktop shell.
                if (!NativeMethods.OpenProcessToken(hShellProcess, 0x0002, ref hShellProcessToken))
                {
                    return;
                }

                var dwTokenRights = 395U;

                // Duplicate the shell's process token to get a primary token.
                // Based on experimentation, this is the minimal set of rights required for CreateProcessWithTokenW (contrary to current documentation).
                if (!NativeMethods.DuplicateTokenEx(hShellProcessToken, dwTokenRights, IntPtr.Zero, SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation, TOKEN_TYPE.TokenPrimary, out hPrimaryToken))
                {
                    return;
                }

                // Start the target process with the new token.
                var si = new STARTUPINFO();
                var pi = new PROCESS_INFORMATION();

                foreach (var fullFileName in GetFullPaths(fileName))
                {
                    var commandLine = string.Format("\"{0}\" {1}", fullFileName, arguments);
                    if (NativeMethods.CreateProcessWithTokenW(hPrimaryToken, 0, null, commandLine, 0, IntPtr.Zero, Path.GetDirectoryName(fullFileName), ref si, out pi))
                    {
                        break;
                    }
                }
            }
            finally
            {
                NativeMethods.CloseHandle(hShellProcessToken);
                NativeMethods.CloseHandle(hPrimaryToken);
                NativeMethods.CloseHandle(hShellProcess);
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
    }
}