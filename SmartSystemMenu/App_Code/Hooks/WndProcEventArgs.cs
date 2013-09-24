using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartSystemMenu.App_Code.Hooks
{
    class WndProcEventArgs : EventArgs
    {
        public IntPtr Handle { get; private set; }

        public IntPtr Message { get; private set; }

        public IntPtr WParam { get; private set; }

        public IntPtr LParam { get; private set; }

        public WndProcEventArgs(IntPtr handle, IntPtr message, IntPtr wParam, IntPtr lParam)
        {
            Handle = handle;
            Message = message;
            WParam = wParam;
            LParam = lParam;
        }
    }
}
