using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using SmartSystemMenu.App_Code.Common;
using SmartSystemMenu.App_Code.Hooks;

namespace SmartSystemMenu.App_Code.Forms
{
    partial class MainForm : Form
    {
        private readonly String _shellWindowName = "Program Manager";
        private IList<Window> _windows;
        private GetMsgHook _getMsgHook;
        private CBTHook _cbtHook;
        private KeyboardHook _keyboardHook;
        private AboutForm _aboutForm;

#if WIN32
        private SystemTrayMenu _systemTrayMenu;
        private Process _64BitProcess;
#endif
        public MainForm()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;
            Application.ThreadException += OnThreadException;

            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            ShowInTaskbar = false;
            Opacity = 0;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

#if WIN32
            if (Environment.Is64BitOperatingSystem)
            {
                String resourceName = "SmartSystemMenu.SmartSystemMenu64.exe";
                String fileName = "SmartSystemMenu64.exe";
                String directoryName = Path.GetDirectoryName(AssemblyUtility.AssemblyLocation);
                String filePath = Path.Combine(directoryName, fileName);
                try
                {
                    AssemblyUtility.ExtractFileFromAssembly(resourceName, filePath);
                    _64BitProcess = Process.Start(filePath);
                }
                catch
                {
                    String message = String.Format("Failed to load {0} process!", fileName);
                    MessageBox.Show(message, AssemblyUtility.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }
            }
            _systemTrayMenu = new SystemTrayMenu();
            _systemTrayMenu.MenuItemAutoStart.Click += MenuItemAutoStartClick;
            _systemTrayMenu.MenuItemAbout.Click += MenuItemAboutClick;
            _systemTrayMenu.MenuItemExit.Click += MenuItemExitClick;
            _systemTrayMenu.MenuItemAutoStart.Checked = AutoStarter.IsAutoStartByRegisterEnabled(AssemblyUtility.AssemblyProductName, AssemblyUtility.AssemblyLocation);
#endif
            _windows = EnumWindows.EnumAllWindows(new String[] { _shellWindowName });
            foreach (var window in _windows)
            {
                window.Menu.Create();
            }

            _getMsgHook = new GetMsgHook(Handle);
            _getMsgHook.GetMsg += WindowGetMsg;
            _getMsgHook.Start();

            _cbtHook = new CBTHook(Handle);
            _cbtHook.CreateWindow += WindowCreated;
            _cbtHook.DestroyWindow += WindowDestroyed;
            _cbtHook.MinMax += WindowMinMax;
            _cbtHook.Start();

            _keyboardHook = new KeyboardHook(Handle);
            _keyboardHook.KeyboardEvent += WindowKeyboardEvent;
            _keyboardHook.Start();

            Hide();
        }

        protected override void OnClosed(EventArgs e)
        {
            if (_getMsgHook != null)
            {
                _getMsgHook.Stop();
            }
            if (_cbtHook != null)
            {
                _cbtHook.Stop();
            }
            if (_windows != null)
            {
                foreach (Window window in _windows)
                {
                    window.Dispose();
                }
            }

            Window.ForceAllMessageLoopsToWakeUp();

#if WIN32
            if (_systemTrayMenu != null)
            {
                _systemTrayMenu.Icon.Visible = false;
            }

            if (Environment.Is64BitOperatingSystem && _64BitProcess != null && !_64BitProcess.HasExited)
            {
                Window.CloseAllWindowsOfProcess(_64BitProcess.Id);
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
        }

        protected override void WndProc(ref Message m)
        {
            if (_cbtHook != null)
            {
                _cbtHook.ProcessWindowMessage(ref m);
            }
            if (_getMsgHook != null)
            {
                _getMsgHook.ProcessWindowMessage(ref m);
            }
            if (_keyboardHook != null)
            {
                _keyboardHook.ProcessWindowMessage(ref m);
            }
            base.WndProc(ref m);
        }

        private void MenuItemAutoStartClick(object sender, EventArgs e)
        {
            String keyName = AssemblyUtility.AssemblyProductName;
            String assemblyLocation = AssemblyUtility.AssemblyLocation;
            Boolean autoStartEnabled = AutoStarter.IsAutoStartByRegisterEnabled(keyName, assemblyLocation);
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
                _aboutForm = new AboutForm();
            }
            _aboutForm.Show();
            _aboutForm.Activate();
        }

        private void MenuItemExitClick(object sender, EventArgs e)
        {
            Close();
        }

        private void WindowCreated(object sender, WindowEventArgs e)
        {
            if (e.Handle != IntPtr.Zero && new SystemMenu(e.Handle).Exists && !_windows.Any(w => w.Handle == e.Handle))
            {
                Int32 processId;
                NativeMethods.GetWindowThreadProcessId(e.Handle, out processId);
                IList<Window> windows = EnumWindows.EnumProcessWindows(processId, _windows.Select(w => w.Handle).ToArray(), new String[] { _shellWindowName });
                foreach (var window in windows)
                {
                    window.Menu.Create();
                    _windows.Add(window);
                }
            }
        }

        private void WindowDestroyed(object sender, WindowEventArgs e)
        {
            Int32 windowIndex = -1;

            for (Int32 i = 0; i < _windows.Count; ++i)
            {
                if (_windows[i].Handle == e.Handle)
                {
                    windowIndex = i;
                    break;
                }
            }

            if (windowIndex != -1 && !_windows[windowIndex].ExistSystemTrayIcon)
            {
                _windows[windowIndex].Dispose();
                _windows.RemoveAt(windowIndex);
            }
        }

        private void WindowMinMax(object sender, SysCommandEventArgs e)
        {
            Window window = _windows.FirstOrDefault(w => w.Handle == e.WParam);
            if (window != null)
            {
                if (e.LParam.ToInt64() == NativeMethods.SW_MAXIMIZE)
                {
                    window.Menu.UncheckSizeMenu();
                }
                if (e.LParam.ToInt64() == NativeMethods.SW_MINIMIZE && window.Menu.IsSystemTrayMenuItemChecked(SystemMenu.SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY))
                {
                    window.MoveToSystemTray();
                }
            }
        }

        private void WindowKeyboardEvent(object sender, BasicHookEventArgs e)
        {
            Int64 wParam = e.WParam.ToInt64();
            if (wParam == NativeMethods.VK_DOWN)
            {
                Int32 controlState = NativeMethods.GetAsyncKeyState(NativeMethods.VK_CONTROL) & 0x8000;
                Int32 shiftState = NativeMethods.GetAsyncKeyState(NativeMethods.VK_SHIFT) & 0x8000;
                Boolean controlKey = Convert.ToBoolean(controlState);
                Boolean shiftKey = Convert.ToBoolean(shiftState);
                if (controlKey && shiftKey)
                {
                    IntPtr handle = NativeMethods.GetForegroundWindow();
                    Window window = _windows.FirstOrDefault(w => w.Handle == handle);
                    if (window != null)
                    {
                        window.MinimizeToSystemTray();
                    }
                }
            }
        }

        private void WindowGetMsg(object sender, WndProcEventArgs e)
        {
            Int64 message = e.Message.ToInt64();
            if (message == NativeMethods.WM_SYSCOMMAND)
            {
                Window window = _windows.FirstOrDefault(w => w.Handle == e.Handle);
                if (window != null)
                {
                    Int64 lowOrder = e.WParam.ToInt64() & 0x0000FFFF;
                    switch (lowOrder)
                    {
                        case NativeMethods.SC_MAXIMIZE:
                            {
                                window.Menu.UncheckSizeMenu();
                            } break;

                        case SystemMenu.SC_MINIMIZE_TO_SYSTEMTRAY:
                            {
                                window.MinimizeToSystemTray();
                            } break;

                        case SystemMenu.SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY:
                            {
                                Boolean r = window.Menu.IsSystemTrayMenuItemChecked(SystemMenu.SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY);
                                window.Menu.CheckOnTopMenuItem(SystemMenu.SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY, !r);
                            } break;

                        case SystemMenu.SC_INFORMATION:
                            {
                                InfoForm infoForm = new InfoForm(window);
                                infoForm.Show();
                                Window.ForceForegroundWindow(infoForm.Handle);
                            } break;

                        case SystemMenu.SC_TOPMOST:
                            {
                                Boolean r = window.Menu.IsOnTopMenuItemChecked(SystemMenu.SC_TOPMOST);
                                window.Menu.CheckOnTopMenuItem(SystemMenu.SC_TOPMOST, !r);
                                window.MakeTopMost(!r);
                            } break;

                        case SystemMenu.SC_SIZE_CURRENT:
                            {
                                window.Menu.UncheckSizeMenu();
                                window.Menu.CheckSizeMenuItem(SystemMenu.SC_SIZE_CURRENT, true);
                                window.ShowNormal();
                                window.RestoreSize();
                            } break;

                        case SystemMenu.SC_SIZE_CUSTOM:
                            {
                                SizeForm sizeForm = new SizeForm(window);
                                sizeForm.Show();
                                Window.ForceForegroundWindow(sizeForm.Handle);
                            } break;

                        case SystemMenu.SC_TRANS_CURRENT:
                            {
                                window.Menu.UncheckTransparencyMenu();
                                window.Menu.CheckTransparencyMenuItem(SystemMenu.SC_TRANS_CURRENT, true);
                                window.RestoreTransparency();
                            } break;

                        case SystemMenu.SC_TRANS_CUSTOM:
                            {
                                OpacityForm opacityForm = new OpacityForm(window);
                                opacityForm.Show();
                                Window.ForceForegroundWindow(opacityForm.Handle);
                            } break;

                        case SystemMenu.SC_SIZE_640_480: SetSizeMenuItem(window, SystemMenu.SC_SIZE_640_480, 640, 480); break;
                        case SystemMenu.SC_SIZE_720_480: SetSizeMenuItem(window, SystemMenu.SC_SIZE_720_480, 720, 480); break;
                        case SystemMenu.SC_SIZE_720_576: SetSizeMenuItem(window, SystemMenu.SC_SIZE_720_576, 720, 576); break;
                        case SystemMenu.SC_SIZE_800_600: SetSizeMenuItem(window, SystemMenu.SC_SIZE_800_600, 800, 600); break;
                        case SystemMenu.SC_SIZE_1024_768: SetSizeMenuItem(window, SystemMenu.SC_SIZE_1024_768, 1024, 768); break;
                        case SystemMenu.SC_SIZE_1152_864: SetSizeMenuItem(window, SystemMenu.SC_SIZE_1152_864, 1152, 864); break;
                        case SystemMenu.SC_SIZE_1280_768: SetSizeMenuItem(window, SystemMenu.SC_SIZE_1280_768, 1280, 768); break;
                        case SystemMenu.SC_SIZE_1280_800: SetSizeMenuItem(window, SystemMenu.SC_SIZE_1280_800, 1280, 800); break;
                        case SystemMenu.SC_SIZE_1280_960: SetSizeMenuItem(window, SystemMenu.SC_SIZE_1280_960, 1280, 960); break;
                        case SystemMenu.SC_SIZE_1280_1024: SetSizeMenuItem(window, SystemMenu.SC_SIZE_1280_1024, 1280, 1024); break;
                        case SystemMenu.SC_SIZE_1440_900: SetSizeMenuItem(window, SystemMenu.SC_SIZE_1440_900, 1440, 900); break;
                        case SystemMenu.SC_SIZE_1600_900: SetSizeMenuItem(window, SystemMenu.SC_SIZE_1600_900, 1600, 900); break;
                        case SystemMenu.SC_SIZE_1680_1050: SetSizeMenuItem(window, SystemMenu.SC_SIZE_1680_1050, 1680, 1050); break;

                        case SystemMenu.SC_TRANS100: SetTransparencyMenuItem(window, SystemMenu.SC_TRANS100, 100); break;
                        case SystemMenu.SC_TRANS90: SetTransparencyMenuItem(window, SystemMenu.SC_TRANS90, 90); break;
                        case SystemMenu.SC_TRANS80: SetTransparencyMenuItem(window, SystemMenu.SC_TRANS80, 80); break;
                        case SystemMenu.SC_TRANS70: SetTransparencyMenuItem(window, SystemMenu.SC_TRANS70, 70); break;
                        case SystemMenu.SC_TRANS60: SetTransparencyMenuItem(window, SystemMenu.SC_TRANS60, 60); break;
                        case SystemMenu.SC_TRANS50: SetTransparencyMenuItem(window, SystemMenu.SC_TRANS50, 50); break;
                        case SystemMenu.SC_TRANS40: SetTransparencyMenuItem(window, SystemMenu.SC_TRANS40, 40); break;
                        case SystemMenu.SC_TRANS30: SetTransparencyMenuItem(window, SystemMenu.SC_TRANS30, 30); break;
                        case SystemMenu.SC_TRANS20: SetTransparencyMenuItem(window, SystemMenu.SC_TRANS20, 20); break;
                        case SystemMenu.SC_TRANS10: SetTransparencyMenuItem(window, SystemMenu.SC_TRANS10, 10); break;
                        case SystemMenu.SC_TRANS00: SetTransparencyMenuItem(window, SystemMenu.SC_TRANS00, 00); break;
                    }
                }
            }
        }

        private void SetTransparencyMenuItem(Window window, Int32 itemId, Int32 transparency)
        {
            window.Menu.UncheckTransparencyMenu();
            window.Menu.CheckTransparencyMenuItem(itemId, true);
            window.SetTrancparencyByPercent(transparency);
        }

        private void SetSizeMenuItem(Window window, Int32 itemId, Int32 width, Int32 height)
        {
            window.Menu.UncheckSizeMenu();
            window.Menu.CheckSizeMenuItem(itemId, true);
            window.ShowNormal();
            window.SetSize(width, height);
        }

        private void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            ex = ex ?? new Exception("OnCurrentDomainUnhandledException");
            OnThreadException(sender, new System.Threading.ThreadExceptionEventArgs(ex));
        }

        private void OnThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString(), AssemblyUtility.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}