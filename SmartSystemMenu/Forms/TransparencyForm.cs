using System;
using System.Windows.Forms;
using SmartSystemMenu.Settings;
using SmartSystemMenu.Native;

namespace SmartSystemMenu.Forms
{
    partial class TransparencyForm : Form
    {
        private Window _window;

        private int WindowTransparency { get; set; }

        public TransparencyForm(Window window, ApplicationSettings settings)
        {
            InitializeComponent();
            InitializeControls(window, settings);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.WM_SYSCOMMAND)
            {
                var lowOrder = m.WParam.ToInt64() & 0x0000FFFF;
                if (lowOrder == MenuItemId.SC_CLOSE)
                {
                    ButtonCancelClick(this, EventArgs.Empty);
                }
            }

            base.WndProc(ref m);
        }

        private void InitializeControls(Window window, ApplicationSettings settings)
        {
            _window = window;
            btnApply.Text = settings.Language.GetValue("trans_btn_apply");
            Text = settings.Language.GetValue("trans_form");
            WindowTransparency = window.Transparency;
            numericTransparency.Value = WindowTransparency;
            DialogResult = DialogResult.Cancel;
            numericTransparency.TextChanged += NumericTransparencyValueChanged;
        }

        private void NumericTransparencyValueChanged(object sender, EventArgs e)
        {
            _window.SetTransparency((int)numericTransparency.Value);
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            WindowTransparency = (int)numericTransparency.Value;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            if (WindowTransparency != _window.Transparency)
            {
                _window.SetTransparency(WindowTransparency);
            }
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
                ButtonCancelClick(sender, e);
            }
        }
    }
}
