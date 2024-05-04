using System;
using System.Windows.Forms;
using System.Globalization;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    partial class InformationForm : Form
    {
        public InformationForm(WindowDetails windowInfo, LanguageSettings settings)
        {
            InitializeComponent();
            InitializeControls(windowInfo, settings);
        }

        private void InitializeControls(WindowDetails windowDetails, LanguageSettings settings)
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
            lblWindowPosition.Text = settings.GetValue("lbl_window_position");
            lblWindowSize.Text = settings.GetValue("lbl_window_size");
            lblExtendedFrameBounds.Text = settings.GetValue("lbl_extended_frame_bounds");
            lblInstance.Text = settings.GetValue("lbl_instance");
            lblProcessId.Text = settings.GetValue("lbl_process_id");
            lblThreadId.Text = settings.GetValue("lbl_thread_id");
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
            txtGetWindowText.Text = windowDetails.GetWindowText;
            txtWmGetText.Text = windowDetails.WM_GETTEXT;
            txtGetClassName.Text = windowDetails.GetClassName;
            txtRealGetWindowClass.Text = windowDetails.RealGetWindowClass;
            txtFontName.Text = windowDetails.FontName;
            txtWindowHandle.Text = $"0x{windowDetails.Handle.ToInt64():X}";
            txtParentWindowHandle.Text = $"0x{windowDetails.ParentHandle.ToInt64():X}";
            txtWindowPosition.Text = $"{windowDetails.Size.Left}, {windowDetails.Size.Top}";
            txtWindowSize.Text = $"{windowDetails.Size.Width}x{windowDetails.Size.Height}  ({windowDetails.ClientSize.Width}x{windowDetails.ClientSize.Height})";
            txtInstance.Text = $"0x{windowDetails.Instance.ToInt64():X}";
            txtProcessId.Text = $"0x{windowDetails.ProcessId:X} ({windowDetails.ProcessId})";
            txtThreadId.Text = $"0x{windowDetails.ThreadId:X} ({windowDetails.ThreadId})";
            txtExtendedFrameBounds.Text = $"{windowDetails.FrameBounds.Top} {windowDetails.FrameBounds.Right} {windowDetails.FrameBounds.Bottom} {windowDetails.FrameBounds.Left}";
            txtGwlStyle.Text = $"0x{windowDetails.GWL_STYLE:X}";
            txtGclStyle.Text = $"0x{windowDetails.GCL_STYLE:X}";
            txtGwlExStyle.Text = $"0x{windowDetails.GWL_EXSTYLE:X}";
            txtWindowInfoExStyle.Text = $"0x{windowDetails.WindowInfoExStyle:X}";
            txtLwaAlpha.Text = windowDetails.LWA_ALPHA ? "+" : "-";
            txtLwaColorKey.Text = windowDetails.LWA_COLORKEY ? "+" : "-";
            txtGwlUserData.Text = $"0x{windowDetails.GWL_USERDATA:X}";
            txtDwlUser.Text = $"0x{windowDetails.DWL_USER:X}";
            txtFullPath.Text = windowDetails.FullPath;
            txtCommandLine.Text = windowDetails.CommandLine;
            txtStartedAt.Text = windowDetails.StartTime == null ? string.Empty : windowDetails.StartTime.Value.ToString("dd.MM.yyyy HH:mm:ss");
            txtOwner.Text = windowDetails.Owner;
            txtParent.Text = windowDetails.Parent;
            txtPriority.Text = windowDetails.Priority.ToString();
            txtThreads.Text = windowDetails.ThreadCount.ToString();
            txtHandles.Text = windowDetails.HandleCount.ToString();
            txtWorkingSetSize.Text = ((decimal)windowDetails.WorkingSetSize).ToString("#,0", nfi);
            txtVirtualSize.Text = ((decimal)windowDetails.VirtualSize).ToString("#,0", nfi);
            txtProductName.Text = windowDetails.ProductName;
            txtCopyright.Text = windowDetails.Copyright;
            txtFileVersion.Text = windowDetails.FileVersion;
            txtProductVersion.Text = windowDetails.ProductVersion;
        }

        private void CloseClick(object sender, EventArgs e) => Close();

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                Close();
            }
        }
    }
}
