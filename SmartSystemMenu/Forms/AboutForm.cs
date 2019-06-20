using System;
using System.Windows.Forms;

namespace SmartSystemMenu.Forms
{
    partial class AboutForm : Form
    {
        private const string URL = "http://illarionov.pro";

        public AboutForm()
        {
            InitializeComponent();
            Text = "About " + AssemblyUtils.AssemblyProductName;
            lblProductName.Text = string.Format("{0} v{1}", AssemblyUtils.AssemblyProductName, AssemblyUtils.AssemblyVersion);
            lblCopyright.Text = string.Format("{0}-{1} {2}", AssemblyUtils.AssemblyCopyright, DateTime.Now.Year, AssemblyUtils.AssemblyCompany);
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
