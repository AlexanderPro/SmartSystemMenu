using System;

namespace SmartSystemMenu.Hooks
{
    class WndProcEventArgs : EventArgs
    {
        public IntPtr Handle { get; }

        public IntPtr Message { get; }

        public IntPtr WParam { get; }

        public IntPtr LParam { get; }

        public WndProcEventArgs(IntPtr handle, IntPtr message, IntPtr wParam, IntPtr lParam)
        {
            Handle = handle;
            Message = message;
            WParam = wParam;
            LParam = lParam;
        }
    }
}
