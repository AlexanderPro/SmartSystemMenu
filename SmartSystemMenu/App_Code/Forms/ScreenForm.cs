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
    partial class ScreenForm : Form
    {
        private Window _window;

        public ScreenForm(Window window)
        {
            InitializeComponent();
            _window = window;
            Object[] screenIds = Enumerable.Range(0, Screen.AllScreens.Length).Cast<Object>().ToArray();
            cmbScreen.Items.AddRange(screenIds);
            cmbScreen.SelectedItem = window.ScreenId;
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            try
            {
                Int32 screenId = Int32.Parse(cmbScreen.SelectedItem.ToString());
                _window.ScreenId = screenId;
                _window.Menu.SetMenuItemText(SystemMenu.SC_ALIGN_MONITOR, "Select Monitor: " + screenId);
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
