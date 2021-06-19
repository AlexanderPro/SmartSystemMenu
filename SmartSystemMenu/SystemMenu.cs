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
        private List<IntPtr> _subMenuHandles = new List<IntPtr>();
        private readonly MenuItems _menuItems;
        private readonly LanguageSettings _languageSettings;
        private bool _wasOriginalBefore;
        private int _numberItems;

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

        public SystemMenu(IntPtr windowHandle, MenuItems menuItems, LanguageSettings languageSettings)
        {
            _menuItems = menuItems;
            _languageSettings = languageSettings;
            _numberItems = 0;
            WindowHandle = windowHandle;
            MoveToMenuItems = SystemUtils.GetMonitors().Select((x, i) => new KeyValuePair<int, IntPtr>(i + 1, x)).ToDictionary(x => x.Key, y => y.Value);
        }

        public void Create()
        {
            var windowMenuHandle = NativeMethods.GetSystemMenu(WindowHandle, false);
            int index = NativeMethods.GetMenuItemCount(windowMenuHandle);
            _wasOriginalBefore = index > 0 && NativeMethods.GetMenuItemID(windowMenuHandle, index - 1) == MenuItemId.SC_CLOSE;

            _numberItems++;
            NativeMethods.InsertMenu(windowMenuHandle, index, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, IntPtr.Zero, "");


            IntPtr subMenuHandle;

            AddMenuItem(MenuItemId.SC_INFORMATION);
            AddMenuItem(MenuItemId.SC_ROLLUP);
            AddMenuItem(MenuItemId.SC_AERO_GLASS);
            AddMenuItem(MenuItemId.SC_TOPMOST);
            AddMenuItem(MenuItemId.SC_SEND_TO_BOTTOM);
            AddMenuItem(MenuItemId.SC_SAVE_SCREEN_SHOT);
            AddMenuItem(MenuItemId.SC_OPEN_FILE_IN_EXPLORER);
            AddMenuItem(MenuItemId.SC_COPY_TEXT_TO_CLIPBOARD);
            AddMenuItem(MenuItemId.SC_DRAG_BY_MOUSE);

            StartCreatingSubMenu();
            AddSubMenuItem(MenuItemId.SC_SIZE_640_480, "640x480");
            AddSubMenuItem(MenuItemId.SC_SIZE_720_480, "720x480");
            AddSubMenuItem(MenuItemId.SC_SIZE_720_576, "720x576");
            AddSubMenuItem(MenuItemId.SC_SIZE_720_576, "720x576");
            AddSubMenuItem(MenuItemId.SC_SIZE_800_600, "800x600");
            AddSubMenuItem(MenuItemId.SC_SIZE_1024_768, "1024x768");
            AddSubMenuItem(MenuItemId.SC_SIZE_1152_864, "1152x846");
            AddSubMenuItem(MenuItemId.SC_SIZE_1280_768, "1280x768");
            AddSubMenuItem(MenuItemId.SC_SIZE_1280_800, "1280x800");
            AddSubMenuItem(MenuItemId.SC_SIZE_1280_960, "1280x960");
            AddSubMenuItem(MenuItemId.SC_SIZE_1280_1024, "1280x1024");
            AddSubMenuItem(MenuItemId.SC_SIZE_1440_900, "1440x900");
            AddSubMenuItem(MenuItemId.SC_SIZE_1600_900, "1600x900");
            AddSubMenuItem(MenuItemId.SC_SIZE_1680_1050, "1680x1050");
            if (_menuItems.WindowSizeItems.Any())
            {
                AddSubMenuSeparator();
                for (int i = 0; i < _menuItems.WindowSizeItems.Count; i++)
                {
                    var menuItemId = MenuItemId.SC_SIZE_DEFINED + i;
                    _menuItems.WindowSizeItems[i].Id = menuItemId;
                    NativeMethods.InsertMenu(subMenuHandle, -1, NativeConstants.MF_BYPOSITION, menuItemId, _menuItems.WindowSizeItems[i].Title);
                }
            }
            AddSubMenuSeparator();
            AddSubMenuItem(MenuItemId.SC_SIZE_DEFAULT);
            AddSubMenuSeparator();
            AddSubMenuItem(MenuItemId.SC_SIZE_CUSTOM);
            FinishSubMenu("size");

            StartCreatingSubMenu();
            foreach (var item in MoveToMenuItems)
            {
                NativeMethods.InsertMenu(subMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_MOVE_TO + item.Key, GetTitle("monitor") + item.Key);
            }
            FinishSubMenu("move_to");

            StartCreatingSubMenu();
            AddSubMenuItem(MenuItemId.SC_ALIGN_TOP_LEFT);
            AddSubMenuItem(MenuItemId.SC_ALIGN_TOP_CENTER);
            AddSubMenuItem(MenuItemId.SC_ALIGN_TOP_RIGHT);
            AddSubMenuItem(MenuItemId.SC_ALIGN_MIDDLE_LEFT);
            AddSubMenuItem(MenuItemId.SC_ALIGN_MIDDLE_CENTER);
            AddSubMenuItem(MenuItemId.SC_ALIGN_MIDDLE_RIGHT);
            AddSubMenuItem(MenuItemId.SC_ALIGN_BOTTOM_LEFT);
            AddSubMenuItem(MenuItemId.SC_ALIGN_BOTTOM_CENTER);
            AddSubMenuItem(MenuItemId.SC_ALIGN_BOTTOM_RIGHT);
            AddSubMenuSeparator();
            AddSubMenuItem(MenuItemId.SC_ALIGN_DEFAULT);
            AddSubMenuSeparator();
            AddSubMenuItem(MenuItemId.SC_ALIGN_CUSTOM);
            FinishSubMenu("alignment");

            StartCreatingSubMenu();
            AddSubMenuItem(MenuItemId.SC_TRANS_00, "0%" + GetTitle("trans_opaque", null, false));
            AddSubMenuItem(MenuItemId.SC_TRANS_10, "10%");
            AddSubMenuItem(MenuItemId.SC_TRANS_20, "20%");
            AddSubMenuItem(MenuItemId.SC_TRANS_30, "30%");
            AddSubMenuItem(MenuItemId.SC_TRANS_40, "40%");
            AddSubMenuItem(MenuItemId.SC_TRANS_50, "50%");
            AddSubMenuItem(MenuItemId.SC_TRANS_60, "60%");
            AddSubMenuItem(MenuItemId.SC_TRANS_70, "70%");
            AddSubMenuItem(MenuItemId.SC_TRANS_80, "80%");
            AddSubMenuItem(MenuItemId.SC_TRANS_90, "90%");
            AddSubMenuItem(MenuItemId.SC_TRANS_100, "100%" + GetTitle("trans_invisible", null, false));
            AddSubMenuSeparator();
            AddSubMenuItem(MenuItemId.SC_TRANS_DEFAULT);
            AddSubMenuSeparator();
            AddSubMenuItem(MenuItemId.SC_TRANS_CUSTOM);
            FinishSubMenu("transparency");

            StartCreatingSubMenu();
            AddSubMenuItem(MenuItemId.SC_PRIORITY_REAL_TIME);
            AddSubMenuItem(MenuItemId.SC_PRIORITY_HIGH);
            AddSubMenuItem(MenuItemId.SC_PRIORITY_ABOVE_NORMAL);
            AddSubMenuItem(MenuItemId.SC_PRIORITY_NORMAL);
            AddSubMenuItem(MenuItemId.SC_PRIORITY_BELOW_NORMAL);
            AddSubMenuItem(MenuItemId.SC_PRIORITY_IDLE);
            FinishSubMenu("priority");

            StartCreatingSubMenu();
            AddSubMenuItem(MenuItemId.SC_MINIMIZE_TO_SYSTEMTRAY);
            AddSubMenuItem(MenuItemId.SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY);
            AddSubMenuItem(MenuItemId.SC_SUSPEND_TO_SYSTEMTRAY);
            FinishSubMenu("system_tray");

            StartCreatingSubMenu();
            AddSubMenuItem(MenuItemId.SC_MINIMIZE_OTHER_WINDOWS);
            AddSubMenuItem(MenuItemId.SC_CLOSE_OTHER_WINDOWS);
            FinishSubMenu("other_windows");

            StartCreatingSubMenu();
            for (int i = 0; i < _menuItems.StartProgramItems.Count; i++)
            {
                NativeMethods.InsertMenu(subMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_START_PROGRAM + i, _menuItems.StartProgramItems[i].Title);
            }
            FinishSubMenu("start_program");


            void StartCreatingSubMenu()
            {
                subMenuHandle = NativeMethods.CreateMenu();
            }

            void AddMenuItem(int id, string title = null, bool showHotkey = true)
            {
                string itemName = MenuItemId.GetName(id);
                if (_menuItems.Items.Any(x => x.Name == itemName && x.Show))
                {
                    _numberItems++;
                    NativeMethods.InsertMenu(windowMenuHandle, ++index, NativeConstants.MF_BYPOSITION, id, GetTitle(itemName, title, showHotkey));
                }
            }

            void AddSubMenuSeparator()
            {
                NativeMethods.InsertMenu(subMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            }

            void AddSubMenuItem(int id, string title = null, bool showHotkey = true)
            {
                string itemName = MenuItemId.GetName(id);
                if (_menuItems.Items.Any(x => x.Name == itemName && x.Show))
                {
                    NativeMethods.InsertMenu(subMenuHandle, -1, NativeConstants.MF_BYPOSITION, id, GetTitle(itemName, title, showHotkey));
                }
            }

            void FinishSubMenu(string itemName, string title = null, bool showHotkey = true)
            {
                if (_menuItems.Items.Any(x => x.Name == itemName && x.Show))
                {
                    _numberItems++;
                    NativeMethods.InsertMenu(windowMenuHandle, ++index, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, subMenuHandle,
                        GetTitle(itemName, title, showHotkey));
                }
                _subMenuHandles.Add(subMenuHandle);
                subMenuHandle = IntPtr.Zero;
            }
        }

        public void Destroy()
        {
            var windowMenuHandle = NativeMethods.GetSystemMenu(WindowHandle, false);
            int Index = NativeMethods.GetMenuItemCount(windowMenuHandle);
            for (int i = 0; i < _numberItems; i++)
            {
                NativeMethods.DeleteMenu(windowMenuHandle, --Index, NativeConstants.MF_BYPOSITION);
            }

            foreach (var handle in _subMenuHandles)
            {
                NativeMethods.DestroyMenu(handle);
            }

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
    }
}