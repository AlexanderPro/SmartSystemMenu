using System;

namespace SmartSystemMenu.HotKeys
{
    class HotKeyHookEventArgs : EventArgs
    {
        public int MenuItemId { get; private set; }

        public HotKeyHookEventArgs(int menuItemId)
        {
            MenuItemId = menuItemId;
        }
    }
}
