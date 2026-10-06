using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using SmartSystemMenu.Settings;
using SmartSystemMenu.Native.Structs;
using static SmartSystemMenu.Native.Kernel32;
using static SmartSystemMenu.Native.User32;
using static SmartSystemMenu.Native.Constants;

namespace SmartSystemMenu.HotKeys
{
    class MouseHook : IDisposable
    {
        private readonly string _moduleName;
        private static MouseHookProc _hookProc;
        private IntPtr _moduleHandle;
        private IntPtr _hookHandle;

        public event EventHandler<EventArgs<Point>> CloserHooked;

        public ApplicationSettings Settings { get; set; }

        public MouseHook(ApplicationSettings settings, string moduleName)
        {
            Settings = settings;
            _moduleName = moduleName;
            _hookProc = HookProc;
        }

        public bool Start()
        {
            _moduleHandle = GetModuleHandle(_moduleName);
            InitializeHook();
            return _hookHandle != IntPtr.Zero;
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

        private int HookProc(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var stopWatch = Stopwatch.StartNew();

                if (Settings.Closer.MouseButton != MouseButton.None && 
                    (wParam == WM_LBUTTONDOWN || wParam == WM_RBUTTONDOWN || wParam == WM_MBUTTONDOWN || wParam == WM_LBUTTONUP || wParam == WM_RBUTTONUP || wParam == WM_MBUTTONUP))
                {
                    var key1 = true;
                    var key2 = true;

                    if (Settings.Closer.Key1 != VirtualKeyModifier.None)
                    {
                        var key1State = GetAsyncKeyState((int)Settings.Closer.Key1) & 0x8000;
                        key1 = Convert.ToBoolean(key1State);
                    }

                    if (Settings.Closer.Key2 != VirtualKeyModifier.None)
                    {
                        var key2State = GetAsyncKeyState((int)Settings.Closer.Key2) & 0x8000;
                        key2 = Convert.ToBoolean(key2State);
                    }

                    if (key1 && key2 && ((Settings.Closer.MouseButton == MouseButton.Left && wParam == WM_LBUTTONDOWN) || (Settings.Closer.MouseButton == MouseButton.Right && wParam == WM_RBUTTONDOWN) || (Settings.Closer.MouseButton == MouseButton.Middle && wParam == WM_MBUTTONDOWN)))
                    {
                        var handler = CloserHooked;
                        if (handler != null)
                        {
                            var mouseHookStruct = (MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseLLHookStruct));
                            var eventArgs = new EventArgs<Point>(mouseHookStruct.pt);
                            handler.BeginInvoke(this, eventArgs, null, null);
                            return 1;
                        }
                    }

                    if (key1 && key2 && ((Settings.Closer.MouseButton == MouseButton.Left && wParam == WM_LBUTTONUP) || (Settings.Closer.MouseButton == MouseButton.Right && wParam == WM_RBUTTONUP) || (Settings.Closer.MouseButton == MouseButton.Middle && wParam == WM_MBUTTONUP)))
                    {
                        return 1;
                    }
                }

                stopWatch.Stop();
                if (stopWatch.ElapsedMilliseconds > Settings.LowLevelHooksTimeout)
                {
                    InitializeHook();
                }
            }

            return CallNextHookEx(_hookHandle, nCode, wParam, lParam);
        }

        private void InitializeHook()
        {
            if (_hookHandle != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookHandle);
                _hookHandle = IntPtr.Zero;
            }
            _hookHandle = SetWindowsHookEx(WH_MOUSE_LL, _hookProc, _moduleHandle, 0);
        }
    }
}
