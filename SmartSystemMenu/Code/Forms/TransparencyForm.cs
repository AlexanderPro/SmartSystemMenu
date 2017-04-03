using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmartSystemMenu.Code.Common;

namespace SmartSystemMenu.Code.Forms
{
    partial class TransparencyForm : Form
    {
        private Window _window;

        public TransparencyForm(Window window)
        {
            InitializeComponent();
            _window = window;
            numericTransparency.Value = _window.Transparency;
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            try
            {
                Byte value = (Byte)numericTransparency.Value;
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
            switch (e.KeyValue)
            {
                case 13:
                    {
                        ButtonApplyClick(sender, (EventArgs)e);
                    } break;

                case 27:
                    {
                        Close();
                    } break;
            }
        }
    }
}
