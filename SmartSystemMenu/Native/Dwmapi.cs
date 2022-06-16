using System;
using System.Runtime.InteropServices;
using SmartSystemMenu.Native.Structs;

namespace SmartSystemMenu.Native
{
    static class Dwmapi
    {
        [DllImport("dwmapi.dll")]
        public static extern void DwmEnableBlurBehindWindow(IntPtr hwnd, ref DWM_BLURBEHIND blurBehind);

        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out Rect pvAttribute, int cbAttribute);
    }
}
