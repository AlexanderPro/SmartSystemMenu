using System;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Automation;
using System.IO;
using System.Threading;
using System.ComponentModel;
using SmartSystemMenu.Native;
using SmartSystemMenu.Native.Enums;
using SmartSystemMenu.Native.Structs;
using SmartSystemMenu.Settings;
using SmartSystemMenu.Extensions;
using SmartSystemMenu.Utils;
using static SmartSystemMenu.Native.User32;
using static SmartSystemMenu.Native.Constants;

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
        private ToolStripMenuItem _menuItemRestore;
        private ToolStripMenuItem _menuItemClose;
        private ContextMenuStrip _systemTrayMenu;

        public const string ConsoleClassName = "ConsoleWindowClass";

        public IntPtr Handle { get; private set; }

        public SystemMenu Menu { get; private set; }

        public WindowState State { get; private set; }

        public Rect Size
        {
            get
            {
                Rect size;
                GetWindowRect(Handle, out size);
                return size;
            }
        }

        public Rect ClientSize
        {
            get
            {
                Rect size;
                GetClientRect(Handle, out size);
                return size;
            }
        }

        public Rect SizeOnMonitor
        {
            get
            {
                var monitorHandle = MonitorFromWindow(Handle, MONITOR_DEFAULTTONEAREST);
                var monitorInfo = new MonitorInfo();
                monitorInfo.Init();
                GetMonitorInfo(monitorHandle, ref monitorInfo);

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
                GetWindowThreadProcessId(Handle, out processId);
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

                var priorityClass = Kernel32.GetPriorityClass(process.GetHandle());
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
                bool isVisible = IsWindowVisible(Handle);
                return isVisible;
            }
        }

        public int Transparency
        {
            get
            {
                int style = GetWindowLong(Handle, GWL_EXSTYLE);
                bool isLayeredWindow = (style & WS_EX_LAYERED) == WS_EX_LAYERED;
                if (!isLayeredWindow) return 0;
                uint key;
                Byte alpha;
                uint flags;
                GetLayeredWindowAttributes(Handle, out key, out alpha, out flags);
                int transparency = 100 - (int)Math.Round(100 * alpha / 255f, MidpointRounding.AwayFromZero);
                return transparency;
            }
        }

        public bool AlwaysOnTop
        {
            get
            {
                int style = GetWindowLong(Handle, GWL_EXSTYLE);
                bool isAlwaysOnTop = (style & WS_EX_TOPMOST) == WS_EX_TOPMOST;
                return isAlwaysOnTop;
            }
        }

        public IntPtr Owner
        {
            get
            {
                IntPtr owner = GetWindow(Handle, GW_OWNER);
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
            var size = Size;
            _defaultWidth = size.Width;
            _defaultHeight = size.Height;
            _defaultLeft = size.Left;
            _defaultTop = size.Top;
            _beforeRollupHeight = size.Height;
            _defaultTransparency = Transparency;
        }

        public Window(IntPtr windowHandle, MenuItems menuItems, LanguageSettings languageSettings)
        {
            Handle = windowHandle;
            _isManaged = true;
            var size = Size;
            _defaultWidth = size.Width;
            _defaultHeight = size.Height;
            _defaultLeft = size.Left;
            _defaultTop = size.Top;
            _beforeRollupHeight = size.Height;
            _defaultTransparency = Transparency;
            State = new WindowState();
            State.Left = size.Left;
            State.Top = size.Top;
            State.Width = size.Width;
            State.Height = size.Height;
            State.ClassName = GetClassName();
            State.ProcessName = Process?.GetMainModuleFileName() ?? string.Empty;
            _menuItemRestore = new ToolStripMenuItem();
            _menuItemRestore.Size = new Size(175, 22);
            _menuItemRestore.Name = $"miRestore_{Handle}";
            _menuItemRestore.Text = languageSettings.GetValue("mi_restore");
            _menuItemRestore.Click += _menuItemRestore_Click;
            _menuItemClose = new ToolStripMenuItem();
            _menuItemClose.Size = new Size(175, 22);
            _menuItemClose.Name = $"miClose_{Handle}";
            _menuItemClose.Text = languageSettings.GetValue("mi_close");
            _menuItemClose.Click += _menuItemClose_Click;
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
                RestoreFromSystemTray();
                Menu?.Destroy();
                _menuItemRestore?.Dispose();
                _menuItemClose?.Dispose();
                _systemTrayMenu?.Dispose();
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
            User32.GetWindowText(Handle, builder, builder.Capacity);
            var windowText = builder.ToString();
            return windowText;
        }

        public string GetClassName()
        {
            var builder = new StringBuilder(1024);
            User32.GetClassName(Handle, builder, builder.Capacity);
            var className = builder.ToString();
            return className;
        }

        private string RealGetWindowClass()
        {
            var builder = new StringBuilder(1024);
            User32.RealGetWindowClass(Handle, builder, builder.Capacity);
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
            info.ParentHandle = GetParent(Handle);
            info.Size = Size;
            info.ClientSize = ClientSize;
            info.FrameBounds = GetSystemMargins();
            info.ProcessId = ProcessId;
            info.ThreadId = WindowUtils.GetThreadId(Handle);
            info.GWL_STYLE = GetWindowLong(Handle, GWL_STYLE);
            info.GWL_EXSTYLE = GetWindowLong(Handle, GWL_EXSTYLE);
            info.GWL_ID = GetWindowLong(Handle, GWL_ID);
            info.GWL_USERDATA = GetWindowLong(Handle, GWL_USERDATA);
            info.GCL_STYLE = GetClassLong(Handle, GCL_STYLE);
            info.GCL_WNDPROC = GetClassLong(Handle, GCL_WNDPROC);
            info.DWL_DLGPROC = GetClassLong(Handle, DWL_DLGPROC);
            info.DWL_USER = GetClassLong(Handle, DWL_USER);
            info.FullPath = process?.GetMainModuleFileName() ?? "";
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
                if (User32.GetWindowInfo(Handle, ref windowInfo))
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
                var result = GetLayeredWindowAttributes(Handle, out key, out alpha, out flags);
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
            State.Transparency = percent;
        }

        public void RestoreTransparency()
        {
            SetTransparency(_defaultTransparency);
            State.Transparency = null;
        }

        public void SetWidth(int width)
        {
            var size = Size;
            MoveWindow(Handle, size.Left, size.Top, width, size.Height, true);
        }

        public void SetHeight(int height)
        {
            var size = Size;
            MoveWindow(Handle, size.Left, size.Top, size.Width, height, true);
        }

        public void SetSize(int width, int height, int? left = null, int? top = null)
        {
            var size = Size;
            var sizeLeft = left == null ? size.Left : left.Value;
            var sizeTop = top == null ? Size.Top : top.Value;
            State.Left = sizeLeft;
            State.Top = sizeTop;
            State.Width = width;
            State.Height = height;
            MoveWindow(Handle, sizeLeft, sizeTop, width, height, true);
        }

        public void RestoreSize()
        {
            State.Left = _defaultLeft;
            State.Top = _defaultTop;
            State.Width = _defaultWidth;
            State.Height = _defaultHeight;
            MoveWindow(Handle, _defaultLeft, _defaultTop, _defaultWidth, _defaultHeight, true);
        }

        public void SetLeft(int left)
        {
            var size = Size;
            State.Left = left;
            State.Top = size.Top;
            State.Width = size.Width;
            State.Height = size.Height;
            MoveWindow(Handle, left, size.Top, size.Width, size.Height, true);
        }

        public void SetTop(int top)
        {
            var size = Size;
            State.Left = size.Left;
            State.Top = top;
            State.Width = size.Width;
            State.Height = size.Height;
            MoveWindow(Handle, size.Left, top, size.Width, size.Height, true);
        }

        public void SetPosition(int left, int top)
        {
            var size = Size;
            State.Left = left;
            State.Top = top;
            State.Width = size.Width;
            State.Height = size.Height;
            MoveWindow(Handle, left, top, size.Width, size.Height, true);
        }

        public void RestorePosition()
        {
            var size = Size;
            State.Left = _defaultLeft;
            State.Top = _defaultTop;
            State.Width = size.Width;
            State.Height = size.Height;
            MoveWindow(Handle, _defaultLeft, _defaultTop, size.Width, size.Height, true);
        }

        public void SaveDefaultSizePosition()
        {
            var size = Size;
            _defaultLeft = size.Left;
            _defaultTop = size.Top;
            _defaultWidth = size.Width;
            _defaultHeight = size.Height;
        }

        public void SetAlignment(WindowAlignment alignment)
        {
            var x = 0;
            var y = 0;
            var screen = Screen.FromHandle(Handle).WorkingArea;
            var window = Size;

            State.Alignment = alignment;
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
            var handleTopMost = (IntPtr)(-1);
            var handleNotTopMost = (IntPtr)(-2);
            SetWindowPos(Handle, topMost ? handleTopMost : handleNotTopMost, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE);
            State.AlwaysOnTop = topMost;
        }

        public void SendToBottom()
        {
            SetWindowPos(Handle, new IntPtr(1), 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE);
        }

        public void MinimizeToSystemTray()
        {
            CreateIconInSystemTray();
            ShowWindowAsync(Handle, (int)WindowShowStyle.Minimize);
            ShowWindowAsync(Handle, (int)WindowShowStyle.Hide);
        }

        public void MoveToSystemTray()
        {
            CreateIconInSystemTray();
            ShowWindowAsync(Handle, (int)WindowShowStyle.Hide);
        }

        public void ShowNormal()
        {
            ShowWindow(Handle, (int)WindowShowStyle.Normal);
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
                Kernel32.SetPriorityClass(process.GetHandle(), priority.GetPriorityClass());
            }
            State.Priority = priority;
        }

        public string ExtractText()
        {
            var text = WindowUtils.ExtractTextFromConsoleWindow(ProcessId);
            text = text ?? ExtractTextFromWindow();
            return text;
        }

        public void AeroGlass(bool enable)
        {
            var version = Environment.OSVersion.Version;
            if (version.Major == 6 && (version.Minor == 0 || version.Minor == 1))
            {
                AeroGlassForVistaAndSeven(enable);
            }
            else if (version.Major >= 6 || (version.Major == 6 && version.Minor > 1))
            {
                AeroGlassForEightAndHigher(enable);
            }
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
            Dwmapi.DwmEnableBlurBehindWindow(Handle, ref blurBehind);
            State.AeroGlass = enable;
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
                SetWindowCompositionAttribute(Handle, ref data);
                State.AeroGlass = enable;
            }
            finally
            {
                Marshal.FreeHGlobal(accentPtr);
            }
        }

        public void MoveToMonitor(IntPtr monitorHandle)
        {
            var currentMonitorHandle = MonitorFromWindow(Handle, MONITOR_DEFAULTTONEAREST);
            if (currentMonitorHandle != monitorHandle)
            {
                var currentMonitorInfo = new MonitorInfo();
                currentMonitorInfo.Init();
                GetMonitorInfo(currentMonitorHandle, ref currentMonitorInfo);

                var newMonitorInfo = new MonitorInfo();
                newMonitorInfo.Init();
                GetMonitorInfo(monitorHandle, ref newMonitorInfo);
                GetWindowRect(Handle, out Rect windowRect);

                var left = newMonitorInfo.rcWork.Left + windowRect.Left - currentMonitorInfo.rcWork.Left;
                var top = newMonitorInfo.rcWork.Top + windowRect.Top - currentMonitorInfo.rcWork.Top;
                if (windowRect.Left - currentMonitorInfo.rcWork.Left > newMonitorInfo.rcWork.Width || windowRect.Top - currentMonitorInfo.rcWork.Top > newMonitorInfo.rcWork.Height)
                {
                    left = newMonitorInfo.rcWork.Left;
                    top = newMonitorInfo.rcWork.Top;
                }

                MoveWindow(Handle, left, top, windowRect.Width, windowRect.Height, true);
                Thread.Sleep(10);
                MoveWindow(Handle, left, top, windowRect.Width, windowRect.Height, true);

                State.Left = left;
                State.Top = top;
                State.Width = windowRect.Width;
                State.Height = windowRect.Height;
            }
        }

        public void SetStateMinimizeToTrayAlways(bool minimizeAlways)
        {
            State.MinimizeToTrayAlways = minimizeAlways;
        }

        public void ApplyState(WindowState state, SaveSelectedItemsSettings settings, IList<WindowSizeMenuItem> sizeItems)
        {
            SetSize(state.Width, state.Height, state.Left, state.Top);

            var sizeItem = sizeItems.FirstOrDefault(x => x.Width == state.Width && x.Height == state.Height);
            if (sizeItem != null)
            {
                Menu.CheckMenuItem(sizeItem.Id, true);
            }

            if (settings.AeroGlass && state.AeroGlass.HasValue)
            {
                AeroGlass(state.AeroGlass.Value);
                Menu.CheckMenuItem(MenuItemId.SC_AERO_GLASS, state.AeroGlass.Value);
            }

            if (settings.AlwaysOnTop && state.AlwaysOnTop.HasValue)
            {
                MakeTopMost(state.AlwaysOnTop.Value);
                Menu.CheckMenuItem(MenuItemId.SC_TOPMOST, state.AlwaysOnTop.Value);
            }

            if (settings.Alignment && state.Alignment.HasValue)
            {
                SetAlignment(state.Alignment.Value);
                Menu.CheckMenuItem(state.Alignment.Value.GetMenuItemId(), true);
            }

            if (settings.Transparency && state.Transparency.HasValue)
            {
                SetTransparency(state.Transparency.Value);
                var menuItemId = EnumUtils.GetTransparencyMenuItemId(state.Transparency.Value);
                if (menuItemId.HasValue)
                {
                    Menu.CheckMenuItem(menuItemId.Value, true);
                }
            }

            if (settings.Priority && state.Priority.HasValue)
            {
                SetPriority(state.Priority.Value);
                Menu.UncheckPriorityMenu();
                Menu.CheckMenuItem(state.Priority.Value.GetMenuItemId(), true);
            }

            if (settings.MinimizeToTrayAlways && state.MinimizeToTrayAlways.HasValue)
            {
                SetStateMinimizeToTrayAlways(state.MinimizeToTrayAlways.Value);
                Menu.CheckMenuItem(MenuItemId.SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY, state.MinimizeToTrayAlways.Value);
            }
        }

        public static void ForceForegroundWindow(IntPtr handle)
        {
            var foreHandle = GetForegroundWindow();
            var foreThread = GetWindowThreadProcessId(foreHandle, IntPtr.Zero);
            var appThread = Kernel32.GetCurrentThreadId();
            if (foreThread != appThread)
            {
                AttachThreadInput(foreThread, appThread, true);
                BringWindowToTop(handle);
                ShowWindow(handle, (int)WindowShowStyle.Show);
                AttachThreadInput(foreThread, appThread, false);
            }
            else
            {
                BringWindowToTop(handle);
                ShowWindow(handle, (int)WindowShowStyle.Show);
            }
        }

        public static void ForceAllMessageLoopsToWakeUp()
        {
            uint result;
            SendMessageTimeout((IntPtr)HWND_BROADCAST, WM_NULL, 0, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG | SendMessageTimeoutFlags.SMTO_NOTIMEOUTIFNOTHUNG, 1000, out result);
        }

        public void RefreshProcessNameState()
        {
            State.ProcessName = Process?.GetMainModuleFileName() ?? string.Empty;
        }

        private void _menuItemRestore_Click(object sender, EventArgs e)
        {
            RestoreFromSystemTray();
        }

        private void _menuItemClose_Click(object sender, EventArgs e)
        {
            RestoreFromSystemTray();
            PostMessage(Handle, WM_CLOSE, 0, 0);
        }

        private string GetFontName()
        {
            var hFont = SendMessage(Handle, WM_GETFONT, 0, 0);
            if (hFont == IntPtr.Zero)
            {
                return "Default system font";
            }
            var font = Font.FromHfont(hFont);
            return font.Name;
        }

        private string GetWmGettext()
        {
            var titleSize = SendMessage(Handle, WM_GETTEXTLENGTH, 0, 0);
            if (titleSize.ToInt32() == 0)
            {
                return string.Empty;
            }

            var title = new StringBuilder(titleSize.ToInt32() + 1);
            SendMessage(Handle, WM_GETTEXT, title.Capacity, title);
            return title.ToString();
        }

        private void SetOpacity(IntPtr handle, byte opacity)
        {
            SetWindowLong(handle, GWL_EXSTYLE, GetWindowLong(handle, GWL_EXSTYLE) | WS_EX_LAYERED);
            SetLayeredWindowAttributes(handle, 0, opacity, LWA_ALPHA);
        }

        private void RestoreFromSystemTray()
        {
            if (_systemTrayIcon != null && _systemTrayIcon.Visible)
            {
                if (_suspended)
                {
                    Resume();
                    Thread.Sleep(100);
                }

                _systemTrayIcon.Visible = false;
                /*_systemTrayMenu?.Dispose();
                _systemTrayIcon?.Dispose();
                _systemTrayMenu = null;
                _systemTrayIcon = null;*/

                ShowWindowAsync(Handle, (int)WindowShowStyle.Show);
                ShowWindowAsync(Handle, (int)WindowShowStyle.Restore);
                SetForegroundWindow(Handle);
            }
        }

        private Icon GetWindowIcon()
        {
            IntPtr icon;
            try
            {
                uint result;
                SendMessageTimeout(Handle, WM_GETICON, ICON_SMALL2, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 100, out result);
                icon = new IntPtr(result);

                if (icon == IntPtr.Zero)
                {
                    SendMessageTimeout(Handle, WM_GETICON, ICON_SMALL, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 100, out result);
                    icon = new IntPtr(result);
                }

                if (icon == IntPtr.Zero)
                {
                    SendMessageTimeout(Handle, WM_GETICON, ICON_BIG, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 100, out result);
                    icon = new IntPtr(result);
                }

                if (icon == IntPtr.Zero)
                {
                    icon = GetClassLongPtr(Handle, GCLP_HICONSM);
                }

                if (icon == IntPtr.Zero)
                {
                    icon = GetClassLongPtr(Handle, GCLP_HICON);
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

        private void CreateIconInSystemTray()
        {
            _systemTrayMenu = _systemTrayMenu ?? CreateSystemTrayMenu();
            _systemTrayIcon = _systemTrayIcon ?? CreateNotifyIcon(_systemTrayMenu);
            _systemTrayIcon.Icon = GetWindowIcon();
            var windowText = GetWindowText();
            _systemTrayIcon.Text = windowText.Length > 63 ? windowText.Substring(0, 60).PadRight(63, '.') : windowText;
            _systemTrayIcon.Visible = true;
        }

        private void SystemTrayIconClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                RestoreFromSystemTray();
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
                GetWindowRect(Handle, out size);
            }
            else if (Dwmapi.DwmGetWindowAttribute(Handle, DWMWA_EXTENDED_FRAME_BOUNDS, out size, Marshal.SizeOf(typeof(Rect))) != 0)
            {
                GetWindowRect(Handle, out size);
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

        private ContextMenuStrip CreateSystemTrayMenu()
        {
            var components = new Container();
            var menu = new ContextMenuStrip(components);
            menu.Items.AddRange(new ToolStripItem[] { _menuItemRestore, _menuItemClose });
            menu.Name = $"systemTrayMenu_{Handle}";
            menu.Size = new Size(176, 80);
            return menu;
        }

        private NotifyIcon CreateNotifyIcon(ContextMenuStrip contextMenuStrip)
        {
            var icon = new NotifyIcon();
            icon.ContextMenuStrip = contextMenuStrip;
            icon.MouseClick += SystemTrayIconClick;
            return icon;
        }
    }
}