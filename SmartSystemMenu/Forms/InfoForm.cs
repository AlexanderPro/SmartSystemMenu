using System;
using System.Windows.Forms;
using System.Globalization;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    partial class InfoForm : Form
    {
        public InfoForm(WindowInfo windowInfo, SmartSystemMenuSettings settings)
        {
            InitializeComponent();
            InitializeControls(windowInfo, settings);
        }

        private void InitializeControls(WindowInfo windowInfo, SmartSystemMenuSettings settings)
        {
            /*tabGeneral.Text = settings.LanguageSettings.GetValue("tab_general");
            tabProcess.Text = settings.LanguageSettings.GetValue("tab_process");
            lblRectangle.Text = settings.LanguageSettings.GetValue("lbl_rectangle");
            lblStyle.Text = settings.LanguageSettings.GetValue("lbl_style");
            lblClass.Text = settings.LanguageSettings.GetValue("lbl_class");
            lblCaption.Text = settings.LanguageSettings.GetValue("lbl_caption");
            lblHandle.Text = settings.LanguageSettings.GetValue("lbl_handle");
            lblThreadId.Text = settings.LanguageSettings.GetValue("lbl_thread_id");
            lblProcessId.Text = settings.LanguageSettings.GetValue("lbl_process_id");
            lblModulePath.Text = settings.LanguageSettings.GetValue("lbl_module_path");
            lblModuleName.Text = settings.LanguageSettings.GetValue("lbl_module_name");*/
            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone(); ;
            nfi.NumberGroupSeparator = ",";
            Text = settings.LanguageSettings.GetValue("information");
            txtGetWindowText.Text = windowInfo.GetWindowText;
            txtWmGetText.Text = windowInfo.WM_GETTEXT;
            txtGetClassName.Text = windowInfo.GetClassName;
            txtRealGetWindowClass.Text = windowInfo.RealGetWindowClass;
            txtFontName.Text = windowInfo.FontName;
            txtWindowHandle.Text = string.Format("0x{0:X8}", windowInfo.Handle);
            txtParentWindowHandle.Text = string.Format("0x{0:X8}", windowInfo.ParentHandle);
            txtWindowSize.Text = string.Format("{0} x {1}", windowInfo.Size.Width, windowInfo.Size.Height);
            txtInstance.Text = string.Format("0x{0:X8}", windowInfo.Instance);
            txtProcessId.Text = string.Format("0x{0:X4}", windowInfo.ProcessId);
            txtThreadId.Text = string.Format("0x{0:X4}", windowInfo.ThreadId);
            txtGclWndProc.Text = string.Format("0x{0:X8}", windowInfo.GCL_WNDPROC);
            txtDwlDlgProc.Text = string.Format("0x{0:X8}", windowInfo.DWL_DLGPROC);
            txtGwlStyle.Text = string.Format("0x{0:X8}", windowInfo.GWL_STYLE);
            txtGclStyle.Text = string.Format("0x{0:X8}", windowInfo.GCL_STYLE);
            txtGwlExStyle.Text = string.Format("0x{0:X8}", windowInfo.GWL_EXSTYLE);
            txtWindowInfoExStyle.Text = string.Format("0x{0:X8}", windowInfo.WindowInfoExStyle);
            txtLwaAlpha.Text = windowInfo.LWA_ALPHA ? "+" : "-";
            txtLwaColorKey.Text = windowInfo.LWA_COLORKEY ? "+" : "-";
            txtGwlUserData.Text = string.Format("0x{0:X8}", windowInfo.GWL_USERDATA);
            txtDwlUser.Text = string.Format("0x{0:X8}", windowInfo.DWL_USER);
            txtFullPath.Text = windowInfo.FullPath;
            txtCommandLine.Text = windowInfo.CommandLine;
            txtStartedAt.Text = windowInfo.StartTime == null ? "" : windowInfo.StartTime.Value.ToString("dd.MM.yyyy HH:mm:ss");
            txtOwner.Text = windowInfo.Owner;
            txtParent.Text = windowInfo.Parent;
            txtPriority.Text = windowInfo.Priority.ToString();
            txtThreads.Text = windowInfo.ThreadCount.ToString();
            txtHandles.Text = windowInfo.HandleCount.ToString();
            txtWorkingSetSize.Text = ((decimal)windowInfo.WorkingSetSize).ToString("#,0", nfi);
            txtVirtualSize.Text = ((decimal)windowInfo.VirtualSize).ToString("#,0", nfi);
            txtProductName.Text = windowInfo.ProductName;
            txtCopyright.Text = windowInfo.Copyright;
            txtFileVersion.Text = windowInfo.FileVersion;
            txtProductVersion.Text = windowInfo.ProductVersion;
        }

        private void CloseClick(object sender, EventArgs e)
        {
            Close();
        }

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                Close();
            }
        }
    }
}
