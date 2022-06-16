using System;
using System.Runtime.InteropServices;

namespace SmartSystemMenu.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    struct KeyboardLLHookStruct
    {
        public int vkCode;
        public int scanCode;
        public int flags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    delegate int KeyboardHookProc(int code, IntPtr wParam, ref KeyboardLLHookStruct lParam);
}
