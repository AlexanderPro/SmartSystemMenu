using System;
using System.Runtime.InteropServices;

namespace SmartSystemMenu.Native
{
    static class Hooks
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
