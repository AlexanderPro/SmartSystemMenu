using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using SmartSystemMenu.Native;
using SmartSystemMenu.Native.Enums;
using SmartSystemMenu.Native.Structs;
using SmartSystemMenu.Settings;
using static SmartSystemMenu.Native.User32;

namespace SmartSystemMenu
{
    class SystemMenu
    {
        private const int DEFAULT_SYSTEM_MENU_NUMBER_ITEMS = 7;

        private readonly MenuItems _menuItems;
        private readonly LanguageSettings _languageSettings;

        public IntPtr WindowHandle { get; }

        public IDictionary<int, IntPtr> MoveToMenuItems { get; }

        public IntPtr MenuHandle
        {
            get
            {
                return GetSystemMenu(WindowHandle, false);
            }
        }

        public bool Exists
        {
            get
            {
                var menuHandle = GetSystemMenu(WindowHandle, false);
                var existsWindowMenu = menuHandle != IntPtr.Zero;
                return existsWindowMenu;
            }
        }

        public SystemMenu(IntPtr windowHandle, MenuItems menuItems, LanguageSettings languageSettings)
        {
            _menuItems = menuItems;
            _languageSettings = languageSettings;
            WindowHandle = windowHandle;
            MoveToMenuItems = SystemUtils.GetMonitors().Select((x, i) => new KeyValuePair<int, IntPtr>(i + 1, x)).ToDictionary(x => x.Key, y => y.Value);
        }

        public bool Create()
        {
            var menuHandle = GetSystemMenu(WindowHandle, false);
            if (menuHandle == IntPtr.Zero)
            {
                return false;
            }

            foreach (var item in _menuItems.Items)
            {
                if (item.Type == MenuItemType.Item && item.Show)
                {
                    var id = MenuItemId.GetId(item.Name);
                    var title = GetTransparencyTitle(id);
                    title = GetTitle(item.Name, title, true);
                    if (!IsMenuItem(menuHandle, id))
                    {
                        InsertMenu(menuHandle, MenuItemId.SC_CLOSE, Constants.MF_BYCOMMAND, id, title);
                    }
                }

                if (item.Type == MenuItemType.Separator && item.Show)
                {
                    InsertMenu(menuHandle, MenuItemId.SC_CLOSE, Constants.MF_BYCOMMAND | Constants.MF_SEPARATOR, MenuItemId.SC_SEPARATOR, null);
                }

                if (item.Type == MenuItemType.Group && item.Show)
                {
                    var subMenuHandle = CreateMenu();

                    if (item.Name.ToLower() == "size")
                    {
                        for (int i = 0; i < _menuItems.WindowSizeItems.Count; i++)
                        {
                            var subItemId = MenuItemId.SC_SIZE_DEFINED + i;
                            _menuItems.WindowSizeItems[i].Id = subItemId;
                            if (!IsMenuItem(subMenuHandle, subItemId))
                            {
                                AppendMenu(subMenuHandle, Constants.MF_BYCOMMAND, subItemId, GetTitle(_menuItems.WindowSizeItems[i]));
                            }
                        }
                    }

                    if (item.Name.ToLower() == "move_to")
                    {
                        foreach (var moveToMenuItem in MoveToMenuItems)
                        {
                            AppendMenu(subMenuHandle, Constants.MF_BYCOMMAND, MenuItemId.SC_MOVE_TO + moveToMenuItem.Key, GetTitle("monitor") + moveToMenuItem.Key);
                        }
                    }

                    if (item.Name.ToLower() == "start_program")
                    {
                        for (int i = 0; i < _menuItems.StartProgramItems.Count; i++)
                        {
                            AppendMenu(subMenuHandle, Constants.MF_BYCOMMAND, MenuItemId.SC_START_PROGRAM + i, _menuItems.StartProgramItems[i].Title);
                        }
                    }

                    foreach (var subItem in item.Items)
                    {
                        if (subItem.Type == MenuItemType.Item && subItem.Show)
                        {
                            var subItemId = MenuItemId.GetId(subItem.Name);
                            var title = GetTransparencyTitle(subItemId);
                            title = GetTitle(subItem.Name, title, true);
                            if (!IsMenuItem(subMenuHandle, subItemId))
                            {
                                InsertMenu(subMenuHandle, -1, Constants.MF_BYCOMMAND, subItemId, title);
                            }
                        }

                        if (subItem.Type == MenuItemType.Separator && subItem.Show)
                        {
                            InsertMenu(subMenuHandle, -1, Constants.MF_BYCOMMAND | Constants.MF_SEPARATOR, MenuItemId.SC_SEPARATOR, null);
                        }
                    }

                    var id = MenuItemId.GetId(item.Name);
                    if (!IsMenuItem(menuHandle, id))
                    {
                        InsertSubMenu(menuHandle, subMenuHandle, MenuItemId.SC_CLOSE, Constants.MF_BYCOMMAND | Constants.MF_POPUP, (uint)id, GetTitle(item.Name, null, true));
                    }
                    subMenuHandle = IntPtr.Zero;
                }
            }

            if (!IsMenuItem(menuHandle, MenuItemId.SC_SEPARATOR_BOTTOM))
            {
                InsertMenu(menuHandle, MenuItemId.SC_CLOSE, Constants.MF_BYCOMMAND | Constants.MF_SEPARATOR, MenuItemId.SC_SEPARATOR_BOTTOM, null);
            }

            return true;
        }

        public void Destroy(bool restoreMenu = true)
        {
            var menuHandle = GetSystemMenu(WindowHandle, false);
            if (menuHandle == IntPtr.Zero)
            {
                return;
            }

            foreach (var item in _menuItems.Items.Where(x => x.Show))
            {
                var id = MenuItemId.GetId(item.Name);
                if (id > 0)
                {
                    DeleteMenu(menuHandle, id, Constants.MF_BYCOMMAND);
                }
                else if (item.Type == MenuItemType.Separator)
                {
                    DeleteMenu(menuHandle, MenuItemId.SC_SEPARATOR, Constants.MF_SEPARATOR);
                }
            }

            DeleteMenu(menuHandle, MenuItemId.SC_SEPARATOR_BOTTOM, Constants.MF_BYCOMMAND);

            if (restoreMenu)
            {
                var numberItems = GetMenuItemCount(menuHandle);
                if (numberItems == DEFAULT_SYSTEM_MENU_NUMBER_ITEMS)
                {
                    GetSystemMenu(WindowHandle, true);
                }
            }
        }

        public void CheckMenuItem(int id, bool check)
        {
            var menuHandle = GetSystemMenu(WindowHandle, false);
            User32.CheckMenuItem(menuHandle, id, check ? Constants.MF_CHECKED : Constants.MF_UNCHECKED);
        }

        public void UncheckMenuItems(params int[] ids)
        {
            var menuHandle = GetSystemMenu(WindowHandle, false);
            foreach (var id in ids)
            {
                User32.CheckMenuItem(menuHandle, id, Constants.MF_UNCHECKED);
            }
        }

        public bool IsMenuItemChecked(int id)
        {
            var menuHandle = GetSystemMenu(WindowHandle, false);
            var flags = GetMenuState(menuHandle, id, Constants.MF_BYCOMMAND);
            var isChecked = flags != -1 && (flags & Constants.MF_CHECKED) != 0;
            return isChecked;
        }

        public void UncheckPriorityMenu()
        {
            var menuHandle = GetSystemMenu(WindowHandle, false);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_PRIORITY_REAL_TIME, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_PRIORITY_HIGH, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_PRIORITY_ABOVE_NORMAL, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_PRIORITY_NORMAL, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_PRIORITY_BELOW_NORMAL, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_PRIORITY_IDLE, Constants.MF_UNCHECKED);
        }

        public void UncheckAlignmentMenu()
        {
            var menuHandle = GetSystemMenu(WindowHandle, false);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_ALIGN_TOP_LEFT, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_ALIGN_TOP_CENTER, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_ALIGN_TOP_RIGHT, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_ALIGN_MIDDLE_LEFT, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_ALIGN_MIDDLE_CENTER, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_ALIGN_MIDDLE_RIGHT, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_ALIGN_BOTTOM_LEFT, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_ALIGN_BOTTOM_CENTER, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_ALIGN_BOTTOM_RIGHT, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_ALIGN_CENTER_HORIZONTALLY, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_ALIGN_CENTER_VERTICALLY, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_ALIGN_DEFAULT, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_ALIGN_CUSTOM, Constants.MF_UNCHECKED);
        }

        public void UncheckSizeMenu()
        {
            var menuHandle = GetSystemMenu(WindowHandle, false);
            var windowSizeMenuItemIds = _menuItems.WindowSizeItems.Select(x => x.Id).ToArray();
            UncheckMenuItems(windowSizeMenuItemIds);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_SIZE_DEFAULT, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_SIZE_CUSTOM, Constants.MF_UNCHECKED);
        }

        public void UncheckTransparencyMenu()
        {
            var menuHandle = GetSystemMenu(WindowHandle, false);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_TRANS_100, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_TRANS_90, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_TRANS_80, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_TRANS_70, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_TRANS_60, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_TRANS_50, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_TRANS_40, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_TRANS_30, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_TRANS_20, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_TRANS_10, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_TRANS_00, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_TRANS_CUSTOM, Constants.MF_UNCHECKED);
            User32.CheckMenuItem(menuHandle, MenuItemId.SC_TRANS_DEFAULT, Constants.MF_UNCHECKED);
        }

        public bool IsMenuItem(IntPtr menuHandle, int item)
        {
            var mmi = new MenuItemInfo();
            mmi.cbSize = (uint)Marshal.SizeOf(mmi);
            mmi.fMask = MIIM.ID;
            return GetMenuItemInfo(menuHandle, item, false, ref mmi);
        }

        private string GetTransparencyTitle(int id) => id switch
        {
            MenuItemId.SC_TRANS_00 => "0%" + GetTitle("trans_opaque", null, false),
            MenuItemId.SC_TRANS_10 => "10%",
            MenuItemId.SC_TRANS_20 => "20%",
            MenuItemId.SC_TRANS_30 => "30%",
            MenuItemId.SC_TRANS_40 => "40%",
            MenuItemId.SC_TRANS_50 => "50%",
            MenuItemId.SC_TRANS_60 => "60%",
            MenuItemId.SC_TRANS_70 => "70%",
            MenuItemId.SC_TRANS_80 => "80%",
            MenuItemId.SC_TRANS_90 => "90%",
            MenuItemId.SC_TRANS_100 => "100%" + GetTitle("trans_invisible", null, false),
            _ => null
        };

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

        private bool InsertSubMenu(IntPtr menuHandle, IntPtr subMenuHandle, int uPosition, int uFlags, uint uIDNewItem, string lpNewItem)
        {
            if (InsertMenu(menuHandle, uPosition, uFlags, subMenuHandle, lpNewItem))
            {
                var mmi = new MenuItemInfo();
                mmi.cbSize = (uint)Marshal.SizeOf(mmi);
                mmi.fMask = MIIM.ID;
                mmi.wID = uIDNewItem;
                return SetMenuItemInfo(menuHandle, subMenuHandle.ToInt32(), false, ref mmi);
            }

            return true;
        }
    }
}