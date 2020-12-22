using System;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Management;
using System.Linq;
using SmartSystemMenu.Native;

namespace SmartSystemMenu.Extensions
{
    static class ProcessExtensions
    {
        public static bool ExistProcessWithSameNameAndDesktop(this Process currentProcess)
        {
            foreach (Process process in Process.GetProcessesByName(currentProcess.ProcessName))
            {
                if (currentProcess.Id != process.Id)
                {
                    int processThreadId = process.GetMainThreadId();
                    int currentProcessThreadId = currentProcess.GetMainThreadId();
                    IntPtr processDesktop = NativeMethods.GetThreadDesktop(processThreadId);
                    IntPtr currentProcessDesktop = NativeMethods.GetThreadDesktop(currentProcessThreadId);
                    if (currentProcessDesktop == processDesktop) return true;
                }
            }
            return false;
        }

        public static int GetMainThreadId(this Process currentProcess)
        {
            var mainThreadId = -1;
            var startTime = DateTime.MaxValue;
            foreach (ProcessThread thread in currentProcess.Threads)
            {
                if (thread.StartTime < startTime)
                {
                    startTime = thread.StartTime;
                    mainThreadId = thread.Id;
                }
            }
            return mainThreadId;
        }

        public static IntPtr GetHandle(this Process currentProcess)
        {
            var handle = Environment.OSVersion.Version.Major >= 6 ? NativeMethods.OpenProcess(NativeConstants.PROCESS_QUERY_LIMITED_INFORMATION | NativeConstants.PROCESS_SET_INFORMATION, false, currentProcess.Id) :
                                                                    NativeMethods.OpenProcess(NativeConstants.PROCESS_QUERY_INFORMATION | NativeConstants.PROCESS_SET_INFORMATION, false, currentProcess.Id);
            return handle;
        }

        public static string GetMainModuleFileName(this Process process, int buffer = 1024)
        {
            try
            {
                return process.MainModule.FileName;
            }
            catch
            {
                var fileNameBuilder = new StringBuilder(buffer);
                var bufferLength = (uint)fileNameBuilder.Capacity + 1;
                return NativeMethods.QueryFullProcessImageName(process.Handle, 0, fileNameBuilder, ref bufferLength) ? fileNameBuilder.ToString() : null;
            }
        }

        public static IList<IntPtr> GetWindowHandles(this Process process)
        {
            var handles = new List<IntPtr>();
            foreach (ProcessThread thread in process.Threads)
            {
                NativeMethods.EnumThreadWindows(thread.Id, (hwnd, lParam) => { handles.Add(hwnd); return true; }, 0);
            }
            return handles;
        }

        public static string GetProcessUser(this Process process)
        {
            IntPtr processHandle = IntPtr.Zero;
            try
            {
                NativeMethods.OpenProcessToken(process.Handle, 8, ref processHandle);
                var wi = new WindowsIdentity(processHandle);
                string user = wi.Name;
                return user.Contains(@"\") ? user.Substring(user.IndexOf(@"\") + 1) : user;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (processHandle != IntPtr.Zero)
                {
                    NativeMethods.CloseHandle(processHandle);
                }
            }
        }


        public static Process GetParentProcess(this Process process)
        {
            var pbi = new PROCESS_BASIC_INFORMATION();
            int returnLength;
            var status = NativeMethods.NtQueryInformationProcess(process.Handle, 0, ref pbi, Marshal.SizeOf(pbi), out returnLength);
            if (status != 0)
            {
                return null;
            }

            try
            {
                return Process.GetProcessById(pbi.InheritedFromUniqueProcessId.ToInt32());
            }
            catch
            {
                return null;
            }
        }

        public static string GetCommandLine(this Process process)
        {
            using (var searcher = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))
            using (var objects = searcher.Get())
            {
                var baseObject = objects.Cast<ManagementBaseObject>().FirstOrDefault();
                return baseObject != null && baseObject["CommandLine"] != null ? baseObject["CommandLine"].ToString() : "";
            }
        }
    }
}
