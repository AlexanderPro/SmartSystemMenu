using System;

namespace SmartSystemMenu.Hooks
{
    class MouseHook : Hook
    {
        private int _msgIdMouse;
        private int _msgIdMouseHookReplaced;

        public event EventHandler<EventArgs> HookReplaced;
        public event EventHandler<BasicHookEventArgs> MouseEvent;

        public MouseHook(IntPtr handle, int dragByMouseMenuItem) : base(handle, dragByMouseMenuItem)
        {
        }

        protected override void OnStart()
        {
            _msgIdMouse = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_MOUSE");
            _msgIdMouseHookReplaced = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_MOUSE_REPLACED");

            if (Environment.OSVersion.Version.Major >= 6)
            {
                NativeMethods.ChangeWindowMessageFilter(_msgIdMouse, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(_msgIdMouseHookReplaced, NativeConstants.MSGFLT_ADD);
            }
            NativeHookMethods.InitializeMouseHook(0, _handle, _dragByMouseMenuItem);
        }

        protected override void OnStop()
        {
            NativeHookMethods.UninitializeMouseHook();
        }

        public override void ProcessWindowMessage(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == _msgIdMouse)
            {
                RaiseEvent(MouseEvent, new BasicHookEventArgs(m.WParam, m.LParam));
            }
            else if (m.Msg == _msgIdMouseHookReplaced)
            {
                RaiseEvent(HookReplaced, EventArgs.Empty);
            }
        }
    }
}
