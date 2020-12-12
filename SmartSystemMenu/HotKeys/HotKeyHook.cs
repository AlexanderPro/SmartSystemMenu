using System;
using System.Collections.Generic;
using System.Linq;
using SmartSystemMenu.Native;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.HotKeys
{
    class HotKeyHook : IDisposable
    {
        private IntPtr _hookHandle;
        private KeyboardHookProc _hookProc;
        private IList<MenuItem> _menuItems;

        public event EventHandler<HotKeyHookEventArgs> Hooked;

        public bool Start(string moduleName, IList<MenuItem> menuItems)
        {
            _menuItems = menuItems;
            _hookProc = HookProc;
            var moduleHandle = NativeMethods.GetModuleHandle(moduleName);
            _hookHandle = NativeMethods.SetWindowsHookEx(NativeConstants.WH_KEYBOARD_LL, _hookProc, moduleHandle, 0);
            var hookStarted = _hookHandle != IntPtr.Zero;
            return hookStarted;
        }

        public bool Stop()
        {
            if (_hookHandle == IntPtr.Zero)
            {
                return true;
            }
            var hookStoped = NativeMethods.UnhookWindowsHookEx(_hookHandle);
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

        ~HotKeyHook()
        {
            Dispose(false);
        }

        private int HookProc(int code, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam)
        {
            if (code == NativeConstants.HC_ACTION)
            {
                if (wParam.ToInt32() == NativeConstants.WM_KEYDOWN || wParam.ToInt32() == NativeConstants.WM_SYSKEYDOWN)
                {
                    foreach (var item in _menuItems)
                    {
                        var key1 = true;
                        var key2 = true;

                        if (item.Key1 != VirtualKeyModifier.None)
                        {
                            var key1State = NativeMethods.GetAsyncKeyState((int)item.Key1) & 0x8000;
                            key1 = Convert.ToBoolean(key1State);
                        }

                        if (item.Key2 != VirtualKeyModifier.None)
                        {
                            var key2State = NativeMethods.GetAsyncKeyState((int)item.Key2) & 0x8000;
                            key2 = Convert.ToBoolean(key2State);
                        }

                        if (key1 && key2 && lParam.vkCode == (int)item.Key3)
                        {
                            var handler = Hooked;
                            if (handler != null)
                            {
                                var menuItemId = MenuItemId.GetMenuItemId(item.Name);
                                var eventArgs = new HotKeyHookEventArgs(menuItemId);
                                handler.BeginInvoke(this, eventArgs, null, null);
                                break;
                            }
                        }
                    }
                }
            }

            return NativeMethods.CallNextHookEx(_hookHandle, code, wParam, ref lParam);
        }
    }
}
