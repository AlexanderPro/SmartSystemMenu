using System;
using System.Runtime.InteropServices;

namespace SmartSystemMenu.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    struct CURSORINFO
    {
        public Int32 cbSize;
        public Int32 flags;
        public IntPtr hCursor;
        public POINTAPI ptScreenPos;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct POINTAPI
    {
        public int x;
        public int y;
    }
}
