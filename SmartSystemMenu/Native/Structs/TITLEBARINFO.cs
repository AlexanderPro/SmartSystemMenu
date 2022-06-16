using System.Runtime.InteropServices;

namespace SmartSystemMenu.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    struct TITLEBARINFO
    {
        public const int CCHILDREN_TITLEBAR = 5;
        public uint cbSize;
        public Rect rcTitleBar;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CCHILDREN_TITLEBAR + 1)]
        public uint[] rgstate;
    }
}
