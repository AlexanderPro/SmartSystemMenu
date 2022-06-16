using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using SmartSystemMenu.Native;
using SmartSystemMenu.Native.Enums;
using SmartSystemMenu.Native.Structs;
using static SmartSystemMenu.Native.User32;
using static SmartSystemMenu.Native.Kernel32;
using static SmartSystemMenu.Native.Constants;

namespace SmartSystemMenu.Utils
{
    static class WindowUtils
    {
        public static Bitmap PrintWindow(IntPtr hWnd)
        {
            Rect rect;
            GetWindowRect(hWnd, out rect);
            var bitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                var hdc = graphics.GetHdc();
                User32.PrintWindow(hWnd, hdc, 0);
                graphics.ReleaseHdc(hdc);
            }
            return bitmap;
        }

        public static IntPtr GetParentWindow(IntPtr hWnd)
        {
            var resultHwnd = hWnd;
            var parentHwnd = IntPtr.Zero;
            while ((parentHwnd = GetParent(resultHwnd)) != IntPtr.Zero)
            {
                resultHwnd = parentHwnd;
            }
            return resultHwnd;
        }

        public static uint GetThreadId(IntPtr hWnd)
        {
            uint threadId = GetWindowThreadProcessId(hWnd, out var processId);
            return threadId;
        }

        public static int GetProcessId(IntPtr hWnd)
        {
            GetWindowThreadProcessId(hWnd, out var processId);
            return processId;
        }

        public static string ExtractTextFromConsoleWindow(int processId)
        {
            try
            {
                FreeConsole();
                var result = AttachConsole(processId);
                if (!result)
                {
                    var error = Marshal.GetLastWin32Error();
                    throw new Win32Exception(error);
                }
                var handle = GetStdHandle(STD_OUTPUT_HANDLE);
                if (handle == IntPtr.Zero)
                {
                    var error = Marshal.GetLastWin32Error();
                    throw new Win32Exception(error);
                }
                ConsoleScreenBufferInfo binfo;
                result = GetConsoleScreenBufferInfo(handle, out binfo);
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
                    if (ReadConsoleOutputCharacter(handle, buffer, (uint)buffer.Length, new Coord(0, (short)i), out numberOfCharsRead))
                    {
                        textBuilder.AppendLine(new string(buffer));
                    }
                }

                var text = textBuilder.ToString().TrimEnd();
                return text;
            }
            catch
            {
                FreeConsole();
                return null;
            }
        }

        public static IList<IntPtr> FindWindowByTitle(string title, int? processId, Func<string, string, bool> compareTitle)
        {
            var handles = new List<IntPtr>();
            EnumWindows((IntPtr hWnd, int lParam) => {
                if (processId.HasValue)
                {
                    GetWindowThreadProcessId(hWnd, out var pid);
                    if (processId.Value == pid)
                    {
                        var builder = new StringBuilder(1024);
                        GetWindowText(hWnd, builder, builder.Capacity);
                        if (compareTitle(title, builder.ToString()))
                        {
                            handles.Add(hWnd);
                        }
                    }
                }
                else
                {
                    var builder = new StringBuilder(1024);
                    GetWindowText(hWnd, builder, builder.Capacity);
                    if (compareTitle(title, builder.ToString()))
                    {
                        handles.Add(hWnd);
                    }
                }
                return true;
            }, 0);
            return handles;
        }

        public static bool IsAltTabWindow(IntPtr hWnd)
        {
            if (!IsWindowVisible(hWnd))
            {
                return false;
            }

            var hwndWalk = IntPtr.Zero;
            var hwndTry = GetAncestor(hWnd, GetAncestorFlags.GetRootOwner);
            while (hwndTry != hwndWalk)
            {
                hwndWalk = hwndTry;
                hwndTry = GetLastActivePopup(hwndWalk);
                if (IsWindowVisible(hwndTry))
                {
                    break;
                }
            }

            if (hwndWalk != hWnd)
            {
                return false;
            }

            var ti = new TITLEBARINFO();
            ti.cbSize = (uint)Marshal.SizeOf(ti);
            GetTitleBarInfo(hWnd, ref ti);
            if ((ti.rgstate[0] & STATE_SYSTEM_INVISIBLE) != 0)
            {
                return false;
            }

            if ((GetWindowLong(hWnd, GWL_EXSTYLE) & WS_EX_TOOLWINDOW) != 0)
            {
                return false;
            }

            return true;
        }
    }
}
