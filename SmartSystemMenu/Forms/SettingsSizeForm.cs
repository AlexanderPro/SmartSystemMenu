using System;
using System.Windows.Forms;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    partial class SettingsSizeForm : Form
    {
        public string Title { get; private set; }

        public int WindowWidth { get; private set; }

        public int WindowHeight { get; private set; }

        public SettingsSizeForm(string title, int width, int height, LanguageSettings settings)
        {

            InitializeComponent();
            InitializeControls(settings);

            txtTitle.Text = title;
            numericWidth.Value = width;
            numericHeight.Value = height;

            Title = title;
            WindowWidth = width;
            WindowHeight = height;
        }

        private void InitializeControls(LanguageSettings settings)
        {
            lblTitle.Text = settings.GetValue("lbl_window_size_title");
            lblWidth.Text = settings.GetValue("lbl_window_size_width");
            lblHeight.Text = settings.GetValue("lbl_window_size_height");
            btnApply.Text = settings.GetValue("window_size_btn_apply");
            btnCancel.Text = settings.GetValue("window_size_btn_cancel");
            Text = settings.GetValue("window_size_form");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            txtTitle.SelectAll();
            txtTitle.Focus();
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                txtTitle.SelectAll();
                txtTitle.Focus();
                return;
            }
            Title = txtTitle.Text;
            WindowWidth = (int)numericWidth.Value;
            WindowHeight = (int)numericHeight.Value;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
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
