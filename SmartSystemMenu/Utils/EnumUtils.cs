using System;
using SmartSystemMenu.Native.Enums;

namespace SmartSystemMenu.Utils
{
    static class EnumUtils
    {
        public static Priority GetPriority(int menuItemId) => menuItemId switch
        {
            MenuItemId.SC_PRIORITY_REAL_TIME => Priority.RealTime,
            MenuItemId.SC_PRIORITY_HIGH => Priority.High,
            MenuItemId.SC_PRIORITY_ABOVE_NORMAL => Priority.AboveNormal,
            MenuItemId.SC_PRIORITY_NORMAL => Priority.Normal,
            MenuItemId.SC_PRIORITY_BELOW_NORMAL => Priority.BelowNormal,
            MenuItemId.SC_PRIORITY_IDLE => Priority.Idle,
            _ => throw new ArgumentException(nameof(menuItemId))
        };

        public static WindowAlignment GetWindowAlignment(int menuItemId) => menuItemId switch
        {
            MenuItemId.SC_ALIGN_TOP_LEFT => WindowAlignment.TopLeft,
            MenuItemId.SC_ALIGN_TOP_CENTER => WindowAlignment.TopCenter,
            MenuItemId.SC_ALIGN_TOP_RIGHT => WindowAlignment.TopRight,
            MenuItemId.SC_ALIGN_MIDDLE_LEFT => WindowAlignment.MiddleLeft,
            MenuItemId.SC_ALIGN_MIDDLE_CENTER => WindowAlignment.MiddleCenter,
            MenuItemId.SC_ALIGN_MIDDLE_RIGHT => WindowAlignment.MiddleRight,
            MenuItemId.SC_ALIGN_BOTTOM_LEFT => WindowAlignment.BottomLeft,
            MenuItemId.SC_ALIGN_BOTTOM_CENTER => WindowAlignment.BottomCenter,
            MenuItemId.SC_ALIGN_BOTTOM_RIGHT => WindowAlignment.BottomRight,
            MenuItemId.SC_ALIGN_CENTER_HORIZONTALLY => WindowAlignment.CenterHorizontally,
            MenuItemId.SC_ALIGN_CENTER_VERTICALLY => WindowAlignment.CenterVertically,
            _ => throw new ArgumentException(nameof(menuItemId))
        };

        public static int GetTransparency(int menuItemId) => menuItemId switch
        {
            MenuItemId.SC_TRANS_00 => 0,
            MenuItemId.SC_TRANS_10 => 10,
            MenuItemId.SC_TRANS_20 => 20,
            MenuItemId.SC_TRANS_30 => 30,
            MenuItemId.SC_TRANS_40 => 40,
            MenuItemId.SC_TRANS_50 => 50,
            MenuItemId.SC_TRANS_60 => 60,
            MenuItemId.SC_TRANS_70 => 70,
            MenuItemId.SC_TRANS_80 => 80,
            MenuItemId.SC_TRANS_90 => 90,
            MenuItemId.SC_TRANS_100 => 100,
            _ => throw new ArgumentException(nameof(menuItemId))
        };

        public static int? GetTransparencyMenuItemId(int transparency) => transparency switch
        {
            0 => MenuItemId.SC_TRANS_00,
            10 => MenuItemId.SC_TRANS_10,
            20 => MenuItemId.SC_TRANS_20,
            30 => MenuItemId.SC_TRANS_30,
            40 => MenuItemId.SC_TRANS_40,
            50 => MenuItemId.SC_TRANS_50,
            60 => MenuItemId.SC_TRANS_60,
            70 => MenuItemId.SC_TRANS_70,
            80 => MenuItemId.SC_TRANS_80,
            90 => MenuItemId.SC_TRANS_90,
            100 => MenuItemId.SC_TRANS_100,
            _ => null
        };
    }
}
