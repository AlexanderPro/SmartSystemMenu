using System;
using SmartSystemMenu.Native;
using static SmartSystemMenu.Native.User32;

namespace SmartSystemMenu.Hooks
{
    class CallWndProcHook : Hook
    {
        private int _msgIdCallWndProc;
        private int _msgIdCallWndProcParams;
        private IntPtr _cacheHandle;
        private IntPtr _cacheMessage;

        public event EventHandler<WndProcEventArgs> CallWndProc;

        public CallWndProcHook(IntPtr handle, int dragByMouseMenuItem) : base(handle, dragByMouseMenuItem)
        {
        }

        protected override void OnStart()
        {
            _msgIdCallWndProc = RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_CALLWNDPROC");
            _msgIdCallWndProcParams = RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_CALLWNDPROC_PARAMS");

            if (Environment.OSVersion.Version.Major >= 6)
            {
                ChangeWindowMessageFilter(_msgIdCallWndProc, Constants.MSGFLT_ADD);
                ChangeWindowMessageFilter(_msgIdCallWndProcParams, Constants.MSGFLT_ADD);
            }
            NativeHookMethods.InitializeCallWndProcHook(0, _handle, _dragByMouseMenuItem);
        }

        protected override void OnStop()
        {
            NativeHookMethods.UninitializeCallWndProcHook();
        }

        public override void ProcessWindowMessage(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == _msgIdCallWndProc)
            {
                _cacheHandle = m.WParam;
                _cacheMessage = m.LParam;
            }
            else if (m.Msg == _msgIdCallWndProcParams)
            {
                if (CallWndProc != null && _cacheHandle != IntPtr.Zero && _cacheMessage != IntPtr.Zero)
                {
                    RaiseEvent( CallWndProc, new WndProcEventArgs(_cacheHandle, _cacheMessage, m.WParam, m.LParam));
                }
                _cacheHandle = IntPtr.Zero;
                _cacheMessage = IntPtr.Zero;
            }
        }
    }
}