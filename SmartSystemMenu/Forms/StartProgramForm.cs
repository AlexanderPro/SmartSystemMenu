using System;
using System.IO;
using System.Windows.Forms;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    public partial class StartProgramForm : Form
    {
        public string Title { get; private set; }

        public string FileName { get; private set; }

        public string Arguments { get; private set; }

        public StartProgramForm(string title, string processName, string arguments, MenuLanguage menuLanguage)
        {
            _menuLanguage = menuLanguage;
            InitializeComponent();
            txtTitle.Text = title;
            txtFileName.Text = processName;
            txtArguments.Text = arguments;
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
                Filter = _menuLanguage.GetStringValue("start_program_browse_file_filter")
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