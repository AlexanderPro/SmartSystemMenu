using System;
using System.Text;
using System.Runtime.InteropServices;

namespace SmartSystemMenu.App_Code.Common
{
    [StructLayout(LayoutKind.Sequential)]
    struct Rect
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

    enum PriorityClass : uint
    {
        ABOVE_NORMAL_PRIORITY_CLASS = 0x8000,
        BELOW_NORMAL_PRIORITY_CLASS = 0x4000,
        HIGH_PRIORITY_CLASS = 0x80,
        IDLE_PRIORITY_CLASS = 0x40,
        NORMAL_PRIORITY_CLASS = 0x20,
        PROCESS_MODE_BACKGROUND_BEGIN = 0x100000,
        PROCESS_MODE_BACKGROUND_END = 0x200000,
        REALTIME_PRIORITY_CLASS = 0x100
    }

    enum Priority :int
    {
        RealTime = 24,
        High = 13,
        AboveNormal = 10,
        Normal = 8,
        BelowNormal = 6,
        Idle = 4
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    struct MenuItemInfo
    {
        public UInt32 cbSize;
        public UInt32 fMask;
        public UInt32 fType;
        public UInt32 fState;
        public UInt32 wID;
        public IntPtr hSubMenu;
        public IntPtr hbmpChecked;
        public IntPtr hbmpUnchecked;
        public UInt32 dwItemData;
        public String dwTypeData;
        public UInt32 cch;
    }
}
