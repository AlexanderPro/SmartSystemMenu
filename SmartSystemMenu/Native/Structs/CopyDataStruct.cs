using System;
using System.Runtime.InteropServices;

namespace SmartSystemMenu.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    struct CopyDataStruct
    {
        public IntPtr dwData;
        public int cbData;
        public IntPtr lpData;
    }
}
