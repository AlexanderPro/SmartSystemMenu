using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Security.Cryptography;

namespace SmartSystemMenu.App_Code.Common
{
    static class SingleInstanceApplication
    {
        private static Mutex mutex;

        private static String AssemblyHash
        {
            get
            {
                String assemblyLocationString = Assembly.GetEntryAssembly().Location;
                Byte[] assemblyLocationArray = Encoding.UTF8.GetBytes(assemblyLocationString);
                Byte[] hashArray = new SHA512Managed().ComputeHash(assemblyLocationArray);
                String hashString = Convert.ToBase64String(hashArray);
                return hashString;
            }
        }

        public static Boolean Start()
        {
            Boolean onlyInstance = false;
            String mutexName = String.Format("Local\\{0}", AssemblyHash);

            // if you want your app to be limited to a single instance
            // across ALL SESSIONS (multiple users & terminal services), then use the following line instead:
            // String mutexName = String.Format("Global\\{0}", AssemblyGuid);

            mutex = new Mutex(true, mutexName, out onlyInstance);
            return onlyInstance;
        }

        public static void Stop()
        {
            mutex.ReleaseMutex();
        }
    }
}
