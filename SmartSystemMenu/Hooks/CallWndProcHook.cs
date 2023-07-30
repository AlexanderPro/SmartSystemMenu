using System;
using System.Windows.Forms;
using static SmartSystemMenu.Native.Constants;
using static SmartSystemMenu.Native.User32;

namespace SmartSystemMenu.Hooks
{
    class CallWndProcHook : Hook
    {
        private IntPtr _cacheHandle;
        private IntPtr _cacheMessage;

        public event EventHandler<WndProcEventArgs> CallWndProc;

        public CallWndProcHook(IntPtr handle, int dragByMouseMenuItem) : base(handle, dragByMouseMenuItem)
        {
        }

        protected override void OnStart()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                ChangeWindowMessageFilter(WM_SSM_HOOK_CALLWNDPROC, MSGFLT_ADD);
                ChangeWindowMessageFilter(WM_SSM_HOOK_CALLWNDPROC_PARAMS, MSGFLT_ADD);
            }

            NativeHookMethods.InitializeCallWndProcHook(0, _handle, _dragByMouseMenuItem);
        }

        protected override void OnStop()
        {
            NativeHookMethods.UninitializeCallWndProcHook();
        }

        public override void ProcessWindowMessage(ref Message m)
        {
            if (m.Msg == WM_SSM_HOOK_CALLWNDPROC)
            {
                _cacheHandle = m.WParam;
                _cacheMessage = m.LParam;
            }
            else if (m.Msg == WM_SSM_HOOK_CALLWNDPROC_PARAMS)
            {
                if (CallWndProc != null && _cacheHandle != IntPtr.Zero && _cacheMessage != IntPtr.Zero)
                {
                    RaiseEvent(CallWndProc, new WndProcEventArgs(_cacheHandle, _cacheMessage, m.WParam, m.LParam));
                }
                _cacheHandle = IntPtr.Zero;
                _cacheMessage = IntPtr.Zero;
            }
        }
    }
}