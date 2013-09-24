using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using SmartSystemMenu.App_Code.Hooks;
using SmartSystemMenu.App_Code.Common;

namespace SmartSystemMenu.App_Code.Forms
{
    partial class TestForm : Form
    {
        private Window window = null;

        public TestForm()
        {            
            InitializeComponent();
        }

        private void AddMenuClick(object sender, EventArgs e)
        {
            Int32 handle = Int32.Parse(txtWindowHandle.Text, System.Globalization.NumberStyles.AllowHexSpecifier, null);
            window = new Window(new IntPtr(handle));
        }

        private void RemoveMenuClick(object sender, EventArgs e)
        {
            window.Dispose();
        }

        private void AddRemoveMenuClick(object sender, EventArgs e)
        {
            //Window handle of calculator app
            IntPtr windowHandle = new IntPtr(0x331300);

            //Get handle of system menu
            IntPtr menuHandle = NativeMethods.GetSystemMenu(windowHandle, false);

            //Determines the number of items in the system menu
            Int32 menuItemCount = NativeMethods.GetMenuItemCount(menuHandle);

            //The identifier of the new menu item 
            Int32 menuItemId = 0x4747;

            //Insert a new menu item into the system menu
            NativeMethods.InsertMenu(menuHandle, menuItemCount, NativeMethods.MF_BYPOSITION, menuItemId, "Menu Item N");
            
            //Set break point to check calculator system menu
            //Delete created menu item from the system menu
            NativeMethods.DeleteMenu(menuHandle, menuItemCount, NativeMethods.MF_BYPOSITION);
        }

        private void ShowInfoClick(object sender, EventArgs e)
        {
            Int32 handle = Int32.Parse(txtWindowHandle.Text, System.Globalization.NumberStyles.AllowHexSpecifier, null);
            window = new Window(new IntPtr(handle));
            InfoForm infoForm = new InfoForm(window);
            infoForm.Show();
        }

        private void SetAutoStartByScheduler(String keyName, String assemblyLocation)
        {
            String fileName = "schtasks.exe";
            String arguments = "/create /sc onlogon /tn \"{0}\" /rl highest /tr \"{1}\"";
            arguments = String.Format(arguments, keyName, assemblyLocation);
            Process scheduleProcess = new Process();
            scheduleProcess.StartInfo.CreateNoWindow = true;
            scheduleProcess.StartInfo.UseShellExecute = false;
            //scheduleProcess.StartInfo.RedirectStandardOutput = true;
            //scheduleProcess.StartInfo.StandardErrorEncoding = Encoding.GetEncoding(866);
            //scheduleProcess.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
            scheduleProcess.StartInfo.FileName = fileName;
            scheduleProcess.StartInfo.Arguments = arguments;
            scheduleProcess.Start();
            //String output = scheduleProcess.StandardOutput.ReadToEnd();
            //String errorOutput = scheduleProcess.StandardError.ReadToEnd();
            if (!scheduleProcess.WaitForExit(30000))
            {
                scheduleProcess.Kill();
            }
        }

        private void UnsetAutoStartByScheduler(String keyName, String assemblyLocation)
        {
            String fileName = "schtasks.exe";
            String arguments = "/delete /tn \"{0}\" /f";
            arguments = String.Format(arguments, keyName);
            Process scheduleProcess = new Process();
            scheduleProcess.StartInfo.CreateNoWindow = true;
            scheduleProcess.StartInfo.UseShellExecute = false;
            //scheduleProcess.StartInfo.RedirectStandardOutput = true;
            //scheduleProcess.StartInfo.StandardErrorEncoding = Encoding.GetEncoding(866);
            //scheduleProcess.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
            scheduleProcess.StartInfo.FileName = fileName;
            scheduleProcess.StartInfo.Arguments = arguments;
            scheduleProcess.Start();
            //String output = scheduleProcess.StandardOutput.ReadToEnd();
            //String errorOutput = scheduleProcess.StandardError.ReadToEnd();
            if (!scheduleProcess.WaitForExit(30000))
            {
                scheduleProcess.Kill();
            }
        }

        private void ScheduleClick(object sender, EventArgs e)
        {
            SetAutoStartByScheduler("Calc", @"C:\Windows\System32\calc.exe");
        }
    }
}
