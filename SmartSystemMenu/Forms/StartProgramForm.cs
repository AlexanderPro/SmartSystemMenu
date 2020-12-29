using System;
using System.IO;
using System.Windows.Forms;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    public partial class StartProgramForm : Form
    {
        private readonly SmartSystemMenuSettings _settings;

        public string Title { get; private set; }

        public string FileName { get; private set; }

        public string Arguments { get; private set; }

        public StartProgramForm(string title, string processName, string arguments, SmartSystemMenuSettings settings)
        {
            _settings = settings;

            InitializeComponent();
            InitializeControls(settings);

            txtTitle.Text = title;
            txtFileName.Text = processName;
            txtArguments.Text = arguments;
        }

        private void InitializeControls(SmartSystemMenuSettings settings)
        {
            lblTitle.Text = settings.LanguageSettings.GetValue("start_program_lbl_title");
            btnApply.Text = settings.LanguageSettings.GetValue("start_program_btn_apply");
            btnCancel.Text = settings.LanguageSettings.GetValue("start_program_btn_Cancel");
            lblFileName.Text = settings.LanguageSettings.GetValue("start_program_lbl_file_name");
            lblArguments.Text = settings.LanguageSettings.GetValue("start_program_lbl_arguments");
            Text = settings.LanguageSettings.GetValue("start_program_form");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            txtTitle.SelectAll();
            txtTitle.Focus();
        }

        private void ButtonBrowseFileClick(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                RestoreDirectory = true,
                Filter = _settings.LanguageSettings.GetValue("start_program_browse_file_filter")
            };

            if (File.Exists(txtFileName.Text))
            {
                dialog.InitialDirectory = Path.GetDirectoryName(txtFileName.Text);
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = dialog.FileName;
            }
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            Title = txtTitle.Text;
            FileName = txtFileName.Text;
            Arguments = txtArguments.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void KeyDownClick(object sender, KeyEventArgs e)
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