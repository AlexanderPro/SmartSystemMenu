using System;
using System.Windows.Forms;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    partial class TransparencyForm : Form
    {
        public int WindowTransparency { get; set; }

        public TransparencyForm(Window window, ApplicationSettings settings)
        {
            InitializeComponent();
            InitializeControls(window, settings);

        }

        private void InitializeControls(Window window, ApplicationSettings settings)
        {
            btnApply.Text = settings.Language.GetValue("trans_btn_apply");
            Text = settings.Language.GetValue("trans_form");
            numericTransparency.Value = window.Transparency;
            DialogResult = DialogResult.Cancel;
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            WindowTransparency = (int)numericTransparency.Value;
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
