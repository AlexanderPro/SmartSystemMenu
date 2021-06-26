using System;
using System.Windows.Forms;
using System.Threading;
using SmartSystemMenu.Forms;

namespace SmartSystemMenu
{
    static class Program
    {
        private static Mutex _mutex;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if WIN32
            var mutexName = "SmartSystemMenuMutex";
#else
            var mutexName = "SmartSystemMenuMutex64";
#endif
            _mutex = new Mutex(false, mutexName, out var createNew);
            if (!createNew)
            {
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
