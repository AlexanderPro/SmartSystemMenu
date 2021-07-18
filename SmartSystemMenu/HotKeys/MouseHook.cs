using System;
using System.Runtime.InteropServices;
using SmartSystemMenu.Native;
using static SmartSystemMenu.Native.NativeMethods;
using static SmartSystemMenu.Native.NativeConstants;

namespace SmartSystemMenu.HotKeys
{
    class MouseHook : IDisposable
    {
        private IntPtr _hookHandle;
        private MouseHookProc _hookProc;
        private VirtualKeyModifier _key1;
        private VirtualKeyModifier _key2;
        private MouseButton _mouseButton;

        public event EventHandler<MouseEventArgs> Hooked;

        public bool Start(string moduleName, VirtualKeyModifier key1, VirtualKeyModifier key2, MouseButton mouseButton)
        {
            _key1 = key1;
            _key2 = key2;
            _mouseButton = mouseButton;
            _hookProc = HookProc;
            var moduleHandle = GetModuleHandle(moduleName);
            _hookHandle = SetWindowsHookEx(WH_MOUSE_LL, _hookProc, moduleHandle, 0);
            var hookStarted = _hookHandle != IntPtr.Zero;
            return hookStarted;
        }

        public bool Stop()
        {
            if (_hookHandle == IntPtr.Zero)
            {
                return true;
            }
            var hookStoped = UnhookWindowsHookEx(_hookHandle);
            return hookStoped;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // get rid of managed resources
            }

            Stop();
        }

        ~MouseHook()
        {
            Dispose(false);
        }

        private int HookProc(int code, int wParam, IntPtr lParam)
        {
            if (code == HC_ACTION)
            {
                if (_mouseButton != MouseButton.None && 
                    (wParam == WM_LBUTTONDOWN || wParam == WM_RBUTTONDOWN || wParam == WM_MBUTTONDOWN))
                {
                    var key1 = true;
                    var key2 = true;

                    if (_key1 != VirtualKeyModifier.None)
                    {
                        var key1State = GetAsyncKeyState((int)_key1) & 0x8000;
                        key1 = Convert.ToBoolean(key1State);
                    }

                    if (_key2 != VirtualKeyModifier.None)
                    {
                        var key2State = GetAsyncKeyState((int)_key2) & 0x8000;
                        key2 = Convert.ToBoolean(key2State);
                    }

                    if (key1 && key2 && ((_mouseButton == MouseButton.Left && wParam == WM_LBUTTONDOWN) || (_mouseButton == MouseButton.Right && wParam == WM_RBUTTONDOWN) || (_mouseButton == MouseButton.Middle && wParam == WM_MBUTTONDOWN)))
                    {
                        var handler = Hooked;
                        if (handler != null)
                        {
                            var mouseHookStruct = (MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseLLHookStruct));
                            var eventArgs = new MouseEventArgs(mouseHookStruct.pt);
                            handler.BeginInvoke(this, eventArgs, null, null);
                        }
                    }
                }
            }

            return CallNextHookEx(_hookHandle, code, wParam, lParam);
        }
    }
}
