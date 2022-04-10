using System;
using SmartSystemMenu.Native;

namespace SmartSystemMenu.Utils
{
    static class EnumUtils
    {
        public static Priority GetPriority(int menuItemId)
        {
            switch (menuItemId)
            {
                case MenuItemId.SC_PRIORITY_REAL_TIME: return Priority.RealTime;
                case MenuItemId.SC_PRIORITY_HIGH: return Priority.High;
                case MenuItemId.SC_PRIORITY_ABOVE_NORMAL: return Priority.AboveNormal;
                case MenuItemId.SC_PRIORITY_NORMAL: return Priority.Normal;
                case MenuItemId.SC_PRIORITY_BELOW_NORMAL: return Priority.BelowNormal;
                case MenuItemId.SC_PRIORITY_IDLE: return Priority.Idle;
                default: throw new ArgumentException(nameof(menuItemId));
            }
        }

        public static WindowAlignment GetWindowAlignment(int menuItemId)
        {
            switch (menuItemId)
            {
                case MenuItemId.SC_ALIGN_TOP_LEFT: return WindowAlignment.TopLeft;
                case MenuItemId.SC_ALIGN_TOP_CENTER: return WindowAlignment.TopCenter;
                case MenuItemId.SC_ALIGN_TOP_RIGHT: return WindowAlignment.TopRight;
                case MenuItemId.SC_ALIGN_MIDDLE_LEFT: return WindowAlignment.MiddleLeft;
                case MenuItemId.SC_ALIGN_MIDDLE_CENTER: return WindowAlignment.MiddleCenter;
                case MenuItemId.SC_ALIGN_MIDDLE_RIGHT: return WindowAlignment.MiddleRight;
                case MenuItemId.SC_ALIGN_BOTTOM_LEFT: return WindowAlignment.BottomLeft;
                case MenuItemId.SC_ALIGN_BOTTOM_CENTER: return WindowAlignment.BottomCenter;
                case MenuItemId.SC_ALIGN_BOTTOM_RIGHT: return WindowAlignment.BottomRight;
                case MenuItemId.SC_ALIGN_CENTER_HORIZONTALLY: return WindowAlignment.CenterHorizontally;
                case MenuItemId.SC_ALIGN_CENTER_VERTICALLY: return WindowAlignment.CenterVertically;
                default: throw new ArgumentException(nameof(menuItemId));
            }
        }

        public static int GetTransparency(int menuItemId)
        {
            switch (menuItemId)
            {
                case MenuItemId.SC_TRANS_00: return 0;
                case MenuItemId.SC_TRANS_10: return 10;
                case MenuItemId.SC_TRANS_20: return 20;
                case MenuItemId.SC_TRANS_30: return 30;
                case MenuItemId.SC_TRANS_40: return 40;
                case MenuItemId.SC_TRANS_50: return 50;
                case MenuItemId.SC_TRANS_60: return 60;
                case MenuItemId.SC_TRANS_70: return 70;
                case MenuItemId.SC_TRANS_80: return 80;
                case MenuItemId.SC_TRANS_90: return 90;
                case MenuItemId.SC_TRANS_100: return 100;
                default: throw new ArgumentException(nameof(menuItemId));
            }
        }

        public static int? GetTransparencyMenuItemId(int transparency)
        {
            switch (transparency)
            {
                case 0: return MenuItemId.SC_TRANS_00;
                case 10: return MenuItemId.SC_TRANS_10;
                case 20: return MenuItemId.SC_TRANS_20;
                case 30: return MenuItemId.SC_TRANS_30;
                case 40: return MenuItemId.SC_TRANS_40;
                case 50: return MenuItemId.SC_TRANS_50;
                case 60: return MenuItemId.SC_TRANS_60;
                case 70: return MenuItemId.SC_TRANS_70;
                case 80: return MenuItemId.SC_TRANS_80;
                case 90: return MenuItemId.SC_TRANS_90;
                case 100: return MenuItemId.SC_TRANS_100;
                default: return null;
            }
        }

    }
}
