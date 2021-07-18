using System;
using System.Drawing;
using System.Drawing.Imaging;
using SmartSystemMenu.Native;

namespace SmartSystemMenu.Utils
{
    static class WindowUtils
    {
        public static Bitmap PrintWindow(IntPtr hWnd)
        {
            Rect rect;
            NativeMethods.GetWindowRect(hWnd, out rect);
            var bitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                var hdc = graphics.GetHdc();
                NativeMethods.PrintWindow(hWnd, hdc, 0);
                graphics.ReleaseHdc(hdc);
            }
            return bitmap;
        }

        public static IntPtr GetParentWindow(IntPtr hWnd)
        {
            var resultHwnd = hWnd;
            var parentHwnd = IntPtr.Zero;
            while ((parentHwnd = NativeMethods.GetParent(resultHwnd)) != IntPtr.Zero)
            {
                resultHwnd = parentHwnd;
            }
            return resultHwnd;
        }

        public static uint GetThreadId(IntPtr hWnd)
        {
            uint threadId = NativeMethods.GetWindowThreadProcessId(hWnd, out var processId);
            return threadId;
        }

        public static int GetProcessId(IntPtr hWnd)
        {
            NativeMethods.GetWindowThreadProcessId(hWnd, out var processId);
            return processId;
        }
    }
}
