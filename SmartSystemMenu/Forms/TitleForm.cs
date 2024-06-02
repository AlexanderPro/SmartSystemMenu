using System;
using System.Windows.Forms;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    partial class TitleForm : Form
    {
        public string Title
        {
            get
            {
                return txtTitle.Text;
            }
            set
            {
                txtTitle.Text = value;
            }
        }

        public TitleForm(LanguageSettings settings)
        {
            InitializeComponent();
            InitializeControls(settings);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtTitle.Focus();
        }

        private void InitializeControls(LanguageSettings settings)
        {
            btnApply.Text = settings.GetValue("change_title_btn_apply");
            btnCancel.Text = settings.GetValue("change_title_btn_cancel");
            Text = settings.GetValue("change_title_form");
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
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
            if (e.KeyValue == 27)
            {
                ButtonCancelClick(sender, e);
            }
        }
    }
}