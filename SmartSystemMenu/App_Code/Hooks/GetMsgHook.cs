using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartSystemMenu.App_Code.Common;

namespace SmartSystemMenu.App_Code.Hooks
{
    class GetMsgHook : Hook
    {
        private Int32 msgID_GetMsg;
        private Int32 msgID_GetMsg_Params;
        private Int32 msgID_GetMsg_HookReplaced;
        private IntPtr cacheHandle;
        private IntPtr cacheMessage;

        public event EventHandler<EventArgs> HookReplaced;
        public event EventHandler<WndProcEventArgs> GetMsg;

        public GetMsgHook(IntPtr handle) : base(handle)
        {
        }

        protected override void OnStart()
        {
            msgID_GetMsg_HookReplaced = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_GETMSG_REPLACED");
            msgID_GetMsg = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_GETMSG");
            msgID_GetMsg_Params = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_GETMSG_PARAMS");

            if (Environment.OSVersion.Version.Major >= 6)
            {
                NativeMethods.ChangeWindowMessageFilter(msgID_GetMsg_HookReplaced, NativeMethods.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(msgID_GetMsg, NativeMethods.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(msgID_GetMsg_Params, NativeMethods.MSGFLT_ADD);
            }
            NativeHookMethods.InitializeGetMsgHook(0, handle);
        }

        protected override void OnStop()
        {
            NativeHookMethods.UninitializeGetMsgHook();
        }

        public override void ProcessWindowMessage(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == msgID_GetMsg)
            {
                cacheHandle = m.WParam;
                cacheMessage = m.LParam;
            }
            else if (m.Msg == msgID_GetMsg_Params)
            {
                if (GetMsg != null && cacheHandle != IntPtr.Zero && cacheMessage != IntPtr.Zero)
                {
                    RaiseEvent(GetMsg, new WndProcEventArgs(cacheHandle, cacheMessage, m.WParam, m.LParam));
                }
                cacheHandle = IntPtr.Zero;
                cacheMessage = IntPtr.Zero;
            }
            else if (m.Msg == msgID_GetMsg_HookReplaced)
            {
                  RaiseEvent(HookReplaced, EventArgs.Empty);
            }
        }
    }
}
