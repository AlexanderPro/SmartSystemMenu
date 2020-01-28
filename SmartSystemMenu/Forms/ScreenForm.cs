using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SmartSystemMenu.Forms
{
    partial class ScreenForm : Form
    {
        private Window _window;

        public ScreenForm(Window window)
        {
            InitializeComponent();
            _window = window;
            var screenIds = Enumerable.Range(0, Screen.AllScreens.Length).Cast<Object>().ToArray();
            cmbScreen.Items.AddRange(screenIds);
            cmbScreen.SelectedItem = window.ScreenId;
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            try
            {
                var screenId = int.Parse(cmbScreen.SelectedItem.ToString());
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
