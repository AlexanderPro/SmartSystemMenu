using System;
using System.Windows.Forms;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    partial class SizeForm : Form
    {
        private Window _window;

        public SizeForm(Window window, SmartSystemMenuSettings settings)
        {
            InitializeComponent();
            InitializeControls(settings);

            _window = window;
            numericWidth.Value = _window.Size.Width;
            numericHeight.Value = _window.Size.Height;
        }

        private void InitializeControls(SmartSystemMenuSettings settings)
        {
            lblWidth.Text = settings.LanguageSettings.GetValue("lbl_width");
            lblHeight.Text = settings.LanguageSettings.GetValue("lbl_height");
            btnApply.Text = settings.LanguageSettings.GetValue("size_btn_apply");
            Text = settings.LanguageSettings.GetValue("size_form");
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            try
            {
                int width = (int)numericWidth.Value;
                int height = (int)numericHeight.Value;
                _window.ShowNormal();
                _window.SetSize(width, height);
                _window.Menu.UncheckSizeMenu();
                _window.Menu.CheckMenuItem(SystemMenu.SC_SIZE_CUSTOM, true);
                _window.Menu.UncheckMenuItems(SystemMenu.SC_ROLLUP);
            }
            catch
            {
            }
            finally
            {
                Close();
            }
        }

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ButtonApplyClick(sender, e);
            }

            if (e.KeyValue == 27)
            {
                Close();
            }
        }
    }
}
