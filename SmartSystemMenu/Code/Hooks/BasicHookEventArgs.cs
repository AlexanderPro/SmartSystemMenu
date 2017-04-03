using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartSystemMenu.Code.Hooks
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
