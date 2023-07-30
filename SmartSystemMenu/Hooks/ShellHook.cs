using System;
using System.Windows.Forms;
using SmartSystemMenu.Native;
using static SmartSystemMenu.Native.Constants;
using static SmartSystemMenu.Native.User32;

namespace SmartSystemMenu.Hooks
{
    class ShellHook : Hook
    {
        public event EventHandler<WindowEventArgs> WindowCreated;
        public event EventHandler<WindowEventArgs> WindowDestroyed;

        public ShellHook(IntPtr handle, int dragByMouseMenuItem) : base(handle, dragByMouseMenuItem)
        {
        }

        protected override void OnStart()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                ChangeWindowMessageFilter(WM_SSM_HOOK_HSHELL_WINDOWCREATED, MSGFLT_ADD);
                ChangeWindowMessageFilter(WM_SSM_HOOK_HSHELL_WINDOWDESTROYED, MSGFLT_ADD);
            }

            NativeHookMethods.InitializeShellHook(0, _handle, _dragByMouseMenuItem);
        }

        protected override void OnStop()
        {
            NativeHookMethods.UninitializeShellHook();
        }

        public override void ProcessWindowMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_SSM_HOOK_HSHELL_WINDOWCREATED:
                    {
                        RaiseEvent(WindowCreated, new WindowEventArgs(m.WParam));
                    }
                    break;

                case WM_SSM_HOOK_HSHELL_WINDOWDESTROYED:
                    {
                        RaiseEvent(WindowDestroyed, new WindowEventArgs(m.WParam));
                    }
                    break;
            }
        }
    }
}