using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using static SmartSystemMenu.Native.Constants;
using static SmartSystemMenu.Native.User32;

namespace SmartSystemMenu.Hooks
{
    class MouseLLHook : Hook
    {
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
            if (Environment.OSVersion.Version.Major >= 6)
            {
                ChangeWindowMessageFilter(WM_SSM_HOOK_MOUSELL, MSGFLT_ADD);
                ChangeWindowMessageFilter(WM_SSM_HOOK_MOUSELL_REPLACED, MSGFLT_ADD);
            }

            NativeHookMethods.InitializeMouseLLHook(0, _handle, _dragByMouseMenuItem);
        }

        protected override void OnStop()
        {
            NativeHookMethods.UninitializeMouseLLHook();
        }

        public override void ProcessWindowMessage(ref Message m)
        {
            if (m.Msg == WM_SSM_HOOK_MOUSELL)
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
            else if (m.Msg == WM_SSM_HOOK_MOUSELL_REPLACED)
            {
                RaiseEvent(HookReplaced, EventArgs.Empty);
            }
        }
    }
}