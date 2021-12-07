using System;
using System.Windows.Forms;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    partial class ParameterForm : Form
    {
        public string ParameterValue { get; private set; }

        public ParameterForm(string parameter, LanguageSettings settings)
        {
            InitializeComponent();
            InitializeControls(parameter, settings);
        }

        private void InitializeControls(string parameter, LanguageSettings settings)
        {
            lblParameter.Text = parameter;
            btnApply.Text = settings.GetValue("parameter_btn_apply");
            Text = settings.GetValue("parameter_form");
            DialogResult = DialogResult.Cancel;
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            ParameterValue = txtParameterValue.Text;
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