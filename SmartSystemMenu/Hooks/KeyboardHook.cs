using System;

namespace SmartSystemMenu.Hooks
{
    class KeyboardHook : Hook
    {
        private int _msgIdKeyboard;
        private int _msgIdKeyboardHookReplaced;

        public event EventHandler<EventArgs> HookReplaced;
        public event EventHandler<BasicHookEventArgs> KeyboardEvent;

        public KeyboardHook(IntPtr handle, int dragByMouseMenuItem) : base(handle, dragByMouseMenuItem)
        {
        }

        protected override void OnStart()
        {
            _msgIdKeyboard = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_KEYBOARD");
            _msgIdKeyboardHookReplaced = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_KEYBOARD_REPLACED");

            if (Environment.OSVersion.Version.Major >= 6)
            {
                NativeMethods.ChangeWindowMessageFilter(_msgIdKeyboard, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(_msgIdKeyboardHookReplaced, NativeConstants.MSGFLT_ADD);
            }
            NativeHookMethods.InitializeKeyboardHook(0, _handle, _dragByMouseMenuItem);
        }

        protected override void OnStop()
        {
            NativeHookMethods.UninitializeKeyboardHook();
        }

        public override void ProcessWindowMessage(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == _msgIdKeyboard)
            {
                RaiseEvent(KeyboardEvent, new BasicHookEventArgs(m.WParam, m.LParam));
            }
            else if (m.Msg == _msgIdKeyboardHookReplaced)
            {
                RaiseEvent(HookReplaced, EventArgs.Empty);
            }
        }
    }
}
