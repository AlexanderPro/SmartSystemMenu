using System;

namespace SmartSystemMenu.Hooks
{
    class WindowEventArgs : EventArgs
    {
        public IntPtr Handle { get; }

        public WindowEventArgs(IntPtr handle)
        {
            Handle = handle;
        }
    }
}
