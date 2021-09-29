using System;
using System.Linq;
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
            InitializeControls(window, settings.LanguageSettings);

            _window = window;
            _settings = settings;
        }

        private void InitializeControls(Window window, LanguageSettings settings)
        {
            lblLeft.Text = settings.GetValue("lbl_size_form_left");
            lblTop.Text = settings.GetValue("lbl_size_form_top");
            lblWidth.Text = settings.GetValue("lbl_size_form_width");
            lblHeight.Text = settings.GetValue("lbl_size_form_height");
            btnApply.Text = settings.GetValue("size_btn_apply");
            Text = settings.GetValue("size_form");

            txtLeft.Text = window.Size.Left.ToString();
            txtTop.Text = window.Size.Top.ToString();
            txtWidth.Text = window.Size.Width.ToString();
            txtHeight.Text = window.Size.Height.ToString();
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            if (!int.TryParse(txtLeft.Text, out var left))
            {
                txtLeft.SelectAll();
                txtLeft.Focus();
                return;
            }

            if (!int.TryParse(txtTop.Text, out var top))
            {
                txtTop.SelectAll();
                txtTop.Focus();
                return;
            }

            if (!int.TryParse(txtWidth.Text, out var width))
            {
                txtWidth.SelectAll();
                txtWidth.Focus();
                return;
            }

            if (!int.TryParse(txtHeight.Text, out var height))
            {
                txtHeight.SelectAll();
                txtHeight.Focus();
                return;
            }

            _window.ShowNormal();

            if (_settings.Sizer == WindowSizerType.WindowWithMargins)
            {
                _window.SetSize(width, height, left, top);
            }
            else if (_settings.Sizer == WindowSizerType.WindowWithoutMargins)
            {
                var margins = _window.GetSystemMargins();
                _window.SetSize(width + margins.Left + margins.Right, height + margins.Top + margins.Bottom, left, top);
            }
            else
            {
                _window.SetSize(width + (_window.Size.Width - _window.ClientSize.Width), height + (_window.Size.Height - _window.ClientSize.Height), left, top);
            }

            var windowSizeMenuItemIds = _settings.MenuItems.WindowSizeItems.Select(x => x.Id).ToArray();
            _window.Menu.UncheckMenuItems(windowSizeMenuItemIds);
            _window.Menu.UncheckSizeMenu();
            _window.Menu.CheckMenuItem(MenuItemId.SC_SIZE_CUSTOM, true);
            _window.Menu.UncheckMenuItems(MenuItemId.SC_ROLLUP);
            Close();
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
