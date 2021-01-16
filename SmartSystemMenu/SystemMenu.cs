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
        private int _numberItems;

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
            _numberItems = 0;
            WindowHandle = windowHandle;
            MoveToMenuItems = SystemUtils.GetMonitors().Select((x, i) => new KeyValuePair<int, IntPtr>(i + 1, x)).ToDictionary(x => x.Key, y => y.Value);
        }

        public void Create()
        {
            var windowMenuHandle = NativeMethods.GetSystemMenu(WindowHandle, false);
            var index = NativeMethods.GetMenuItemCount(windowMenuHandle);
            _wasOriginalBefore = index > 0 && NativeMethods.GetMenuItemID(windowMenuHandle, index - 1) == MenuItemId.SC_CLOSE;

            _numberItems++;
            NativeMethods.InsertMenu(windowMenuHandle, index, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, IntPtr.Zero, "");
            var name = "information";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                _numberItems++;
                NativeMethods.InsertMenu(windowMenuHandle, ++index, NativeConstants.MF_BYPOSITION, MenuItemId.SC_INFORMATION, GetTitle(name));
            }

            name = "roll_up";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                _numberItems++;
                NativeMethods.InsertMenu(windowMenuHandle, ++index, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ROLLUP, GetTitle(name));
            }

            name = "aero_glass";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                _numberItems++;
                NativeMethods.InsertMenu(windowMenuHandle, ++index, NativeConstants.MF_BYPOSITION, MenuItemId.SC_AERO_GLASS, GetTitle(name));
            }

            name = "always_on_top";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                _numberItems++;
                NativeMethods.InsertMenu(windowMenuHandle, ++index, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TOPMOST, GetTitle(name));
            }

            name = "send_to_bottom";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                _numberItems++;
                NativeMethods.InsertMenu(windowMenuHandle, ++index, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SEND_TO_BOTTOM, GetTitle(name));
            }

            name = "save_screenshot";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                _numberItems++;
                NativeMethods.InsertMenu(windowMenuHandle, ++index, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SAVE_SCREEN_SHOT, GetTitle(name));
            }

            name = "open_file_in_explorer";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                _numberItems++;
                NativeMethods.InsertMenu(windowMenuHandle, ++index, NativeConstants.MF_BYPOSITION, MenuItemId.SC_OPEN_FILE_IN_EXPLORER, GetTitle(name));
            }

            name = "copy_text_to_clipboard";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                _numberItems++;
                NativeMethods.InsertMenu(windowMenuHandle, ++index, NativeConstants.MF_BYPOSITION, MenuItemId.SC_COPY_TEXT_TO_CLIPBOARD, GetTitle(name));
            }

            name = "drag_by_mouse";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                _numberItems++;
                NativeMethods.InsertMenu(windowMenuHandle, ++index, NativeConstants.MF_BYPOSITION, MenuItemId.SC_DRAG_BY_MOUSE, GetTitle(name));
            }

            _sizeMenuHandle = NativeMethods.CreateMenu();

            name = "640_480";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_640_480, GetTitle(name, "640x480"));
            }

            name = "720_480";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_720_480, GetTitle(name, "720x480"));
            }

            name = "720_576";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_720_576, GetTitle(name, "720x576"));
            }

            name = "800_600";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_800_600, GetTitle(name, "800x600"));
            }

            name = "1024_768";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_1024_768, GetTitle(name, "1024x768"));
            }

            name = "1152_864";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_1152_864, GetTitle(name, "1152x864"));
            }

            name = "1280_768";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_1280_768, GetTitle(name, "1280x768"));
            }

            name = "1280_800";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_1280_800, GetTitle(name, "1280x800"));
            }

            name = "1280_960";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_1280_960, GetTitle(name, "1280x960"));
            }

            name = "1280_1024";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_1280_1024, GetTitle(name, "1280x1024"));
            }

            name = "1440_900";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_1440_900, GetTitle(name, "1440x900"));
            }

            name = "1600_900";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_1600_900, GetTitle(name, "1600x900"));
            }

            name = "1680_1050";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_1680_1050, GetTitle(name, "1680x1050"));
            }

            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_DEFAULT, GetTitle("size_default"));
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_SIZE_CUSTOM, GetTitle("size_custom"));

            name = "size";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                _numberItems++;
                NativeMethods.InsertMenu(windowMenuHandle, ++index, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _sizeMenuHandle, GetTitle(name));
            }

            _moveToMenuHandle = NativeMethods.CreateMenu();
            foreach (var item in MoveToMenuItems)
            {
                NativeMethods.InsertMenu(_moveToMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_MOVE_TO + item.Key, GetTitle("monitor") + item.Key);
            }

            name = "move_to";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                _numberItems++;
                NativeMethods.InsertMenu(windowMenuHandle, ++index, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _moveToMenuHandle, GetTitle(name));
            }

            _alignmentMenuHandle = NativeMethods.CreateMenu();

            name = "align_top_left";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ALIGN_TOP_LEFT, GetTitle(name));
            }

            name = "align_top_center";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ALIGN_TOP_CENTER, GetTitle(name));
            }

            name = "align_top_right";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ALIGN_TOP_RIGHT, GetTitle(name));
            }

            name = "align_middle_left";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ALIGN_MIDDLE_LEFT, GetTitle(name));
            }

            name = "align_middle_center";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ALIGN_MIDDLE_CENTER, GetTitle(name));
            }

            name = "align_middle_right";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ALIGN_MIDDLE_RIGHT, GetTitle(name));
            }

            name = "align_bottom_left";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ALIGN_BOTTOM_LEFT, GetTitle(name));
            }

            name = "align_bottom_center";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ALIGN_BOTTOM_CENTER, GetTitle(name));
            }

            name = "align_bottom_right";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ALIGN_BOTTOM_RIGHT, GetTitle(name));
            }

            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ALIGN_DEFAULT, GetTitle("align_default"));
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_ALIGN_CUSTOM, GetTitle("align_custom"));

            name = "alignment";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                _numberItems++;
                NativeMethods.InsertMenu(windowMenuHandle, ++index, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _alignmentMenuHandle, GetTitle(name));
            }

            _transparencyMenuHandle = NativeMethods.CreateMenu();

            name = "trans_opaque";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_00, GetTitle(name, "0%" + GetTitle(name, null, false)));
            }

            name = "10%";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_10, GetTitle(name, "10%"));
            }

            name = "20%";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_20, GetTitle(name, "20%"));
            }

            name = "30%";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_30, GetTitle(name, "30%"));
            }

            name = "40%";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_40, GetTitle(name, "40%"));
            }

            name = "50%";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_50, GetTitle(name, "50%"));
            }

            name = "60%";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_60, GetTitle(name, "60%"));
            }

            name = "70%";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_70, GetTitle(name, "70%"));
            }

            name = "80%";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_80, GetTitle(name, "80%"));
            }

            name = "90%";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_90, GetTitle(name, "90%"));
            }

            name = "trans_invisible";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_100, GetTitle(name, "100%" + GetTitle(name, null, false)));
            }

            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_DEFAULT, GetTitle("trans_default"));
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_TRANS_CUSTOM, GetTitle("trans_custom"));

            name = "transparency";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                _numberItems++;
                NativeMethods.InsertMenu(windowMenuHandle, ++index, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _transparencyMenuHandle, GetTitle(name));
            }

            _priorityMenuHandle = NativeMethods.CreateMenu();

            name = "priority_real_time";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_PRIORITY_REAL_TIME, GetTitle(name));
            }

            name = "priority_high";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_PRIORITY_HIGH, GetTitle(name));
            }

            name = "priority_above_normal";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_PRIORITY_ABOVE_NORMAL, GetTitle(name));
            }

            name = "priority_normal";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_PRIORITY_NORMAL, GetTitle(name));
            }

            name = "priority_below_normal";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_PRIORITY_BELOW_NORMAL, GetTitle(name));
            }

            name = "priority_idle";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_PRIORITY_IDLE, GetTitle(name));
            }

            name = "priority";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                _numberItems++;
                NativeMethods.InsertMenu(windowMenuHandle, ++index, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _priorityMenuHandle, GetTitle(name));
            }

            _systemTrayMenuHandle = NativeMethods.CreateMenu();

            name = "minimize_to_systemtray";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_systemTrayMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_MINIMIZE_TO_SYSTEMTRAY, GetTitle(name));
            }

            name = "minimize_always_to_systemtray";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_systemTrayMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY, GetTitle(name));
            }

            name = "system_tray";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                _numberItems++;
                NativeMethods.InsertMenu(windowMenuHandle, ++index, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _systemTrayMenuHandle, GetTitle(name));
            }

            _otherWindowsHandle = NativeMethods.CreateMenu();

            name = "minimize_other_windows";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_otherWindowsHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_MINIMIZE_OTHER_WINDOWS, GetTitle(name));
            }

            name = "close_other_windows";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                NativeMethods.InsertMenu(_otherWindowsHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_CLOSE_OTHER_WINDOWS, GetTitle(name));
            }

            name = "other_windows";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                _numberItems++;
                NativeMethods.InsertMenu(windowMenuHandle, ++index, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _otherWindowsHandle, GetTitle(name));
            }

            _startProgramsHandle = NativeMethods.CreateMenu();
            for (int i = 0; i < _menuItems.StartProgramItems.Count; i++)
            {
                NativeMethods.InsertMenu(_startProgramsHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_START_PROGRAM + i, _menuItems.StartProgramItems[i].Title);
            }

            name = "start_program";
            if (_menuItems.Items.Any(x => x.Name == name && x.Show))
            {
                _numberItems++;
                NativeMethods.InsertMenu(windowMenuHandle, ++index, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _startProgramsHandle, GetTitle(name));
            }
        }

        public void Destroy()
        {
            var windowMenuHandle = NativeMethods.GetSystemMenu(WindowHandle, false);
            var Index = NativeMethods.GetMenuItemCount(windowMenuHandle);
            for (int i = 0; i < _numberItems; i++)
            {
                NativeMethods.DeleteMenu(windowMenuHandle, --Index, NativeConstants.MF_BYPOSITION);

            }
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
            info.cbSize = (uint)Marshal.SizeOf(info);
            info.fMask = NativeConstants.MIIM_TYPE;
            info.fType = NativeConstants.MFT_STRING;
            info.dwTypeData = text;
            info.cch = (uint)text.Length;
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

        private string GetTitle(string name, string title = null, bool showHotKey = true)
        {
            title = title != null ? title : _languageSettings.GetValue(name);
            if (showHotKey)
            {
                var hotKey = _menuItems.GetHotKeysCombination(name);
                return string.IsNullOrEmpty(hotKey) ? title : title + "\t" + hotKey;
            }
            else
            {
                return title;
            }
        }

        #endregion
    }
}