using System;
using System.Windows.Forms;
using System.Diagnostics;
using SmartSystemMenu.Forms;
using SmartSystemMenu.Extensions;

namespace SmartSystemMenu
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Process.GetCurrentProcess().ExistProcessWithSameNameAndDesktop()) return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
