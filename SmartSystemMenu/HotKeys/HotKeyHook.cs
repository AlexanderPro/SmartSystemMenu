using System;
using System.Linq;
using SmartSystemMenu.Native.Structs;
using SmartSystemMenu.Settings;
using SmartSystemMenu.Extensions;
using static SmartSystemMenu.Native.User32;
using static SmartSystemMenu.Native.Kernel32;
using static SmartSystemMenu.Native.Constants;


namespace SmartSystemMenu.HotKeys
{
    class HotKeyHook : IDisposable
    {
        private IntPtr _hookHandle;
        private KeyboardHookProc _hookProc;
        private MenuItems _menuItems;

        public event EventHandler<HotKeyEventArgs> Hooked;

        public bool Start(string moduleName, MenuItems menuItems)
        {
            _menuItems = menuItems;
            _hookProc = HookProc;
            var moduleHandle = GetModuleHandle(moduleName);
            _hookHandle = SetWindowsHookEx(WH_KEYBOARD_LL, _hookProc, moduleHandle, 0);
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

        ~HotKeyHook()
        {
            Dispose(false);
        }

        private int HookProc(int code, IntPtr wParam, ref KeyboardLLHookStruct lParam)
        {
            if (code == HC_ACTION)
            {
                if (wParam.ToInt32() == WM_KEYDOWN || wParam.ToInt32() == WM_SYSKEYDOWN)
                {
                    foreach (var item in _menuItems.Items.Flatten(x => x.Items).Where(x => x.Type == MenuItemType.Item))
                    {
                        if (item.Key3 == VirtualKey.None || lParam.vkCode != (int)item.Key3)
                        {
                            continue;
                        }

                        var key1 = true;
                        var key2 = true;

                        if (item.Key1 != VirtualKeyModifier.None)
                        {
                            var key1State = GetAsyncKeyState((int)item.Key1) & 0x8000;
                            key1 = Convert.ToBoolean(key1State);
                        }

                        if (item.Key2 != VirtualKeyModifier.None)
                        {
                            var key2State = GetAsyncKeyState((int)item.Key2) & 0x8000;
                            key2 = Convert.ToBoolean(key2State);
                        }

                        if (key1 && key2 && lParam.vkCode == (int)item.Key3)
                        {
                            var handler = Hooked;
                            if (handler != null)
                            {
                                var menuItemId = MenuItemId.GetId(item.Name);
                                var eventArgs = new HotKeyEventArgs(menuItemId);
                                handler.Invoke(this, eventArgs);
                                if (eventArgs.Succeeded)
                                {
                                    return 1;
                                }
                            }
                        }
                    }

                    foreach (var item in _menuItems.WindowSizeItems)
                    {
                        if (item.Key3 == VirtualKey.None || lParam.vkCode != (int)item.Key3)
                        {
                            continue;
                        }

                        var key1 = true;
                        var key2 = true;

                        if (item.Key1 != VirtualKeyModifier.None)
                        {
                            var key1State = GetAsyncKeyState((int)item.Key1) & 0x8000;
                            key1 = Convert.ToBoolean(key1State);
                        }

                        if (item.Key2 != VirtualKeyModifier.None)
                        {
                            var key2State = GetAsyncKeyState((int)item.Key2) & 0x8000;
                            key2 = Convert.ToBoolean(key2State);
                        }

                        if (key1 && key2 && lParam.vkCode == (int)item.Key3)
                        {
                            var handler = Hooked;
                            if (handler != null)
                            {
                                var eventArgs = new HotKeyEventArgs(item.Id);
                                handler.Invoke(this, eventArgs);
                                if (eventArgs.Succeeded)
                                {
                                    return 1;
                                }
                            }
                        }
                    }
                }
            }

            return CallNextHookEx(_hookHandle, code, wParam, ref lParam);
        }
    }
}
