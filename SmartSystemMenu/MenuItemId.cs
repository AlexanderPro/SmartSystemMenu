using System.Collections.Generic;

namespace SmartSystemMenu
{
    internal static class MenuItemId
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
        public const int SC_OPEN_FILE_IN_EXPLORER = 0x4804;
        public const int SC_CLOSE_OTHER_WINDOWS = 0x4805;
        public const int SC_MINIMIZE_OTHER_WINDOWS = 0x4806;
        public const int SC_AERO_GLASS = 0x4807;
        public const int SC_SEND_TO_BOTTOM = 0x4808;
        public const int SC_DRAG_BY_MOUSE = 0x4809;
        public const int SC_SUSPEND_TO_SYSTEMTRAY = 0x4810;
        public const int SC_COPY_TEXT_TO_CLIPBOARD = 0x4811;
        public const int SC_CLEAR_CLIPBOARD = 0x4812;
        public const int SC_START_PROGRAM = 0x4900;
        public const int SC_MOVE_TO = 0x5000;
        public const int SC_SIZE_DEFINED = 0x5100;

        private static readonly Dictionary<string, int> NameToId = new Dictionary<string, int>();
        private static readonly Dictionary<int, string> IdToName = new Dictionary<int, string>();

        static MenuItemId()
        {
            NameToId["information"] = SC_INFORMATION;
            NameToId["roll_up"] = SC_ROLLUP;
            NameToId["aero_glass"] = SC_AERO_GLASS;
            NameToId["always_on_top"] = SC_TOPMOST;
            NameToId["send_to_bottom"] = SC_SEND_TO_BOTTOM;
            NameToId["save_screenshot"] = SC_SAVE_SCREEN_SHOT;
            NameToId["open_file_in_explorer"] = SC_OPEN_FILE_IN_EXPLORER;
            NameToId["drag_by_mouse"] = SC_DRAG_BY_MOUSE;
            NameToId["size_default"] = SC_SIZE_DEFAULT;
            NameToId["size_custom"] = SC_SIZE_CUSTOM;
            NameToId["align_top_left"] = SC_ALIGN_TOP_LEFT;
            NameToId["align_top_center"] = SC_ALIGN_TOP_CENTER;
            NameToId["align_top_right"] = SC_ALIGN_TOP_RIGHT;
            NameToId["align_middle_left"] = SC_ALIGN_MIDDLE_LEFT;
            NameToId["align_middle_center"] = SC_ALIGN_MIDDLE_CENTER;
            NameToId["align_middle_right"] = SC_ALIGN_MIDDLE_RIGHT;
            NameToId["align_bottom_left"] = SC_ALIGN_BOTTOM_LEFT;
            NameToId["align_bottom_center"] = SC_ALIGN_BOTTOM_CENTER;
            NameToId["align_bottom_right"] = SC_ALIGN_BOTTOM_RIGHT;
            NameToId["align_default"] = SC_ALIGN_DEFAULT;
            NameToId["align_custom"] = SC_ALIGN_CUSTOM;
            NameToId["trans_opaque"] = SC_TRANS_00;
            NameToId["10%"] = SC_TRANS_10;
            NameToId["20%"] = SC_TRANS_20;
            NameToId["30%"] = SC_TRANS_30;
            NameToId["40%"] = SC_TRANS_40;
            NameToId["50%"] = SC_TRANS_50;
            NameToId["60%"] = SC_TRANS_60;
            NameToId["70%"] = SC_TRANS_70;
            NameToId["80%"] = SC_TRANS_80;
            NameToId["90%"] = SC_TRANS_90;
            NameToId["trans_invisible"] = SC_TRANS_100;
            NameToId["trans_default"] = SC_TRANS_DEFAULT;
            NameToId["trans_custom"] = SC_TRANS_CUSTOM;
            NameToId["priority_real_time"] = SC_PRIORITY_REAL_TIME;
            NameToId["priority_high"] = SC_PRIORITY_HIGH;
            NameToId["priority_above_normal"] = SC_PRIORITY_ABOVE_NORMAL;
            NameToId["priority_normal"] = SC_PRIORITY_NORMAL;
            NameToId["priority_below_normal"] = SC_PRIORITY_BELOW_NORMAL;
            NameToId["priority_idle"] = SC_PRIORITY_IDLE;
            NameToId["copy_text_to_clipboard"] = SC_COPY_TEXT_TO_CLIPBOARD;
            NameToId["clear_clipboard"] = SC_CLEAR_CLIPBOARD;
            NameToId["minimize_to_systemtray"] = SC_MINIMIZE_TO_SYSTEMTRAY;
            NameToId["suspend_to_systemtray"] = SC_SUSPEND_TO_SYSTEMTRAY;
            NameToId["minimize_always_to_systemtray"] = SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY;
            NameToId["minimize_other_windows"] = SC_MINIMIZE_OTHER_WINDOWS;
            NameToId["close_other_windows"] = SC_CLOSE_OTHER_WINDOWS;

            foreach (var pair in NameToId)
            {
                IdToName[pair.Value] = pair.Key;
            }
        }

        public static int GetId(string name)
        {
            if (NameToId.TryGetValue(name.ToLower(), out int id))
            {
                return id;
            }

            return 0;
        }

        public static string GetName(int id)
        {
            if (IdToName.TryGetValue(id, out string name))
            {
                return name;
            }

            return "";
        }
    }
}
