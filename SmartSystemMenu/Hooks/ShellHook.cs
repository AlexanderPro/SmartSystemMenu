using System;

namespace SmartSystemMenu.Hooks
{
    class ShellHook : Hook
    {
        private int _msgIdShellActivateShellWindow;
        private int _msgIdShellGetMinRect;
        private int _msgIdShellLanguage;
        private int _msgIdShellRedraw;
        private int _msgIdShellTaskman;
        private int _msgIdShellHookReplaced;
        private int _msgIdShellWindowActivated;
        private int _msgIdShellWindowCreated;
        private int _msgIdShellWindowDestroyed;

        public event EventHandler<EventArgs> HookReplaced;
        public event EventHandler<EventArgs> ActivateShellWindow;
        public event EventHandler<WindowEventArgs> GetMinRect;
        public event EventHandler<WindowEventArgs> Language;
        public event EventHandler<WindowEventArgs> Redraw;
        public event EventHandler<EventArgs> Taskman;
        public event EventHandler<WindowEventArgs> WindowActivated;
        public event EventHandler<WindowEventArgs> WindowCreated;
        public event EventHandler<WindowEventArgs> WindowDestroyed;

        public ShellHook(IntPtr handle, int dragByMouseMenuItem) : base(handle, dragByMouseMenuItem)
        {
        }

        protected override void OnStart()
        {
            _msgIdShellHookReplaced = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_SHELL_REPLACED");
            _msgIdShellActivateShellWindow = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HSHELL_ACTIVATESHELLWINDOW");
            _msgIdShellGetMinRect = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HSHELL_GETMINRECT");
            _msgIdShellLanguage = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HSHELL_LANGUAGE");
            _msgIdShellRedraw = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HSHELL_REDRAW");
            _msgIdShellTaskman = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HSHELL_TASKMAN");
            _msgIdShellWindowActivated = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HSHELL_WINDOWACTIVATED");
            _msgIdShellWindowCreated = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HSHELL_WINDOWCREATED");
            _msgIdShellWindowDestroyed = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HSHELL_WINDOWDESTROYED");

            if (Environment.OSVersion.Version.Major >= 6)
            {
                NativeMethods.ChangeWindowMessageFilter(_msgIdShellHookReplaced, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(_msgIdShellActivateShellWindow, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(_msgIdShellGetMinRect, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(_msgIdShellLanguage, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(_msgIdShellRedraw, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(_msgIdShellTaskman, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(_msgIdShellWindowActivated, NativeConstants.MSGFLT_ADD);
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
            if (m.Msg == _msgIdShellHookReplaced)
            {
                RaiseEvent(HookReplaced, EventArgs.Empty);
            }
            else if (m.Msg == _msgIdShellActivateShellWindow)
            {
                RaiseEvent(ActivateShellWindow, EventArgs.Empty);
            }
            else if (m.Msg == _msgIdShellGetMinRect)
            {
                RaiseEvent(GetMinRect, new WindowEventArgs(m.WParam));
            }
            else if (m.Msg == _msgIdShellLanguage)
            {
                RaiseEvent(Language, new WindowEventArgs(m.WParam));
            }
            else if (m.Msg == _msgIdShellRedraw)
            {
                RaiseEvent(Redraw, new WindowEventArgs(m.WParam));
            }
            else if (m.Msg == _msgIdShellTaskman)
            {
                RaiseEvent(Taskman, EventArgs.Empty);
            }
            else if (m.Msg == _msgIdShellWindowActivated)
            {
                RaiseEvent(WindowActivated, new WindowEventArgs(m.WParam));
            }
            else if (m.Msg == _msgIdShellWindowCreated)
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
