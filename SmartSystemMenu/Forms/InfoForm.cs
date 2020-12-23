using System;
using System.Windows.Forms;
using System.IO;
using System.Text;
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
            lblModuleName.Text = settings.LanguageSettings.GetValue("lbl_module_name");
            Text = settings.LanguageSettings.GetValue("information");
            txtHandleValue.Text = windowInfo.Handle.ToInt64().ToString("X8");
            txtCaptionValue.Text = windowInfo.GetWindowText;
            txtClassValue.Text = windowInfo.GetClassName;
            lblStyleValue.Text = windowInfo.GWL_STYLE.ToString("X8");
            lblRectangleValue.Text = string.Format("({0},{1}) - ({2},{3})  -  {4}x{5}", windowInfo.Size.Left, windowInfo.Size.Top, windowInfo.Size.Right, windowInfo.Size.Bottom, windowInfo.Size.Width, windowInfo.Size.Height);
            lblProcessIdValue.Text = string.Format("{0:X8} ({0})", windowInfo.ProcessId);
            lblThreadIdValue.Text = string.Format("{0:X8} ({0})", windowInfo.ThreadId);
            var process = SystemUtils.GetProcessByIdSafely(windowInfo.ProcessId);
            if (process != null)
            {
                txtModuleNameValue.Text = Path.GetFileName(process.MainModule.FileName);
                txtModulePathValue.Text = process.MainModule.FileName;
            }*/
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
