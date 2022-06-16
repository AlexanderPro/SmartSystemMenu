using System;
using SmartSystemMenu.Native;
using static SmartSystemMenu.Native.User32;

namespace SmartSystemMenu.Hooks
{
    class GetMsgHook : Hook
    {
        private int _msgIdGetMsg;
        private int _msgIdGetMsgParams;
        private IntPtr _cacheHandle;
        private IntPtr _cacheMessage;

        public event EventHandler<WndProcEventArgs> GetMsg;

        public GetMsgHook(IntPtr handle, int dragByMouseMenuItem) : base(handle, dragByMouseMenuItem)
        {
        }

        protected override void OnStart()
        {
            _msgIdGetMsg = RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_GETMSG");
            _msgIdGetMsgParams = RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_GETMSG_PARAMS");

            if (Environment.OSVersion.Version.Major >= 6)
            {
                ChangeWindowMessageFilter(_msgIdGetMsg, Constants.MSGFLT_ADD);
                ChangeWindowMessageFilter(_msgIdGetMsgParams, Constants.MSGFLT_ADD);
            }
            NativeHookMethods.InitializeGetMsgHook(0, _handle, _dragByMouseMenuItem);
        }

        protected override void OnStop()
        {
            NativeHookMethods.UninitializeGetMsgHook();
        }

        public override void ProcessWindowMessage(ref System.Windows.Forms.Message m)
        {
            //string dbgMessage = "";
            if (m.Msg == _msgIdGetMsg)
            {
                //if (m.LParam.ToInt64() == NativeConstants.WM_SYSCOMMAND)
                //{
                //    dbgMessage = string.Format("WM_SYSCOMMAND, GetMsg, Handle = {0}", m.WParam);
                //    System.Diagnostics.Trace.WriteLine(dbgMessage);
                //}
                _cacheHandle = m.WParam;
                _cacheMessage = m.LParam;
            }
            else if (m.Msg == _msgIdGetMsgParams)
            {
                if (GetMsg != null && _cacheHandle != IntPtr.Zero && _cacheMessage != IntPtr.Zero)
                {
                    //if (cacheMessage.ToInt64() == NativeConstants.WM_SYSCOMMAND)
                    //{
                    //    dbgMessage = string.Format("WM_SYSCOMMAND, GetMsgParams, Handle = {0}, WParam = {1}", cacheHandle, m.WParam);
                    //    System.Diagnostics.Trace.WriteLine(dbgMessage);
                    //}
                    RaiseEvent(GetMsg, new WndProcEventArgs(_cacheHandle, _cacheMessage, m.WParam, m.LParam));
                }
                _cacheHandle = IntPtr.Zero;
                _cacheMessage = IntPtr.Zero;
            }
        }
    }
}
