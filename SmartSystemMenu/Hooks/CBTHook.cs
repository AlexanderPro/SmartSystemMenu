using System;
using SmartSystemMenu.Native;

namespace SmartSystemMenu.Hooks
{
    class CBTHook : Hook
    {
        private int _msgIdCbtHookReplaced;
        private int _msgIdCbtActivate;
        private int _msgIdCbtCreateWnd;
        private int _msgIdCbtDestroyWnd;
        private int _msgIdCbtMinMax;
        private int _msgIdCbtMoveSize;
        private int _msgIdCbtSetFocus;
        private int _msgIdCbtSysCommand;

        public event EventHandler<EventArgs> HookReplaced;
        public event EventHandler<WindowEventArgs> WindowActivated;
        public event EventHandler<WindowEventArgs> WindowCreated;
        public event EventHandler<WindowEventArgs> WindowDestroyed;
        public event EventHandler<SysCommandEventArgs> MinMax;
        public event EventHandler<WindowEventArgs> MoveSize;
        public event EventHandler<WindowEventArgs> SetFocus;
        public event EventHandler<SysCommandEventArgs> SysCommand;

        public CBTHook(IntPtr handle, int dragByMouseMenuItem) : base(handle, dragByMouseMenuItem)
        {
        }

        protected override void OnStart()
        {
            _msgIdCbtHookReplaced = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_CBT_REPLACED");
            _msgIdCbtActivate = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HCBT_ACTIVATE");
            _msgIdCbtCreateWnd = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HCBT_CREATEWND");
            _msgIdCbtDestroyWnd = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HCBT_DESTROYWND");
            _msgIdCbtMinMax = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HCBT_MINMAX");
            _msgIdCbtMoveSize = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HCBT_MOVESIZE");
            _msgIdCbtSetFocus = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HCBT_SETFOCUS");
            _msgIdCbtSysCommand = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_HCBT_SYSCOMMAND");

            if (Environment.OSVersion.Version.Major >= 6)
            {
                NativeMethods.ChangeWindowMessageFilter(_msgIdCbtHookReplaced, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(_msgIdCbtActivate, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(_msgIdCbtCreateWnd, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(_msgIdCbtDestroyWnd, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(_msgIdCbtMinMax, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(_msgIdCbtMoveSize, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(_msgIdCbtSetFocus, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(_msgIdCbtSysCommand, NativeConstants.MSGFLT_ADD);
            }
            NativeHookMethods.InitializeCbtHook(0, _handle, _dragByMouseMenuItem);
        }

        protected override void OnStop()
        {
            NativeHookMethods.UninitializeCbtHook();
        }

        public override void ProcessWindowMessage(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == _msgIdCbtHookReplaced)
            {
                RaiseEvent(HookReplaced, EventArgs.Empty);
            }
            else if (m.Msg == _msgIdCbtActivate)
            {
                RaiseEvent(WindowActivated, new WindowEventArgs(m.WParam));
            }
            else if (m.Msg == _msgIdCbtCreateWnd)
            {
                RaiseEvent(WindowCreated, new WindowEventArgs(m.WParam));
            }
            else if (m.Msg == _msgIdCbtDestroyWnd)
            {
                RaiseEvent(WindowDestroyed, new WindowEventArgs(m.WParam));
            }
            else if (m.Msg == _msgIdCbtMinMax)
            {
                RaiseEvent(MinMax, new SysCommandEventArgs(m.WParam, m.LParam));
            }
            else if (m.Msg == _msgIdCbtMoveSize)
            {
                RaiseEvent(MoveSize, new WindowEventArgs(m.WParam));
            }
            else if (m.Msg == _msgIdCbtSetFocus)
            {
                RaiseEvent(SetFocus, new WindowEventArgs(m.WParam));
            }
            else if (m.Msg == _msgIdCbtSysCommand)
            {
                RaiseEvent(SysCommand, new SysCommandEventArgs(m.WParam, m.LParam));
            }
        }
    }
}