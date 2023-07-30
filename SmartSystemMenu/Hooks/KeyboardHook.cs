using System;
using System.Windows.Forms;
using static SmartSystemMenu.Native.Constants;
using static SmartSystemMenu.Native.User32;

namespace SmartSystemMenu.Hooks
{
    class KeyboardHook : Hook
    {
        public event EventHandler<EventArgs> HookReplaced;
        public event EventHandler<BasicHookEventArgs> KeyboardEvent;

        public KeyboardHook(IntPtr handle, int dragByMouseMenuItem) : base(handle, dragByMouseMenuItem)
        {
        }

        protected override void OnStart()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                ChangeWindowMessageFilter(WM_SSM_HOOK_KEYBOARD, MSGFLT_ADD);
                ChangeWindowMessageFilter(WM_SSM_HOOK_KEYBOARD_REPLACED, MSGFLT_ADD);
            }

            NativeHookMethods.InitializeKeyboardHook(0, _handle, _dragByMouseMenuItem);
        }

        protected override void OnStop()
        {
            NativeHookMethods.UninitializeKeyboardHook();
        }

        public override void ProcessWindowMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_SSM_HOOK_KEYBOARD:
                    {
                        RaiseEvent(KeyboardEvent, new BasicHookEventArgs(m.WParam, m.LParam));
                    }
                    break;

                case WM_SSM_HOOK_KEYBOARD_REPLACED:
                    {
                        RaiseEvent(HookReplaced, EventArgs.Empty);
                    }
                    break;

            }
        }
    }
}