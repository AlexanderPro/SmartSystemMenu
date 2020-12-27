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
            lblGetWindowText.Text = settings.LanguageSettings.GetValue("lbl_get_window_text");
            lblWmGetText.Text = settings.LanguageSettings.GetValue("lbl_wm_gettext");
            lblGetClassName.Text = settings.LanguageSettings.GetValue("lbl_get_class_name");
            lblRealGetWindowClass.Text = settings.LanguageSettings.GetValue("lbl_real_get_window_class");
            lblFontName.Text = settings.LanguageSettings.GetValue("lbl_font_name");
            lblWindowHandle.Text = settings.LanguageSettings.GetValue("lbl_window_handle");
            lblParentWindowHandle.Text = settings.LanguageSettings.GetValue("lbl_parent_window_handle");
            lblWindowSize.Text = settings.LanguageSettings.GetValue("lbl_window_size");
            lblProcessId.Text = settings.LanguageSettings.GetValue("lbl_process_id");
            lblThreadId.Text = settings.LanguageSettings.GetValue("lbl_thread_id");
            lblGclWndProc.Text = settings.LanguageSettings.GetValue("lbl_gcl_wnd_proc");
            lblDwlDlgProc.Text = settings.LanguageSettings.GetValue("lbl_dwl_dlg_proc");
            lblGwlStyle.Text = settings.LanguageSettings.GetValue("lbl_gwl_style");
            lblGclStyle.Text = settings.LanguageSettings.GetValue("lbl_gcl_style");
            lblGwlExStyle.Text = settings.LanguageSettings.GetValue("lbl_gwl_exstyle");
            lblWindowInfoExStyle.Text = settings.LanguageSettings.GetValue("lbl_windowinfo_exstyle");
            lblLwaAlpha.Text = settings.LanguageSettings.GetValue("lbl_lwa_alpha");
            lblLwaColorKey.Text = settings.LanguageSettings.GetValue("lbl_lwa_colorkey");
            lblGwlUserData.Text = settings.LanguageSettings.GetValue("lbl_gwl_userdata");
            lblDwlUser.Text = settings.LanguageSettings.GetValue("lbl_dwl_user");
            lblFullPath.Text = settings.LanguageSettings.GetValue("lbl_full_path");
            lblCommandLine.Text = settings.LanguageSettings.GetValue("lbl_command_line");
            lblStartedAt.Text = settings.LanguageSettings.GetValue("lbl_started_at");
            lblOwner.Text = settings.LanguageSettings.GetValue("lbl_owner");
            lblThreads.Text = settings.LanguageSettings.GetValue("lbl_threads");
            lblWorkingSetSize.Text = settings.LanguageSettings.GetValue("lbl_working_set_size");
            lblParent.Text = settings.LanguageSettings.GetValue("lbl_parent");
            lblPriority.Text = settings.LanguageSettings.GetValue("lbl_priority");
            lblHandles.Text = settings.LanguageSettings.GetValue("lbl_handles");
            lblVirtualSize.Text = settings.LanguageSettings.GetValue("lbl_virtual_size");
            lblProductName.Text = settings.LanguageSettings.GetValue("lbl_product_name");
            lblCopyright.Text = settings.LanguageSettings.GetValue("lbl_copyright");
            lblFileVersion.Text = settings.LanguageSettings.GetValue("lbl_file_version");
            lblProductVersion.Text = settings.LanguageSettings.GetValue("lbl_product_version");

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
