using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartSystemMenu.Code.Common;

namespace SmartSystemMenu.Code.Hooks
{
    class KeyboardHook : Hook
    {
        private Int32 msgID_Keyboard;
        private Int32 msgID_Keyboard_HookReplaced;

        public event EventHandler<EventArgs> HookReplaced;
        public event EventHandler<BasicHookEventArgs> KeyboardEvent;

        public KeyboardHook(IntPtr handle) : base(handle)
        {
        }

        protected override void OnStart()
        {
            msgID_Keyboard = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_KEYBOARD");
            msgID_Keyboard_HookReplaced = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_KEYBOARD_REPLACED");

            if (Environment.OSVersion.Version.Major >= 6)
            {
                NativeMethods.ChangeWindowMessageFilter(msgID_Keyboard, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(msgID_Keyboard_HookReplaced, NativeConstants.MSGFLT_ADD);
            }
            NativeHookMethods.InitializeKeyboardHook(0, handle);
        }

        protected override void OnStop()
        {
            NativeHookMethods.UninitializeKeyboardHook();
        }

        public override void ProcessWindowMessage(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == msgID_Keyboard)
            {
                RaiseEvent(KeyboardEvent, new BasicHookEventArgs(m.WParam, m.LParam));
            }
            else if (m.Msg == msgID_Keyboard_HookReplaced)
            {
                RaiseEvent(HookReplaced, EventArgs.Empty);
            }
        }
    }
}
