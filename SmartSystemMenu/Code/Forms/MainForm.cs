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
using SmartSystemMenu.Code.Common;
using SmartSystemMenu.Code.Common.Extensions;
using SmartSystemMenu.Code.Hooks;

namespace SmartSystemMenu.Code.Forms
{
    partial class MainForm : Form
    {
        private readonly String _shellWindowName = "Program Manager";
        private List<Window> _windows;
        private GetMsgHook _getMsgHook;
        private ShellHook _shellHook;
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
                    if (!File.Exists(filePath))
                    {
                        AssemblyUtility.ExtractFileFromAssembly(resourceName, filePath);
                    }
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
            _windows = EnumWindows.EnumAllWindows(new String[] { _shellWindowName }).ToList();
            foreach (var window in _windows)
            {
                window.Menu.Create();
                Int32 menuItemId = window.ProcessPriority.GetMenuItemId();
                window.Menu.CheckMenuItem(menuItemId, true);
                window.Menu.SetMenuItemText(SystemMenu.SC_ALIGN_MONITOR, "Select Monitor: " + Screen.AllScreens.ToList().FindIndex(s => s.Primary));
                if (window.AlwaysOnTop) window.Menu.CheckMenuItem(SystemMenu.SC_TOPMOST, true);
            }

            _getMsgHook = new GetMsgHook(Handle);
            _getMsgHook.GetMsg += WindowGetMsg;
            _getMsgHook.Start();

            _shellHook = new ShellHook(Handle);
            _shellHook.WindowCreated += WindowCreated;
            _shellHook.WindowDestroyed += WindowDestroyed;
            _shellHook.Start();

            _cbtHook = new CBTHook(Handle);
            _cbtHook.WindowCreated += WindowCreated;
            _cbtHook.WindowDestroyed += WindowDestroyed;
            _cbtHook.MoveSize += WindowMoveSize;
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
            if (_shellHook != null)
            {
                _shellHook.Stop();
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
            if (_shellHook != null)
            {
                _shellHook.ProcessWindowMessage(ref m);
            }
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
                IList<Window> windows = new List<Window>();
                try
                {
                    windows = EnumWindows.EnumProcessWindows(processId, _windows.Select(w => w.Handle).ToArray(), new String[] { _shellWindowName });
                }
                catch
                {
                }
                foreach (var window in windows)
                {
                    window.Menu.Create();
                    Int32 menuItemId = window.ProcessPriority.GetMenuItemId();
                    window.Menu.CheckMenuItem(menuItemId, true);
                    window.Menu.SetMenuItemText(SystemMenu.SC_ALIGN_MONITOR, "Select Monitor: " + Screen.AllScreens.ToList().FindIndex(s => s.Primary));
                    if (window.AlwaysOnTop) window.Menu.CheckMenuItem(SystemMenu.SC_TOPMOST, true);
                    _windows.Add(window);
                }
            }
        }

        private void WindowDestroyed(object sender, WindowEventArgs e)
        {
            Int32 windowIndex = _windows.FindIndex(w => w.Handle == e.Handle);
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
                if (e.LParam.ToInt64() == NativeConstants.SW_MAXIMIZE)
                {
                    window.Menu.UncheckSizeMenu();
                }
                if (e.LParam.ToInt64() == NativeConstants.SW_MINIMIZE && window.Menu.IsMenuItemChecked(SystemMenu.SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY))
                {
                    window.MoveToSystemTray();
                }
            }
        }

        private void WindowMoveSize(object sender, WindowEventArgs e)
        {
            Window window = _windows.FirstOrDefault(w => w.Handle == e.Handle);
            if (window != null)
            {
                window.SaveDefaultSizePosition();
            }
        }

        private void WindowKeyboardEvent(object sender, BasicHookEventArgs e)
        {
            Int64 wParam = e.WParam.ToInt64();
            if (wParam == NativeConstants.VK_DOWN)
            {
                Int32 controlState = NativeMethods.GetAsyncKeyState(NativeConstants.VK_CONTROL) & 0x8000;
                Int32 shiftState = NativeMethods.GetAsyncKeyState(NativeConstants.VK_SHIFT) & 0x8000;
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
            if (message == NativeConstants.WM_SYSCOMMAND)
            {
                //String dbgMessage = String.Format("WM_SYSCOMMAND, Form, Handle = {0}, WParam = {1}", e.Handle, e.WParam);
                //System.Diagnostics.Trace.WriteLine(dbgMessage);
                Window window = _windows.FirstOrDefault(w => w.Handle == e.Handle);
                if (window != null)
                {
                    Int64 lowOrder = e.WParam.ToInt64() & 0x0000FFFF;
                    switch (lowOrder)
                    {
                        case NativeConstants.SC_MAXIMIZE:
                            {
                                window.Menu.UncheckSizeMenu();
                            } break;

                        case SystemMenu.SC_MINIMIZE_TO_SYSTEMTRAY:
                            {
                                window.MinimizeToSystemTray();
                            } break;

                        case SystemMenu.SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY:
                            {
                                Boolean r = window.Menu.IsMenuItemChecked(SystemMenu.SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY);
                                window.Menu.CheckMenuItem(SystemMenu.SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY, !r);
                            } break;

                        case SystemMenu.SC_INFORMATION:
                            {
                                InfoForm infoForm = new InfoForm(window);
                                infoForm.Show();
                                Window.ForceForegroundWindow(infoForm.Handle);
                            } break;

                        case SystemMenu.SC_TOPMOST:
                            {
                                Boolean r = window.Menu.IsMenuItemChecked(SystemMenu.SC_TOPMOST);
                                window.Menu.CheckMenuItem(SystemMenu.SC_TOPMOST, !r);
                                window.MakeTopMost(!r);
                            } break;

                        case SystemMenu.SC_ROLLUP:
                            {
                                Boolean r = window.Menu.IsMenuItemChecked(SystemMenu.SC_ROLLUP);
                                window.Menu.CheckMenuItem(SystemMenu.SC_ROLLUP, !r);
                                if (!r)
                                {
                                    window.RollUp();
                                }
                                else
                                {
                                    window.UnRollUp();
                                }
                            } break;


                        case SystemMenu.SC_SIZE_DEFAULT:
                            {
                                window.Menu.UncheckSizeMenu();
                                window.Menu.CheckMenuItem(SystemMenu.SC_SIZE_DEFAULT, true);
                                window.ShowNormal();
                                window.RestoreSize();
                            } break;

                        case SystemMenu.SC_SIZE_CUSTOM:
                            {
                                SizeForm sizeForm = new SizeForm(window);
                                sizeForm.Show();
                                Window.ForceForegroundWindow(sizeForm.Handle);
                            } break;

                        case SystemMenu.SC_TRANS_DEFAULT:
                            {
                                window.Menu.UncheckTransparencyMenu();
                                window.Menu.CheckMenuItem(SystemMenu.SC_TRANS_DEFAULT, true);
                                window.RestoreTransparency();
                            } break;

                        case SystemMenu.SC_TRANS_CUSTOM:
                            {
                                TransparencyForm opacityForm = new TransparencyForm(window);
                                opacityForm.Show();
                                Window.ForceForegroundWindow(opacityForm.Handle);
                            } break;

                        case SystemMenu.SC_ALIGN_DEFAULT:
                            {
                                window.Menu.UncheckAlignmentMenu();
                                window.Menu.CheckMenuItem(SystemMenu.SC_ALIGN_DEFAULT, true);
                                window.RestorePosition();
                            } break;

                        case SystemMenu.SC_ALIGN_CUSTOM:
                            {
                                PositionForm positionForm = new PositionForm(window);
                                positionForm.Show();
                                Window.ForceForegroundWindow(positionForm.Handle);
                            } break;

                        case SystemMenu.SC_ALIGN_MONITOR:
                            {
                                ScreenForm screenForm = new ScreenForm(window);
                                screenForm.Show();
                                Window.ForceForegroundWindow(screenForm.Handle);
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

                        case SystemMenu.SC_TRANS_100: SetTransparencyMenuItem(window, SystemMenu.SC_TRANS_100, 100); break;
                        case SystemMenu.SC_TRANS_90: SetTransparencyMenuItem(window, SystemMenu.SC_TRANS_90, 90); break;
                        case SystemMenu.SC_TRANS_80: SetTransparencyMenuItem(window, SystemMenu.SC_TRANS_80, 80); break;
                        case SystemMenu.SC_TRANS_70: SetTransparencyMenuItem(window, SystemMenu.SC_TRANS_70, 70); break;
                        case SystemMenu.SC_TRANS_60: SetTransparencyMenuItem(window, SystemMenu.SC_TRANS_60, 60); break;
                        case SystemMenu.SC_TRANS_50: SetTransparencyMenuItem(window, SystemMenu.SC_TRANS_50, 50); break;
                        case SystemMenu.SC_TRANS_40: SetTransparencyMenuItem(window, SystemMenu.SC_TRANS_40, 40); break;
                        case SystemMenu.SC_TRANS_30: SetTransparencyMenuItem(window, SystemMenu.SC_TRANS_30, 30); break;
                        case SystemMenu.SC_TRANS_20: SetTransparencyMenuItem(window, SystemMenu.SC_TRANS_20, 20); break;
                        case SystemMenu.SC_TRANS_10: SetTransparencyMenuItem(window, SystemMenu.SC_TRANS_10, 10); break;
                        case SystemMenu.SC_TRANS_00: SetTransparencyMenuItem(window, SystemMenu.SC_TRANS_00, 0); break;

                        case SystemMenu.SC_PRIORITY_REAL_TIME: SetPriorityMenuItem(window, SystemMenu.SC_PRIORITY_REAL_TIME, Priority.RealTime); break;
                        case SystemMenu.SC_PRIORITY_HIGH: SetPriorityMenuItem(window, SystemMenu.SC_PRIORITY_HIGH, Priority.High); break;
                        case SystemMenu.SC_PRIORITY_ABOVE_NORMAL: SetPriorityMenuItem(window, SystemMenu.SC_PRIORITY_ABOVE_NORMAL, Priority.AboveNormal); break;
                        case SystemMenu.SC_PRIORITY_NORMAL: SetPriorityMenuItem(window, SystemMenu.SC_PRIORITY_NORMAL, Priority.Normal); break;
                        case SystemMenu.SC_PRIORITY_BELOW_NORMAL: SetPriorityMenuItem(window, SystemMenu.SC_PRIORITY_BELOW_NORMAL, Priority.BelowNormal); break;
                        case SystemMenu.SC_PRIORITY_IDLE: SetPriorityMenuItem(window, SystemMenu.SC_PRIORITY_IDLE, Priority.Idle); break;

                        case SystemMenu.SC_ALIGN_TOP_LEFT: SetAlignmentMenuItem(window, SystemMenu.SC_ALIGN_TOP_LEFT, WindowAlignment.TopLeft); break;
                        case SystemMenu.SC_ALIGN_TOP_CENTER: SetAlignmentMenuItem(window, SystemMenu.SC_ALIGN_TOP_CENTER, WindowAlignment.TopCenter); break;
                        case SystemMenu.SC_ALIGN_TOP_RIGHT: SetAlignmentMenuItem(window, SystemMenu.SC_ALIGN_TOP_RIGHT, WindowAlignment.TopRight); break;
                        case SystemMenu.SC_ALIGN_MIDDLE_LEFT: SetAlignmentMenuItem(window, SystemMenu.SC_ALIGN_MIDDLE_LEFT, WindowAlignment.MiddleLeft); break;
                        case SystemMenu.SC_ALIGN_MIDDLE_CENTER: SetAlignmentMenuItem(window, SystemMenu.SC_ALIGN_MIDDLE_CENTER, WindowAlignment.MiddleCenter); break;
                        case SystemMenu.SC_ALIGN_MIDDLE_RIGHT: SetAlignmentMenuItem(window, SystemMenu.SC_ALIGN_MIDDLE_RIGHT, WindowAlignment.MiddleRight); break;
                        case SystemMenu.SC_ALIGN_BOTTOM_LEFT: SetAlignmentMenuItem(window, SystemMenu.SC_ALIGN_BOTTOM_LEFT, WindowAlignment.BottomLeft); break;
                        case SystemMenu.SC_ALIGN_BOTTOM_CENTER: SetAlignmentMenuItem(window, SystemMenu.SC_ALIGN_BOTTOM_CENTER, WindowAlignment.BottomCenter); break;
                        case SystemMenu.SC_ALIGN_BOTTOM_RIGHT: SetAlignmentMenuItem(window, SystemMenu.SC_ALIGN_BOTTOM_RIGHT, WindowAlignment.BottomRight); break;
                    }
                }
            }
        }

        private void SetPriorityMenuItem(Window window, Int32 itemId, Priority priority)
        {
            window.Menu.UncheckPriorityMenu();
            window.Menu.CheckMenuItem(itemId, true);
            window.SetPriority(priority);
        }

        private void SetAlignmentMenuItem(Window window, Int32 itemId, WindowAlignment alignment)
        {
            window.Menu.UncheckAlignmentMenu();
            window.Menu.CheckMenuItem(itemId, true);
            window.ShowNormal();
            window.SetAlignment(alignment);
        }

        private void SetSizeMenuItem(Window window, Int32 itemId, Int32 width, Int32 height)
        {
            window.Menu.UncheckSizeMenu();
            window.Menu.CheckMenuItem(itemId, true);
            window.ShowNormal();
            window.SetSize(width, height);
        }

        private void SetTransparencyMenuItem(Window window, Int32 itemId, Int32 transparency)
        {
            window.Menu.UncheckTransparencyMenu();
            window.Menu.CheckMenuItem(itemId, true);
            window.SetTrancparency(transparency);
        }

        private void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            ex = ex ?? new Exception("OnCurrentDomainUnhandledException");
            OnThreadException(sender, new System.Threading.ThreadExceptionEventArgs(ex));
        }

        private void OnThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            String exceptionText = e.Exception.ToString();
            if (e.Exception is Win32Exception)
            {
                exceptionText = String.Format("Win32 Error Code = {0},{1}{2}", ((Win32Exception)e.Exception).ErrorCode, Environment.NewLine, exceptionText);
            }
            MessageBox.Show(exceptionText, AssemblyUtility.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}