using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Drawing.Imaging;
using System.Text;
using System.Threading;
using SmartSystemMenu.Native;
using SmartSystemMenu.Extensions;
using SmartSystemMenu.Utils;
using SmartSystemMenu.Hooks;
using SmartSystemMenu.HotKeys;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    partial class MainForm : Form
    {
        private const string SHELL_WINDOW_NAME = "Program Manager";
        private List<Window> _windows;
        private GetMsgHook _getMsgHook;
        private ShellHook _shellHook;
        private CBTHook _cbtHook;
        private Hooks.MouseHook _mouseHook;
        private AboutForm _aboutForm;
        private SettingsForm _settingsForm;
        private SmartSystemMenuSettings _settings;

#if WIN32
        private SystemTrayMenu _systemTrayMenu;
        private HotKeyHook _hotKeyHook;
        private HotKeys.MouseHook _hotKeyMouseHook;
        private Process _64BitProcess;
#endif

        public MainForm()
        {
            InitializeComponent();

            _settings = new SmartSystemMenuSettings();
            AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;
            Application.ThreadException += OnThreadException;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var settingsFileName = Path.Combine(AssemblyUtils.AssemblyDirectory, "SmartSystemMenu.xml");
            var languageFileName = Path.Combine(AssemblyUtils.AssemblyDirectory, "Language.xml");
            if (File.Exists(settingsFileName) && File.Exists(languageFileName))
            {
                _settings = SmartSystemMenuSettings.Read(settingsFileName, languageFileName);
            }
#if WIN32
            if (Environment.Is64BitOperatingSystem)
            {
                string resourceName = "SmartSystemMenu.SmartSystemMenu64.exe";
                string fileName = "SmartSystemMenu64.exe";
                string directoryName = Path.GetDirectoryName(AssemblyUtils.AssemblyLocation);
                string filePath = Path.Combine(directoryName, fileName);
                try
                {
                    if (!File.Exists(filePath))
                    {
                        AssemblyUtils.ExtractFileFromAssembly(resourceName, filePath);
                    }
                    _64BitProcess = Process.Start(filePath);
                }
                catch
                {
                    string message = string.Format("Failed to load {0} process!", fileName);
                    MessageBox.Show(message, AssemblyUtils.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }
            }

            if (_settings.ShowSystemTrayIcon)
            {
                _systemTrayMenu = new SystemTrayMenu(_settings.LanguageSettings);
                _systemTrayMenu.MenuItemAutoStartClick += MenuItemAutoStartClick;
                _systemTrayMenu.MenuItemSettingsClick += MenuItemSettingsClick;
                _systemTrayMenu.MenuItemAboutClick += MenuItemAboutClick;
                _systemTrayMenu.MenuItemExitClick += MenuItemExitClick;
                _systemTrayMenu.Create();
                _systemTrayMenu.CheckMenuItemAutoStart(AutoStarter.IsAutoStartByRegisterEnabled(AssemblyUtils.AssemblyProductName, AssemblyUtils.AssemblyLocation));
            }

            var moduleName = Process.GetCurrentProcess().MainModule.ModuleName;

            _hotKeyHook = new HotKeyHook();
            _hotKeyHook.Hooked += HotKeyHooked;
            if (_settings.MenuItems.Items.Flatten(x => x.Items).Any(x => x.Type == MenuItemType.Item && x.Key3 != VirtualKey.None && x.Show) || _settings.MenuItems.WindowSizeItems.Any(x => x.Key3 != VirtualKey.None))
            {
                _hotKeyHook.Start(moduleName, _settings.MenuItems);
            }

            _hotKeyMouseHook = new HotKeys.MouseHook();
            _hotKeyMouseHook.Hooked += HotKeyMouseHooked;
            if (_settings.Closer.MouseButton != MouseButton.None)
            {
                _hotKeyMouseHook.Start(moduleName, _settings.Closer.Key1, _settings.Closer.Key2, _settings.Closer.MouseButton);
            }

#endif
            _windows = EnumWindows.EnumAllWindows(_settings.MenuItems, _settings.LanguageSettings, new string[] { SHELL_WINDOW_NAME }).ToList();

            foreach (var window in _windows)
            {
                var processName = "";

                try
                {
                    processName = Path.GetFileName(window.Process.GetMainModuleFileName());
                }
                catch
                {
                }

                if (string.IsNullOrEmpty(processName) || _settings.ProcessExclusions.Contains(processName.ToLower()))
                {
                    continue;
                }

                window.Menu.Create();
                int menuItemId = window.ProcessPriority.GetMenuItemId();
                window.Menu.CheckMenuItem(menuItemId, true);
                if (window.AlwaysOnTop) window.Menu.CheckMenuItem(MenuItemId.SC_TOPMOST, true);
            }

            _getMsgHook = new GetMsgHook(Handle, MenuItemId.SC_DRAG_BY_MOUSE);
            _getMsgHook.GetMsg += WindowGetMsg;
            _getMsgHook.Start();

            _shellHook = new ShellHook(Handle, MenuItemId.SC_DRAG_BY_MOUSE);
            _shellHook.WindowCreated += WindowCreated;
            _shellHook.WindowDestroyed += WindowDestroyed;
            _shellHook.Start();

            _cbtHook = new CBTHook(Handle, MenuItemId.SC_DRAG_BY_MOUSE);
            _cbtHook.WindowCreated += WindowCreated;
            _cbtHook.WindowDestroyed += WindowDestroyed;
            _cbtHook.MoveSize += WindowMoveSize;
            _cbtHook.MinMax += WindowMinMax;
            _cbtHook.Start();

            _mouseHook = new Hooks.MouseHook(Handle, MenuItemId.SC_DRAG_BY_MOUSE);
            var dragByMouseItemName = MenuItemId.GetName(MenuItemId.SC_DRAG_BY_MOUSE);
            if (_settings.MenuItems.Items.Flatten(x => x.Items).Any(x => x.Type == MenuItemType.Item && x.Name == dragByMouseItemName && x.Show))
            {
                _mouseHook.Start();
            }

            Hide();
        }

        private void HotKeyMouseHooked(object sender, HotKeys.MouseEventArgs e)
        {
            if (_settings.Closer.Type == WindowCloserType.CloseForegroundWindow)
            {
                var handle = NativeMethods.GetForegroundWindow();
                NativeMethods.PostMessage(handle, NativeConstants.WM_CLOSE, 0, 0);
            }
            else if (_settings.Closer.Type == WindowCloserType.CloseWindowUnderCursor)
            {
                var handle = NativeMethods.WindowFromPoint(e.Point);
                handle = WindowUtils.GetParentWindow(handle);
                NativeMethods.PostMessage(handle, NativeConstants.WM_CLOSE, 0, 0);
            }
            else if (_settings.Closer.Type == WindowCloserType.KillProcessWithForegroundWindow)
            {
                var handle = NativeMethods.GetForegroundWindow();
                var processId = WindowUtils.GetProcessId(handle);
                SystemUtils.TerminateProcess(processId, 0);
            }
            else if (_settings.Closer.Type == WindowCloserType.KillProcessWithWindowUnderCursor)
            {
                var handle = NativeMethods.WindowFromPoint(e.Point);
                handle = WindowUtils.GetParentWindow(handle);
                var processId = WindowUtils.GetProcessId(handle);
                SystemUtils.TerminateProcess(processId, 0);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            _getMsgHook?.Stop();
            NativeMethods.SendNotifyMessage((IntPtr)NativeConstants.HWND_BROADCAST, NativeConstants.WM_NULL, 0, 0);
            _shellHook?.Stop();
            NativeMethods.SendNotifyMessage((IntPtr)NativeConstants.HWND_BROADCAST, NativeConstants.WM_NULL, 0, 0);
            _cbtHook?.Stop();
            NativeMethods.SendNotifyMessage((IntPtr)NativeConstants.HWND_BROADCAST, NativeConstants.WM_NULL, 0, 0);

            if (_windows != null)
            {
                foreach (Window window in _windows)
                {
                    window.Dispose();
                }
            }

            Window.ForceAllMessageLoopsToWakeUp();

#if WIN32
            _systemTrayMenu?.Dispose();
            _hotKeyHook?.Dispose();

            if (Environment.Is64BitOperatingSystem && _64BitProcess != null && !_64BitProcess.HasExited)
            {
                foreach (var handle in _64BitProcess.GetWindowHandles())
                {
                    NativeMethods.PostMessage(handle, NativeConstants.WM_CLOSE, 0, 0);
                }

                if (!_64BitProcess.WaitForExit(5000))
                {
                    _64BitProcess.Kill();
                }

                try
                {
                    File.Delete(_64BitProcess.StartInfo.FileName);
                }
                catch
                {
                }
            }
#endif
            base.OnClosed(e);
            NativeMethods.SendNotifyMessage((IntPtr)NativeConstants.HWND_BROADCAST, NativeConstants.WM_NULL, 0, 0);
        }

        protected override void WndProc(ref Message m)
        {
            _shellHook?.ProcessWindowMessage(ref m);
            _cbtHook?.ProcessWindowMessage(ref m);
            _getMsgHook?.ProcessWindowMessage(ref m);
            
            base.WndProc(ref m);
        }

        private void MenuItemAutoStartClick(object sender, EventArgs e)
        {
            var keyName = AssemblyUtils.AssemblyProductName;
            var assemblyLocation = AssemblyUtils.AssemblyLocation;
            var autoStartEnabled = AutoStarter.IsAutoStartByRegisterEnabled(keyName, assemblyLocation);
            if (autoStartEnabled)
            {
                AutoStarter.UnsetAutoStartByRegister(keyName);
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    AutoStarter.UnsetAutoStartByScheduler(keyName);
                }
            }
            else
            {
                AutoStarter.SetAutoStartByRegister(keyName, assemblyLocation);
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    AutoStarter.SetAutoStartByScheduler(keyName, assemblyLocation);
                }
            }
            ((ToolStripMenuItem)sender).Checked = !autoStartEnabled;
        }

        private void MenuItemAboutClick(object sender, EventArgs e)
        {
            if (_aboutForm == null || _aboutForm.IsDisposed || !_aboutForm.IsHandleCreated)
            {
                _aboutForm = new AboutForm(_settings.LanguageSettings);
            }
            _aboutForm.Show();
            _aboutForm.Activate();
        }

        private void MenuItemSettingsClick(object sender, EventArgs e)
        {
            if (_settingsForm == null || _settingsForm.IsDisposed || !_settingsForm.IsHandleCreated)
            {
                _settingsForm = new SettingsForm(_settings);
                _settingsForm.OkClick += (object s, SmartSystemMenuSettingsEventArgs ea) => { _settings = ea.Settings; };
            }

            _settingsForm.Show();
            _settingsForm.Activate();
        }

        private void MenuItemExitClick(object sender, EventArgs e)
        {
            Close();
        }

        private void WindowCreated(object sender, WindowEventArgs e)
        {
            if (e.Handle != IntPtr.Zero && new SystemMenu(e.Handle, _settings.MenuItems, _settings.LanguageSettings).Exists && !_windows.Any(w => w.Handle == e.Handle))
            {
                NativeMethods.GetWindowThreadProcessId(e.Handle, out int processId);
                IList<Window> windows = new List<Window>();
                try
                {
                    windows = EnumWindows.EnumProcessWindows(processId, _windows.Select(w => w.Handle).ToArray(), _settings.MenuItems, _settings.LanguageSettings, new string[] { SHELL_WINDOW_NAME });
                }
                catch
                {
                }

                foreach (var window in windows)
                {
                    var processName = "";

                    try
                    {
                        processName = Path.GetFileName(window.Process.GetMainModuleFileName());
                    }
                    catch
                    {
                    }

                    if (string.IsNullOrEmpty(processName) || _settings.ProcessExclusions.Contains(processName.ToLower()))
                    {
                        continue;
                    }

                    window.Menu.Create();
                    int menuItemId = window.ProcessPriority.GetMenuItemId();
                    window.Menu.CheckMenuItem(menuItemId, true);
                    if (window.AlwaysOnTop) window.Menu.CheckMenuItem(MenuItemId.SC_TOPMOST, true);
                    _windows.Add(window);
                }
            }
        }

        private void WindowDestroyed(object sender, WindowEventArgs e)
        {
            int windowIndex = _windows.FindIndex(w => w.Handle == e.Handle);
            if (windowIndex != -1 && !_windows[windowIndex].ExistSystemTrayIcon)
            {
                _windows[windowIndex].Dispose();
                _windows.RemoveAt(windowIndex);
            }
        }

        private void WindowMinMax(object sender, SysCommandEventArgs e)
        {
            var window = _windows.FirstOrDefault(w => w.Handle == e.WParam);
            if (window != null)
            {
                if (e.LParam.ToInt64() == NativeConstants.SW_MAXIMIZE)
                {
                    window.Menu.UncheckSizeMenu();
                }
                if (e.LParam.ToInt64() == NativeConstants.SW_MINIMIZE && window.Menu.IsMenuItemChecked(MenuItemId.SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY))
                {
                    window.MoveToSystemTray();
                }
            }
        }

        private void WindowMoveSize(object sender, WindowEventArgs e)
        {
            var window = _windows.FirstOrDefault(w => w.Handle == e.Handle);
            window?.SaveDefaultSizePosition();
        }

        private void WindowKeyboardEvent(object sender, BasicHookEventArgs e)
        {
            var wParam = e.WParam.ToInt64();
            if (wParam == NativeConstants.VK_DOWN)
            {
                var controlState = NativeMethods.GetAsyncKeyState(NativeConstants.VK_CONTROL) & 0x8000;
                var shiftState = NativeMethods.GetAsyncKeyState(NativeConstants.VK_SHIFT) & 0x8000;
                var controlKey = Convert.ToBoolean(controlState);
                var shiftKey = Convert.ToBoolean(shiftState);
                if (controlKey && shiftKey)
                {
                    IntPtr handle = NativeMethods.GetForegroundWindow();
                    Window window = _windows.FirstOrDefault(w => w.Handle == handle);
                    window?.MinimizeToSystemTray();
                }
            }
        }

        private void WindowGetMsg(object sender, WndProcEventArgs e)
        {
            var message = e.Message.ToInt64();
            if (message == NativeConstants.WM_SYSCOMMAND)
            {
                //string dbgMessage = string.Format("WM_SYSCOMMAND, Form, Handle = {0}, WParam = {1}", e.Handle, e.WParam);
                //System.Diagnostics.Trace.WriteLine(dbgMessage);
                var window = _windows.FirstOrDefault(w => w.Handle == e.Handle);
                if (window != null)
                {
                    long lowOrder = e.WParam.ToInt64() & 0x0000FFFF;
                    switch (lowOrder)
                    {
                        case NativeConstants.SC_MAXIMIZE:
                            {
                                window.Menu.UncheckSizeMenu();
                            }
                            break;

                        case MenuItemId.SC_MINIMIZE_TO_SYSTEMTRAY:
                            {
                                window.MinimizeToSystemTray();
                            }
                            break;

                        case MenuItemId.SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY:
                            {
                                bool r = window.Menu.IsMenuItemChecked(MenuItemId.SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY);
                                window.Menu.CheckMenuItem(MenuItemId.SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY, !r);
                            }
                            break;

                        case MenuItemId.SC_SUSPEND_TO_SYSTEMTRAY:
                            {
                                window.MinimizeToSystemTray();
                                window.Suspend();
                            }
                            break;

                        case MenuItemId.SC_INFORMATION:
                            {
                                var infoForm = new InfoForm(window.GetWindowInfo(), _settings.LanguageSettings);
                                infoForm.Show(window.Win32Window);
                            }
                            break;

                        case MenuItemId.SC_SAVE_SCREEN_SHOT:
                            {
                                var bitmap = WindowUtils.PrintWindow(window.Handle);
                                var dialog = new SaveFileDialog
                                {
                                    OverwritePrompt = true,
                                    ValidateNames = true,
                                    Title = _settings.LanguageSettings.GetValue("save_screenshot_title"),
                                    FileName = _settings.LanguageSettings.GetValue("save_screenshot_filename"),
                                    DefaultExt = _settings.LanguageSettings.GetValue("save_screenshot_default_ext"),
                                    RestoreDirectory = false,
                                    Filter = _settings.LanguageSettings.GetValue("save_screenshot_filter")
                                };
                                if (dialog.ShowDialog(window.Win32Window) == DialogResult.OK)
                                {
                                    var fileExtension = Path.GetExtension(dialog.FileName).ToLower();
                                    var imageFormat = fileExtension == ".bmp" ? ImageFormat.Bmp :
                                        fileExtension == ".gif" ? ImageFormat.Gif :
                                        fileExtension == ".jpeg" ? ImageFormat.Jpeg :
                                        fileExtension == ".png" ? ImageFormat.Png :
                                        fileExtension == ".tiff" ? ImageFormat.Tiff : ImageFormat.Wmf;
                                    bitmap.Save(dialog.FileName, imageFormat);
                                }
                            }
                            break;

                        case MenuItemId.SC_COPY_TEXT_TO_CLIPBOARD:
                            {
                                var text = window.ExtractText();
                                if (text != null)
                                {
                                    Clipboard.SetText(text);
                                }
                            }
                            break;

                        case MenuItemId.SC_CLEAR_CLIPBOARD:
                            {
                                Clipboard.Clear();
                            }
                            break;

                        case MenuItemId.SC_DRAG_BY_MOUSE:
                            {
                                var isChecked = window.Menu.IsMenuItemChecked(MenuItemId.SC_DRAG_BY_MOUSE);
                                window.Menu.CheckMenuItem(MenuItemId.SC_DRAG_BY_MOUSE, !isChecked);
                            }
                            break;

                        case MenuItemId.SC_OPEN_FILE_IN_EXPLORER:
                            {
                                try
                                {
                                    SystemUtils.RunAsDesktopUser("explorer.exe", "/select, " + window.Process.GetMainModuleFileName());
                                }
                                catch
                                {
                                }
                            }
                            break;

                        case MenuItemId.SC_MINIMIZE_OTHER_WINDOWS:
                        case MenuItemId.SC_CLOSE_OTHER_WINDOWS:
                            {
                                foreach (var process in Process.GetProcesses())
                                {
                                    try
                                    {
                                        if (process.MainWindowHandle != IntPtr.Zero && process.MainWindowHandle != Handle && process.MainWindowHandle != window.Handle)
                                        {
                                            if (process.ProcessName.ToLower() == "explorer")
                                            {
                                                foreach (var handle in process.GetWindowHandles().Where(x => x != window.Handle).ToList())
                                                {
                                                    var builder = new StringBuilder(1024);
                                                    NativeMethods.GetClassName(handle, builder, builder.Capacity);
                                                    var className = builder.ToString().Trim();
                                                    if (className == "CabinetWClass" || className == "ExplorerWClass")
                                                    {
                                                        if (lowOrder == MenuItemId.SC_CLOSE_OTHER_WINDOWS)
                                                        {
                                                            NativeMethods.PostMessage(handle, NativeConstants.WM_CLOSE, 0, 0);
                                                        }
                                                        else
                                                        {
                                                            NativeMethods.PostMessage(handle, NativeConstants.WM_SYSCOMMAND, NativeConstants.SC_MINIMIZE, 0);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (lowOrder == MenuItemId.SC_CLOSE_OTHER_WINDOWS)
                                                {
                                                    NativeMethods.PostMessage(process.MainWindowHandle, NativeConstants.WM_CLOSE, 0, 0);
                                                }
                                                else
                                                {
                                                    NativeMethods.PostMessage(process.MainWindowHandle, NativeConstants.WM_SYSCOMMAND, NativeConstants.SC_MINIMIZE, 0);
                                                }
                                            }
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                            break;

                        case MenuItemId.SC_TOPMOST:
                            {
                                var isChecked = window.Menu.IsMenuItemChecked(MenuItemId.SC_TOPMOST);
                                window.Menu.CheckMenuItem(MenuItemId.SC_TOPMOST, !isChecked);
                                window.MakeTopMost(!isChecked);
                            }
                            break;

                        case MenuItemId.SC_SEND_TO_BOTTOM:
                            {
                                window.SendToBottom();
                            }
                            break;

                        case MenuItemId.SC_AERO_GLASS:
                            {
                                var isChecked = window.Menu.IsMenuItemChecked(MenuItemId.SC_AERO_GLASS);
                                var version = Environment.OSVersion.Version;
                                if (version.Major == 6 && (version.Minor == 0 || version.Minor == 1))
                                {
                                    window.AeroGlassForVistaAndSeven(!isChecked);
                                    window.Menu.CheckMenuItem(MenuItemId.SC_AERO_GLASS, !isChecked);
                                }
                                else if (version.Major >= 6)
                                {
                                    window.AeroGlassForEightAndHigher(!isChecked);
                                    window.Menu.CheckMenuItem(MenuItemId.SC_AERO_GLASS, !isChecked);
                                }
                            }
                            break;

                        case MenuItemId.SC_ROLLUP:
                            {
                                var isChecked = window.Menu.IsMenuItemChecked(MenuItemId.SC_ROLLUP);
                                window.Menu.CheckMenuItem(MenuItemId.SC_ROLLUP, !isChecked);
                                if (!isChecked)
                                {
                                    window.RollUp();
                                    window.Menu.UncheckSizeMenu();
                                }
                                else
                                {
                                    window.UnRollUp();
                                }
                            }
                            break;


                        case MenuItemId.SC_SIZE_DEFAULT:
                            {
                                window.Menu.UncheckSizeMenu();
                                window.Menu.CheckMenuItem(MenuItemId.SC_SIZE_DEFAULT, true);
                                window.ShowNormal();
                                window.RestoreSize();
                                window.Menu.UncheckMenuItems(MenuItemId.SC_ROLLUP);
                            }
                            break;

                        case MenuItemId.SC_SIZE_CUSTOM:
                            {
                                var sizeForm = new SizeForm(window, _settings);
                                var result = sizeForm.ShowDialog(window.Win32Window);
                                if (result == DialogResult.OK)
                                {
                                    window.ShowNormal();

                                    if (_settings.Sizer == WindowSizerType.WindowWithMargins)
                                    {
                                        window.SetSize(sizeForm.WindowWidth, sizeForm.WindowHeight, sizeForm.WindowLeft, sizeForm.WindowTop);
                                    }
                                    else if (_settings.Sizer == WindowSizerType.WindowWithoutMargins)
                                    {
                                        var margins = window.GetSystemMargins();
                                        window.SetSize(sizeForm.WindowWidth + margins.Left + margins.Right, sizeForm.WindowHeight + margins.Top + margins.Bottom, sizeForm.WindowLeft, sizeForm.WindowTop);
                                    }
                                    else
                                    {
                                        window.SetSize(sizeForm.WindowWidth + (window.Size.Width - window.ClientSize.Width), sizeForm.WindowHeight + (window.Size.Height - window.ClientSize.Height), sizeForm.WindowLeft, sizeForm.WindowTop);
                                    }

                                    window.Menu.UncheckSizeMenu();
                                    window.Menu.CheckMenuItem(MenuItemId.SC_SIZE_CUSTOM, true);
                                    window.Menu.UncheckMenuItems(MenuItemId.SC_ROLLUP);
                                }
                            }
                            break;

                        case MenuItemId.SC_TRANS_DEFAULT:
                            {
                                window.Menu.UncheckTransparencyMenu();
                                window.Menu.CheckMenuItem(MenuItemId.SC_TRANS_DEFAULT, true);
                                window.RestoreTransparency();
                            }
                            break;

                        case MenuItemId.SC_TRANS_CUSTOM:
                            {
                                var opacityForm = new TransparencyForm(window, _settings);
                                var result = opacityForm.ShowDialog(window.Win32Window);
                                if (result == DialogResult.OK)
                                {
                                    window.SetTrancparency(opacityForm.WindowTransparency);
                                    window.Menu.UncheckTransparencyMenu();
                                    window.Menu.CheckMenuItem(MenuItemId.SC_TRANS_CUSTOM, true);
                                }
                            }
                            break;

                        case MenuItemId.SC_ALIGN_DEFAULT:
                            {
                                window.Menu.UncheckAlignmentMenu();
                                window.Menu.CheckMenuItem(MenuItemId.SC_ALIGN_DEFAULT, true);
                                window.RestorePosition();
                            }
                            break;

                        case MenuItemId.SC_ALIGN_CUSTOM:
                            {
                                var positionForm = new PositionForm(window, _settings.LanguageSettings);
                                var result = positionForm.ShowDialog(window.Win32Window);

                                if (result == DialogResult.OK)
                                {
                                    window.ShowNormal();
                                    window.SetPosition(positionForm.WindowLeft, positionForm.WindowTop);
                                    window.Menu.UncheckAlignmentMenu();
                                    window.Menu.CheckMenuItem(MenuItemId.SC_ALIGN_CUSTOM, true);
                                }
                            }
                            break;

                        case MenuItemId.SC_TRANS_100: SetTransparencyMenuItem(window, MenuItemId.SC_TRANS_100, 100); break;
                        case MenuItemId.SC_TRANS_90: SetTransparencyMenuItem(window, MenuItemId.SC_TRANS_90, 90); break;
                        case MenuItemId.SC_TRANS_80: SetTransparencyMenuItem(window, MenuItemId.SC_TRANS_80, 80); break;
                        case MenuItemId.SC_TRANS_70: SetTransparencyMenuItem(window, MenuItemId.SC_TRANS_70, 70); break;
                        case MenuItemId.SC_TRANS_60: SetTransparencyMenuItem(window, MenuItemId.SC_TRANS_60, 60); break;
                        case MenuItemId.SC_TRANS_50: SetTransparencyMenuItem(window, MenuItemId.SC_TRANS_50, 50); break;
                        case MenuItemId.SC_TRANS_40: SetTransparencyMenuItem(window, MenuItemId.SC_TRANS_40, 40); break;
                        case MenuItemId.SC_TRANS_30: SetTransparencyMenuItem(window, MenuItemId.SC_TRANS_30, 30); break;
                        case MenuItemId.SC_TRANS_20: SetTransparencyMenuItem(window, MenuItemId.SC_TRANS_20, 20); break;
                        case MenuItemId.SC_TRANS_10: SetTransparencyMenuItem(window, MenuItemId.SC_TRANS_10, 10); break;
                        case MenuItemId.SC_TRANS_00: SetTransparencyMenuItem(window, MenuItemId.SC_TRANS_00, 0); break;

                        case MenuItemId.SC_PRIORITY_REAL_TIME: SetPriorityMenuItem(window, MenuItemId.SC_PRIORITY_REAL_TIME, Priority.RealTime); break;
                        case MenuItemId.SC_PRIORITY_HIGH: SetPriorityMenuItem(window, MenuItemId.SC_PRIORITY_HIGH, Priority.High); break;
                        case MenuItemId.SC_PRIORITY_ABOVE_NORMAL: SetPriorityMenuItem(window, MenuItemId.SC_PRIORITY_ABOVE_NORMAL, Priority.AboveNormal); break;
                        case MenuItemId.SC_PRIORITY_NORMAL: SetPriorityMenuItem(window, MenuItemId.SC_PRIORITY_NORMAL, Priority.Normal); break;
                        case MenuItemId.SC_PRIORITY_BELOW_NORMAL: SetPriorityMenuItem(window, MenuItemId.SC_PRIORITY_BELOW_NORMAL, Priority.BelowNormal); break;
                        case MenuItemId.SC_PRIORITY_IDLE: SetPriorityMenuItem(window, MenuItemId.SC_PRIORITY_IDLE, Priority.Idle); break;

                        case MenuItemId.SC_ALIGN_TOP_LEFT: SetAlignmentMenuItem(window, MenuItemId.SC_ALIGN_TOP_LEFT, WindowAlignment.TopLeft); break;
                        case MenuItemId.SC_ALIGN_TOP_CENTER: SetAlignmentMenuItem(window, MenuItemId.SC_ALIGN_TOP_CENTER, WindowAlignment.TopCenter); break;
                        case MenuItemId.SC_ALIGN_TOP_RIGHT: SetAlignmentMenuItem(window, MenuItemId.SC_ALIGN_TOP_RIGHT, WindowAlignment.TopRight); break;
                        case MenuItemId.SC_ALIGN_MIDDLE_LEFT: SetAlignmentMenuItem(window, MenuItemId.SC_ALIGN_MIDDLE_LEFT, WindowAlignment.MiddleLeft); break;
                        case MenuItemId.SC_ALIGN_MIDDLE_CENTER: SetAlignmentMenuItem(window, MenuItemId.SC_ALIGN_MIDDLE_CENTER, WindowAlignment.MiddleCenter); break;
                        case MenuItemId.SC_ALIGN_MIDDLE_RIGHT: SetAlignmentMenuItem(window, MenuItemId.SC_ALIGN_MIDDLE_RIGHT, WindowAlignment.MiddleRight); break;
                        case MenuItemId.SC_ALIGN_BOTTOM_LEFT: SetAlignmentMenuItem(window, MenuItemId.SC_ALIGN_BOTTOM_LEFT, WindowAlignment.BottomLeft); break;
                        case MenuItemId.SC_ALIGN_BOTTOM_CENTER: SetAlignmentMenuItem(window, MenuItemId.SC_ALIGN_BOTTOM_CENTER, WindowAlignment.BottomCenter); break;
                        case MenuItemId.SC_ALIGN_BOTTOM_RIGHT: SetAlignmentMenuItem(window, MenuItemId.SC_ALIGN_BOTTOM_RIGHT, WindowAlignment.BottomRight); break;
                    }

                    var moveToSubMenuItem = (int)lowOrder - MenuItemId.SC_MOVE_TO;
                    if (window.Menu.MoveToMenuItems.ContainsKey(moveToSubMenuItem))
                    {
                        var monitorHandle = window.Menu.MoveToMenuItems[moveToSubMenuItem];
                        window.MoveToMonitor(monitorHandle);
                    }

                    var windowSizeItem = _settings.MenuItems.WindowSizeItems.FirstOrDefault(x => x.Id == lowOrder);
                    if (windowSizeItem != null)
                    {
                        SetSizeMenuItem(window, (int)lowOrder, windowSizeItem);
                    }

                    for (int i = 0; i < _settings.MenuItems.StartProgramItems.Count; i++)
                    {
                        if (lowOrder - MenuItemId.SC_START_PROGRAM == i)
                        {
                            try
                            {
                                SystemUtils.RunAsDesktopUser(_settings.MenuItems.StartProgramItems[i].FileName, _settings.MenuItems.StartProgramItems[i].Arguments);
                            }
                            catch
                            {
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void HotKeyHooked(object sender, HotKeyEventArgs e)
        {
            var handle = NativeMethods.GetForegroundWindow();
            var systemMenuHandle = NativeMethods.GetSystemMenu(handle, false);

            if (handle != null && handle != IntPtr.Zero && systemMenuHandle != null && systemMenuHandle != IntPtr.Zero)
            {
                var processName = "";

                try
                {
                    NativeMethods.GetWindowThreadProcessId(handle, out var processId);
                    var process = SystemUtils.GetProcessByIdSafely(processId);
                    processName = Path.GetFileName(process.GetMainModuleFileName());
                }
                catch
                {
                }

                if (!_settings.ProcessExclusions.Contains(processName.ToLower()))
                {
                    NativeMethods.PostMessage(handle, NativeConstants.WM_SYSCOMMAND, (uint)e.MenuItemId, 0);
                    e.Succeeded = true;
                }
            }
        }

        private void SetPriorityMenuItem(Window window, int itemId, Priority priority)
        {
            window.Menu.UncheckPriorityMenu();
            window.Menu.CheckMenuItem(itemId, true);
            window.SetPriority(priority);
        }

        private void SetAlignmentMenuItem(Window window, int itemId, WindowAlignment alignment)
        {
            window.Menu.UncheckAlignmentMenu();
            window.Menu.CheckMenuItem(itemId, true);
            window.ShowNormal();
            window.SetAlignment(alignment);
        }

        private void SetSizeMenuItem(Window window, int itemId, WindowSizeMenuItem item)
        {
            window.Menu.UncheckSizeMenu();
            window.Menu.CheckMenuItem(itemId, true);
            window.ShowNormal();
            if (_settings.Sizer == WindowSizerType.WindowWithMargins)
            {
                window.SetSize(item.Width, item.Height, item.Left, item.Top);
            }
            else if (_settings.Sizer == WindowSizerType.WindowWithoutMargins)
            {
                var margins = window.GetSystemMargins();
                window.SetSize(item.Width + margins.Left + margins.Right, item.Height + margins.Top + margins.Bottom, item.Left, item.Top);
            }
            else
            {
                window.SetSize(item.Width + (window.Size.Width - window.ClientSize.Width), item.Height + (window.Size.Height - window.ClientSize.Height), item.Left, item.Top);
            }
            window.Menu.UncheckMenuItems(MenuItemId.SC_ROLLUP);
        }

        private void SetTransparencyMenuItem(Window window, int itemId, int transparency)
        {
            window.Menu.UncheckTransparencyMenu();
            window.Menu.CheckMenuItem(itemId, true);
            window.SetTrancparency(transparency);
        }

        private void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            ex = ex ?? new Exception("OnCurrentDomainUnhandledException");
            OnThreadException(sender, new ThreadExceptionEventArgs(ex));
        }

        private void OnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString(), AssemblyUtils.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}