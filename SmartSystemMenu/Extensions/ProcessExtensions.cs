using System;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using SmartSystemMenu.Native;

namespace SmartSystemMenu.Extensions
{
    static class ProcessExtensions
    {
        //public static readonly bool Is64BitProcess = IntPtr.Size > 4;
        //public static readonly bool Is64BitOperatingSystem = Is64BitProcess || Is64BitChecker.InternalCheckIsWow64();

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

        /*public static string GetCurrentDirectory(this Process process)
        {
            return GetProcessParametersString(process.Id, PEB_OFFSET.CurrentDirectory);
        }

        public static string GetCommandLine(this Process process)
        {
            return GetProcessParametersString(process.Id, PEB_OFFSET.CommandLine);
        }

        private static string GetProcessParametersString(int processId, PEB_OFFSET Offset)
        {
            IntPtr handle = NativeMethods.OpenProcess(NativeConstants.PROCESS_QUERY_INFORMATION | NativeConstants.PROCESS_VM_READ, false, processId);
            if (handle == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            bool IsWow64Process = Is64BitChecker.InternalCheckIsWow64();
            bool IsTargetWow64Process = Is64BitChecker.GetProcessIsWow64(handle);
            bool IsTarget64BitProcess = Is64BitOperatingSystem && !IsTargetWow64Process;

            long offset = 0;
            long processParametersOffset = IsTarget64BitProcess ? 0x20 : 0x10;
            switch (Offset)
            {
                case PEB_OFFSET.CurrentDirectory:
                    offset = IsTarget64BitProcess ? 0x38 : 0x24;
                    break;
                case PEB_OFFSET.CommandLine:
                default:
                    return null;
            }

            try
            {
                long pebAddress = 0;
                if (IsTargetWow64Process) // OS : 64Bit, Cur : 32 or 64, Tar: 32bit
                {
                    IntPtr peb32 = new IntPtr();

                    int hr = NativeMethods.NtQueryInformationProcess(handle, (int)PROCESSINFOCLASS.ProcessWow64Information, ref peb32, IntPtr.Size, IntPtr.Zero);
                    if (hr != 0) throw new Win32Exception(hr);
                    pebAddress = peb32.ToInt64();

                    IntPtr pp = new IntPtr();
                    if (!NativeMethods.ReadProcessMemory(handle, new IntPtr(pebAddress + processParametersOffset), ref pp, new IntPtr(Marshal.SizeOf(pp)), IntPtr.Zero))
                        throw new Win32Exception(Marshal.GetLastWin32Error());

                    UNICODE_STRING_32 us = new UNICODE_STRING_32();
                    if (!NativeMethods.ReadProcessMemory(handle, new IntPtr(pp.ToInt64() + offset), ref us, new IntPtr(Marshal.SizeOf(us)), IntPtr.Zero))
                        throw new Win32Exception(Marshal.GetLastWin32Error());

                    if ((us.Buffer == 0) || (us.Length == 0))
                        return null;

                    string s = new string('\0', us.Length / 2);
                    if (!NativeMethods.ReadProcessMemory(handle, new IntPtr(us.Buffer), s, new IntPtr(us.Length), IntPtr.Zero))
                        throw new Win32Exception(Marshal.GetLastWin32Error());

                    return s;
                }
                else if (IsWow64Process)//Os : 64Bit, Cur 32, Tar 64
                {
                    PROCESS_BASIC_INFORMATION_WOW64 pbi = new PROCESS_BASIC_INFORMATION_WOW64();
                    int hr = NativeMethods.NtWow64QueryInformationProcess64(handle, (int)PROCESSINFOCLASS.ProcessBasicInformation, ref pbi, Marshal.SizeOf(pbi), IntPtr.Zero);
                    if (hr != 0) throw new Win32Exception(hr);
                    pebAddress = pbi.PebBaseAddress;

                    long pp = 0;
                    hr = NativeMethods.NtWow64ReadVirtualMemory64(handle, pebAddress + processParametersOffset, ref pp, Marshal.SizeOf(pp), IntPtr.Zero);
                    if (hr != 0)
                        throw new Win32Exception(hr);

                    UNICODE_STRING_WOW64 us = new UNICODE_STRING_WOW64();
                    hr = NativeMethods.NtWow64ReadVirtualMemory64(handle, pp + offset, ref us, Marshal.SizeOf(us), IntPtr.Zero);
                    if (hr != 0)
                        throw new Win32Exception(hr);

                    if ((us.Buffer == 0) || (us.Length == 0))
                        return null;

                    string s = new string('\0', us.Length / 2);
                    hr = NativeMethods.NtWow64ReadVirtualMemory64(handle, us.Buffer, s, us.Length, IntPtr.Zero);
                    if (hr != 0)
                        throw new Win32Exception(hr);

                    return s;
                }
                else// Os,Cur,Tar : 64 or 32
                {
                    PROCESS_BASIC_INFORMATION pbi = new PROCESS_BASIC_INFORMATION();
                    int hr = NativeMethods.NtQueryInformationProcess(handle, (int)PROCESSINFOCLASS.ProcessBasicInformation, ref pbi, Marshal.SizeOf(pbi), IntPtr.Zero);
                    if (hr != 0) throw new Win32Exception(hr);
                    pebAddress = pbi.PebBaseAddress.ToInt64();

                    IntPtr pp = new IntPtr();
                    if (!NativeMethods.ReadProcessMemory(handle, new IntPtr(pebAddress + processParametersOffset), ref pp, new IntPtr(Marshal.SizeOf(pp)), IntPtr.Zero))
                        throw new Win32Exception(Marshal.GetLastWin32Error());

                    UNICODE_STRING us = new UNICODE_STRING();
                    if (!NativeMethods.ReadProcessMemory(handle, new IntPtr((long)pp + offset), ref us, new IntPtr(Marshal.SizeOf(us)), IntPtr.Zero))
                        throw new Win32Exception(Marshal.GetLastWin32Error());

                    if ((us.Buffer == IntPtr.Zero) || (us.Length == 0))
                        return null;

                    string s = new string('\0', us.Length / 2);
                    if (!NativeMethods.ReadProcessMemory(handle, us.Buffer, s, new IntPtr(us.Length), IntPtr.Zero))
                        throw new Win32Exception(Marshal.GetLastWin32Error());

                    return s;
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            finally
            {
                NativeMethods.CloseHandle(handle);
            }
        }

        private class Is64BitChecker
        {
            [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool IsWow64Process(
                [In] IntPtr hProcess,
                [Out] out bool wow64Process
            );

            public static bool GetProcessIsWow64(IntPtr hProcess)
            {
                if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) ||
                    Environment.OSVersion.Version.Major >= 6)
                {
                    bool retVal;
                    if (!IsWow64Process(hProcess, out retVal))
                    {
                        return false;
                    }
                    return retVal;
                }
                else
                {
                    return false;
                }
            }

            public static bool InternalCheckIsWow64()
            {
                if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) ||
                    Environment.OSVersion.Version.Major >= 6)
                {
                    using (Process p = Process.GetCurrentProcess())
                    {
                        bool retVal;
                        if (!IsWow64Process(p.Handle, out retVal))
                        {
                            return false;
                        }
                        return retVal;
                    }
                }
                else
                {
                    return false;
                }
            }
        }*/
    }
}
