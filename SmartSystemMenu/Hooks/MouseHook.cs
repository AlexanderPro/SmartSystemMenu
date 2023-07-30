using System;
using System.Windows.Forms;
using static SmartSystemMenu.Native.Constants;
using static SmartSystemMenu.Native.User32;

namespace SmartSystemMenu.Hooks
{
    class MouseHook : Hook
    {
        public event EventHandler<EventArgs> HookReplaced;
        public event EventHandler<BasicHookEventArgs> MouseEvent;

        public MouseHook(IntPtr handle, int dragByMouseMenuItem) : base(handle, dragByMouseMenuItem)
        {
        }

        protected override void OnStart()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                ChangeWindowMessageFilter(WM_SSM_HOOK_MOUSE, MSGFLT_ADD);
                ChangeWindowMessageFilter(WM_SSM_HOOK_MOUSE_REPLACED, MSGFLT_ADD);
            }

            NativeHookMethods.InitializeMouseHook(0, _handle, _dragByMouseMenuItem);
        }

        protected override void OnStop()
        {
            NativeHookMethods.UninitializeMouseHook();
        }

        public override void ProcessWindowMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_SSM_HOOK_MOUSE:
                    {
                        RaiseEvent(MouseEvent, new BasicHookEventArgs(m.WParam, m.LParam));
                    } break;

                case WM_SSM_HOOK_MOUSE_REPLACED:
                    {
                        RaiseEvent(HookReplaced, EventArgs.Empty);
                    } break;
            }
        }
    }
}
