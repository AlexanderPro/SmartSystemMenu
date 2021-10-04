using System;
using System.Windows.Forms;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    partial class PositionForm : Form
    {
        public int WindowLeft { get; private set; }

        public int WindowTop { get; private set; }

        public PositionForm(Window window, LanguageSettings settings)
        {
            InitializeComponent();
            InitializeControls(window, settings);
        }

        private void InitializeControls(Window window, LanguageSettings settings)
        {
            lblLeft.Text = settings.GetValue("lbl_left");
            lblTop.Text = settings.GetValue("lbl_top");
            btnApply.Text = settings.GetValue("align_btn_apply");
            Text = settings.GetValue("align_form");

            var left = window.Size.Left;
            var top = window.Size.Top;

            WindowLeft = left;
            WindowTop = top;

            txtLeft.Text = left.ToString();
            txtTop.Text = top.ToString();

            DialogResult = DialogResult.Cancel;
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

            WindowLeft = left;
            WindowTop = top;

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