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
            var index = NativeMethods.GetMenuItemCount(windowMenuHandle);
            _wasOriginalBefore = index > 0 && NativeMethods.GetMenuItemID(windowMenuHandle, index - 1) == MenuItemId.SC_CLOSE;
            _numberItems++;
            NativeMethods.InsertMenu(windowMenuHandle, index, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, IntPtr.Zero, "");

            foreach(var item in _menuItems.Items)
            {
                if (item.Type == MenuItemType.Item && item.Show)
                {
                    _numberItems++;
                    var id = MenuItemId.GetId(item.Name);
                    var title = GetTransparencyTitle(id);
                    title = GetTitle(item.Name, title, true);
                    NativeMethods.InsertMenu(windowMenuHandle, ++index, NativeConstants.MF_BYPOSITION, id, title);
                }

                if (item.Type == MenuItemType.Separator && item.Show)
                {
                    _numberItems++;
                    NativeMethods.InsertMenu(windowMenuHandle, ++index, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
                }

                if (item.Type == MenuItemType.Group && item.Show)
                {
                    var subMenuHandle = NativeMethods.CreateMenu();

                    if (item.Name.ToLower() == "size")
                    {
                        for (int i = 0; i < _menuItems.WindowSizeItems.Count; i++)
                        {
                            var id = MenuItemId.SC_SIZE_DEFINED + i;
                            _menuItems.WindowSizeItems[i].Id = id;
                            NativeMethods.InsertMenu(subMenuHandle, -1, NativeConstants.MF_BYPOSITION, id, GetTitle(_menuItems.WindowSizeItems[i]));
                        }
                    }

                    if (item.Name.ToLower() == "move_to")
                    {
                        foreach (var moveToMenuItem in MoveToMenuItems)
                        {
                            NativeMethods.InsertMenu(subMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_MOVE_TO + moveToMenuItem.Key, GetTitle("monitor") + moveToMenuItem.Key);
                        }
                    }

                    if (item.Name.ToLower() == "start_program")
                    {
                        for (int i = 0; i < _menuItems.StartProgramItems.Count; i++)
                        {
                            NativeMethods.InsertMenu(subMenuHandle, -1, NativeConstants.MF_BYPOSITION, MenuItemId.SC_START_PROGRAM + i, _menuItems.StartProgramItems[i].Title);
                        }
                    }

                    foreach (var subItem in item.Items)
                    {
                        if (subItem.Type == MenuItemType.Item && subItem.Show)
                        {
                            var id = MenuItemId.GetId(subItem.Name);
                            var title = GetTransparencyTitle(id);
                            title = GetTitle(subItem.Name, title, true);
                            NativeMethods.InsertMenu(subMenuHandle, -1, NativeConstants.MF_BYPOSITION, id, title);
                        }

                        if (subItem.Type == MenuItemType.Separator && subItem.Show)
                        {
                            NativeMethods.InsertMenu(subMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
                        }
                    }
                    _numberItems++;
                    NativeMethods.InsertMenu(windowMenuHandle, ++index, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, subMenuHandle, GetTitle(item.Name, null, true));
                    _subMenuHandles.Add(subMenuHandle);
                    subMenuHandle = IntPtr.Zero;
                }
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
            var isChecked = flags != -1 && (flags & NativeConstants.MF_CHECKED) != 0;
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
            var windowSizeMenuItemIds = _menuItems.WindowSizeItems.Select(x => x.Id).ToArray();
            UncheckMenuItems(windowSizeMenuItemIds);
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

        private string GetTransparencyTitle(int id)
        {
            switch (id)
            {
                case MenuItemId.SC_TRANS_00: return "0%" + GetTitle("trans_opaque", null, false);
                case MenuItemId.SC_TRANS_10: return "10%";
                case MenuItemId.SC_TRANS_20: return "20%";
                case MenuItemId.SC_TRANS_30: return "30%";
                case MenuItemId.SC_TRANS_40: return "40%";
                case MenuItemId.SC_TRANS_50: return "50%";
                case MenuItemId.SC_TRANS_60: return "60%";
                case MenuItemId.SC_TRANS_70: return "70%";
                case MenuItemId.SC_TRANS_80: return "80%";
                case MenuItemId.SC_TRANS_90: return "90%";
                case MenuItemId.SC_TRANS_100: return "100%" + GetTitle("trans_invisible", null, false);
                default: return null;
            }
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

        private string GetTitle(WindowSizeMenuItem item)
        {
            var hotKey = _menuItems.GetHotKeysCombination(item.Id);
            return string.IsNullOrEmpty(hotKey) ? item.Title : item.Title + "\t" + hotKey;
        }
    }
}