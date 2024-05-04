using System;
using System.Windows.Forms;

namespace SmartSystemMenu
{
    class Win32Window : IWin32Window
    {
        public IntPtr Handle { get; }

        public Win32Window(IntPtr handle)
        {
            Handle = handle;
        }
    }
}
