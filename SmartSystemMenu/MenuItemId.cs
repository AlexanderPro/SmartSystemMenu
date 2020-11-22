namespace SmartSystemMenu
{
    static class MenuItemId
    {
        public const int SC_CLOSE = 0xF060;
        public const int SC_TRANS_100 = 0x4740;
        public const int SC_TRANS_90 = 0x4742;
        public const int SC_TRANS_80 = 0x4744;
        public const int SC_TRANS_70 = 0x4746;
        public const int SC_TRANS_60 = 0x4748;
        public const int SC_TRANS_50 = 0x4750;
        public const int SC_TRANS_40 = 0x4752;
        public const int SC_TRANS_30 = 0x4754;
        public const int SC_TRANS_20 = 0x4756;
        public const int SC_TRANS_10 = 0x4758;
        public const int SC_TRANS_00 = 0x4760;
        public const int SC_TRANS_CUSTOM = 0x4761;
        public const int SC_TRANS_DEFAULT = 0x4762;
        public const int SC_TOPMOST = 0x4763;
        public const int SC_SIZE_640_480 = 0x4765;
        public const int SC_SIZE_720_480 = 0x4766;
        public const int SC_SIZE_720_576 = 0x4767;
        public const int SC_SIZE_800_600 = 0x4768;
        public const int SC_SIZE_1024_768 = 0x4769;
        public const int SC_SIZE_1152_864 = 0x4770;
        public const int SC_SIZE_1280_768 = 0x4771;
        public const int SC_SIZE_1280_800 = 0x4772;
        public const int SC_SIZE_1280_960 = 0x4773;
        public const int SC_SIZE_1280_1024 = 0x4774;
        public const int SC_SIZE_1440_900 = 0x4775;
        public const int SC_SIZE_1600_900 = 0x4776;
        public const int SC_SIZE_1680_1050 = 0x4777;
        public const int SC_SIZE_DEFAULT = 0x4778;
        public const int SC_SIZE_CUSTOM = 0x4779;
        public const int SC_MINIMIZE_TO_SYSTEMTRAY = 0x4780;
        public const int SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY = 0x4781;
        public const int SC_INFORMATION = 0x4782;
        public const int SC_ROLLUP = 0x4783;
        public const int SC_PRIORITY_REAL_TIME = 0x4784;
        public const int SC_PRIORITY_HIGH = 0x4785;
        public const int SC_PRIORITY_ABOVE_NORMAL = 0x4786;
        public const int SC_PRIORITY_NORMAL = 0x4787;
        public const int SC_PRIORITY_BELOW_NORMAL = 0x4788;
        public const int SC_PRIORITY_IDLE = 0x4789;
        public const int SC_ALIGN_TOP_LEFT = 0x4790;
        public const int SC_ALIGN_TOP_CENTER = 0x4791;
        public const int SC_ALIGN_TOP_RIGHT = 0x4792;
        public const int SC_ALIGN_MIDDLE_LEFT = 0x4793;
        public const int SC_ALIGN_MIDDLE_CENTER = 0x4794;
        public const int SC_ALIGN_MIDDLE_RIGHT = 0x4795;
        public const int SC_ALIGN_BOTTOM_LEFT = 0x4796;
        public const int SC_ALIGN_BOTTOM_CENTER = 0x4797;
        public const int SC_ALIGN_BOTTOM_RIGHT = 0x4798;
        public const int SC_ALIGN_DEFAULT = 0x4799;
        public const int SC_ALIGN_CUSTOM = 0x4800;
        public const int SC_SAVE_SCREEN_SHOT = 0x4802;
        public const int SC_COPY_TEXT_TO_CLIPBOARD = 0x4803;
        public const int SC_OPEN_FILE_IN_EXPLORER = 0x4804;
        public const int SC_CLOSE_OTHER_WINDOWS = 0x4805;
        public const int SC_MINIMIZE_OTHER_WINDOWS = 0x4806;
        public const int SC_AERO_GLASS = 0x4807;
        public const int SC_SEND_TO_BOTTOM = 0x4808;
        public const int SC_DRAG_BY_MOUSE = 0x4809;
        public const int SC_START_PROGRAM = 0x4900;
        public const int SC_MOVE_TO = 0x5000;

        public static int GetMenuItemId(string name)
        {
            switch (name.ToLower())
            {
                case "information": return SC_INFORMATION;
                case "roll_up": return SC_ROLLUP;
                case "aero_glass": return SC_AERO_GLASS;
                case "always_on_top": return SC_TOPMOST;
                case "send_to_bottom": return SC_SEND_TO_BOTTOM;
                case "save_screenshot": return SC_SAVE_SCREEN_SHOT;
                case "open_file_in_explorer": return SC_OPEN_FILE_IN_EXPLORER;
                case "copy_text_to_clipboard": return SC_COPY_TEXT_TO_CLIPBOARD;
                case "drag_by_mouse": return SC_DRAG_BY_MOUSE;
                case "size_default": return SC_SIZE_DEFAULT;
                case "size_custom": return SC_SIZE_CUSTOM;
                case "640_480": return SC_SIZE_640_480;
                case "720_480": return SC_SIZE_720_480;
                case "720_576": return SC_SIZE_720_576;
                case "800_600": return SC_SIZE_800_600;
                case "1024_768": return SC_SIZE_1024_768;
                case "1152_864": return SC_SIZE_1152_864;
                case "1280_768": return SC_SIZE_1280_768;
                case "1280_800": return SC_SIZE_1280_800;
                case "1280_960": return SC_SIZE_1280_960;
                case "1280_1024": return SC_SIZE_1280_1024;
                case "1440_900": return SC_SIZE_1440_900;
                case "1600_900": return SC_SIZE_1600_900;
                case "1680_1050": return SC_SIZE_1680_1050;
                case "align_top_left": return SC_ALIGN_TOP_LEFT;
                case "align_top_center": return SC_ALIGN_TOP_CENTER;
                case "align_top_right": return SC_ALIGN_TOP_RIGHT;
                case "align_middle_left": return SC_ALIGN_MIDDLE_LEFT;
                case "align_middle_center": return SC_ALIGN_MIDDLE_CENTER;
                case "align_middle_right": return SC_ALIGN_MIDDLE_RIGHT;
                case "align_bottom_left": return SC_ALIGN_BOTTOM_LEFT;
                case "align_bottom_center": return SC_ALIGN_BOTTOM_CENTER;
                case "align_bottom_right": return SC_ALIGN_BOTTOM_RIGHT;
                case "align_default": return SC_ALIGN_DEFAULT;
                case "align_custom": return SC_ALIGN_CUSTOM;
                case "trans_opaque": return SC_TRANS_00;
                case "10%": return SC_TRANS_10;
                case "20%": return SC_TRANS_20;
                case "30%": return SC_TRANS_30;
                case "40%": return SC_TRANS_40;
                case "50%": return SC_TRANS_50;
                case "60%": return SC_TRANS_60;
                case "70%": return SC_TRANS_70;
                case "80%": return SC_TRANS_80;
                case "90%": return SC_TRANS_90;
                case "trans_invisible": return SC_TRANS_100;
                case "trans_default": return SC_TRANS_DEFAULT;
                case "trans_custom": return SC_TRANS_CUSTOM;
                case "priority_real_time": return SC_PRIORITY_REAL_TIME;
                case "priority_high": return SC_PRIORITY_HIGH;
                case "priority_above_normal": return SC_PRIORITY_ABOVE_NORMAL;
                case "priority_normal": return SC_PRIORITY_NORMAL;
                case "priority_below_normal": return SC_PRIORITY_BELOW_NORMAL;
                case "priority_idle": return SC_PRIORITY_IDLE;
                case "minimize_to_systemtray": return SC_MINIMIZE_TO_SYSTEMTRAY;
                case "minimize_always_to_systemtray": return SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY;
                case "minimize_other_windows": return SC_MINIMIZE_OTHER_WINDOWS;
                case "close_other_windows": return SC_CLOSE_OTHER_WINDOWS;
                default: return 0;
            }
        }
    }
}
