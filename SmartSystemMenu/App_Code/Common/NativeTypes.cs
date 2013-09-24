using System;
using System.Text;
using System.Runtime.InteropServices;

namespace SmartSystemMenu.App_Code.Common
{
    [StructLayout(LayoutKind.Sequential)]
    struct RECT
    {
        public Int32 Left;
        public Int32 Top;
        public Int32 Right;
        public Int32 Bottom;

        public Int32 Width { get { return Right - Left; } }
        public Int32 Height { get { return Bottom - Top; } }
    }

    enum WindowShowStyle : uint
    {
        Hide = 0,
        ShowNormal = 1,
        Normal = 1,
        ShowMinimized = 2,
        ShowMaximized = 3,
        Maximize = 3,
        ShowNoActivate = 4,
        Show = 5,
        Minimize = 6,
        ShowMinNoActive = 7,
        ShowNa = 8,
        Restore = 9,
        ShowDefault = 10,
        ForceMinimize = 11,
        Max = 11
    }

    [Flags]
    enum SendMessageTimeoutFlags : uint
    {
        SMTO_NORMAL = 0x0000,
        SMTO_BLOCK = 0x0001,
        SMTO_ABORTIFHUNG = 0x0002,
        SMTO_NOTIMEOUTIFNOTHUNG = 0x0008,
    }
}
