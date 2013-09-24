using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using SmartSystemMenu.App_Code.Forms;
using SmartSystemMenu.App_Code.Common;
using SmartSystemMenu.App_Code.Common.Extensions;

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

            String[] args = Environment.GetCommandLineArgs();
            for (Int32 i = 1; i < args.Length; i++)
            {
                if (String.Compare(args[i], "-debug", true) == 0)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new TestForm());
                    return;
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
