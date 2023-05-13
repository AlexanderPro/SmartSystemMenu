using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Windows.Automation;
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
                        if (compareTitle(title, GetWindowText(hWnd)))
                        {
                            handles.Add(hWnd);
                        }
                    }
                }
                else
                {
                    if (compareTitle(title, GetWindowText(hWnd)))
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

            if (IsExToolWindow(hWnd))
            {
                return false;
            }

            return true;
        }

        public static bool IsAlwaysOnTop(IntPtr hWnd) => (GetWindowLong(hWnd, GWL_EXSTYLE) & WS_EX_TOPMOST) != 0;

        public static bool IsExToolWindow(IntPtr hWnd) => (GetWindowLong(hWnd, GWL_EXSTYLE) & WS_EX_TOOLWINDOW) != 0;

        public static bool IsDisabledMinimizeButton(IntPtr hWnd) => (GetWindowLong(hWnd, GWL_STYLE) & WS_MINIMIZEBOX) == 0;

        public static bool IsDisabledMaximizeButton(IntPtr hWnd) => (GetWindowLong(hWnd, GWL_STYLE) & WS_MAXIMIZEBOX) == 0;

        public static bool IsClickThrough(IntPtr hWnd)
        {
            var style = GetWindowLong(hWnd, GWL_EXSTYLE);
            return (style & WS_EX_LAYERED) != 0 && (style & WS_EX_TRANSPARENT) != 0;
        }

        public static bool IsLayered(IntPtr hWnd)
        {
            var style = GetWindowLong(hWnd, GWL_EXSTYLE);
            return (style & WS_EX_LAYERED) != 0;
        }

        public static void DisableMinimizeButton(IntPtr hWnd, bool disable)
        {
            var exStyle = GetWindowLong(hWnd, GWL_STYLE);
            exStyle = disable ? exStyle & ~WS_MINIMIZEBOX : exStyle | WS_MINIMIZEBOX;
            SetWindowLong(hWnd, GWL_STYLE, exStyle);
        }

        public static void DisableMaximizeButton(IntPtr hWnd, bool disable)
        {
            var exStyle = GetWindowLong(hWnd, GWL_STYLE);
            exStyle = disable ? exStyle & ~WS_MAXIMIZEBOX : exStyle | WS_MAXIMIZEBOX;
            SetWindowLong(hWnd, GWL_STYLE, exStyle);
        }

        public static void SetExToolWindow(IntPtr hWnd)
        {
            var exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            exStyle |= WS_EX_TOOLWINDOW;
            SetWindowLong(hWnd, GWL_EXSTYLE, exStyle);
        }

        public static void UnsetExToolWindow(IntPtr hWnd)
        {
            var exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            exStyle &= ~WS_EX_TOOLWINDOW;
            SetWindowLong(hWnd, GWL_EXSTYLE, exStyle);
        }

        public static void SetClickThrough(IntPtr hWnd)
        {
            var exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            exStyle |= WS_EX_LAYERED;
            exStyle |= WS_EX_TRANSPARENT;
            SetWindowLong(hWnd, GWL_EXSTYLE, exStyle);
        }

        public static void UnsetClickThrough(IntPtr hWnd)
        {
            var exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            exStyle &= ~WS_EX_LAYERED;
            exStyle &= ~WS_EX_TRANSPARENT;
            SetWindowLong(hWnd, GWL_EXSTYLE, exStyle);
        }

        public static void UnsetTransparent(IntPtr hWnd)
        {
            var exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            exStyle &= ~WS_EX_TRANSPARENT;
            SetWindowLong(hWnd, GWL_EXSTYLE, exStyle);
        }

        public static Icon GetIcon(IntPtr hWnd)
        {
            IntPtr icon;
            try
            {
                uint result;
                SendMessageTimeout(hWnd, WM_GETICON, ICON_SMALL2, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 100, out result);
                icon = new IntPtr(result);

                if (icon == IntPtr.Zero)
                {
                    SendMessageTimeout(hWnd, WM_GETICON, ICON_SMALL, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 100, out result);
                    icon = new IntPtr(result);
                }

                if (icon == IntPtr.Zero)
                {
                    SendMessageTimeout(hWnd, WM_GETICON, ICON_BIG, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 100, out result);
                    icon = new IntPtr(result);
                }

                if (icon == IntPtr.Zero)
                {
                    icon = GetClassLongPtr(hWnd, GCLP_HICONSM);
                }

                if (icon == IntPtr.Zero)
                {
                    icon = GetClassLongPtr(hWnd, GCLP_HICON);
                }

                if (icon == IntPtr.Zero)
                {
                    icon = LoadIcon(IntPtr.Zero, IDI_APPLICATION);
                }
            }
            catch
            {
                icon = LoadIcon(IntPtr.Zero, IDI_APPLICATION);
            }

            return Icon.FromHandle(icon);
        }

        public static string GetFontName(IntPtr hWnd)
        {
            var hFont = SendMessage(hWnd, WM_GETFONT, 0, 0);
            if (hFont == IntPtr.Zero)
            {
                return "Default system font";
            }
            var font = Font.FromHfont(hFont);
            return font.Name;
        }

        public static string GetWmGettext(IntPtr hWnd)
        {
            var titleSize = SendMessage(hWnd, WM_GETTEXTLENGTH, 0, 0);
            if (titleSize.ToInt32() == 0)
            {
                return string.Empty;
            }

            var title = new StringBuilder(titleSize.ToInt32() + 1);
            SendMessage(hWnd, WM_GETTEXT, title.Capacity, title);
            return title.ToString();
        }

        public static void SetOpacity(IntPtr hWnd, byte opacity)
        {
            var exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            SetWindowLong(hWnd, GWL_EXSTYLE, exStyle | WS_EX_LAYERED);
            SetLayeredWindowAttributes(hWnd, 0, opacity, LWA_ALPHA);
        }


        public static string GetWindowText(IntPtr hWnd)
        {
            var builder = new StringBuilder(1024);
            User32.GetWindowText(hWnd, builder, builder.Capacity);
            var windowText = builder.ToString();
            return windowText;
        }

        public static string GetClassName(IntPtr hWnd)
        {
            var builder = new StringBuilder(1024);
            User32.GetClassName(hWnd, builder, builder.Capacity);
            var className = builder.ToString();
            return className;
        }

        public static string RealGetWindowClass(IntPtr hWnd)
        {
            var builder = new StringBuilder(1024);
            User32.RealGetWindowClass(hWnd, builder, builder.Capacity);
            var className = builder.ToString();
            return className;
        }

        public static string ExtractTextFromWindow(IntPtr hWnd)
        {
            try
            {
                var builder = new StringBuilder();
                foreach (AutomationElement window in AutomationElement.FromHandle(hWnd).FindAll(TreeScope.Descendants, Condition.TrueCondition))
                {
                    try
                    {
                        if (window.Current.IsEnabled && !string.IsNullOrEmpty(window.Current.Name))
                        {
                            builder.AppendLine(window.Current.Name).AppendLine();

                            object pattern;
                            if (!string.IsNullOrEmpty(window.Current.ClassName) && window.TryGetCurrentPattern(TextPattern.Pattern, out pattern) && pattern is TextPattern)
                            {
                                var text = ((TextPattern)pattern).DocumentRange.GetText(-1);
                                if (!string.IsNullOrEmpty(text))
                                {
                                    builder.AppendLine(text).AppendLine();
                                }
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }

                return builder.ToString();
            }
            catch
            {
                return null;
            }
        }

        public static void ForceForegroundWindow(IntPtr hWnd)
        {
            var foreHandle = GetForegroundWindow();
            var foreThread = GetWindowThreadProcessId(foreHandle, IntPtr.Zero);
            var appThread = GetCurrentThreadId();
            if (foreThread != appThread)
            {
                AttachThreadInput(foreThread, appThread, true);
                BringWindowToTop(hWnd);
                ShowWindow(hWnd, (int)WindowShowStyle.Show);
                AttachThreadInput(foreThread, appThread, false);
            }
            else
            {
                BringWindowToTop(hWnd);
                ShowWindow(hWnd, (int)WindowShowStyle.Show);
            }
        }

        public static void AeroGlassForVistaAndSeven(IntPtr hWnd, bool enable)
        {
            var blurBehind = new DWM_BLURBEHIND()
            {
                dwFlags = DWM_BB.Enable,
                fEnable = enable,
                hRgnBlur = IntPtr.Zero,
                fTransitionOnMaximized = false
            };
            Dwmapi.DwmEnableBlurBehindWindow(hWnd, ref blurBehind);
        }

        public static void AeroGlassForEightAndHigher(IntPtr hWnd, bool enable)
        {
            var accent = new AccentPolicy();
            var accentStructSize = Marshal.SizeOf(accent);
            accent.AccentState = enable ? AccentState.ACCENT_ENABLE_BLURBEHIND : AccentState.ACCENT_DISABLED;
            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            try
            {
                Marshal.StructureToPtr(accent, accentPtr, false);
                var data = new WindowCompositionAttributeData();
                data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
                data.SizeOfData = accentStructSize;
                data.Data = accentPtr;
                SetWindowCompositionAttribute(hWnd, ref data);
            }
            finally
            {
                Marshal.FreeHGlobal(accentPtr);
            }
        }

        public static void ForceAllMessageLoopsToWakeUp() => SendMessageTimeout((IntPtr)HWND_BROADCAST, WM_NULL, 0, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG | SendMessageTimeoutFlags.SMTO_NOTIMEOUTIFNOTHUNG, 1000, out var result);
    }
}
