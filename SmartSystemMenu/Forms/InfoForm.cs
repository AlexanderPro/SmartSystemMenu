using System.Windows.Forms;
using System.IO;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    partial class InfoForm : Form
    {
        public InfoForm(Window window, SmartSystemMenuSettings settings)
        {
            InitializeComponent();
            InitializeControls(window, settings);
        }

        private void InitializeControls(Window window, SmartSystemMenuSettings settings)
        {
            tabGeneral.Text = settings.LanguageSettings.GetValue("tab_general");
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
            txtHandleValue.Text = window.Handle.ToInt64().ToString("X8");
            txtCaptionValue.Text = window.WindowText;
            txtClassValue.Text = window.ClassName;
            lblStyleValue.Text = window.Style.ToString("X8");
            lblRectangleValue.Text = string.Format("({0},{1}) - ({2},{3})  -  {4}x{5}", window.Size.Left, window.Size.Top, window.Size.Right, window.Size.Bottom, window.Size.Width, window.Size.Height);
            lblProcessIdValue.Text = string.Format("{0:X8} ({0})", window.ProcessId);
            lblThreadIdValue.Text = string.Format("{0:X8} ({0})", window.ThreadId);
            var process = SystemUtils.GetProcessByIdSafely(window.ProcessId);
            if (process != null)
            {
                txtModuleNameValue.Text = Path.GetFileName(process.MainModule.FileName);
                txtModulePathValue.Text = process.MainModule.FileName;
            }
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
