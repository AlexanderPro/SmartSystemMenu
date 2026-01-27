using System;
using System.IO;
using System.Windows.Forms;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    public partial class StartProgramForm : Form
    {
        private readonly LanguageSettings _settings;

        public StartProgramMenuItem MenuItem { get; private set; }

        public StartProgramForm(StartProgramMenuItem menuItem, LanguageSettings settings)
        {
            _settings = settings;

            InitializeComponent();
            InitializeControls(menuItem, settings);
        }

        private void InitializeControls(StartProgramMenuItem menuItem, LanguageSettings settings)
        {
            lblTitle.Text = settings.GetValue("start_program_lbl_title");
            btnApply.Text = settings.GetValue("start_program_btn_apply");
            btnCancel.Text = settings.GetValue("start_program_btn_cancel");
            lblFileName.Text = settings.GetValue("start_program_lbl_file_name");
            lblArguments.Text = settings.GetValue("start_program_lbl_arguments");
            lblRunAs.Text = settings.GetValue("start_program_lbl_runas");
            lblBegin.Text = settings.GetValue("start_program_lbl_begin");
            lblEnd.Text = settings.GetValue("start_program_lbl_end");
            chkShowWindow.Text = settings.GetValue("start_program_show_window");
            chkUseWindowWorkingDirectory.Text = settings.GetValue("start_program_use_window_working_directory");
            Text = settings.GetValue("start_program_form");
            cmbRunAs.Items.Clear();
            cmbRunAs.Items.Add(settings.GetValue("start_program_normal"));
            cmbRunAs.Items.Add(settings.GetValue("start_program_administrator"));
            if (menuItem == null)
            {
                cmbRunAs.SelectedIndex = 0;
            }
            else
            {
                txtTitle.Text = menuItem.Title;
                txtFileName.Text = menuItem.FileName;
                txtArguments.Text = menuItem.Arguments;
                txtBegin.Text = menuItem.BeginParameter;
                txtEnd.Text = menuItem.EndParameter;
                txtParameter.Text = $"{menuItem.BeginParameter}{settings.GetValue("start_program_parameter")}{menuItem.EndParameter}";
                chkShowWindow.Checked = menuItem.ShowWindow;
                chkUseWindowWorkingDirectory.Checked = menuItem.UseWindowWorkingDirectory;
                cmbRunAs.SelectedIndex = menuItem.RunAs == UserType.Normal ? 0 : 1;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            txtTitle.SelectAll();
            txtTitle.Focus();
        }

        private void BeginParameterTextChanged(object sender, EventArgs e)
        {
            txtParameter.Text = $"{txtBegin.Text}{_settings.GetValue("start_program_parameter")}{txtEnd.Text}";
        }

        private void EndParameterTextChanged(object sender, EventArgs e)
        {
            txtParameter.Text = $"{txtBegin.Text}{_settings.GetValue("start_program_parameter")}{txtEnd.Text}";
        }

        private void ButtonBrowseFileClick(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                RestoreDirectory = true,
                Filter = _settings.GetValue("start_program_browse_file_filter")
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
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                txtTitle.SelectAll();
                txtTitle.Focus();
                return;
            }

            MenuItem = new StartProgramMenuItem
            {
                Title = txtTitle.Text,
                FileName = txtFileName.Text,
                Arguments = txtArguments.Text,
                BeginParameter = txtBegin.Text,
                EndParameter = txtEnd.Text,
                ShowWindow = chkShowWindow.Checked,
                UseWindowWorkingDirectory = chkUseWindowWorkingDirectory.Checked,
                RunAs = cmbRunAs.SelectedIndex == 0 ? UserType.Normal : UserType.Administrator
            };
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