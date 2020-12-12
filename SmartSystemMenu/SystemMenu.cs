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
            _wasOriginalBefore = index > 0 && NativeMethods.GetMenuItemID(windowMenuHandle, index - 1) == MenuItemId.SC_CLOSE;

            NativeMethods.InsertMenu(windowMenuHandle, index, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, IntPtr.Zero, "");
            NativeMethods.InsertMenu(windowMenuHandle, index + 1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_INFORMATION, GetTitle("information"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 2, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ROLLUP, GetTitle("roll_up"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 3, NativeConstants.MF_BYPOSITION, MenuItemId.SC_AERO_GLASS, GetTitle("aero_glass"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 4, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TOPMOST, GetTitle("always_on_top"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 5, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SEND_TO_BOTTOM, GetTitle("send_to_bottom"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 6, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SAVE_SCREEN_SHOT, GetTitle("save_screenshot"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 7, NativeConstants.MF_BYPOSITION, MenuItemId.SC_OPEN_FILE_IN_EXPLORER, GetTitle("open_file_in_explorer"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 8, NativeConstants.MF_BYPOSITION, MenuItemId.SC_COPY_TEXT_TO_CLIPBOARD, GetTitle("copy_text_to_clipboard"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 9, NativeConstants.MF_BYPOSITION, MenuItemId.SC_DRAG_BY_MOUSE, GetTitle("drag_by_mouse"));

            _sizeMenuHandle = NativeMethods.CreateMenu();
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_640_480, GetTitle("640_480", "640x480"));
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_720_480, GetTitle("720_480", "720x480"));
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_720_576, GetTitle("720_576", "720x576"));
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_800_600, GetTitle("800_600", "800x600"));
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_1024_768, GetTitle("1024_768", "1024x768"));
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_1152_864, GetTitle("1152_864", "1152x864"));
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_1280_768, GetTitle("1280_768", "1280x768"));
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_1280_800, GetTitle("1280_800", "1280x800"));
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_1280_960, GetTitle("1280_960", "1280x960"));
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_1280_1024, GetTitle("1280_1024", "1280x1024"));
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_1440_900, GetTitle("1440_900", "1440x900"));
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_1600_900, GetTitle("1600_900", "1600x900"));
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_1680_1050, GetTitle("1680_1050", "1680x1050"));
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_DEFAULT, GetTitle("size_default"));
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_CUSTOM, GetTitle("size_custom"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 10, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _sizeMenuHandle, GetTitle("size"));

            _moveToMenuHandle = NativeMethods.CreateMenu();
            foreach (var item in MoveToMenuItems)
            {
                NativeMethods.InsertMenu(_moveToMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_MOVE_TO + item.Key, GetTitle("monitor") + item.Key);
            }

            NativeMethods.InsertMenu(windowMenuHandle, index + 11, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _moveToMenuHandle, GetTitle("move_to"));

            _alignmentMenuHandle = NativeMethods.CreateMenu();
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ALIGN_TOP_LEFT, GetTitle("align_top_left"));
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ALIGN_TOP_CENTER, GetTitle("align_top_center"));
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ALIGN_TOP_RIGHT, GetTitle("align_top_right"));
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ALIGN_MIDDLE_LEFT, GetTitle("align_middle_left"));
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ALIGN_MIDDLE_CENTER, GetTitle("align_middle_center"));
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ALIGN_MIDDLE_RIGHT, GetTitle("align_middle_right"));
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ALIGN_BOTTOM_LEFT, GetTitle("align_bottom_left"));
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ALIGN_BOTTOM_CENTER, GetTitle("align_bottom_center"));
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ALIGN_BOTTOM_RIGHT, GetTitle("align_bottom_right"));
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ALIGN_DEFAULT, GetTitle("align_default"));
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ALIGN_CUSTOM, GetTitle("align_custom"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 12, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _alignmentMenuHandle, GetTitle("alignment"));

            _transparencyMenuHandle = NativeMethods.CreateMenu();
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_00, GetTitle("trans_opaque", "0%" + GetTitle("trans_opaque")));
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_10, GetTitle("10%", "10%"));
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_20, GetTitle("20%", "20%"));
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_30, GetTitle("30%", "30%"));
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_40, GetTitle("40%", "40%"));
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_50, GetTitle("50%", "50%"));
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_60, GetTitle("60%", "60%"));
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_70, GetTitle("70%", "70%"));
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_80, GetTitle("80%", "80%"));
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_90, GetTitle("90%", "90%"));
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_100, GetTitle("trans_invisible", "100%" + GetTitle("trans_invisible")));
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_DEFAULT, GetTitle("trans_default"));
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_CUSTOM, GetTitle("trans_custom"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 13, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _transparencyMenuHandle, GetTitle("transparency"));

            _priorityMenuHandle = NativeMethods.CreateMenu();
            NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_PRIORITY_REAL_TIME, GetTitle("priority_real_time"));
            NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_PRIORITY_HIGH, GetTitle("priority_high"));
            NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_PRIORITY_ABOVE_NORMAL, GetTitle("priority_above_normal"));
            NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_PRIORITY_NORMAL, GetTitle("priority_normal"));
            NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_PRIORITY_BELOW_NORMAL, GetTitle("priority_below_normal"));
            NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_PRIORITY_IDLE, GetTitle("priority_idle"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 14, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _priorityMenuHandle, GetTitle("priority"));

            _systemTrayMenuHandle = NativeMethods.CreateMenu();
            NativeMethods.InsertMenu(_systemTrayMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_MINIMIZE_TO_SYSTEMTRAY, GetTitle("minimize_to_systemtray"));
            NativeMethods.InsertMenu(_systemTrayMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY, GetTitle("minimize_always_to_systemtray"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 15, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _systemTrayMenuHandle, GetTitle("system_tray"));

            _otherWindowsHandle = NativeMethods.CreateMenu();
            NativeMethods.InsertMenu(_otherWindowsHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_MINIMIZE_OTHER_WINDOWS, GetTitle("minimize_other_windows"));
            NativeMethods.InsertMenu(_otherWindowsHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_CLOSE_OTHER_WINDOWS, GetTitle("close_other_windows"));
            NativeMethods.InsertMenu(windowMenuHandle, index + 16, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _otherWindowsHandle, GetTitle("other_windows"));

            _startProgramsHandle = NativeMethods.CreateMenu();
            for (int i = 0; i < _menuItems.StartProgramItems.Count; i++)
            {
                NativeMethods.InsertMenu(_startProgramsHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_START_PROGRAM + i, _menuItems.StartProgramItems[i].Title);
            }
            NativeMethods.InsertMenu(windowMenuHandle, index + 17, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _startProgramsHandle, GetTitle("start_program"));
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
            CheckMenuItem(MenuItemId.SC_PRIORITY_REAL_TIME, false);
            CheckMenuItem(MenuItemId.SC_PRIORITY_HIGH, false);
            CheckMenuItem(MenuItemId.SC_PRIORITY_ABOVE_NORMAL, false);
            CheckMenuItem(MenuItemId.SC_PRIORITY_NORMAL, false);
            CheckMenuItem(MenuItemId.SC_PRIORITY_BELOW_NORMAL, false);
            CheckMenuItem(MenuItemId.SC_PRIORITY_IDLE, false);
        }

        public void UncheckAlignmentMenu()
        {
            CheckMenuItem(MenuItemId.SC_ALIGN_TOP_LEFT, false);
            CheckMenuItem(MenuItemId.SC_ALIGN_TOP_CENTER, false);
            CheckMenuItem(MenuItemId.SC_ALIGN_TOP_RIGHT, false);
            CheckMenuItem(MenuItemId.SC_ALIGN_MIDDLE_LEFT, false);
            CheckMenuItem(MenuItemId.SC_ALIGN_MIDDLE_CENTER, false);
            CheckMenuItem(MenuItemId.SC_ALIGN_MIDDLE_RIGHT, false);
            CheckMenuItem(MenuItemId.SC_ALIGN_BOTTOM_LEFT, false);
            CheckMenuItem(MenuItemId.SC_ALIGN_BOTTOM_CENTER, false);
            CheckMenuItem(MenuItemId.SC_ALIGN_BOTTOM_RIGHT, false);
            CheckMenuItem(MenuItemId.SC_ALIGN_DEFAULT, false);
            CheckMenuItem(MenuItemId.SC_ALIGN_CUSTOM, false);
        }

        public void UncheckSizeMenu()
        {
            CheckMenuItem(MenuItemId.SC_SIZE_640_480, false);
            CheckMenuItem(MenuItemId.SC_SIZE_720_480, false);
            CheckMenuItem(MenuItemId.SC_SIZE_720_576, false);
            CheckMenuItem(MenuItemId.SC_SIZE_800_600, false);
            CheckMenuItem(MenuItemId.SC_SIZE_1024_768, false);
            CheckMenuItem(MenuItemId.SC_SIZE_1152_864, false);
            CheckMenuItem(MenuItemId.SC_SIZE_1280_768, false);
            CheckMenuItem(MenuItemId.SC_SIZE_1280_800, false);
            CheckMenuItem(MenuItemId.SC_SIZE_1280_960, false);
            CheckMenuItem(MenuItemId.SC_SIZE_1280_1024, false);
            CheckMenuItem(MenuItemId.SC_SIZE_1440_900, false);
            CheckMenuItem(MenuItemId.SC_SIZE_1600_900, false);
            CheckMenuItem(MenuItemId.SC_SIZE_1680_1050, false);
            CheckMenuItem(MenuItemId.SC_SIZE_DEFAULT, false);
            CheckMenuItem(MenuItemId.SC_SIZE_CUSTOM, false);
        }

        public void UncheckTransparencyMenu()
        {
            CheckMenuItem(MenuItemId.SC_TRANS_100, false);
            CheckMenuItem(MenuItemId.SC_TRANS_90, false);
            CheckMenuItem(MenuItemId.SC_TRANS_80, false);
            CheckMenuItem(MenuItemId.SC_TRANS_70, false);
            CheckMenuItem(MenuItemId.SC_TRANS_60, false);
            CheckMenuItem(MenuItemId.SC_TRANS_50, false);
            CheckMenuItem(MenuItemId.SC_TRANS_40, false);
            CheckMenuItem(MenuItemId.SC_TRANS_30, false);
            CheckMenuItem(MenuItemId.SC_TRANS_20, false);
            CheckMenuItem(MenuItemId.SC_TRANS_10, false);
            CheckMenuItem(MenuItemId.SC_TRANS_00, false);
            CheckMenuItem(MenuItemId.SC_TRANS_CUSTOM, false);
            CheckMenuItem(MenuItemId.SC_TRANS_DEFAULT, false);
        }

        #endregion


        #region Methods.Private

        private string GetTitle(string name, string title = null)
        {
            title = title != null ? title : _languageSettings.GetValue(name);
            var hotKey = _menuItems.GetHotKeysCombination(name);
            return string.IsNullOrEmpty(hotKey) ? title : title + "\t" + hotKey;
        }

        #endregion
    }
}