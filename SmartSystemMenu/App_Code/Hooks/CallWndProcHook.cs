using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartSystemMenu.App_Code.Common;

namespace SmartSystemMenu.App_Code.Hooks
{
    class CallWndProcHook : Hook
    {
        private Int32 msgID_CallWndProc;
        private Int32 msgID_CallWndProc_Params;
        private Int32 msgID_CallWndProc_HookReplaced;
        private IntPtr cacheHandle;
        private IntPtr cacheMessage;

        public event EventHandler<EventArgs> HookReplaced;
        public event EventHandler<WndProcEventArgs> CallWndProc;

        public CallWndProcHook(IntPtr handle) : base(handle)
        {
        }

        protected override void OnStart()
        {
            msgID_CallWndProc_HookReplaced = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_CALLWNDPROC_REPLACED");
            msgID_CallWndProc = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_CALLWNDPROC");
            msgID_CallWndProc_Params = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_CALLWNDPROC_PARAMS");

            if (Environment.OSVersion.Version.Major >= 6)
            {
                NativeMethods.ChangeWindowMessageFilter(msgID_CallWndProc_HookReplaced, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(msgID_CallWndProc, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(msgID_CallWndProc_Params, NativeConstants.MSGFLT_ADD);
            }
            NativeHookMethods.InitializeCallWndProcHook(0, handle);
        }

        protected override void OnStop()
        {
            NativeHookMethods.UninitializeCallWndProcHook();
        }

        public override void ProcessWindowMessage(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == msgID_CallWndProc)
            {
                cacheHandle = m.WParam;
                cacheMessage = m.LParam;
            }
            else if (m.Msg == msgID_CallWndProc_Params)
            {
                if (CallWndProc != null && cacheHandle != IntPtr.Zero && cacheMessage != IntPtr.Zero)
                {
                    RaiseEvent( CallWndProc, new WndProcEventArgs(cacheHandle, cacheMessage, m.WParam, m.LParam));
                }
                cacheHandle = IntPtr.Zero;
                cacheMessage = IntPtr.Zero;
            }
            else if (m.Msg == msgID_CallWndProc_HookReplaced)
            {
                   RaiseEvent(HookReplaced, EventArgs.Empty);
            }
        }
    }
}
