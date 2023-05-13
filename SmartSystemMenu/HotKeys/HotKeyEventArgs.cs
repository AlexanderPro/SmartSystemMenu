using System;

namespace SmartSystemMenu.HotKeys
{
    class HotKeyEventArgs : EventArgs
    {
        public int MenuItemId { get; }

        public bool Succeeded { get; set; }

        public HotKeyEventArgs(int menuItemId)
        {
            MenuItemId = menuItemId;
        }
    }
}
