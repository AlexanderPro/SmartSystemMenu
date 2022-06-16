using System;
using System.Runtime.InteropServices;
using SmartSystemMenu.Native.Structs;

namespace SmartSystemMenu.Native
{
    static class Ntdll
    {
        [DllImport("ntdll.dll")]
        public static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass, ref PROCESS_BASIC_INFORMATION pbi, int processInformationLength, out int returnLength);
    }
}
