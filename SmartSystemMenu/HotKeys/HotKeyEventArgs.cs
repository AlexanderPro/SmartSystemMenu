using System;

namespace SmartSystemMenu.HotKeys
{
    class HotKeyEventArgs : EventArgs
    {
        public int MenuItemId { get; private set; }

        public HotKeyEventArgs(int menuItemId)
        {
            MenuItemId = menuItemId;
        }
    }
}
