using System;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
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
        private bool _isLayered;
        private bool _hasThickFrame;
        private NotifyIcon _systemTrayIcon;
        private ToolStripMenuItem _menuItemRestore;
        private ToolStripMenuItem _menuItemClose;
        private ContextMenuStrip _systemTrayMenu;

        public const string ConsoleClassName = "ConsoleWindowClass";

        public IntPtr Handle { get; }

        public SystemMenu Menu { get; }

        public WindowState State { get; }

        public Rect Size
        {
            get
            {
                GetWindowRect(Handle, out var size);
                return size;
            }
        }

        public Rect ClientSize
        {
            get
            {
                GetClientRect(Handle, out var size);
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
                GetWindowThreadProcessId(Handle, out var processId);
                return processId;
            }
        }

        public Process Process => SystemUtils.GetProcessByIdSafely(ProcessId);

        public Priority ProcessPriority => Process.GetPriority();

        public bool IsVisible => IsWindowVisible(Handle);

        public bool IsHidden { get; private set; }

        public int Transparency
        {
            get
            {
                int style = GetWindowLong(Handle, GWL_EXSTYLE);
                bool isLayeredWindow = (style & WS_EX_LAYERED) == WS_EX_LAYERED;
                if (!isLayeredWindow) return 0;
                GetLayeredWindowAttributes(Handle, out var _, out var alpha, out var _);
                return WindowUtils.AlphaOpacityToTransparency(alpha);
            }
        }

        public bool AlwaysOnTop => WindowUtils.IsAlwaysOnTop(Handle);

        public bool IsDisabledMinimizeButton => WindowUtils.IsDisabledMinimizeButton(Handle);

        public bool IsDisabledMaximizeButton => WindowUtils.IsDisabledMaximizeButton(Handle);

        public bool IsDisabledCloseButton
        {
            get
            {
                var flags = GetMenuState(Menu.MenuHandle, MenuItemId.SC_CLOSE, MF_BYCOMMAND);
                return flags != -1 && (flags & MF_DISABLED) != 0 && (flags & MF_GRAYED) != 0;
            }
        }

        public bool IsExToolWindow => WindowUtils.IsExToolWindow(Handle);

        public bool IsClickThrough => WindowUtils.IsClickThrough(Handle);

        public IntPtr Owner => GetWindow(Handle, GW_OWNER);
        
        public bool ExistSystemTrayIcon => _systemTrayIcon != null && _systemTrayIcon.Visible;

        public IWin32Window Win32Window => new Win32Window(Handle);

        public bool NoRestoreMenu { get; set; }

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
            _isLayered = false;
            _hasThickFrame = false;
            _defaultTransparency = Transparency;
            if (_defaultTransparency == 100)
            {
                _defaultTransparency = 0;
            }

            State = new WindowState();
            State.Left = size.Left;
            State.Top = size.Top;
            State.Width = size.Width;
            State.Height = size.Height;
            State.ClassName = GetClassName();
            State.ProcessName = Process?.GetMainModuleFileName() ?? string.Empty;
        }

        public Window(IntPtr windowHandle, MenuItems menuItems, LanguageSettings languageSettings) : this(windowHandle)
        {
            _menuItemRestore = new ToolStripMenuItem();
            _menuItemRestore.Size = new Size(175, 22);
            _menuItemRestore.Name = $"miRestore_{Handle}";
            _menuItemRestore.Text = languageSettings.GetValue("mi_restore");
            _menuItemRestore.Click += MenuItemRestoreClick;
            _menuItemClose = new ToolStripMenuItem();
            _menuItemClose.Size = new Size(175, 22);
            _menuItemClose.Name = $"miClose_{Handle}";
            _menuItemClose.Text = languageSettings.GetValue("mi_close");
            _menuItemClose.Click += MenuItemCloseClick;
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
                if (IsHidden)
                {
                    Show();
                }

                RestoreFromSystemTray();
                Menu?.Destroy(!NoRestoreMenu);
                _menuItemRestore?.Dispose();
                _menuItemClose?.Dispose();
                _systemTrayMenu?.Dispose();
            }
            _isManaged = false;
        }

        public override string ToString() => WindowUtils.GetWindowText(Handle);

        public void SetWindowText(string text) => WindowUtils.SetWindowText(Handle, text);

        public string GetWindowText() => WindowUtils.GetWindowText(Handle);

        public string GetClassName() => WindowUtils.GetClassName(Handle);

        private string RealGetWindowClass() => WindowUtils.RealGetWindowClass(Handle);

        public WindowDetails GetWindowInfo()
        {
            var process = Process;
            var info = new WindowDetails();
            info.GetWindowText = GetWindowText();
            info.WM_GETTEXT = WindowUtils.GetWmGettext(Handle);
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
            info.Priority = ProcessPriority;

            try
            {
                info.FullPath = process?.GetMainModuleFileName() ?? string.Empty;
                info.StartTime = process?.StartTime;
            }
            catch
            {
            }

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
                info.FontName = WindowUtils.GetFontName(Handle);
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
                var result = GetLayeredWindowAttributes(Handle, out var _, out var _, out var flags);
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
            var opacity = WindowUtils.TransparencyToAlphaOpacity(percent);
            WindowUtils.SetOpacity(Handle, opacity);
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

        public void SetSize(int? width, int? height, int? left = null, int? top = null)
        {
            var size = Size;
            var sizeLeft = left == null ? size.Left : left.Value;
            var sizeTop = top == null ? size.Top : top.Value;
            var sizeWidth = width == null ? size.Width : width.Value;
            var sizeHeight = height == null ? size.Height : height.Value;
            State.Left = sizeLeft;
            State.Top = sizeTop;
            State.Width = sizeWidth;
            State.Height = sizeHeight;
            MoveWindow(Handle, sizeLeft, sizeTop, sizeWidth, sizeHeight, true);
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

        public void Show()
        {
            IsHidden = false;
            ShowWindow(Handle, (int)WindowShowStyle.Show);
        }

        public void Hide()
        {
            IsHidden = true;
            ShowWindow(Handle, (int)WindowShowStyle.Hide);
        }

        public void MakeTopMost(bool topMost)
        {
            SetWindowPos(Handle, topMost ? HWND_TOPMOST : HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE);
            State.AlwaysOnTop = topMost;
        }

        public void DisableMinimizeButton(bool disable)
        {
            WindowUtils.DisableMinimizeButton(Handle, disable);
            State.IsDisabledMinimizeButton = disable;
        }

        public void DisableMaximizeButton(bool disable)
        {
            WindowUtils.DisableMaximizeButton(Handle, disable);
            State.IsDisabledMaximizeButton = disable;
        }

        public void DisableCloseButton(bool disable)
        {
            var systemMenuHandle = GetSystemMenu(Handle, false);
            EnableMenuItem(systemMenuHandle, MenuItemId.SC_CLOSE, disable ? MF_BYCOMMAND | MF_DISABLED | MF_GRAYED : MF_BYCOMMAND | MF_ENABLED);
            State.IsDisabledCloseButton = disable;
        }

        public void HideForAltTab(bool enable)
        {
            if (enable)
            {
                WindowUtils.SetExToolWindow(Handle);
            }
            else
            {
                WindowUtils.UnsetExToolWindow(Handle);
            }
            State.HideForAltTab = enable;
        }

        public void ClickThrough(bool enable)
        {
            if (enable)
            {
                _isLayered = WindowUtils.IsLayered(Handle);
                WindowUtils.SetClickThrough(Handle);
            }
            else
            {
                if (_isLayered)
                {
                    WindowUtils.UnsetTransparent(Handle);
                }
                else
                {
                    WindowUtils.UnsetClickThrough(Handle);
                }
            }
        }

        public void MakeResizable(bool enable)
        {
            if (enable)
            {
                _hasThickFrame = WindowUtils.HasThickFrame(Handle);
                if (!_hasThickFrame)
                {
                    WindowUtils.SetThickFrame(Handle);
                }
            }
            else
            {
                if (!_hasThickFrame)
                {
                    WindowUtils.UnsetThickFrame(Handle);
                }
            }
            State.Resizable = enable;
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
            text ??= WindowUtils.ExtractTextFromWindow(Handle);
            return text;
        }

        public void AeroGlass(bool enable)
        {
            var version = Environment.OSVersion.Version;
            if (version.Major == 6 && (version.Minor == 0 || version.Minor == 1))
            {
                WindowUtils.AeroGlassForVistaAndSeven(Handle, enable);
                State.AeroGlass = enable;
            }
            else if (version.Major >= 6 || (version.Major == 6 && version.Minor > 1))
            {
                WindowUtils.AeroGlassForEightAndHigher(Handle, enable);
                State.AeroGlass = enable;
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

        public void CheckDefaultMenuItems()
        {
            var menuItemId = ProcessPriority.GetMenuItemId();
            Menu.CheckMenuItem(menuItemId, true);
            if (AlwaysOnTop)
            {
                Menu.CheckMenuItem(MenuItemId.SC_TOPMOST, true);
            }

            if (IsExToolWindow)
            {
                Menu.CheckMenuItem(MenuItemId.SC_HIDE_FOR_ALT_TAB, true);
            }

            if (IsDisabledMinimizeButton)
            {
                Menu.CheckMenuItem(MenuItemId.SC_DISABLE_MINIMIZE_BUTTON, true);
            }

            if (IsDisabledMaximizeButton)
            {
                Menu.CheckMenuItem(MenuItemId.SC_DISABLE_MAXIMIZE_BUTTON, true);
            }

            if (IsDisabledCloseButton)
            {
                Menu.CheckMenuItem(MenuItemId.SC_DISABLE_CLOSE_BUTTON, true);
            }
        }

        public void ApplyState(WindowState state, SaveSelectedItemsSettings settings, IList<WindowSizeMenuItem> sizeItems)
        {
            if (state.Width > 0 && state.Height > 0)
            {
                SetSize(state.Width, state.Height, state.Left, state.Top);
            }

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

            if (settings.HideForAltTab && state.HideForAltTab.HasValue)
            {
                HideForAltTab(state.HideForAltTab.Value);
                Menu.CheckMenuItem(MenuItemId.SC_HIDE_FOR_ALT_TAB, state.HideForAltTab.Value);
            }

            if (settings.Resizable && state.Resizable.HasValue)
            {
                MakeResizable(state.Resizable.Value);
                Menu.CheckMenuItem(MenuItemId.SC_RESIZABLE, state.Resizable.Value);
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

            if (settings.Buttons)
            {
                if (state.IsDisabledMinimizeButton.HasValue)
                {
                    DisableMinimizeButton(state.IsDisabledMinimizeButton.Value);
                    Menu.CheckMenuItem(MenuItemId.SC_DISABLE_MINIMIZE_BUTTON, state.IsDisabledMinimizeButton.Value);
                }

                if (state.IsDisabledMaximizeButton.HasValue)
                {
                    DisableMaximizeButton(state.IsDisabledMaximizeButton.Value);
                    Menu.CheckMenuItem(MenuItemId.SC_DISABLE_MAXIMIZE_BUTTON, state.IsDisabledMaximizeButton.Value);
                }

                if (state.IsDisabledCloseButton.HasValue)
                {
                    DisableCloseButton(state.IsDisabledCloseButton.Value);
                    Menu.CheckMenuItem(MenuItemId.SC_DISABLE_CLOSE_BUTTON, state.IsDisabledCloseButton.Value);
                }
            }
        }

        public void RefreshState()
        {
            var size = Size;
            State.Left = size.Left;
            State.Top = size.Top;
            State.Width = size.Width;
            State.Height = size.Height;
            State.ProcessName = Process?.GetMainModuleFileName() ?? string.Empty;
            var alwaysOnTop = AlwaysOnTop;
            if (alwaysOnTop)
            {
                State.AlwaysOnTop = alwaysOnTop;
            }

            var isExToolWindow = IsExToolWindow;
            if (isExToolWindow)
            {
                State.HideForAltTab = isExToolWindow;
            }

            var isDisabledMinimizeButton = IsDisabledMinimizeButton;
            if (isDisabledMinimizeButton)
            {
                State.IsDisabledMinimizeButton = isDisabledMinimizeButton;
            }

            var isDisabledMaximizeButton = IsDisabledMaximizeButton;
            if (isDisabledMaximizeButton)
            {
                State.IsDisabledMaximizeButton = isDisabledMaximizeButton;
            }

            var isDisabledCloseButton = IsDisabledCloseButton;
            if (isDisabledCloseButton)
            {
                State.IsDisabledCloseButton = isDisabledCloseButton;
            }
        }

        public void ChangeIcon(string fileName)
        {
            using var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using var bitmap = new Bitmap(fileStream);
            var iconHandle = bitmap.GetHicon();
            if (iconHandle != IntPtr.Zero)
            {
                SendMessage(Handle, WM_SETICON, new IntPtr(ICON_SMALL), iconHandle);
                SendMessage(Handle, WM_SETICON, new IntPtr(ICON_BIG), iconHandle);
            }
        }

        private void MenuItemRestoreClick(object sender, EventArgs e)
        {
            RestoreFromSystemTray();
        }

        private void MenuItemCloseClick(object sender, EventArgs e)
        {
            RestoreFromSystemTray();
            PostMessage(Handle, WM_CLOSE, 0, 0);
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

        private void CreateIconInSystemTray()
        {
            _systemTrayMenu ??= CreateSystemTrayMenu();
            _systemTrayIcon ??= CreateNotifyIcon(_systemTrayMenu);
            _systemTrayIcon.Icon = WindowUtils.GetIcon(Handle);
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

        public Rect GetSystemMargins() => WindowUtils.GetSystemMargins(Handle);

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