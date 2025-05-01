using System;
using System.Runtime.InteropServices;

namespace SmartSystemMenu.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    struct ICONINFO
    {
        public bool fIcon;
        public Int32 xHotspot;
        public Int32 yHotspot;
        public IntPtr hbmMask;
        public IntPtr hbmColor;
    }
}
