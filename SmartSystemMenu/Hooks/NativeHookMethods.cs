using System;
using System.Runtime.InteropServices;

namespace SmartSystemMenu.Hooks
{
    static class NativeHookMethods
    {
#if WIN32

        [DllImport("SmartSystemMenuHook.dll")]
        public static extern bool InitializeCbtHook(int threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern void UninitializeCbtHook();
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern bool InitializeShellHook(int threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern void UninitializeShellHook();
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern bool InitializeKeyboardHook(int threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern void UninitializeKeyboardHook();
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern bool InitializeMouseHook(int threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern void UninitializeMouseHook();
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern bool InitializeKeyboardLLHook(int threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern void UninitializeKeyboardLLHook();
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern bool InitializeMouseLLHook(int threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern void UninitializeMouseLLHook();
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern bool InitializeCallWndProcHook(int threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern void UninitializeCallWndProcHook();
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern bool InitializeGetMsgHook(int threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern void UninitializeGetMsgHook();

#else

        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern bool InitializeCbtHook(int threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern void UninitializeCbtHook();
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern bool InitializeShellHook(int threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern void UninitializeShellHook();
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern bool InitializeKeyboardHook(int threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern void UninitializeKeyboardHook();
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern bool InitializeMouseHook(int threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern void UninitializeMouseHook();
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern bool InitializeKeyboardLLHook(int threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern void UninitializeKeyboardLLHook();
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern bool InitializeMouseLLHook(int threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern void UninitializeMouseLLHook();
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern bool InitializeCallWndProcHook(int threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern void UninitializeCallWndProcHook();
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern bool InitializeGetMsgHook(int threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern void UninitializeGetMsgHook();
#endif
    }
}
