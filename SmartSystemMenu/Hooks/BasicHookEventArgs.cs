using System;

namespace SmartSystemMenu.Hooks
{
    class BasicHookEventArgs : EventArgs
    {
        public IntPtr WParam { get; }

        public IntPtr LParam { get; }

        public BasicHookEventArgs(IntPtr wParam, IntPtr lParam)
        {
            WParam = wParam;
            LParam = lParam;
        }
    }
}
