using System;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing;

namespace SmartSystemMenu.App_Code.Common
{
    class Window : IDisposable
    {
        #region Fields.Private

        private IntPtr _handle;
        private Boolean _isManaged;
        private Int32 _originalWindowLongExStyle;
        private Int32 _originalWindowWidth;
        private Int32 _originalWindowHeight;
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
                return sb.ToString().Trim();
            }
        }

        public String ClassName
        {
            get
            {
                StringBuilder sb = new StringBuilder(1024);
                NativeMethods.GetClassName(_handle, sb, sb.Capacity);
                return sb.ToString().Trim();
            }
        }

        public Int32 Style
        {
            get
            {
                Int32 style = NativeMethods.GetWindowLong(_handle, NativeMethods.GWL_STYLE);
                return style;
            }
        }

        public RECT Size
        {
            get
            {
                RECT size;
                NativeMethods.GetWindowRect(_handle, out size);
                return size;
            }
        }

        public RECT ClientSize
        {
            get
            {
                RECT size;
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

        public Boolean IsVisible
        {
            get
            {
                return NativeMethods.IsWindowVisible(_handle);
            }
        }

        public IntPtr GetOwner
        {
            get
            {
                return NativeMethods.GetWindow(_handle, NativeMethods.GW_OWNER);
            }
        }

        public SystemMenu Menu
        {
            get
            {
                return _systemMenu;
            }
        }

        #endregion


        #region Methods.Public

        public Window(IntPtr windowHandle)
        {
            _handle = windowHandle;
            _isManaged = true;
            _originalWindowWidth = Size.Width;
            _originalWindowHeight = Size.Height;
            _originalWindowLongExStyle = NativeMethods.GetWindowLong(_handle, NativeMethods.GWL_EXSTYLE);
            _systemMenu = new SystemMenu(windowHandle);
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

        public void SetTransparency(Int32 transparecy)
        {
            SetTransparency(_handle, (Byte)transparecy);
        }

        public void SetTrancparencyByPercent(Int32 percent)
        {
            SetTransparency(_handle, (Byte)(255 * percent / 100));
        }

        public void RestoreTransparency()
        {
            NativeMethods.SetWindowLong(_handle, NativeMethods.GWL_EXSTYLE, _originalWindowLongExStyle);
        }

        public void SetSize(Int32 width, Int32 height)
        {
            NativeMethods.MoveWindow(_handle, Size.Left, Size.Top, width, height, true);
        }

        public void RestoreSize()
        {
            NativeMethods.MoveWindow(_handle, Size.Left, Size.Top, _originalWindowWidth, _originalWindowHeight, true);
        }

        public void MakeTopMost(Boolean topMost)
        {
            IntPtr handleTopMost = (IntPtr)(-1);
            IntPtr handleNotTopMost = (IntPtr)(-2);
            NativeMethods.SetWindowPos(_handle, topMost ? handleTopMost : handleNotTopMost, 0, 0, 0, 0, NativeMethods.TOPMOST_FLAGS);
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

        public static void CloseAllWindowsOfProcess(Int32 processId)
        {
            NativeMethods.EnumWindowDelegate d = delegate(IntPtr hWnd, Int32 lParam)
            {
                Int32 pid;
                NativeMethods.GetWindowThreadProcessId(hWnd, out pid);
                if ((Int32)pid == processId)
                {
                    NativeMethods.PostMessage(hWnd, NativeMethods.WM_CLOSE, 0, 0);
                }
                return true;
            };

            NativeMethods.EnumWindows(d, 0);
        }

        public static void SetTransparency(IntPtr handle, Byte transparecy)
        {
            NativeMethods.SetWindowLong(handle, NativeMethods.GWL_EXSTYLE, NativeMethods.GetWindowLong(handle, NativeMethods.GWL_EXSTYLE) | NativeMethods.WS_EX_LAYERED);
            NativeMethods.SetLayeredWindowAttributes(handle, 0, transparecy, NativeMethods.LWA_ALPHA);
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
            NativeMethods.SendMessageTimeout((IntPtr)NativeMethods.HWND_BROADCAST, NativeMethods.WM_NULL, 0, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG | SendMessageTimeoutFlags.SMTO_NOTIMEOUTIFNOTHUNG, 1000, out result);
        }

        #endregion


        #region Methods.Private

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
                NativeMethods.SendMessageTimeout(_handle, NativeMethods.WM_GETICON, NativeMethods.ICON_SMALL2, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 100, out result);
                icon = new IntPtr(result);

                if (icon == IntPtr.Zero)
                {
                    NativeMethods.SendMessageTimeout(_handle, NativeMethods.WM_GETICON, NativeMethods.ICON_SMALL, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 100, out result);
                    icon = new IntPtr(result);
                }

                if (icon == IntPtr.Zero)
                {
                    NativeMethods.SendMessageTimeout(_handle, NativeMethods.WM_GETICON, NativeMethods.ICON_BIG, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 100, out result);
                    icon = new IntPtr(result);
                }

                if (icon == IntPtr.Zero)
                {
                    icon = NativeMethods.GetClassLongPtr(_handle, NativeMethods.GCLP_HICONSM);
                }

                if (icon == IntPtr.Zero)
                {
                    icon = NativeMethods.GetClassLongPtr(_handle, NativeMethods.GCLP_HICON);
                }

                if (icon == IntPtr.Zero)
                {
                    icon = NativeMethods.LoadIcon(IntPtr.Zero, NativeMethods.IDI_APPLICATION);
                }
            }
            catch
            {
                icon = NativeMethods.LoadIcon(IntPtr.Zero, NativeMethods.IDI_APPLICATION);
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