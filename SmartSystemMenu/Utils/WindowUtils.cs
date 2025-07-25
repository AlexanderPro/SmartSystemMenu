﻿using System;
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
using static SmartSystemMenu.Native.Gdi32;
using static SmartSystemMenu.Native.User32;
using static SmartSystemMenu.Native.Kernel32;
using static SmartSystemMenu.Native.Constants;

namespace SmartSystemMenu.Utils
{
    static class WindowUtils
    {
        public static bool PrintWindow(IntPtr hWnd, out Bitmap bitmap)
        {
            GetWindowRect(hWnd, out var rect);
            bitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            using var graphics = Graphics.FromImage(bitmap);
            var hdc = graphics.GetHdc();
            var result = User32.PrintWindow(hWnd, hdc, 0);
            graphics.ReleaseHdc(hdc);
            return result;
        }

        public static bool CaptureWindow(IntPtr hWnd, bool captureCursor, out Bitmap bitmap)
        {
            var rectangle = GetWindowRectWithoutMargins(hWnd);
            var posX = rectangle.Left;
            var posY = rectangle.Top;
            var width = rectangle.Width;
            var height = rectangle.Height;

            var hDesk = GetDesktopWindow();
            var hSrce = GetWindowDC(hDesk);
            var hDest = CreateCompatibleDC(hSrce);
            var hBmp = CreateCompatibleBitmap(hSrce, width, height);
            var hOldBmp = SelectObject(hDest, hBmp);

            var result = BitBlt(hDest, 0, 0, width, height, hSrce, posX, posY, CopyPixelOperations.SourceCopy | CopyPixelOperations.CaptureBlt);

            try
            {
                if (result)
                {
                    bitmap = Image.FromHbitmap(hBmp);
                    if (captureCursor)
                    {
                        CURSORINFO cursorInfo;
                        cursorInfo.cbSize = Marshal.SizeOf(typeof(CURSORINFO));
                        if (GetCursorInfo(out cursorInfo) && cursorInfo.flags == CURSOR_SHOWING && GetIconInfo(cursorInfo.hCursor, out var iconInfo))
                        {
                            using var graphics = Graphics.FromImage(bitmap);
                            var x = cursorInfo.ptScreenPos.x - rectangle.Left - iconInfo.xHotspot;
                            var y = cursorInfo.ptScreenPos.y - rectangle.Top - iconInfo.yHotspot;
                            var hdc = graphics.GetHdc();
                            DrawIconEx(hdc, x, y, cursorInfo.hCursor, 0, 0, 0, IntPtr.Zero, DI_NORMAL | DI_COMPAT);
                            DestroyIcon(cursorInfo.hCursor);
                            DeleteObject(iconInfo.hbmMask);
                            DeleteObject(iconInfo.hbmColor);
                            graphics.ReleaseHdc();
                            DeleteDC(hdc);
                        }
                    }
                }
                else
                {
                    bitmap = new Bitmap(1, 1);
                }

                return result;
            }
            finally
            {
                SelectObject(hDest, hOldBmp);
                DeleteObject(hBmp);
                DeleteDC(hDest);
                ReleaseDC(hDesk, hSrce);
            }
        }

        public static bool IsCorrectScreenshot(IntPtr hWnd, Bitmap bitmap)
        {
            var margins = GetSystemMargins(hWnd);
            if (bitmap == null || bitmap.Width <= 0 || bitmap.Height <= 0 ||
                bitmap.Width <= (margins.Left + margins.Right + 2) || bitmap.Height <= (margins.Top + margins.Bottom + 2))
            {
                return false;
            }

            var x = margins.Left + 1;
            var y = bitmap.Height / 2;
            var startColor = bitmap.GetPixel(x, y);
            for (; y < (bitmap.Height - margins.Bottom - 1); y++)
            {
                for (; x < (bitmap.Width - margins.Right - 1); x++)
                {
                    var color = bitmap.GetPixel(x, y);
                    if (!(startColor.R == color.R && startColor.G == color.G && startColor.B == color.B))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static IntPtr GetParentWindow(IntPtr hWnd)
        {
            IntPtr parentHwnd;
            var resultHwnd = hWnd;
            while ((parentHwnd = GetParent(resultHwnd)) != IntPtr.Zero)
            {
                resultHwnd = parentHwnd;
            }
            return resultHwnd;
        }

        public static uint GetThreadId(IntPtr hWnd)
        {
            uint threadId = GetWindowThreadProcessId(hWnd, out _);
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
                result = GetConsoleScreenBufferInfo(handle, out var binfo);
                if (!result)
                {
                    var error = Marshal.GetLastWin32Error();
                    throw new Win32Exception(error);
                }

                var buffer = new char[binfo.srWindow.Right];
                var textBuilder = new StringBuilder();
                for (var i = 0; i < binfo.dwSize.Y; i++)
                {
                    if (ReadConsoleOutputCharacter(handle, buffer, (uint)buffer.Length, new Coord(0, (short)i), out var numberOfCharsRead))
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

        public static bool HasThickFrame(IntPtr hWnd) => (GetWindowLong(hWnd, GWL_STYLE) & WS_THICKFRAME) != 0;

        public static bool IsClickThrough(IntPtr hWnd)
        {
            var exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            return (exStyle & WS_EX_LAYERED) != 0 && (exStyle & WS_EX_TRANSPARENT) != 0;
        }

        public static bool IsLayered(IntPtr hWnd)
        {
            var exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            return (exStyle & WS_EX_LAYERED) != 0;
        }

        public static void DisableMinimizeButton(IntPtr hWnd, bool disable)
        {
            var style = GetWindowLong(hWnd, GWL_STYLE);
            style = disable ? style & ~WS_MINIMIZEBOX : style | WS_MINIMIZEBOX;
            SetWindowLong(hWnd, GWL_STYLE, style);
        }

        public static void DisableMaximizeButton(IntPtr hWnd, bool disable)
        {
            var style = GetWindowLong(hWnd, GWL_STYLE);
            style = disable ? style & ~WS_MAXIMIZEBOX : style | WS_MAXIMIZEBOX;
            SetWindowLong(hWnd, GWL_STYLE, style);
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

        public static void SetThickFrame(IntPtr hWnd)
        {
            var style = GetWindowLong(hWnd, GWL_STYLE);
            style |= WS_THICKFRAME;
            SetWindowLong(hWnd, GWL_STYLE, style);
        }

        public static void UnsetThickFrame(IntPtr hWnd)
        {
            var style = GetWindowLong(hWnd, GWL_STYLE);
            style &= ~WS_THICKFRAME;
            SetWindowLong(hWnd, GWL_STYLE, style);
        }

        public static Icon GetIcon(IntPtr hWnd)
        {
            IntPtr icon;
            try
            {
                SendMessageTimeout(hWnd, WM_GETICON, ICON_SMALL2, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 100, out var result);
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

        public static void SetWindowText(IntPtr hWnd, string text)
        {
            User32.SetWindowText(hWnd, text);
        }

        public static string GetWindowText(IntPtr hWnd)
        {
            var length = GetWindowTextLength(hWnd);
            if (length > 0)
            {
                var builder = new StringBuilder(length + 1);
                User32.GetWindowText(hWnd, builder, builder.Capacity);
                return builder.ToString();
            }
            else
            {
                return string.Empty;
            }
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

                            if (!string.IsNullOrEmpty(window.Current.ClassName) && window.TryGetCurrentPattern(TextPattern.Pattern, out var pattern) && pattern is TextPattern textPattern)
                            {
                                var text = textPattern.DocumentRange.GetText(-1);
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
                var data = new WindowCompositionAttributeData
                {
                    Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                    SizeOfData = accentStructSize,
                    Data = accentPtr
                };
                SetWindowCompositionAttribute(hWnd, ref data);
            }
            finally
            {
                Marshal.FreeHGlobal(accentPtr);
            }
        }

        public static Rect GetWindowRectWithoutMargins(IntPtr hWnd)
        {
            Rect size;
            if (Environment.OSVersion.Version.Major < 6)
            {
                GetWindowRect(hWnd, out size);
            }
            else if (Dwmapi.DwmGetWindowAttribute(hWnd, DWMWA_EXTENDED_FRAME_BOUNDS, out size, Marshal.SizeOf(typeof(Rect))) != 0)
            {
                GetWindowRect(hWnd, out size);
            }
            return size;
        }

        public static Rect GetSystemMargins(IntPtr hWnd)
        {
            GetWindowRect(hWnd, out var rect);
            var rectWithoutMargin = GetWindowRectWithoutMargins(hWnd);
            return new Rect
            {
                Left = rectWithoutMargin.Left - rect.Left,
                Top = rectWithoutMargin.Top - rect.Top,
                Right = rect.Right - rectWithoutMargin.Right,
                Bottom = rect.Bottom - rectWithoutMargin.Bottom
            };
        }

        public static Func<int, double> TransparencyToOpacity = t => 1 - (t / 100.0);

        public static Func<int, byte> TransparencyToAlphaOpacity = t => (byte)Math.Round(255 * (100 - t) / 100f, MidpointRounding.AwayFromZero);

        public static Func<byte, int> AlphaOpacityToTransparency = o => 100 - (int)Math.Round(100 * o / 255f, MidpointRounding.AwayFromZero);
    }
}
