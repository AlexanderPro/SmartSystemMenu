using System;
using System.Windows.Forms;
using static SmartSystemMenu.Native.Constants;
using static SmartSystemMenu.Native.User32;

namespace SmartSystemMenu.Hooks
{
    class GetMsgHook : Hook
    {
        private IntPtr _cacheHandle;
        private IntPtr _cacheMessage;

        public event EventHandler<WndProcEventArgs> GetMsg;

        public GetMsgHook(IntPtr handle, int dragByMouseMenuItem) : base(handle, dragByMouseMenuItem)
        {
        }

        protected override void OnStart()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                ChangeWindowMessageFilter(WM_SSM_HOOK_GETMSG, MSGFLT_ADD);
                ChangeWindowMessageFilter(WM_SSM_HOOK_GETMSG_PARAMS, MSGFLT_ADD);
            }

            NativeHookMethods.InitializeGetMsgHook(0, _handle, _dragByMouseMenuItem);
        }

        protected override void OnStop()
        {
            NativeHookMethods.UninitializeGetMsgHook();
        }

        public override void ProcessWindowMessage(ref Message m)
        {
            if (m.Msg == WM_SSM_HOOK_GETMSG)
            {
                _cacheHandle = m.WParam;
                _cacheMessage = m.LParam;
            }
            else if (m.Msg == WM_SSM_HOOK_GETMSG_PARAMS)
            {
                if (GetMsg != null && _cacheHandle != IntPtr.Zero && _cacheMessage != IntPtr.Zero)
                {
                    RaiseEvent(GetMsg, new WndProcEventArgs(_cacheHandle, _cacheMessage, m.WParam, m.LParam));
                }
                _cacheHandle = IntPtr.Zero;
                _cacheMessage = IntPtr.Zero;
            }
        }
    }
}