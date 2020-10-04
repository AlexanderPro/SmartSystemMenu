using System;
using System.Windows.Forms;

namespace SmartSystemMenu.Forms
{
    partial class AboutForm : Form
    {
        private const string URL = "https://github.com/AlexanderPro/SmartSystemMenu";

        public AboutForm()
        {
            InitializeComponent();
            Text = "About " + AssemblyUtils.AssemblyProductName;
            lblProductName.Text = string.Format("{0} v{1}", AssemblyUtils.AssemblyProductName, AssemblyUtils.AssemblyProductVersion);
            lblCopyright.Text = string.Format("{0}-{1} {2}", AssemblyUtils.AssemblyCopyright, DateTime.Now.Year, AssemblyUtils.AssemblyCompany);
            linkUrl.Text = URL;
        }

        private void CloseClick(object sender, EventArgs e)
        {
            Close();
        }

        private void LinkClick(object sender, EventArgs e)
        {
            try
            {
                SystemUtils.RunAsDesktopUser(SystemUtils.GetDefaultBrowserModuleName(), URL);
            }
            catch
            {
            }
        }

        private void KeyDownClick(object sender, KeyEventArgs e)
        {
            CloseClick(sender, e);
        }
    }
}
