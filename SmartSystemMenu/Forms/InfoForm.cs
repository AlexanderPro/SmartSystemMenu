using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    partial class InfoForm : Form
    {
        private Window _window;

        public InfoForm(Window window, MenuLanguage menuLanguage)
        {
            _window = window;
            InitializeComponent(menuLanguage);
            InitializeControls();
        }

        private void InitializeControls()
        {
            txtHandleValue.Text = _window.Handle.ToInt64().ToString("X8");
            txtCaptionValue.Text = _window.WindowText;
            txtClassValue.Text = _window.ClassName;
            lblStyleValue.Text = _window.Style.ToString("X8");
            lblRectangleValue.Text = string.Format("({0},{1}) - ({2},{3})  -  {4}x{5}", _window.Size.Left, _window.Size.Top, _window.Size.Right, _window.Size.Bottom, _window.Size.Width, _window.Size.Height);
            lblProcessIdValue.Text = string.Format("{0:X8} ({0})", _window.ProcessId);
            lblThreadIdValue.Text = string.Format("{0:X8} ({0})", _window.ThreadId);
            var process = SystemUtils.GetProcessByIdSafely(_window.ProcessId);
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
