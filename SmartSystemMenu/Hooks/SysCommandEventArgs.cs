using System;

namespace SmartSystemMenu.Hooks
{
    class SysCommandEventArgs : EventArgs
    {
        public IntPtr WParam { get; }

        public IntPtr LParam { get; }

        public SysCommandEventArgs(IntPtr wParam, IntPtr lParam)
        {
            WParam = wParam;
            LParam = lParam;
        }
    }
}
