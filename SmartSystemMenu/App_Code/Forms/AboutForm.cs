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
    partial class AboutForm : Form
    {
        private const String URL = "http://illarionov.pro";

        public AboutForm()
        {
            InitializeComponent();
            Text = "About " + AssemblyUtility.AssemblyProductName;
            lblProductName.Text = String.Format("{0} v{1}", AssemblyUtility.AssemblyProductName, AssemblyUtility.AssemblyVersion);
            lblCopyright.Text = AssemblyUtility.AssemblyCopyright + " " + AssemblyUtility.AssemblyCompany;
            linkUrl.Text = URL;
        }

        private void CloseClick(object sender, EventArgs e)
        {
            Close();
        }

        private void LinkClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(URL);
        }

        private void KeyDownClick(object sender, KeyEventArgs e)
        {
            CloseClick(sender, (EventArgs)e);
        }
    }
}
