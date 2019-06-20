using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmartSystemMenu;

namespace SmartSystemMenu.Forms
{
    partial class SizeForm : Form
    {
        private Window _window;

        public SizeForm(Window window)
        {
            InitializeComponent();

            _window = window;
            numericWidth.Value = _window.Size.Width;
            numericHeight.Value = _window.Size.Height;
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            try
            {
                int width = (int)numericWidth.Value;
                int height = (int)numericHeight.Value;
                _window.ShowNormal();
                _window.SetSize(width, height);
                _window.Menu.UncheckSizeMenu();
                _window.Menu.CheckMenuItem(SystemMenu.SC_SIZE_CUSTOM, true);
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
