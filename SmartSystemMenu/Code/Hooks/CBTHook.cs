using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartSystemMenu.Code.Common;

namespace SmartSystemMenu.Code.Hooks
{
    class CBTHook : Hook
    {
        private Int32 msgID_CBT_HookReplaced;
        private Int32 msgID_CBT_Activate;
        private Int32 msgID_CBT_CreateWnd;
        private Int32 msgID_CBT_DestroyWnd;
        private Int32 msgID_CBT_MinMax;
        private Int32 msgID_CBT_MoveSize;
        private Int32 msgID_CBT_SetFocus;
        private Int32 msgID_CBT_SysCommand;

        public event EventHandler<EventArgs> HookReplaced;
        public event EventHandler<WindowEventArgs> WindowActivated;
        public event EventHandler<WindowEventArgs> WindowCreated;
        public event EventHandler<WindowEventArgs> WindowDestroyed;
        public event EventHandler<SysCommandEventArgs> MinMax;
        public event EventHandler<WindowEventArgs> MoveSize;
        public event EventHandler<WindowEventArgs> SetFocus;
        public event EventHandler<SysCommandEventArgs> SysCommand;

        public CBTHook(IntPtr handle) : base(handle)
        {
        }

        protected override void OnStart()
        {
            msgID_CBT_HookReplaced = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_CBT_REPLACED");
            msgID_CBT_Activate = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HCBT_ACTIVATE");
            msgID_CBT_CreateWnd = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HCBT_CREATEWND");
            msgID_CBT_DestroyWnd = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HCBT_DESTROYWND");
            msgID_CBT_MinMax = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HCBT_MINMAX");
            msgID_CBT_MoveSize = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HCBT_MOVESIZE");
            msgID_CBT_SetFocus = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HCBT_SETFOCUS");
            msgID_CBT_SysCommand = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HCBT_SYSCOMMAND");

            if (Environment.OSVersion.Version.Major >= 6)
            {
                NativeMethods.ChangeWindowMessageFilter(msgID_CBT_HookReplaced, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(msgID_CBT_Activate, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(msgID_CBT_CreateWnd, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(msgID_CBT_DestroyWnd, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(msgID_CBT_MinMax, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(msgID_CBT_MoveSize, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(msgID_CBT_SetFocus, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(msgID_CBT_SysCommand, NativeConstants.MSGFLT_ADD);
            }
            NativeHookMethods.InitializeCbtHook(0, handle);
        }

        protected override void OnStop()
        {
            NativeHookMethods.UninitializeCbtHook();
        }

        public override void ProcessWindowMessage(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == msgID_CBT_HookReplaced)
            {
                RaiseEvent(HookReplaced, EventArgs.Empty);
            }
            else if (m.Msg == msgID_CBT_Activate)
            {
                RaiseEvent(WindowActivated, new WindowEventArgs(m.WParam));
            }
            else if (m.Msg == msgID_CBT_CreateWnd)
            {
                RaiseEvent(WindowCreated, new WindowEventArgs(m.WParam));
            }
            else if (m.Msg == msgID_CBT_DestroyWnd)
            {
                RaiseEvent(WindowDestroyed, new WindowEventArgs(m.WParam));
            }
            else if (m.Msg == msgID_CBT_MinMax)
            {
                RaiseEvent(MinMax, new SysCommandEventArgs(m.WParam, m.LParam));
            }
            else if (m.Msg == msgID_CBT_MoveSize)
            {
                RaiseEvent(MoveSize, new WindowEventArgs(m.WParam));
            }
            else if (m.Msg == msgID_CBT_SetFocus)
            {
                RaiseEvent(SetFocus, new WindowEventArgs(m.WParam));
            }
            else if (m.Msg == msgID_CBT_SysCommand)
            {
                RaiseEvent(SysCommand, new SysCommandEventArgs(m.WParam, m.LParam));
            }
        }
    }
}