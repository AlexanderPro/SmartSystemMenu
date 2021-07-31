using System;
using System.Windows.Forms;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    partial class SizeForm : Form
    {
        private Window _window;
        private SmartSystemMenuSettings _settings;

        public SizeForm(Window window, SmartSystemMenuSettings settings)
        {
            InitializeComponent();
            InitializeControls(settings.LanguageSettings);

            _window = window;
            _settings = settings;
            numericWidth.Value = _window.Size.Width;
            numericHeight.Value = _window.Size.Height;
        }

        private void InitializeControls(LanguageSettings settings)
        {
            lblWidth.Text = settings.GetValue("lbl_width");
            lblHeight.Text = settings.GetValue("lbl_height");
            btnApply.Text = settings.GetValue("size_btn_apply");
            Text = settings.GetValue("size_form");
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            try
            {
                int width = (int)numericWidth.Value;
                int height = (int)numericHeight.Value;
                _window.ShowNormal();

                if (_settings.Sizer == WindowSizerType.WindowWithMargins)
                {
                    _window.SetSize(width, height);
                }
                else if (_settings.Sizer == WindowSizerType.WindowWithoutMargins)
                {
                    var margins = _window.GetSystemMargins();
                    _window.SetSize(width + margins.Left + margins.Right, height + margins.Top + margins.Bottom);
                }
                else
                {
                    _window.SetSize(width + (_window.Size.Width - _window.ClientSize.Width), height + (_window.Size.Height - _window.ClientSize.Height));
                }

                _window.Menu.UncheckSizeMenu();
                _window.Menu.CheckMenuItem(MenuItemId.SC_SIZE_CUSTOM, true);
                _window.Menu.UncheckMenuItems(MenuItemId.SC_ROLLUP);
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
