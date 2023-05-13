using System;
using System.Windows.Forms;

namespace SmartSystemMenu
{
    class Win32WindowWrapper : IWin32Window
    {
        public IntPtr Handle { get; }

        public Win32WindowWrapper(IntPtr handle)
        {
            Handle = handle;
        }
    }
}
