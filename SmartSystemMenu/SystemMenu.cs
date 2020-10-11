using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using SmartSystemMenu.Native;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu
{
   class SystemMenu
    {
        #region Fields.Private

        private IntPtr _priorityMenuHandle;
        private IntPtr _alignmentMenuHandle;
        private IntPtr _sizeMenuHandle;
        private IntPtr _transparencyMenuHandle;
        private IntPtr _systemTrayMenuHandle;
        private IntPtr _otherWindowsHandle;
        private IntPtr _startProgramsHandle;
        private IntPtr _moveToMenuHandle;
        private readonly MenuItems _menuItems;
        private readonly LanguageSettings _languageSettings;
        private bool _wasOriginalBefore;
        #endregion


        #region Properties.Public

        public IntPtr WindowHandle { get; private set; }

        public IDictionary<int, IntPtr> MoveToMenuItems { get; private set; }

        public bool Exists
        {
            get
            {
                var windowMenuHandle = NativeMethods.GetSystemMenu(WindowHandle, false);
                var existsWindowMenu = windowMenuHandle != IntPtr.Zero;
                return existsWindowMenu;
            }
        }

        #endregion


        #region Constants.Public

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

        #endregion


        #region Methods.Public

        public SystemMenu(IntPtr windowHandle, MenuItems menuItems, LanguageSettings languageSettings)
        {
            _priorityMenuHandle = IntPtr.Zero;
            _alignmentMenuHandle = IntPtr.Zero;
            _sizeMenuHandle = IntPtr.Zero;
            _transparencyMenuHandle = IntPtr.Zero;
            _systemTrayMenuHandle = IntPtr.Zero;
            _otherWindowsHandle = IntPtr.Zero;
            _startProgramsHandle = IntPtr.Zero;
            _moveToMenuHandle = IntPtr.Zero;
            _menuItems = menuItems;
            _languageSettings = languageSettings;
            WindowHandle = windowHandle;
            MoveToMenuItems = SystemUtils.GetMonitors().Select((x, i) => new KeyValuePair<int, IntPtr>(i + 1, x)).ToDictionary(x => x.Key, y => y.Value);
        }

        public void Create()
        {
            var windowMenuHandle = NativeMethods.GetSystemMenu(WindowHandle, false);
            var index = NativeMethods.GetMenuItemCount(windowMenuHandle);
            _wasOriginalBefore = index > 0 && NativeMethods.GetMenuItemID(windowMenuHandle, index - 1) == SC_CLOSE;

            NativeMethods.InsertMenu(windowMenuHandle, index, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, IntPtr.Zero, "");
            NativeMethods.InsertMenu(windowMenuHandle, index + 1, NativeConstants.MF_BYPOSITION, SC_INFORMATION, _languageSettings.GetValue("information"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 2, NativeConstants.MF_BYPOSITION, SC_ROLLUP, _languageSettings.GetValue("roll_up"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 3, NativeConstants.MF_BYPOSITION, SC_AERO_GLASS, _languageSettings.GetValue("aero_glass"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 4, NativeConstants.MF_BYPOSITION, SC_TOPMOST, _languageSettings.GetValue("always_on_top"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 5, NativeConstants.MF_BYPOSITION, SC_SEND_TO_BOTTOM, _languageSettings.GetValue("send_to_bottom"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 6, NativeConstants.MF_BYPOSITION, SC_SAVE_SCREEN_SHOT, _languageSettings.GetValue("save_screenshot"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 7, NativeConstants.MF_BYPOSITION, SC_OPEN_FILE_IN_EXPLORER, _languageSettings.GetValue("open_file_in_explorer"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 8, NativeConstants.MF_BYPOSITION, SC_COPY_TEXT_TO_CLIPBOARD, _languageSettings.GetValue("copy_text_to_clipboard"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 9, NativeConstants.MF_BYPOSITION, SC_DRAG_BY_MOUSE, _languageSettings.GetValue("drag_by_mouse"));

            _sizeMenuHandle = NativeMethods.CreateMenu();
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_640_480, "640x480");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_720_480, "720x480");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_720_576, "720x576");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_800_600, "800x600");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_1024_768, "1024x768");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_1152_864, "1152x864");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_1280_768, "1280x768");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_1280_800, "1280x800");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_1280_960, "1280x960");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_1280_1024, "1280x1024");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_1440_900, "1440x900");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_1600_900, "1600x900");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_1680_1050, "1680x1050");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_DEFAULT, _languageSettings.GetValue("size_default"));
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_CUSTOM, _languageSettings.GetValue("size_custom"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 10, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _sizeMenuHandle, _languageSettings.GetValue("size"));

            _moveToMenuHandle = NativeMethods.CreateMenu();
            foreach (var item in MoveToMenuItems)
            {
                NativeMethods.InsertMenu(_moveToMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_MOVE_TO + item.Key, _languageSettings.GetValue("monitor") + item.Key);
            }

            NativeMethods.InsertMenu(windowMenuHandle, index + 11, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _moveToMenuHandle, _languageSettings.GetValue("move_to"));

            _alignmentMenuHandle = NativeMethods.CreateMenu();
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_TOP_LEFT, _languageSettings.GetValue("align_top_left"));
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_TOP_CENTER, _languageSettings.GetValue("align_top_center"));
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_TOP_RIGHT, _languageSettings.GetValue("align_top_right"));
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_MIDDLE_LEFT, _languageSettings.GetValue("align_middle_left"));
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_MIDDLE_CENTER, _languageSettings.GetValue("align_middle_center"));
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_MIDDLE_RIGHT, _languageSettings.GetValue("align_middle_right"));
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_BOTTOM_LEFT, _languageSettings.GetValue("align_bottom_left"));
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_BOTTOM_CENTER, _languageSettings.GetValue("align_bottom_center"));
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_BOTTOM_RIGHT, _languageSettings.GetValue("align_bottom_right"));
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_DEFAULT, _languageSettings.GetValue("align_default"));
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_CUSTOM, _languageSettings.GetValue("align_custom"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 12, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _alignmentMenuHandle, _languageSettings.GetValue("alignment"));

            _transparencyMenuHandle = NativeMethods.CreateMenu();
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_00, "0%" + _languageSettings.GetValue("trans_opaque"));
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_10, "10%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_20, "20%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_30, "30%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_40, "40%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_50, "50%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_60, "60%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_70, "70%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_80, "80%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_90, "90%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_100, "100%" + _languageSettings.GetValue("trans_invisible"));
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_DEFAULT, _languageSettings.GetValue("trans_default"));
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_CUSTOM, _languageSettings.GetValue("trans_custom"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 13, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _transparencyMenuHandle, _languageSettings.GetValue("transparency"));

            _priorityMenuHandle = NativeMethods.CreateMenu();
            NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_PRIORITY_REAL_TIME, _languageSettings.GetValue("priority_real_time"));
            NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_PRIORITY_HIGH, _languageSettings.GetValue("priority_high"));
            NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_PRIORITY_ABOVE_NORMAL, _languageSettings.GetValue("priority_above_normal"));
            NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_PRIORITY_NORMAL, _languageSettings.GetValue("priority_normal"));
            NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_PRIORITY_BELOW_NORMAL, _languageSettings.GetValue("priority_below_normal"));
            NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_PRIORITY_IDLE, _languageSettings.GetValue("priority_idle"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 14, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _priorityMenuHandle, _languageSettings.GetValue("priority"));

            _systemTrayMenuHandle = NativeMethods.CreateMenu();
            NativeMethods.InsertMenu(_systemTrayMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_MINIMIZE_TO_SYSTEMTRAY, _languageSettings.GetValue("minimize_to_systemtray"));
            NativeMethods.InsertMenu(_systemTrayMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY, _languageSettings.GetValue("minimize_always_to_systemtray"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 15, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _systemTrayMenuHandle, _languageSettings.GetValue("system_tray"));

            _otherWindowsHandle = NativeMethods.CreateMenu();
            NativeMethods.InsertMenu(_otherWindowsHandle, -1, NativeConstants.MF_BYPOSITION, SC_MINIMIZE_OTHER_WINDOWS, _languageSettings.GetValue("minimize_other_windows"));
            NativeMethods.InsertMenu(_otherWindowsHandle, -1, NativeConstants.MF_BYPOSITION, SC_CLOSE_OTHER_WINDOWS, _languageSettings.GetValue("close_other_windows"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 16, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _otherWindowsHandle, _languageSettings.GetValue("other_windows"));

            _startProgramsHandle = NativeMethods.CreateMenu();
            for (int i = 0; i < _menuItems.StartProgramItems.Count; i++)
            {
                NativeMethods.InsertMenu(_startProgramsHandle, -1, NativeConstants.MF_BYPOSITION, SC_START_PROGRAM + i, _menuItems.StartProgramItems[i].Title);
            }
            NativeMethods.InsertMenu(windowMenuHandle, index + 17, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _startProgramsHandle, _languageSettings.GetValue("start_program"));
        }

        public void Destroy()
        {
            var windowMenuHandle = NativeMethods.GetSystemMenu(WindowHandle, false);
            var Index = NativeMethods.GetMenuItemCount(windowMenuHandle);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 1, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 2, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 3, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 4, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 5, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 6, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 7, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 8, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 9, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 10, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 11, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 12, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 13, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 14, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 15, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 16, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 17, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 18, NativeConstants.MF_BYPOSITION);
            NativeMethods.DestroyMenu(_priorityMenuHandle);
            NativeMethods.DestroyMenu(_alignmentMenuHandle);
            NativeMethods.DestroyMenu(_moveToMenuHandle);
            NativeMethods.DestroyMenu(_sizeMenuHandle);
            NativeMethods.DestroyMenu(_transparencyMenuHandle);
            NativeMethods.DestroyMenu(_otherWindowsHandle);
            NativeMethods.DestroyMenu(_systemTrayMenuHandle);
            NativeMethods.DestroyMenu(_startProgramsHandle);
            if (_wasOriginalBefore)
            {
                NativeMethods.GetSystemMenu(WindowHandle, true);
            }
        }

        public void SetMenuItemText(int id, string text)
        {
            var info = new MenuItemInfo();
            info.cbSize = (UInt32)Marshal.SizeOf(info);
            info.fMask = NativeConstants.MIIM_TYPE;
            info.fType = NativeConstants.MFT_STRING;
            info.dwTypeData = text;
            info.cch = (UInt32)text.Length;
            var windowMenuHandle = NativeMethods.GetSystemMenu(WindowHandle, false);
            NativeMethods.SetMenuItemInfo(windowMenuHandle, id, false, ref info);
        }

        public void CheckMenuItem(int id, bool check)
        {
            var windowMenuHandle = NativeMethods.GetSystemMenu(WindowHandle, false);
            NativeMethods.CheckMenuItem(windowMenuHandle, id, check ? NativeConstants.MF_CHECKED : NativeConstants.MF_UNCHECKED);
        }

        public void UncheckMenuItems(params int[] ids)
        {
            var windowMenuHandle = NativeMethods.GetSystemMenu(WindowHandle, false);
            foreach (var id in ids)
            {
                NativeMethods.CheckMenuItem(windowMenuHandle, id, NativeConstants.MF_UNCHECKED);
            }
        }

        public bool IsMenuItemChecked(int id)
        {
            var windowMenuHandle = NativeMethods.GetSystemMenu(WindowHandle, false);
            var flags = NativeMethods.GetMenuState(windowMenuHandle, id, NativeConstants.MF_BYCOMMAND);
            var isChecked = (flags & NativeConstants.MF_CHECKED) != 0;
            return isChecked;
        }

        public void UncheckPriorityMenu()
        {
            CheckMenuItem(SC_PRIORITY_REAL_TIME, false);
            CheckMenuItem(SC_PRIORITY_HIGH, false);
            CheckMenuItem(SC_PRIORITY_ABOVE_NORMAL, false);
            CheckMenuItem(SC_PRIORITY_NORMAL, false);
            CheckMenuItem(SC_PRIORITY_BELOW_NORMAL, false);
            CheckMenuItem(SC_PRIORITY_IDLE, false);
        }

        public void UncheckAlignmentMenu()
        {
            CheckMenuItem(SC_ALIGN_TOP_LEFT, false);
            CheckMenuItem(SC_ALIGN_TOP_CENTER, false);
            CheckMenuItem(SC_ALIGN_TOP_RIGHT, false);
            CheckMenuItem(SC_ALIGN_MIDDLE_LEFT, false);
            CheckMenuItem(SC_ALIGN_MIDDLE_CENTER, false);
            CheckMenuItem(SC_ALIGN_MIDDLE_RIGHT, false);
            CheckMenuItem(SC_ALIGN_BOTTOM_LEFT, false);
            CheckMenuItem(SC_ALIGN_BOTTOM_CENTER, false);
            CheckMenuItem(SC_ALIGN_BOTTOM_RIGHT, false);
            CheckMenuItem(SC_ALIGN_DEFAULT, false);
            CheckMenuItem(SC_ALIGN_CUSTOM, false);
        }

        public void UncheckSizeMenu()
        {
            CheckMenuItem(SC_SIZE_640_480, false);
            CheckMenuItem(SC_SIZE_720_480, false);
            CheckMenuItem(SC_SIZE_720_576, false);
            CheckMenuItem(SC_SIZE_800_600, false);
            CheckMenuItem(SC_SIZE_1024_768, false);
            CheckMenuItem(SC_SIZE_1152_864, false);
            CheckMenuItem(SC_SIZE_1280_768, false);
            CheckMenuItem(SC_SIZE_1280_800, false);
            CheckMenuItem(SC_SIZE_1280_960, false);
            CheckMenuItem(SC_SIZE_1280_1024, false);
            CheckMenuItem(SC_SIZE_1440_900, false);
            CheckMenuItem(SC_SIZE_1600_900, false);
            CheckMenuItem(SC_SIZE_1680_1050, false);
            CheckMenuItem(SC_SIZE_DEFAULT, false);
            CheckMenuItem(SC_SIZE_CUSTOM, false);
        }

        public void UncheckTransparencyMenu()
        {
            CheckMenuItem(SC_TRANS_100, false);
            CheckMenuItem(SC_TRANS_90, false);
            CheckMenuItem(SC_TRANS_80, false);
            CheckMenuItem(SC_TRANS_70, false);
            CheckMenuItem(SC_TRANS_60, false);
            CheckMenuItem(SC_TRANS_50, false);
            CheckMenuItem(SC_TRANS_40, false);
            CheckMenuItem(SC_TRANS_30, false);
            CheckMenuItem(SC_TRANS_20, false);
            CheckMenuItem(SC_TRANS_10, false);
            CheckMenuItem(SC_TRANS_00, false);
            CheckMenuItem(SC_TRANS_CUSTOM, false);
            CheckMenuItem(SC_TRANS_DEFAULT, false);
        }

        #endregion
    }
}