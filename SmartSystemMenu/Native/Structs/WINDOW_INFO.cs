using System.Runtime.InteropServices;

namespace SmartSystemMenu.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    struct WINDOW_INFO
    {
        public int cbSize;
        public Rect rcWindow;
        public Rect rcClient;
        public uint dwStyle;
        public uint dwExStyle;
        public uint dwWindowStatus;
        public int cxWindowBorders;
        public int cyWindowBorders;
        public ushort atomWindowType;
        public ushort wCreatorVersion;
    }
}
