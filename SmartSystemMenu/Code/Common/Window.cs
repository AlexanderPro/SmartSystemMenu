using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using SmartSystemMenu.Code.Common.Extensions;

namespace SmartSystemMenu.Code.Common
{
    class Window : IDisposable
    {
        #region Fields.Private

        private IntPtr _handle;
        private Boolean _isManaged;
        private Int32 _defaultTransparency;
        private Int32 _defaultWidth;
        private Int32 _defaultHeight;
        private Int32 _defaultLeft;
        private Int32 _defaultTop;
        private Int32 _beforeRollupHeight;
        private NotifyIcon _systemTrayIcon;
        private SystemMenu _systemMenu;

        #endregion


        #region Properties.Public

        public IntPtr Handle
        {
            get
            {
                return _handle;
            }
        }

        public String WindowText
        {
            get
            {
                StringBuilder sb = new StringBuilder(1024);
                NativeMethods.GetWindowText(_handle, sb, sb.Capacity);
                String windowText = sb.ToString().Trim();
                return windowText;
            }
        }

        public String ClassName
        {
            get
            {
                StringBuilder sb = new StringBuilder(1024);
                NativeMethods.GetClassName(_handle, sb, sb.Capacity);
                String className = sb.ToString().Trim();
                return className;
            }
        }

        public Int32 Style
        {
            get
            {
                Int32 style = NativeMethods.GetWindowLong(_handle, NativeConstants.GWL_STYLE);
                return style;
            }
        }

        public Rect Size
        {
            get
            {
                Rect size;
                NativeMethods.GetWindowRect(_handle, out size);
                return size;
            }
        }

        public Rect ClientSize
        {
            get
            {
                Rect size;
                NativeMethods.GetClientRect(_handle, out size);
                return size;
            }
        }

        public Int32 ProcessId
        {
            get
            {
                Int32 processId;
                NativeMethods.GetWindowThreadProcessId(_handle, out processId);
                return processId;
            }
        }

        public UInt32 ThreadId
        {
            get
            {
                Int32 processId;
                UInt32 threadId = NativeMethods.GetWindowThreadProcessId(_handle, out processId);
                return threadId;
            }
        }

        public Priority ProcessPriority
        {
            get
            {
                PriorityClass priorityClass = NativeMethods.GetPriorityClass(Process.GetProcessById(ProcessId).GetHandle());

                switch (priorityClass)
                {
                    case PriorityClass.REALTIME_PRIORITY_CLASS: return Priority.RealTime;
                    case PriorityClass.HIGH_PRIORITY_CLASS: return Priority.High;
                    case PriorityClass.ABOVE_NORMAL_PRIORITY_CLASS: return Priority.AboveNormal;
                    case PriorityClass.NORMAL_PRIORITY_CLASS: return Priority.Normal;
                    case PriorityClass.BELOW_NORMAL_PRIORITY_CLASS: return Priority.BelowNormal;
                    case PriorityClass.IDLE_PRIORITY_CLASS: return Priority.Idle;
                    default: return Priority.Normal;
                }
            }
        }

        public Int32 ScreenId { get; set; }

        public Boolean IsVisible
        {
            get
            {
                Boolean isVisible = NativeMethods.IsWindowVisible(_handle);
                return isVisible;
            }
        }

        public Int32 Transparency
        {
            get
            {
                Int32 style = NativeMethods.GetWindowLong(_handle, NativeConstants.GWL_EXSTYLE);
                Boolean isLayeredWindow = (style & NativeConstants.WS_EX_LAYERED) == NativeConstants.WS_EX_LAYERED;
                if (!isLayeredWindow) return 0;
                UInt32 key;
                Byte alpha;
                UInt32 flags;
                NativeMethods.GetLayeredWindowAttributes(_handle, out key, out alpha, out flags);
                Int32 transparency = 100 - (Int32)Math.Round(100 * alpha / 255f, MidpointRounding.AwayFromZero);
                return transparency;
            }
        }

        public Boolean AlwaysOnTop
        {
            get
            {
                Int32 style = NativeMethods.GetWindowLong(_handle, NativeConstants.GWL_EXSTYLE);
                Boolean isAlwaysOnTop = (style & NativeConstants.WS_EX_TOPMOST) == NativeConstants.WS_EX_TOPMOST;
                return isAlwaysOnTop;
            }
        }

        public IntPtr Owner
        {
            get
            {
                IntPtr owner = NativeMethods.GetWindow(_handle, NativeConstants.GW_OWNER);
                return owner;
            }
        }

        public SystemMenu Menu
        {
            get
            {
                return _systemMenu;
            }
        }

        public Boolean ExistSystemTrayIcon
        {
            get
            {
                Boolean exist = _systemTrayIcon != null && _systemTrayIcon.Visible;
                return exist;
            }
        }

        public IWin32Window Win32Window
        {
            get
            {
                return new Win32WindowWrapper(Handle);
            }
        }

        #endregion


        #region Methods.Public

        public Window(IntPtr windowHandle)
        {
            _handle = windowHandle;
            _isManaged = true;
            _defaultWidth = Size.Width;
            _defaultHeight = Size.Height;
            _defaultLeft = Size.Left;
            _defaultTop = Size.Top;
            _beforeRollupHeight = Size.Height;
            _defaultTransparency = Transparency;
            _systemMenu = new SystemMenu(windowHandle);
            ScreenId = Screen.AllScreens.ToList().FindIndex(s => s.Primary);

            //_systemMenu.Create();
        }

        ~Window()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_isManaged)
            {
                _systemMenu.Destroy();
                //RestoreTransparency();
                //RestoreSize();
                RestoreFromSystemTray();
            }
            _isManaged = false;
        }

        public override String ToString()
        {
            return WindowText;
        }

        public void SetTrancparency(Int32 percent)
        {
            Byte opacity = (Byte)Math.Round(255 * (100 - percent) / 100f, MidpointRounding.AwayFromZero);
            SetOpacity(_handle, opacity);
        }

        public void RestoreTransparency()
        {
            SetTrancparency(_defaultTransparency);
        }

        public void SetSize(Int32 width, Int32 height)
        {
            NativeMethods.MoveWindow(_handle, Size.Left, Size.Top, width, height, true);
        }

        public void RestoreSize()
        {
            NativeMethods.MoveWindow(_handle, Size.Left, Size.Top, _defaultWidth, _defaultHeight, true);
        }

        public void SetPosition(Int32 left, Int32 top)
        {
            NativeMethods.MoveWindow(_handle, left, top, Size.Width, Size.Height, true);
        }

        public void RestorePosition()
        {
            NativeMethods.MoveWindow(_handle, _defaultLeft, _defaultTop, Size.Width, Size.Height, true);
        }

        public void SaveDefaultSizePosition()
        {
            _defaultLeft = Size.Left;
            _defaultTop = Size.Top;
            _defaultWidth = Size.Width;
            _defaultHeight = Size.Height;
        }

        public void SetAlignment(WindowAlignment alignment)
        {
            Int32 x, y;
            Rectangle screen = ScreenId < Screen.AllScreens.Length ? Screen.AllScreens[ScreenId].WorkingArea : Screen.PrimaryScreen.WorkingArea;
            Rect window = Size;

            switch (alignment)
            {
                case WindowAlignment.TopLeft:
                    {
                        x = screen.X;
                        y = screen.Y;
                        SetPosition(x, y);
                    }
                    break;

                case WindowAlignment.TopCenter:
                    {
                        x = ((screen.Width - window.Width) / 2) + screen.X;
                        y = screen.Y;
                        SetPosition(x, y);
                    }
                    break;

                case WindowAlignment.TopRight:
                    {
                        x = screen.Width - window.Width + screen.X;
                        y = screen.Y;
                        SetPosition(x, y);
                    }
                    break;

                case WindowAlignment.MiddleLeft:
                    {
                        x = screen.X;
                        y = ((screen.Height - window.Height) / 2) + screen.Y;
                        SetPosition(x, y);
                    }
                    break;

                case WindowAlignment.MiddleCenter:
                    {
                        x = ((screen.Width - window.Width) / 2) + screen.X;
                        y = ((screen.Height - window.Height) / 2) + screen.Y;
                        SetPosition(x, y);
                    }
                    break;

                case WindowAlignment.MiddleRight:
                    {
                        x = screen.Width - window.Width + screen.X;
                        y = ((screen.Height - window.Height) / 2) + screen.Y;
                        SetPosition(x, y);
                    }
                    break;

                case WindowAlignment.BottomLeft:
                    {
                        x = screen.X;
                        y = screen.Height - window.Height + screen.Y;
                        SetPosition(x, y);
                    }
                    break;

                case WindowAlignment.BottomCenter:
                    {
                        x = ((screen.Width - window.Width) / 2) + screen.X;
                        y = screen.Height - window.Height + screen.Y;
                        SetPosition(x, y);
                    }
                    break;

                case WindowAlignment.BottomRight:
                    {
                        x = screen.Width - window.Width + screen.X;
                        y = screen.Height - window.Height + screen.Y;
                        SetPosition(x, y);
                    }
                    break;
            }
        }

        public void MakeTopMost(Boolean topMost)
        {
            IntPtr handleTopMost = (IntPtr)(-1);
            IntPtr handleNotTopMost = (IntPtr)(-2);
            NativeMethods.SetWindowPos(_handle, topMost ? handleTopMost : handleNotTopMost, 0, 0, 0, 0, NativeConstants.TOPMOST_FLAGS);
        }

        public void MinimizeToSystemTray()
        {
            CreateIconInSystemTray();
            NativeMethods.ShowWindowAsync(_handle, (Int32)WindowShowStyle.Minimize);
            NativeMethods.ShowWindowAsync(_handle, (Int32)WindowShowStyle.Hide);
        }

        public void MoveToSystemTray()
        {
            CreateIconInSystemTray();
            NativeMethods.ShowWindowAsync(_handle, (Int32)WindowShowStyle.Hide);
        }

        public void ShowNormal()
        {
            NativeMethods.ShowWindow(_handle, (Int32)WindowShowStyle.Normal);
        }

        public void RollUp()
        {
            _beforeRollupHeight = Size.Height;
            SetSize(Size.Width, 0);
        }

        public void UnRollUp()
        {
            SetSize(Size.Width, _beforeRollupHeight);
        }

        public void SetPriority(Priority priority)
        {
            IntPtr processHandle = Process.GetProcessById(ProcessId).GetHandle();
            PriorityClass priorityClass = priority.GetPriorityClass();
            NativeMethods.SetPriorityClass(processHandle, priorityClass);
        }

        public Bitmap PrintWindow()
        {
            Rect rect;
            NativeMethods.GetWindowRect(Handle, out rect);
            var bitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                var hdc = graphics.GetHdc();
                NativeMethods.PrintWindow(Handle, hdc, 0);
                graphics.ReleaseHdc(hdc);
            }
            return bitmap;
        }

        public static void CloseAllWindowsOfProcess(Int32 processId)
        {
            NativeMethods.EnumWindowDelegate d = delegate(IntPtr hWnd, Int32 lParam)
            {
                Int32 pId;
                NativeMethods.GetWindowThreadProcessId(hWnd, out pId);
                if ((Int32)pId == processId)
                {
                    NativeMethods.PostMessage(hWnd, NativeConstants.WM_CLOSE, 0, 0);
                }
                return true;
            };

            NativeMethods.EnumWindows(d, 0);
        }

        public static void ForceForegroundWindow(IntPtr handle)
        {
            IntPtr foreHandle = NativeMethods.GetForegroundWindow();
            UInt32 foreThread = NativeMethods.GetWindowThreadProcessId(foreHandle, IntPtr.Zero);
            UInt32 appThread = NativeMethods.GetCurrentThreadId();
            if (foreThread != appThread)
            {
                NativeMethods.AttachThreadInput(foreThread, appThread, true);
                NativeMethods.BringWindowToTop(handle);
                NativeMethods.ShowWindow(handle, (Int32)WindowShowStyle.Show);
                NativeMethods.AttachThreadInput(foreThread, appThread, false);
            }
            else
            {
                NativeMethods.BringWindowToTop(handle);
                NativeMethods.ShowWindow(handle, (Int32)WindowShowStyle.Show);
            }
        }

        public static void ForceAllMessageLoopsToWakeUp()
        {
            UInt32 result;
            NativeMethods.SendMessageTimeout((IntPtr)NativeConstants.HWND_BROADCAST, NativeConstants.WM_NULL, 0, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG | SendMessageTimeoutFlags.SMTO_NOTIMEOUTIFNOTHUNG, 1000, out result);
        }

        #endregion


        #region Methods.Private

        private void SetOpacity(IntPtr handle, Byte opacity)
        {
            NativeMethods.SetWindowLong(handle, NativeConstants.GWL_EXSTYLE, NativeMethods.GetWindowLong(handle, NativeConstants.GWL_EXSTYLE) | NativeConstants.WS_EX_LAYERED);
            NativeMethods.SetLayeredWindowAttributes(handle, 0, opacity, NativeConstants.LWA_ALPHA);
        }

        private void RestoreFromSystemTray()
        {
            if (_systemTrayIcon != null && _systemTrayIcon.Visible)
            {
                _systemTrayIcon.Visible = false;

                NativeMethods.ShowWindowAsync(_handle, (Int32)WindowShowStyle.Show);
                NativeMethods.ShowWindowAsync(_handle, (Int32)WindowShowStyle.Restore);
                NativeMethods.SetForegroundWindow(_handle);
            }
        }

        private Icon GetWindowIcon()
        {
            IntPtr icon;
            try
            {
                UInt32 result;
                NativeMethods.SendMessageTimeout(_handle, NativeConstants.WM_GETICON, NativeConstants.ICON_SMALL2, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 100, out result);
                icon = new IntPtr(result);

                if (icon == IntPtr.Zero)
                {
                    NativeMethods.SendMessageTimeout(_handle, NativeConstants.WM_GETICON, NativeConstants.ICON_SMALL, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 100, out result);
                    icon = new IntPtr(result);
                }

                if (icon == IntPtr.Zero)
                {
                    NativeMethods.SendMessageTimeout(_handle, NativeConstants.WM_GETICON, NativeConstants.ICON_BIG, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 100, out result);
                    icon = new IntPtr(result);
                }

                if (icon == IntPtr.Zero)
                {
                    icon = NativeMethods.GetClassLongPtr(_handle, NativeConstants.GCLP_HICONSM);
                }

                if (icon == IntPtr.Zero)
                {
                    icon = NativeMethods.GetClassLongPtr(_handle, NativeConstants.GCLP_HICON);
                }

                if (icon == IntPtr.Zero)
                {
                    icon = NativeMethods.LoadIcon(IntPtr.Zero, NativeConstants.IDI_APPLICATION);
                }
            }
            catch
            {
                icon = NativeMethods.LoadIcon(IntPtr.Zero, NativeConstants.IDI_APPLICATION);
            }
            return Icon.FromHandle(icon);
        }

        private void CreateIconInSystemTray()
        {
            _systemTrayIcon = _systemTrayIcon ?? new NotifyIcon();
            _systemTrayIcon.MouseClick -= SystemTrayIconClick;
            _systemTrayIcon.MouseClick += SystemTrayIconClick;
            _systemTrayIcon.Icon = GetWindowIcon();
            _systemTrayIcon.Text = WindowText.Length > 63 ? WindowText.Substring(0, 60).PadRight(63, '.') : WindowText;
            _systemTrayIcon.Visible = true;
        }

        private void SystemTrayIconClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                _systemTrayIcon.Visible = false;
                NativeMethods.ShowWindowAsync(_handle, (Int32)WindowShowStyle.Show);
                NativeMethods.ShowWindowAsync(_handle, (Int32)WindowShowStyle.Restore);
                NativeMethods.SetForegroundWindow(_handle);
            }
        }

        #endregion
    }
}