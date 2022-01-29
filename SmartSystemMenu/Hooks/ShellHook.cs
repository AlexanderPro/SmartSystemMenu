using System;
using SmartSystemMenu.Native;

namespace SmartSystemMenu.Hooks
{
    class ShellHook : Hook
    {
        private int _msgIdShellWindowCreated;
        private int _msgIdShellWindowDestroyed;

        public event EventHandler<WindowEventArgs> WindowCreated;
        public event EventHandler<WindowEventArgs> WindowDestroyed;

        public ShellHook(IntPtr handle, int dragByMouseMenuItem) : base(handle, dragByMouseMenuItem)
        {
        }

        protected override void OnStart()
        {
            _msgIdShellWindowCreated = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HSHELL_WINDOWCREATED");
            _msgIdShellWindowDestroyed = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HSHELL_WINDOWDESTROYED");

            if (Environment.OSVersion.Version.Major >= 6)
            {
                NativeMethods.ChangeWindowMessageFilter(_msgIdShellWindowCreated, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(_msgIdShellWindowDestroyed, NativeConstants.MSGFLT_ADD);
            }
            NativeHookMethods.InitializeShellHook(0, _handle, _dragByMouseMenuItem);
        }

        protected override void OnStop()
        {
            NativeHookMethods.UninitializeShellHook();
        }

        public override void ProcessWindowMessage(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == _msgIdShellWindowCreated)
            {
                RaiseEvent(WindowCreated, new WindowEventArgs(m.WParam));
            }
            else if (m.Msg == _msgIdShellWindowDestroyed)
            {
                RaiseEvent(WindowDestroyed, new WindowEventArgs(m.WParam));
            }
        }
    }
}
