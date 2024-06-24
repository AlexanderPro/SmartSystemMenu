using System;
using System.Linq;
using System.Windows.Forms;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    partial class SizeForm : Form
    {
        public int? WindowLeft { get; private set; }

        public int? WindowTop { get; private set; }

        public int? WindowWidth { get; private set; }

        public int? WindowHeight { get; private set; }

        public SizeForm(Window window, ApplicationSettings settings)
        {
            InitializeComponent();
            InitializeControls(window, settings.Language);
        }

        private void InitializeControls(Window window, LanguageSettings settings)
        {
            lblLeft.Text = settings.GetValue("lbl_size_form_left");
            lblTop.Text = settings.GetValue("lbl_size_form_top");
            lblWidth.Text = settings.GetValue("lbl_size_form_width");
            lblHeight.Text = settings.GetValue("lbl_size_form_height");
            btnApply.Text = settings.GetValue("size_btn_apply");
            Text = settings.GetValue("size_form");

            var size = window.Size;

            WindowLeft = size.Left;
            WindowTop = size.Top;
            WindowWidth = size.Width;
            WindowHeight = size.Height;

            txtLeft.Text = size.Left.ToString();
            txtTop.Text = size.Top.ToString();
            txtWidth.Text = size.Width.ToString();
            txtHeight.Text = size.Height.ToString();

            DialogResult = DialogResult.Cancel;
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            WindowLeft = int.TryParse(txtLeft.Text, out var left) ? left : null;
            WindowTop = int.TryParse(txtTop.Text, out var top) ? top : null;
            WindowWidth = int.TryParse(txtWidth.Text, out var width) ? width : null;
            WindowHeight = int.TryParse(txtHeight.Text, out var height) ? height : null;
            DialogResult = DialogResult.OK;
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
