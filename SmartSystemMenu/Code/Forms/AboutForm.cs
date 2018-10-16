using System;
using System.Windows.Forms;
using SmartSystemMenu.Code.Common;

namespace SmartSystemMenu.Code.Forms
{
    partial class AboutForm : Form
    {
        private const String URL = "http://illarionov.pro";

        public AboutForm()
        {
            InitializeComponent();
            Text = "About " + AssemblyUtility.AssemblyProductName;
            lblProductName.Text = String.Format("{0} v{1}", AssemblyUtility.AssemblyProductName, AssemblyUtility.AssemblyVersion);
            lblCopyright.Text = String.Format("{0}-{1} {2}", AssemblyUtility.AssemblyCopyright, DateTime.Now.Year, AssemblyUtility.AssemblyCompany);
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
            CloseClick(sender, e);
        }
    }
}
