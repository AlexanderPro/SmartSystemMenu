using System;
using System.Windows.Forms;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    partial class TransparencyForm : Form
    {
        private Window _window;

        public TransparencyForm(Window window, SmartSystemMenuSettings settings)
        {
            InitializeComponent();
            InitializeControls(settings);

            _window = window;
            numericTransparency.Value = _window.Transparency;
        }

        private void InitializeControls(SmartSystemMenuSettings settings)
        {
            btnApply.Text = settings.LanguageSettings.GetValue("trans_btn_apply");
            Text = settings.LanguageSettings.GetValue("trans_form");
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            try
            {
                var value = (Byte)numericTransparency.Value;
                _window.SetTrancparency(value);
                _window.Menu.UncheckTransparencyMenu();
                _window.Menu.CheckMenuItem(MenuItemId.SC_TRANS_CUSTOM, true);
            }
            catch
            {
            }
            finally
            {
                Close();
            }
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
