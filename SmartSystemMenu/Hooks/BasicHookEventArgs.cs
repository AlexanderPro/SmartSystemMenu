using System;

namespace SmartSystemMenu.Hooks
{
    class BasicHookEventArgs : EventArgs
    {
        public IntPtr WParam { get; private set; }

        public IntPtr LParam { get; private set; }

        public BasicHookEventArgs(IntPtr wParam, IntPtr lParam)
        {
            WParam = wParam;
            LParam = lParam;
        }
    }
}
