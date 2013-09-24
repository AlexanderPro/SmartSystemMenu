using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartSystemMenu.App_Code.Common;

namespace SmartSystemMenu.App_Code.Hooks
{
    class MouseHook : Hook
    {
        private Int32 msgID_Mouse;
        private Int32 msgID_Mouse_HookReplaced;

        public event EventHandler<EventArgs> HookReplaced;
        public event EventHandler<BasicHookEventArgs> MouseEvent;

        public MouseHook(IntPtr handle) : base(handle)
        {
        }

        protected override void OnStart()
        {
            msgID_Mouse = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_MOUSE");
            msgID_Mouse_HookReplaced = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_MOUSE_REPLACED");

            if (Environment.OSVersion.Version.Major >= 6)
            {
                NativeMethods.ChangeWindowMessageFilter(msgID_Mouse, NativeMethods.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(msgID_Mouse_HookReplaced, NativeMethods.MSGFLT_ADD);
            }
            NativeHookMethods.InitializeMouseHook(0, handle);
        }
        
        protected override void OnStop()
        {
            NativeHookMethods.UninitializeMouseHook();
        }

        public override void ProcessWindowMessage(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == msgID_Mouse)
            {
                RaiseEvent(MouseEvent, new BasicHookEventArgs(m.WParam, m.LParam));
            }
            else if (m.Msg == msgID_Mouse_HookReplaced)
            {
                RaiseEvent(HookReplaced, EventArgs.Empty);
            }
        }
    }
}
