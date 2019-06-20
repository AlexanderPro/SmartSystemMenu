using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartSystemMenu;

namespace SmartSystemMenu.Hooks
{
    class ShellHook : Hook
    {
        private int msgID_Shell_ActivateShellWindow;
        private int msgID_Shell_GetMinRect;
        private int msgID_Shell_Language;
        private int msgID_Shell_Redraw;
        private int msgID_Shell_Taskman;
        private int msgID_Shell_HookReplaced;
        private int msgID_Shell_WindowActivated;
        private int msgID_Shell_WindowCreated;
        private int msgID_Shell_WindowDestroyed;

        public event EventHandler<EventArgs> HookReplaced;
        public event EventHandler<EventArgs> ActivateShellWindow;
        public event EventHandler<WindowEventArgs> GetMinRect;
        public event EventHandler<WindowEventArgs> Language;
        public event EventHandler<WindowEventArgs> Redraw;
        public event EventHandler<EventArgs> Taskman;
        public event EventHandler<WindowEventArgs> WindowActivated;
        public event EventHandler<WindowEventArgs> WindowCreated;
        public event EventHandler<WindowEventArgs> WindowDestroyed;

        public ShellHook(IntPtr handle) : base(handle)
        {
        }

        protected override void OnStart()
        {
            msgID_Shell_HookReplaced = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_SHELL_REPLACED");
            msgID_Shell_ActivateShellWindow = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HSHELL_ACTIVATESHELLWINDOW");
            msgID_Shell_GetMinRect = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HSHELL_GETMINRECT");
            msgID_Shell_Language = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HSHELL_LANGUAGE");
            msgID_Shell_Redraw = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HSHELL_REDRAW");
            msgID_Shell_Taskman = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HSHELL_TASKMAN");
            msgID_Shell_WindowActivated = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HSHELL_WINDOWACTIVATED");
            msgID_Shell_WindowCreated = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HSHELL_WINDOWCREATED");
            msgID_Shell_WindowDestroyed = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HSHELL_WINDOWDESTROYED");

            if (Environment.OSVersion.Version.Major >= 6)
            {
                NativeMethods.ChangeWindowMessageFilter(msgID_Shell_HookReplaced, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(msgID_Shell_ActivateShellWindow, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(msgID_Shell_GetMinRect, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(msgID_Shell_Language, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(msgID_Shell_Redraw, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(msgID_Shell_Taskman, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(msgID_Shell_WindowActivated, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(msgID_Shell_WindowCreated, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(msgID_Shell_WindowDestroyed, NativeConstants.MSGFLT_ADD);
            }
            NativeHookMethods.InitializeShellHook(0, handle);
        }

        protected override void OnStop()
        {
            NativeHookMethods.UninitializeShellHook();
        }

        public override void ProcessWindowMessage(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == msgID_Shell_HookReplaced)
            {
                RaiseEvent(HookReplaced, EventArgs.Empty);
            }
            else if (m.Msg == msgID_Shell_ActivateShellWindow)
            {
                RaiseEvent(ActivateShellWindow, EventArgs.Empty);
            }
            else if (m.Msg == msgID_Shell_GetMinRect)
            {
                RaiseEvent(GetMinRect, new WindowEventArgs(m.WParam));
            }
            else if (m.Msg == msgID_Shell_Language)
            {
                RaiseEvent(Language, new WindowEventArgs(m.WParam));
            }
            else if (m.Msg == msgID_Shell_Redraw)
            {
                RaiseEvent(Redraw, new WindowEventArgs(m.WParam));
            }
            else if (m.Msg == msgID_Shell_Taskman)
            {
                RaiseEvent(Taskman, EventArgs.Empty);
            }
            else if (m.Msg == msgID_Shell_WindowActivated)
            {
                RaiseEvent(WindowActivated, new WindowEventArgs(m.WParam));
            }
            else if (m.Msg == msgID_Shell_WindowCreated)
            {
                RaiseEvent(WindowCreated, new WindowEventArgs(m.WParam));
            }
            else if (m.Msg == msgID_Shell_WindowDestroyed)
            {
                RaiseEvent(WindowDestroyed, new WindowEventArgs(m.WParam));
            }
        }
    }
}
