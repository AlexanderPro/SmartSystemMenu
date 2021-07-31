using System;
using System.Windows.Forms;
using System.Globalization;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    partial class InfoForm : Form
    {
        public InfoForm(WindowInfo windowInfo, LanguageSettings settings)
        {
            InitializeComponent();
            InitializeControls(windowInfo, settings);
        }

        private void InitializeControls(WindowInfo windowInfo, LanguageSettings settings)
        {
            grpWindow.Text = settings.GetValue("grp_window");
            grpProcess.Text = settings.GetValue("grp_process");
            lblGetWindowText.Text = settings.GetValue("lbl_get_window_text");
            lblWmGetText.Text = settings.GetValue("lbl_wm_gettext");
            lblGetClassName.Text = settings.GetValue("lbl_get_class_name");
            lblRealGetWindowClass.Text = settings.GetValue("lbl_real_get_window_class");
            lblFontName.Text = settings.GetValue("lbl_font_name");
            lblWindowHandle.Text = settings.GetValue("lbl_window_handle");
            lblParentWindowHandle.Text = settings.GetValue("lbl_parent_window_handle");
            lblWindowSize.Text = settings.GetValue("lbl_window_size");
            lblInstance.Text = settings.GetValue("lbl_instance");
            lblProcessId.Text = settings.GetValue("lbl_process_id");
            lblThreadId.Text = settings.GetValue("lbl_thread_id");
            lblGclWndProc.Text = settings.GetValue("lbl_gcl_wnd_proc");
            lblDwlDlgProc.Text = settings.GetValue("lbl_dwl_dlg_proc");
            lblGwlStyle.Text = settings.GetValue("lbl_gwl_style");
            lblGclStyle.Text = settings.GetValue("lbl_gcl_style");
            lblGwlExStyle.Text = settings.GetValue("lbl_gwl_exstyle");
            lblWindowInfoExStyle.Text = settings.GetValue("lbl_windowinfo_exstyle");
            lblLwaAlpha.Text = settings.GetValue("lbl_lwa_alpha");
            lblLwaColorKey.Text = settings.GetValue("lbl_lwa_colorkey");
            lblGwlUserData.Text = settings.GetValue("lbl_gwl_userdata");
            lblDwlUser.Text = settings.GetValue("lbl_dwl_user");
            lblFullPath.Text = settings.GetValue("lbl_full_path");
            lblCommandLine.Text = settings.GetValue("lbl_command_line");
            lblStartedAt.Text = settings.GetValue("lbl_started_at");
            lblOwner.Text = settings.GetValue("lbl_owner");
            lblThreads.Text = settings.GetValue("lbl_threads");
            lblWorkingSetSize.Text = settings.GetValue("lbl_working_set_size");
            lblParent.Text = settings.GetValue("lbl_parent");
            lblPriority.Text = settings.GetValue("lbl_priority");
            lblHandles.Text = settings.GetValue("lbl_handles");
            lblVirtualSize.Text = settings.GetValue("lbl_virtual_size");
            lblProductName.Text = settings.GetValue("lbl_product_name");
            lblCopyright.Text = settings.GetValue("lbl_copyright");
            lblFileVersion.Text = settings.GetValue("lbl_file_version");
            lblProductVersion.Text = settings.GetValue("lbl_product_version");
            btnOk.Text = settings.GetValue("information_btn_apply");
            Text = settings.GetValue("information");

            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone(); ;
            nfi.NumberGroupSeparator = ",";
            txtGetWindowText.Text = windowInfo.GetWindowText;
            txtWmGetText.Text = windowInfo.WM_GETTEXT;
            txtGetClassName.Text = windowInfo.GetClassName;
            txtRealGetWindowClass.Text = windowInfo.RealGetWindowClass;
            txtFontName.Text = windowInfo.FontName;
            txtWindowHandle.Text = string.Format("0x{0:X}", windowInfo.Handle.ToInt64());
            txtParentWindowHandle.Text = string.Format("0x{0:X}", windowInfo.ParentHandle.ToInt64());
            txtWindowSize.Text = string.Format("{0}x{1}  ({2}x{3})", windowInfo.Size.Width, windowInfo.Size.Height, windowInfo.ClientSize.Width, windowInfo.ClientSize.Height);
            txtInstance.Text = string.Format("0x{0:X}", windowInfo.Instance.ToInt64());
            txtProcessId.Text = string.Format("0x{0:X} ({0})", windowInfo.ProcessId);
            txtThreadId.Text = string.Format("0x{0:X} ({0})", windowInfo.ThreadId);
            txtGclWndProc.Text = string.Format("0x{0:X}", windowInfo.GCL_WNDPROC);
            txtDwlDlgProc.Text = string.Format("0x{0:X}", windowInfo.DWL_DLGPROC);
            txtGwlStyle.Text = string.Format("0x{0:X}", windowInfo.GWL_STYLE);
            txtGclStyle.Text = string.Format("0x{0:X}", windowInfo.GCL_STYLE);
            txtGwlExStyle.Text = string.Format("0x{0:X}", windowInfo.GWL_EXSTYLE);
            txtWindowInfoExStyle.Text = string.Format("0x{0:X}", windowInfo.WindowInfoExStyle);
            txtLwaAlpha.Text = windowInfo.LWA_ALPHA ? "+" : "-";
            txtLwaColorKey.Text = windowInfo.LWA_COLORKEY ? "+" : "-";
            txtGwlUserData.Text = string.Format("0x{0:X}", windowInfo.GWL_USERDATA);
            txtDwlUser.Text = string.Format("0x{0:X}", windowInfo.DWL_USER);
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
