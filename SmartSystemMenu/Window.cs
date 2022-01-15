using System;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Automation;
using System.IO;
using System.Threading;
using SmartSystemMenu.Native;
using SmartSystemMenu.Settings;
using SmartSystemMenu.Extensions;
using SmartSystemMenu.Utils;

namespace SmartSystemMenu
{
    class Window : IDisposable
    {
        private bool _isManaged;
        private int _defaultTransparency;
        private int _defaultWidth;
        private int _defaultHeight;
        private int _defaultLeft;
        private int _defaultTop;
        private int _beforeRollupHeight;
        private bool _suspended;
        private NotifyIcon _systemTrayIcon;


        public IntPtr Handle { get; private set; }

        public SystemMenu Menu { get; private set; }        

        public Rect Size
        {
            get
            {
                Rect size;
                NativeMethods.GetWindowRect(Handle, out size);
                return size;
            }
        }

        public Rect ClientSize
        {
            get
            {
                Rect size;
                NativeMethods.GetClientRect(Handle, out size);
                return size;
            }
        }

        public Rect SizeOnMonitor
        {
            get
            {
                var monitorHandle = NativeMethods.MonitorFromWindow(Handle, NativeConstants.MONITOR_DEFAULTTONEAREST);
                var monitorInfo = new MonitorInfo();
                monitorInfo.Init();
                NativeMethods.GetMonitorInfo(monitorHandle, ref monitorInfo);

                var size = new Rect()
                {
                    Left = Size.Left - monitorInfo.rcWork.Left,
                    Right = Size.Right - monitorInfo.rcWork.Right,
                    Top = Size.Top - monitorInfo.rcWork.Top,
                    Bottom = Size.Bottom - monitorInfo.rcWork.Bottom
                };
                return size;
            }
        }

        public int ProcessId
        {
            get
            {
                int processId;
                NativeMethods.GetWindowThreadProcessId(Handle, out processId);
                return processId;
            }
        }

        public Process Process
        {
            get
            {
                return SystemUtils.GetProcessByIdSafely(ProcessId);
            }
        }

        public Priority ProcessPriority
        {
            get
            {
                var process = Process;
                if (process == null)
                {
                    return Priority.Normal;
                }

                var priorityClass = NativeMethods.GetPriorityClass(process.GetHandle());
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

        public bool IsVisible
        {
            get
            {
                bool isVisible = NativeMethods.IsWindowVisible(Handle);
                return isVisible;
            }
        }

        public int Transparency
        {
            get
            {
                int style = NativeMethods.GetWindowLong(Handle, NativeConstants.GWL_EXSTYLE);
                bool isLayeredWindow = (style & NativeConstants.WS_EX_LAYERED) == NativeConstants.WS_EX_LAYERED;
                if (!isLayeredWindow) return 0;
                uint key;
                Byte alpha;
                uint flags;
                NativeMethods.GetLayeredWindowAttributes(Handle, out key, out alpha, out flags);
                int transparency = 100 - (int)Math.Round(100 * alpha / 255f, MidpointRounding.AwayFromZero);
                return transparency;
            }
        }

        public bool AlwaysOnTop
        {
            get
            {
                int style = NativeMethods.GetWindowLong(Handle, NativeConstants.GWL_EXSTYLE);
                bool isAlwaysOnTop = (style & NativeConstants.WS_EX_TOPMOST) == NativeConstants.WS_EX_TOPMOST;
                return isAlwaysOnTop;
            }
        }

        public IntPtr Owner
        {
            get
            {
                IntPtr owner = NativeMethods.GetWindow(Handle, NativeConstants.GW_OWNER);
                return owner;
            }
        }

        public bool ExistSystemTrayIcon
        {
            get
            {
                bool exist = _systemTrayIcon != null && _systemTrayIcon.Visible;
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

        public Window(IntPtr windowHandle)
        {
            Handle = windowHandle;
            _isManaged = true;
            _defaultWidth = Size.Width;
            _defaultHeight = Size.Height;
            _defaultLeft = Size.Left;
            _defaultTop = Size.Top;
            _beforeRollupHeight = Size.Height;
            _defaultTransparency = Transparency;
        }

        public Window(IntPtr windowHandle, MenuItems menuItems, LanguageSettings languageSettings)
        {
            Handle = windowHandle;
            _isManaged = true;
            _defaultWidth = Size.Width;
            _defaultHeight = Size.Height;
            _defaultLeft = Size.Left;
            _defaultTop = Size.Top;
            _beforeRollupHeight = Size.Height;
            _defaultTransparency = Transparency;
            Menu = new SystemMenu(windowHandle, menuItems, languageSettings);

            //Menu.Create();
        }

        ~Window()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_isManaged)
            {
                Menu?.Destroy();
                //RestoreTransparency();
                //RestoreSize();
                RestoreFromSystemTray();
            }
            _isManaged = false;
        }

        public override string ToString()
        {
            return GetWindowText();
        }

        public string GetWindowText()
        {
            var builder = new StringBuilder(1024);
            NativeMethods.GetWindowText(Handle, builder, builder.Capacity);
            var windowText = builder.ToString();
            return windowText;
        }

        public string GetClassName()
        {
            var builder = new StringBuilder(1024);
            NativeMethods.GetClassName(Handle, builder, builder.Capacity);
            var className = builder.ToString();
            return className;
        }

        private string RealGetWindowClass()
        {
            var builder = new StringBuilder(1024);
            NativeMethods.RealGetWindowClass(Handle, builder, builder.Capacity);
            var className = builder.ToString();
            return className;
        }

        public WindowInfo GetWindowInfo()
        {
            var process = Process;
            var info = new WindowInfo();
            info.GetWindowText = GetWindowText();
            info.WM_GETTEXT = GetWmGettext();
            info.GetClassName = GetClassName();
            info.RealGetWindowClass = RealGetWindowClass();
            info.Handle = Handle;
            info.ParentHandle = NativeMethods.GetParent(Handle);
            info.Size = Size;
            info.ClientSize = ClientSize;
            info.FrameBounds = GetSystemMargins();
            info.ProcessId = ProcessId;
            info.ThreadId = WindowUtils.GetThreadId(Handle);
            info.GWL_STYLE = NativeMethods.GetWindowLong(Handle, NativeConstants.GWL_STYLE);
            info.GWL_EXSTYLE = NativeMethods.GetWindowLong(Handle, NativeConstants.GWL_EXSTYLE);
            info.GWL_ID = NativeMethods.GetWindowLong(Handle, NativeConstants.GWL_ID);
            info.GWL_USERDATA = NativeMethods.GetWindowLong(Handle, NativeConstants.GWL_USERDATA);
            info.GCL_STYLE = NativeMethods.GetClassLong(Handle, NativeConstants.GCL_STYLE);
            info.GCL_WNDPROC = NativeMethods.GetClassLong(Handle, NativeConstants.GCL_WNDPROC);
            info.DWL_DLGPROC = NativeMethods.GetClassLong(Handle, NativeConstants.DWL_DLGPROC);
            info.DWL_USER = NativeMethods.GetClassLong(Handle, NativeConstants.DWL_USER);
            info.FullPath = process == null ? "" : process.GetMainModuleFileName();
            info.FullPath = info.FullPath == null ? "" : info.FullPath;
            info.Priority = ProcessPriority;
            info.StartTime = process == null ? (DateTime?)null : process.StartTime;

            try
            {
                var processInfo = SystemUtils.GetWmiProcessInfo(process.Id);
                info.Owner = processInfo.Owner;
                info.CommandLine = processInfo.CommandLine;
                info.ThreadCount = processInfo.ThreadCount;
                info.HandleCount = processInfo.HandleCount;
                info.VirtualSize = processInfo.VirtualSize;
                info.WorkingSetSize = processInfo.WorkingSetSize;
            }
            catch
            {
            }

            try
            {
                info.FontName = GetFontName();
            }
            catch
            {
            }

            try
            {
                var windowInfo = new WINDOW_INFO();
                windowInfo.cbSize = Marshal.SizeOf(windowInfo);
                if (NativeMethods.GetWindowInfo(Handle, ref windowInfo))
                {
                    info.WindowInfoExStyle = windowInfo.dwExStyle;
                }
            }
            catch
            {
            }

            try
            {
                uint key;
                Byte alpha;
                uint flags;
                var result = NativeMethods.GetLayeredWindowAttributes(Handle, out key, out alpha, out flags);
                var layeredWindow = (LayeredWindow)flags;
                info.LWA_ALPHA = layeredWindow.HasFlag(LayeredWindow.LWA_ALPHA);
                info.LWA_COLORKEY = layeredWindow.HasFlag(LayeredWindow.LWA_COLORKEY);
            }
            catch
            {
            }

            try
            {
                info.Instance = process == null ? IntPtr.Zero : process.Modules[0].BaseAddress;
            }
            catch
            {
            }

            try
            {
                info.Parent = Path.GetFileName(process.GetParentProcess().GetMainModuleFileName());
            }
            catch
            {
            }

            try
            {
                var fileVersionInfo = process.MainModule.FileVersionInfo;
                info.ProductName = fileVersionInfo.ProductName;
                info.ProductVersion = fileVersionInfo.ProductVersion;
                info.FileVersion = fileVersionInfo.FileVersion;
                info.Copyright = fileVersionInfo.LegalCopyright;
            }
            catch
            {
            }

            /*try
            {
                var control = Control.FromHandle(Handle);
                var accessibilityObject = control.AccessibilityObject;
                info.AccessibleName = accessibilityObject == null ? "" : accessibilityObject.Name;
                info.AccessibleValue = accessibilityObject == null ? "" : accessibilityObject.Value;
                info.AccessibleRole = accessibilityObject == null ? "" : accessibilityObject.Role.ToString();
                info.AccessibleDescription = accessibilityObject == null ? "" : accessibilityObject.Description;
            }
            catch
            {
            }*/

            return info;
        }

        public void Suspend()
        {
            _suspended = true;
            Process.Suspend();
        }

        public void Resume()
        {
            _suspended = false;
            Process.Resume();
        }

        public void SetTransparency(int percent)
        {
            var opacity = (byte)Math.Round(255 * (100 - percent) / 100f, MidpointRounding.AwayFromZero);
            SetOpacity(Handle, opacity);
        }

        public void RestoreTransparency()
        {
            SetTransparency(_defaultTransparency);
        }

        public void SetWidth(int width)
        {
            NativeMethods.MoveWindow(Handle, Size.Left, Size.Top, width, Size.Height, true);
        }

        public void SetHeight(int height)
        {
            NativeMethods.MoveWindow(Handle, Size.Left, Size.Top, Size.Width, height, true);
        }

        public void SetSize(int width, int height, int? left = null, int? top = null)
        {
            NativeMethods.MoveWindow(Handle, left == null ? Size.Left : left.Value, top == null ? Size.Top : top.Value, width, height, true);
        }

        public void RestoreSize()
        {
            NativeMethods.MoveWindow(Handle, _defaultLeft, _defaultTop, _defaultWidth, _defaultHeight, true);
        }

        public void SetLeft(int left)
        {
            NativeMethods.MoveWindow(Handle, left, Size.Top, Size.Width, Size.Height, true);
        }

        public void SetTop(int top)
        {
            NativeMethods.MoveWindow(Handle, Size.Left, top, Size.Width, Size.Height, true);
        }

        public void SetPosition(int left, int top)
        {
            NativeMethods.MoveWindow(Handle, left, top, Size.Width, Size.Height, true);
        }

        public void RestorePosition()
        {
            NativeMethods.MoveWindow(Handle, _defaultLeft, _defaultTop, Size.Width, Size.Height, true);
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
            var x = 0;
            var y = 0;
            var screen = Screen.FromHandle(Handle).WorkingArea;
            var window = Size;

            if (alignment == WindowAlignment.CenterHorizontally)
            {
                SetLeft(((screen.Width - window.Width) / 2) + screen.X);
                return;
            }

            if (alignment == WindowAlignment.CenterVertically)
            {
                SetTop(((screen.Height - window.Height) / 2) + screen.Y);
                return;
            }

            switch (alignment)
            {
                case WindowAlignment.TopLeft:
                    {
                        x = screen.X;
                        y = screen.Y;
                    }
                    break;

                case WindowAlignment.TopCenter:
                    {
                        x = ((screen.Width - window.Width) / 2) + screen.X;
                        y = screen.Y;
                    }
                    break;

                case WindowAlignment.TopRight:
                    {
                        x = screen.Width - window.Width + screen.X;
                        y = screen.Y;
                    }
                    break;

                case WindowAlignment.MiddleLeft:
                    {
                        x = screen.X;
                        y = (((screen.Height - window.Height) / 2) + screen.Y);
                    }
                    break;

                case WindowAlignment.MiddleCenter:
                    {
                        x = ((screen.Width - window.Width) / 2) + screen.X;
                        y = ((screen.Height - window.Height) / 2) + screen.Y;
                    }
                    break;

                case WindowAlignment.MiddleRight:
                    {
                        x = screen.Width - window.Width + screen.X;
                        y = (((screen.Height - window.Height) / 2) + screen.Y);
                    }
                    break;

                case WindowAlignment.BottomLeft:
                    {
                        x = screen.X;
                        y = screen.Height - window.Height + screen.Y;
                    }
                    break;

                case WindowAlignment.BottomCenter:
                    {
                        x = ((screen.Width - window.Width) / 2) + screen.X;
                        y = screen.Height - window.Height + screen.Y;
                    }
                    break;

                case WindowAlignment.BottomRight:
                    {
                        x = screen.Width - window.Width + screen.X;
                        y = screen.Height - window.Height + screen.Y;
                    }
                    break;
            }
            SetPosition(x, y);
        }

        public void MakeTopMost(bool topMost)
        {
            IntPtr handleTopMost = (IntPtr)(-1);
            IntPtr handleNotTopMost = (IntPtr)(-2);
            NativeMethods.SetWindowPos(Handle, topMost ? handleTopMost : handleNotTopMost, 0, 0, 0, 0, NativeConstants.SWP_NOSIZE | NativeConstants.SWP_NOMOVE);
        }

        public void SendToBottom()
        {
            NativeMethods.SetWindowPos(Handle, new IntPtr(1), 0, 0, 0, 0, NativeConstants.SWP_NOSIZE | NativeConstants.SWP_NOMOVE);
        }

        public void MinimizeToSystemTray()
        {
            CreateIconInSystemTray();
            NativeMethods.ShowWindowAsync(Handle, (int)WindowShowStyle.Minimize);
            NativeMethods.ShowWindowAsync(Handle, (int)WindowShowStyle.Hide);
        }

        public void MoveToSystemTray()
        {
            CreateIconInSystemTray();
            NativeMethods.ShowWindowAsync(Handle, (int)WindowShowStyle.Hide);
        }

        public void ShowNormal()
        {
            NativeMethods.ShowWindow(Handle, (int)WindowShowStyle.Normal);
        }

        public void RollUp()
        {
            _beforeRollupHeight = Size.Height;
            SetSize(Size.Width, SystemInformation.CaptionHeight);
        }

        public void UnRollUp()
        {
            SetSize(Size.Width, _beforeRollupHeight);
        }

        public void SetPriority(Priority priority)
        {
            var process = Process;
            if (process != null)
            {
                NativeMethods.SetPriorityClass(process.GetHandle(), priority.GetPriorityClass());
            }
        }

        public string ExtractText()
        {
            var text = WindowUtils.ExtractTextFromConsoleWindow(ProcessId);
            text = text ?? ExtractTextFromWindow();
            return text;
        }

        public void AeroGlassForVistaAndSeven(bool enable)
        {
            var blurBehind = new DWM_BLURBEHIND()
            {
                dwFlags = DWM_BB.Enable,
                fEnable = enable,
                hRgnBlur = IntPtr.Zero,
                fTransitionOnMaximized = false
            };
            NativeMethods.DwmEnableBlurBehindWindow(Handle, ref blurBehind);
        }

        public void AeroGlassForEightAndHigher(bool enable)
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
                NativeMethods.SetWindowCompositionAttribute(Handle, ref data);
            }
            finally
            {
                Marshal.FreeHGlobal(accentPtr);
            }
        }

        public void MoveToMonitor(IntPtr monitorHandle)
        {
            var currentMonitorHandle = NativeMethods.MonitorFromWindow(Handle, NativeConstants.MONITOR_DEFAULTTONEAREST);
            if (currentMonitorHandle != monitorHandle)
            {
                var currentMonitorInfo = new MonitorInfo();
                currentMonitorInfo.Init();
                NativeMethods.GetMonitorInfo(currentMonitorHandle, ref currentMonitorInfo);

                var newMonitorInfo = new MonitorInfo();
                newMonitorInfo.Init();
                NativeMethods.GetMonitorInfo(monitorHandle, ref newMonitorInfo);
                NativeMethods.GetWindowRect(Handle, out Rect windowRect);

                var left = newMonitorInfo.rcWork.Left + windowRect.Left - currentMonitorInfo.rcWork.Left;
                var top = newMonitorInfo.rcWork.Top + windowRect.Top - currentMonitorInfo.rcWork.Top;
                if (windowRect.Left - currentMonitorInfo.rcWork.Left > newMonitorInfo.rcWork.Width || windowRect.Top - currentMonitorInfo.rcWork.Top > newMonitorInfo.rcWork.Height)
                {
                    left = newMonitorInfo.rcWork.Left;
                    top = newMonitorInfo.rcWork.Top;
                }

                NativeMethods.MoveWindow(Handle, left, top, windowRect.Width, windowRect.Height, true);
                Thread.Sleep(10);
                NativeMethods.MoveWindow(Handle, left, top, windowRect.Width, windowRect.Height, true);
            }
        }

        public static void ForceForegroundWindow(IntPtr handle)
        {
            IntPtr foreHandle = NativeMethods.GetForegroundWindow();
            uint foreThread = NativeMethods.GetWindowThreadProcessId(foreHandle, IntPtr.Zero);
            uint appThread = NativeMethods.GetCurrentThreadId();
            if (foreThread != appThread)
            {
                NativeMethods.AttachThreadInput(foreThread, appThread, true);
                NativeMethods.BringWindowToTop(handle);
                NativeMethods.ShowWindow(handle, (int)WindowShowStyle.Show);
                NativeMethods.AttachThreadInput(foreThread, appThread, false);
            }
            else
            {
                NativeMethods.BringWindowToTop(handle);
                NativeMethods.ShowWindow(handle, (int)WindowShowStyle.Show);
            }
        }

        public static void ForceAllMessageLoopsToWakeUp()
        {
            uint result;
            NativeMethods.SendMessageTimeout((IntPtr)NativeConstants.HWND_BROADCAST, NativeConstants.WM_NULL, 0, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG | SendMessageTimeoutFlags.SMTO_NOTIMEOUTIFNOTHUNG, 1000, out result);
        }


        private string GetFontName()
        {
            var hFont = NativeMethods.SendMessage(Handle, NativeConstants.WM_GETFONT, 0, 0);
            if (hFont == IntPtr.Zero)
            {
                return "Default system font";
            }
            var font = Font.FromHfont(hFont);
            return font.Name;
        }

        private string GetWmGettext()
        {
            var titleSize = NativeMethods.SendMessage(Handle, NativeConstants.WM_GETTEXTLENGTH, 0, 0);
            if (titleSize.ToInt32() == 0)
            {
                return String.Empty;
            }

            var title = new StringBuilder(titleSize.ToInt32() + 1);
            NativeMethods.SendMessage(Handle, NativeConstants.WM_GETTEXT, title.Capacity, title);
            return title.ToString();
        }

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

                NativeMethods.ShowWindowAsync(Handle, (int)WindowShowStyle.Show);
                NativeMethods.ShowWindowAsync(Handle, (int)WindowShowStyle.Restore);
                NativeMethods.SetForegroundWindow(Handle);
            }
        }

        private Icon GetWindowIcon()
        {
            IntPtr icon;
            try
            {
                uint result;
                NativeMethods.SendMessageTimeout(Handle, NativeConstants.WM_GETICON, NativeConstants.ICON_SMALL2, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 100, out result);
                icon = new IntPtr(result);

                if (icon == IntPtr.Zero)
                {
                    NativeMethods.SendMessageTimeout(Handle, NativeConstants.WM_GETICON, NativeConstants.ICON_SMALL, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 100, out result);
                    icon = new IntPtr(result);
                }

                if (icon == IntPtr.Zero)
                {
                    NativeMethods.SendMessageTimeout(Handle, NativeConstants.WM_GETICON, NativeConstants.ICON_BIG, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 100, out result);
                    icon = new IntPtr(result);
                }

                if (icon == IntPtr.Zero)
                {
                    icon = NativeMethods.GetClassLongPtr(Handle, NativeConstants.GCLP_HICONSM);
                }

                if (icon == IntPtr.Zero)
                {
                    icon = NativeMethods.GetClassLongPtr(Handle, NativeConstants.GCLP_HICON);
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
            string windowText = GetWindowText();
            _systemTrayIcon.Text = windowText.Length > 63 ? windowText.Substring(0, 60).PadRight(63, '.') : windowText;
            _systemTrayIcon.Visible = true;
        }

        private void SystemTrayIconClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_suspended)
                {
                    Resume();
                    Thread.Sleep(100);
                }

                _systemTrayIcon.Visible = false;
                NativeMethods.ShowWindowAsync(Handle, (int)WindowShowStyle.Show);
                NativeMethods.ShowWindowAsync(Handle, (int)WindowShowStyle.Restore);
                NativeMethods.SetForegroundWindow(Handle);
            }
        }

        private string ExtractTextFromWindow()
        {
            try
            {
                var builder = new StringBuilder();
                foreach (AutomationElement window in AutomationElement.FromHandle(Handle).FindAll(TreeScope.Descendants, Condition.TrueCondition))
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

        private Rect GetSizeWithMargins()
        {
            Rect size;
            if (Environment.OSVersion.Version.Major < 6)
            {
                NativeMethods.GetWindowRect(Handle, out size);
            }
            else if (NativeMethods.DwmGetWindowAttribute(Handle, NativeConstants.DWMWA_EXTENDED_FRAME_BOUNDS, out size, Marshal.SizeOf(typeof(Rect))) != 0)
            {
                NativeMethods.GetWindowRect(Handle, out size);
            }
            return size;
        }

        public Rect GetSystemMargins()
        {
            var withMargin = GetSizeWithMargins();
            return new Rect
            {
                Left = withMargin.Left - Size.Left,
                Top = withMargin.Top - Size.Top,
                Right = Size.Right - withMargin.Right,
                Bottom = Size.Bottom - withMargin.Bottom
            };
        }
    }
}