using System.Diagnostics;
using Microsoft.Win32;

namespace SmartSystemMenu
{
    static class AutoStarter
    {
        private const string RUN_LOCATION = @"Software\Microsoft\Windows\CurrentVersion\Run";

        public static void SetAutoStartByRegister(string keyName, string assemblyLocation)
        {
            using var key = Registry.CurrentUser.CreateSubKey(RUN_LOCATION);
            key.SetValue(keyName, assemblyLocation);
        }

        public static void UnsetAutoStartByRegister(string keyName)
        {
            using var key = Registry.CurrentUser.CreateSubKey(RUN_LOCATION);
            key.DeleteValue(keyName);
        }

        public static void SetAutoStartByScheduler(string keyName, string assemblyLocation)
        {
            var fileName = "schtasks.exe";
            var arguments = "/create /sc onlogon /tn \"{0}\" /rl highest /tr \"{1}\"";
            arguments = string.Format(arguments, keyName, assemblyLocation);
            var scheduleProcess = new Process();
            scheduleProcess.StartInfo.CreateNoWindow = true;
            scheduleProcess.StartInfo.UseShellExecute = false;
            scheduleProcess.StartInfo.FileName = fileName;
            scheduleProcess.StartInfo.Arguments = arguments;
            scheduleProcess.Start();
            if (!scheduleProcess.WaitForExit(30000))
            {
                scheduleProcess.Kill();
            }
        }

        public static void UnsetAutoStartByScheduler(string keyName)
        {
            var fileName = "schtasks.exe";
            var arguments = "/delete /tn \"{0}\" /f";
            arguments = string.Format(arguments, keyName);
            var scheduleProcess = new Process();
            scheduleProcess.StartInfo.CreateNoWindow = true;
            scheduleProcess.StartInfo.UseShellExecute = false;
            scheduleProcess.StartInfo.FileName = fileName;
            scheduleProcess.StartInfo.Arguments = arguments;
            scheduleProcess.Start();
            if (!scheduleProcess.WaitForExit(30000))
            {
                scheduleProcess.Kill();
            }
        }

        public static bool IsAutoStartByRegisterEnabled(string keyName, string assemblyLocation)
        {
            using var key = Registry.CurrentUser.OpenSubKey(RUN_LOCATION);
            if (key == null) return false;
            var value = (string)key.GetValue(keyName);
            if (string.IsNullOrEmpty(value)) return false;
            var result = (value == assemblyLocation);
            return result;
        }
    }
}
