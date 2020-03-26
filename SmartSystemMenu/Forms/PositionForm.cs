using System;
using System.Windows.Forms;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    partial class PositionForm : Form
    {
        private Window _window;

        public PositionForm(Window window, SmartSystemMenuSettings _settins)
        {
            InitializeComponent(_settins.MenuLanguage);

            _window = window;
            numericLeft.Value = _window.SizeOnMonitor.Left;
            numericTop.Value = _window.SizeOnMonitor.Top;
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            try
            {
                int left = (int)numericLeft.Value;
                int top = (int)numericTop.Value;
                _window.ShowNormal();
                _window.SetPosition(left, top);
                _window.Menu.UncheckAlignmentMenu();
                _window.Menu.CheckMenuItem(SystemMenu.SC_ALIGN_CUSTOM, true);
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
