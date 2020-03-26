using System;
using System.Windows.Forms;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    partial class TransparencyForm : Form
    {
        private Window _window;

        public TransparencyForm(Window window, SmartSystemMenuSettings _settings)
        {
            InitializeComponent(_settings.MenuLanguage);
            _window = window;
            numericTransparency.Value = _window.Transparency;
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            try
            {
                var value = (Byte)numericTransparency.Value;
                _window.SetTrancparency(value);
                _window.Menu.UncheckTransparencyMenu();
                _window.Menu.CheckMenuItem(SystemMenu.SC_TRANS_CUSTOM, true);
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
