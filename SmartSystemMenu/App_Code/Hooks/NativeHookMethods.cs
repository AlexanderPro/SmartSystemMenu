using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using SmartSystemMenu.App_Code.Common;

namespace SmartSystemMenu.App_Code.Hooks
{
    static class NativeHookMethods
    {
#if WIN32

        [DllImport("SmartSystemMenuHook.dll")]
        public static extern Boolean InitializeCbtHook(Int32 threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern void UninitializeCbtHook();
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern Boolean InitializeShellHook(Int32 threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern void UninitializeShellHook();
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern Boolean InitializeKeyboardHook(Int32 threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern void UninitializeKeyboardHook();
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern Boolean InitializeMouseHook(Int32 threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern void UninitializeMouseHook();
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern Boolean InitializeKeyboardLLHook(Int32 threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern void UninitializeKeyboardLLHook();
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern Boolean InitializeMouseLLHook(Int32 threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern void UninitializeMouseLLHook();
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern Boolean InitializeCallWndProcHook(Int32 threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern void UninitializeCallWndProcHook();
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern Boolean InitializeGetMsgHook(Int32 threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook.dll")]
        public static extern void UninitializeGetMsgHook();

#else

        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern Boolean InitializeCbtHook(Int32 threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern void UninitializeCbtHook();
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern Boolean InitializeShellHook(Int32 threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern void UninitializeShellHook();
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern Boolean InitializeKeyboardHook(Int32 threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern void UninitializeKeyboardHook();
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern Boolean InitializeMouseHook(Int32 threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern void UninitializeMouseHook();
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern Boolean InitializeKeyboardLLHook(Int32 threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern void UninitializeKeyboardLLHook();
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern Boolean InitializeMouseLLHook(Int32 threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern void UninitializeMouseLLHook();
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern Boolean InitializeCallWndProcHook(Int32 threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern void UninitializeCallWndProcHook();
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern Boolean InitializeGetMsgHook(Int32 threadID, IntPtr destWindow);
        [DllImport("SmartSystemMenuHook64.dll")]
        public static extern void UninitializeGetMsgHook();
#endif
    }
}
