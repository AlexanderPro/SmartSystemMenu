using System;
using System.Runtime.InteropServices;
using SmartSystemMenu.Native.Enums;

namespace SmartSystemMenu.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    struct MenuItemInfo
    {
        public uint cbSize;
        public MIIM fMask;
        public uint fType;
        public uint fState;
        public uint wID;
        public IntPtr hSubMenu;
        public IntPtr hbmpChecked;
        public IntPtr hbmpUnchecked;
        public uint dwItemData;
        public string dwTypeData;
        public uint cch;
    }
}
