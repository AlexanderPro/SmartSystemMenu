using System;
using System.Diagnostics;

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
            int mainThreadId = -1;
            DateTime startTime = DateTime.MaxValue;
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
            IntPtr handle = Environment.OSVersion.Version.Major >= 6 ? NativeMethods.OpenProcess(NativeConstants.PROCESS_QUERY_LIMITED_INFORMATION | NativeConstants.PROCESS_SET_INFORMATION, false, currentProcess.Id) :
                                                                       NativeMethods.OpenProcess(NativeConstants.PROCESS_QUERY_INFORMATION | NativeConstants.PROCESS_SET_INFORMATION, false, currentProcess.Id);
            return handle;
        }
    }
}
