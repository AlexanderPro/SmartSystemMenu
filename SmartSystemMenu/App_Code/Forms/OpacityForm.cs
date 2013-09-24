using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmartSystemMenu.App_Code.Common;

namespace SmartSystemMenu.App_Code.Forms
{
    partial class OpacityForm : Form
    {
        private Window _window;

        public OpacityForm(Window window)
        {
            InitializeComponent();
            _window = window;           
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            try
            {
                Byte value = (Byte)numericOpacity.Value;
                _window.SetTrancparencyByPercent(value);
                _window.Menu.UncheckTransparencyMenu();
                _window.Menu.CheckTransparencyMenuItem(SystemMenu.SC_TRANS_CUSTOM, true);
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
