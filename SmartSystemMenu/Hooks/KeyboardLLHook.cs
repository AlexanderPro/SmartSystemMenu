using System;
using SmartSystemMenu.Native;

namespace SmartSystemMenu.Hooks
{
    class KeyboardLLHook : Hook
    {
        private int _msgIdKeyboardLL;
        private int _msgIdKeyboardLLHookReplaced;

        public event EventHandler<EventArgs> HookReplaced;
        public event EventHandler<BasicHookEventArgs> KeyboardLLEvent;

        public KeyboardLLHook(IntPtr handle, int dragByMouseMenuItem) : base(handle, dragByMouseMenuItem)
        {
        }

        protected override void OnStart()
        {
            _msgIdKeyboardLL = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_KEYBOARDLL");
            _msgIdKeyboardLLHookReplaced = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_KEYBOARDLL_REPLACED");

            if (Environment.OSVersion.Version.Major >= 6)
            {
                NativeMethods.ChangeWindowMessageFilter(_msgIdKeyboardLL, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(_msgIdKeyboardLLHookReplaced, NativeConstants.MSGFLT_ADD);
            }
            NativeHookMethods.InitializeKeyboardLLHook(0, _handle, _dragByMouseMenuItem);
        }

        protected override void OnStop()
        {
            NativeHookMethods.UninitializeKeyboardLLHook();
        }

        public override void ProcessWindowMessage(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == _msgIdKeyboardLL)
            {
                RaiseEvent(KeyboardLLEvent, new BasicHookEventArgs(m.WParam, m.LParam));
            }
            else if (m.Msg == _msgIdKeyboardLLHookReplaced)
            {
                RaiseEvent(HookReplaced, EventArgs.Empty);
            }
        }
    }
}
