using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
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

        public static string ExtractTextFromConsoleWindow(int processId)
        {
            try
            {
                NativeMethods.FreeConsole();
                var result = NativeMethods.AttachConsole(processId);
                if (!result)
                {
                    var error = Marshal.GetLastWin32Error();
                    throw new Win32Exception(error);
                }
                var handle = NativeMethods.GetStdHandle(NativeConstants.STD_OUTPUT_HANDLE);
                if (handle == IntPtr.Zero)
                {
                    var error = Marshal.GetLastWin32Error();
                    throw new Win32Exception(error);
                }
                ConsoleScreenBufferInfo binfo;
                result = NativeMethods.GetConsoleScreenBufferInfo(handle, out binfo);
                if (!result)
                {
                    var error = Marshal.GetLastWin32Error();
                    throw new Win32Exception(error);
                }

                var buffer = new char[binfo.srWindow.Right];
                var textBuilder = new StringBuilder();
                for (var i = 0; i < binfo.dwSize.Y; i++)
                {
                    uint numberOfCharsRead;
                    if (NativeMethods.ReadConsoleOutputCharacter(handle, buffer, (uint)buffer.Length, new Coord(0, (short)i), out numberOfCharsRead))
                    {
                        textBuilder.AppendLine(new string(buffer));
                    }
                }

                var text = textBuilder.ToString().TrimEnd();
                return text;
            }
            catch
            {
                NativeMethods.FreeConsole();
                return null;
            }
        }

        public static IntPtr FindWindowByTitle(string title, int? processId)
        {
            var handle = IntPtr.Zero;
            NativeMethods.EnumWindows((IntPtr hWnd, int lParam) => {
                if (processId.HasValue)
                {
                    NativeMethods.GetWindowThreadProcessId(hWnd, out var pid);
                    if (processId.Value == pid)
                    {
                        var builder = new StringBuilder(1024);
                        NativeMethods.GetWindowText(hWnd, builder, builder.Capacity);
                        if (string.Compare(title, builder.ToString()) == 0)
                        {
                            handle = hWnd;
                            return false;
                        }
                    }
                }
                else
                {
                    var builder = new StringBuilder(1024);
                    NativeMethods.GetWindowText(hWnd, builder, builder.Capacity);
                    if (string.Compare(title, builder.ToString()) == 0)
                    {
                        handle = hWnd;
                        return false;
                    }
                }
                return true;
            }, 0);
            return handle;
        }
    }
}
