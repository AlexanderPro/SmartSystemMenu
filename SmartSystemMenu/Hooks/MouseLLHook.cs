using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using SmartSystemMenu.Native;

namespace SmartSystemMenu.Hooks
{
    class MouseLLHook : Hook
    {
        private int _msgIdMouseLL;
        private int _msgIdMouseLLHookReplaced;

        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_LBUTTONUP = 0x0202;
        private const int WM_LBUTTONDBLCLK = 0x0203;
        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WM_RBUTTONUP = 0x0205;
        private const int WM_RBUTTONDBLCLK = 0x0206;
        private const int WM_MBUTTONDOWN = 0x0207;
        private const int WM_MBUTTONUP = 0x0208;
        private const int WM_MBUTTONDBLCLK = 0x0209;
        private const int WM_MOUSEWHEEL = 0x020A;

        public event EventHandler<EventArgs> HookReplaced;
        public event EventHandler<BasicHookEventArgs> MouseLLEvent;
        public event EventHandler<MouseEventArgs> MouseDown;
        public event EventHandler<MouseEventArgs> MouseMove;
        public event EventHandler<MouseEventArgs> MouseUp;

        struct MSLLHOOKSTRUCT
        {
            #pragma warning disable 0649

            public System.Drawing.Point pt;
            public int mouseData;
            public int flags;
            public int time;
            public IntPtr dwExtraInfo;

            #pragma warning restore 0649
        };

        public MouseLLHook(IntPtr handle, int dragByMouseMenuItem) : base(handle, dragByMouseMenuItem)
        {
        }

        protected override void OnStart()
        {
            _msgIdMouseLL = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_MOUSELL");
            _msgIdMouseLLHookReplaced = NativeMethods.RegisterWindowMessage("SMART_SYSTEM_MENU_HOOK_MOUSELL_REPLACED");

            if (Environment.OSVersion.Version.Major >= 6)
            {
                NativeMethods.ChangeWindowMessageFilter(_msgIdMouseLL, NativeConstants.MSGFLT_ADD);
                NativeMethods.ChangeWindowMessageFilter(_msgIdMouseLLHookReplaced, NativeConstants.MSGFLT_ADD);
            }
            NativeHookMethods.InitializeMouseLLHook(0, _handle, _dragByMouseMenuItem);
        }

        protected override void OnStop()
        {
            NativeHookMethods.UninitializeMouseLLHook();
        }

        public override void ProcessWindowMessage(ref Message m)
        {
            if (m.Msg == _msgIdMouseLL)
            {
                RaiseEvent(MouseLLEvent, new BasicHookEventArgs(m.WParam, m.LParam));

                var msl = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(m.LParam, typeof(MSLLHOOKSTRUCT));
                if (m.WParam.ToInt64() == WM_MOUSEMOVE)
                {
                    RaiseEvent(MouseMove, new MouseEventArgs(MouseButtons.None, 0, msl.pt.X, msl.pt.Y, 0));
                }
                else if (m.WParam.ToInt64() == WM_LBUTTONDOWN)
                {
                    RaiseEvent(MouseDown, new MouseEventArgs(MouseButtons.Left, 0, msl.pt.X, msl.pt.Y, 0));
                }
                else if (m.WParam.ToInt64() == WM_RBUTTONDOWN)
                {
                    RaiseEvent(MouseDown, new MouseEventArgs(MouseButtons.Right, 0, msl.pt.X, msl.pt.Y, 0));
                }
                else if (m.WParam.ToInt64() == WM_LBUTTONUP)
                {
                    RaiseEvent(MouseUp, new MouseEventArgs(MouseButtons.Left, 0, msl.pt.X, msl.pt.Y, 0));
                }
                else if (m.WParam.ToInt64() == WM_RBUTTONUP)
                {
                    RaiseEvent(MouseUp, new MouseEventArgs(MouseButtons.Right, 0, msl.pt.X, msl.pt.Y, 0));
                }
            }
            else if (m.Msg == _msgIdMouseLLHookReplaced)
            {
                RaiseEvent(HookReplaced, EventArgs.Empty);
            }
        }
    }
}