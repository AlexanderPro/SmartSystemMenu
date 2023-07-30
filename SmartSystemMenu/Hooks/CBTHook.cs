using System;
using System.Windows.Forms;
using static SmartSystemMenu.Native.Constants;
using static SmartSystemMenu.Native.User32;


namespace SmartSystemMenu.Hooks
{
    class CBTHook : Hook
    {
        public event EventHandler<WindowEventArgs> WindowCreated;
        public event EventHandler<WindowEventArgs> WindowDestroyed;
        public event EventHandler<SysCommandEventArgs> MinMax;
        public event EventHandler<WindowEventArgs> MoveSize;
        public event EventHandler<WindowEventArgs> Activate;

        public CBTHook(IntPtr handle, int dragByMouseMenuItem) : base(handle, dragByMouseMenuItem)
        {
        }

        protected override void OnStart()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                ChangeWindowMessageFilter(WM_SSM_HOOK_HCBT_CREATEWND, MSGFLT_ADD);
                ChangeWindowMessageFilter(WM_SSM_HOOK_HCBT_DESTROYWND, MSGFLT_ADD);
                ChangeWindowMessageFilter(WM_SSM_HOOK_HCBT_MINMAX, MSGFLT_ADD);
                ChangeWindowMessageFilter(WM_SSM_HOOK_HCBT_MOVESIZE, MSGFLT_ADD);
                ChangeWindowMessageFilter(WM_SSM_HOOK_HCBT_ACTIVATE, MSGFLT_ADD);
            }

            NativeHookMethods.InitializeCbtHook(0, _handle, _dragByMouseMenuItem);
        }

        protected override void OnStop()
        {
            NativeHookMethods.UninitializeCbtHook();
        }

        public override void ProcessWindowMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_SSM_HOOK_HCBT_CREATEWND:
                    {
                        RaiseEvent(WindowCreated, new WindowEventArgs(m.WParam));
                    }
                    break;

                case WM_SSM_HOOK_HCBT_DESTROYWND:
                    {
                        RaiseEvent(WindowDestroyed, new WindowEventArgs(m.WParam));
                    }
                    break;

                case WM_SSM_HOOK_HCBT_MINMAX:
                    {
                        RaiseEvent(MinMax, new SysCommandEventArgs(m.WParam, m.LParam));
                    }
                    break;

                case WM_SSM_HOOK_HCBT_MOVESIZE:
                    {
                        RaiseEvent(MoveSize, new WindowEventArgs(m.WParam));
                    }
                    break;

                case WM_SSM_HOOK_HCBT_ACTIVATE:
                    {
                        RaiseEvent(Activate, new WindowEventArgs(m.WParam));
                    }
                    break;
            };
        }
    }
}