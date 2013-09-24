using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartSystemMenu.App_Code.Hooks
{
    class WindowEventArgs : EventArgs
    {
        public IntPtr Handle { get; private set; }

        public WindowEventArgs(IntPtr handle)
        {
            Handle = handle;
        }
    }
}
