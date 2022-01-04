using System;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SmartSystemMenu.Native;
using static SmartSystemMenu.Native.NativeMethods;
using static SmartSystemMenu.Native.NativeConstants;

namespace SmartSystemMenu.Extensions
{
    static class ProcessExtensions
    {
        public static IntPtr GetHandle(this Process process)
        {
            var hProcess = Environment.OSVersion.Version.Major >= 6 ?
                OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION | PROCESS_SET_INFORMATION, false, process.Id) :
                OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_SET_INFORMATION, false, process.Id);
            return hProcess;
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
                return QueryFullProcessImageName(process.GetHandle(), 0, fileNameBuilder, ref bufferLength) ? fileNameBuilder.ToString() : null;
            }
        }

        public static IList<IntPtr> GetWindowHandles(this Process process)
        {
            var handles = new List<IntPtr>();
            foreach (ProcessThread thread in process.Threads)
            {
                EnumThreadWindows(thread.Id, (hwnd, lParam) => { handles.Add(hwnd); return true; }, 0);
            }
            return handles;
        }

        public static Process GetParentProcess(this Process process)
        {
            var pbi = new PROCESS_BASIC_INFORMATION();
            int returnLength;
            var status = NtQueryInformationProcess(process.GetHandle(), 0, ref pbi, Marshal.SizeOf(pbi), out returnLength);
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

        public static void Suspend(this Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }
                SuspendThread(pOpenThread);
                CloseHandle(pOpenThread);
            }
        }

        public static void Resume(this Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }

                var suspendCount = 0;
                do
                {
                    suspendCount = ResumeThread(pOpenThread);
                } while (suspendCount > 0);

                CloseHandle(pOpenThread);
            }
        }

        public static bool IsSuspended(this Process process)
        {
            return process.Threads[0].ThreadState == ThreadState.Wait
                && process.Threads[0].WaitReason == ThreadWaitReason.Suspended;
        }
    }
}
