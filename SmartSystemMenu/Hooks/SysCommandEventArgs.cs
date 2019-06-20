using System;

namespace SmartSystemMenu.Hooks
{
    class SysCommandEventArgs : EventArgs
    {
        public IntPtr WParam { get; private set; }

        public IntPtr LParam { get; private set; }

        public SysCommandEventArgs(IntPtr wParam, IntPtr lParam)
        {
            WParam = wParam;
            LParam = lParam;
        }
    }
}
