using System;
using System.Windows.Forms;
using static SmartSystemMenu.Native.Constants;
using static SmartSystemMenu.Native.User32;
using static SmartSystemMenu.Native.Hooks;

namespace SmartSystemMenu.Hooks
{
    class GetMsgHook : Hook
    {
        private IntPtr _cacheHandle;
        private IntPtr _cacheMessage;
        private int _dragByMouseMenuItem;

        public event EventHandler<SysCommandEventArgs> SysCommand;
        public event EventHandler<SysCommandEventArgs> InitMenu;

        public GetMsgHook(IntPtr handle, int dragByMouseMenuItem) : base(handle)
        {
            _dragByMouseMenuItem = dragByMouseMenuItem;
        }

        protected override void OnStart()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                ChangeWindowMessageFilter(WM_SSM_HOOK_GETMSG_SYSCOMMAND, MSGFLT_ADD);
                ChangeWindowMessageFilter(WM_SSM_HOOK_GETMSG_SYSCOMMAND_PARAMS, MSGFLT_ADD);
                ChangeWindowMessageFilter(WM_SSM_HOOK_GETMSG_INITMENU, MSGFLT_ADD);
            }

            InitializeGetMsgHook(0, _handle, _dragByMouseMenuItem);
        }

        protected override void OnStop()
        {
            UninitializeGetMsgHook();
        }

        public override void ProcessWindowMessage(ref Message m)
        {
            if (m.Msg == WM_SSM_HOOK_GETMSG_SYSCOMMAND)
            {
                _cacheHandle = m.WParam;
                _cacheMessage = m.LParam;
            }
            else if (m.Msg == WM_SSM_HOOK_GETMSG_SYSCOMMAND_PARAMS)
            {
                if (SysCommand != null && _cacheHandle != IntPtr.Zero && _cacheMessage != IntPtr.Zero)
                {
                    RaiseEvent(SysCommand, new SysCommandEventArgs(_cacheHandle, _cacheMessage, m.WParam, m.LParam));
                }
                _cacheHandle = IntPtr.Zero;
                _cacheMessage = IntPtr.Zero;
            }
            else if (m.Msg == WM_SSM_HOOK_GETMSG_INITMENU)
            {
                RaiseEvent(InitMenu, new SysCommandEventArgs(IntPtr.Zero, IntPtr.Zero, m.WParam, m.LParam));
            }
        }
    }
}