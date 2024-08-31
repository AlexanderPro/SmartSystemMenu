using System;
using System.Windows.Forms;
using static SmartSystemMenu.Native.Constants;
using static SmartSystemMenu.Native.User32;
using static SmartSystemMenu.Native.Hooks;

namespace SmartSystemMenu.Hooks
{
    class ShellHook : Hook
    {
        public event EventHandler<WindowEventArgs> WindowCreated;
        public event EventHandler<WindowEventArgs> WindowDestroyed;

        public ShellHook(IntPtr handle) : base(handle)
        {
        }

        protected override void OnStart()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                ChangeWindowMessageFilter(WM_SSM_HOOK_HSHELL_WINDOWCREATED, MSGFLT_ADD);
                ChangeWindowMessageFilter(WM_SSM_HOOK_HSHELL_WINDOWDESTROYED, MSGFLT_ADD);
            }

            InitializeShellHook(0, _handle);
        }

        protected override void OnStop()
        {
            UninitializeShellHook();
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