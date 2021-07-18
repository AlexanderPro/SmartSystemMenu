using System;
using System.Windows.Forms;
using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    partial class AboutForm : Form
    {
        private const string URL_SMART_SYSTEM_MENU = "https://github.com/AlexanderPro/SmartSystemMenu";
        private const string URL_LIGHT_APIS = "https://github.com/LightAPIs";
        private const string URL_WENGH = "https://github.com/wengh";
        private const string URL_JAEHYUNG_LEE = "http://www.kolanp.com";

        public AboutForm(LanguageSettings settings)
        {
            InitializeComponent();
            btnOk.Text = settings.GetValue("about_btn_ok");
            Text = settings.GetValue("about_form") + AssemblyUtils.AssemblyProductName;
            lblProductName.Text = string.Format("{0} v{1}", AssemblyUtils.AssemblyProductName, AssemblyUtils.AssemblyProductVersion);
            lblCopyright.Text = string.Format("{0}-{1} {2}", AssemblyUtils.AssemblyCopyright, DateTime.Now.Year, AssemblyUtils.AssemblyCompany);
            linkUrl.Text = URL_SMART_SYSTEM_MENU;
        }

        private void CloseClick(object sender, EventArgs e)
        {
            Close();
        }

        private void LinkClick(object sender, EventArgs e)
        {
            try
            {
                var controlName = ((LinkLabel)sender).Name;
                SystemUtils.RunAsDesktopUser(SystemUtils.GetDefaultBrowserModuleName(),
                    controlName == "linkLightAPIs" ? URL_LIGHT_APIS :
                    controlName == "linkWengh" ? URL_WENGH :
                    controlName == "linkJaehyungLee" ? URL_JAEHYUNG_LEE :
                    URL_SMART_SYSTEM_MENU);
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
