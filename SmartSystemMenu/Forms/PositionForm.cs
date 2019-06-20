using System;
using System.Windows.Forms;
using SmartSystemMenu;

namespace SmartSystemMenu.Forms
{
    partial class PositionForm : Form
    {
        private Window _window;

        public PositionForm(Window window)
        {
            InitializeComponent();

            _window = window;
            numericLeft.Value = _window.Size.Left;
            numericTop.Value = _window.Size.Top;
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
            switch (e.KeyValue)
            {
                case 13:
                    {
                        ButtonApplyClick(sender, (EventArgs)e);
                    }break;

                case 27:
                    {
                        Close();
                    } break;
            }
        }
    }
}
