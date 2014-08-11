using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Win32;

namespace SmartSystemMenu.App_Code.Common
{
    static class AutoStarter
    {
        private const String RUN_LOCATION = @"Software\Microsoft\Windows\CurrentVersion\Run";

        public static void SetAutoStartByRegister(String keyName, String assemblyLocation)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(RUN_LOCATION);
            key.SetValue(keyName, assemblyLocation);
        }

        public static void UnsetAutoStartByRegister(String keyName)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(RUN_LOCATION);
            key.DeleteValue(keyName);
        }

        public static void SetAutoStartByScheduler(String keyName, String assemblyLocation)
        {
            String fileName = "schtasks.exe";
            String arguments = "/create /sc onlogon /tn \"{0}\" /rl highest /tr \"{1}\"";
            arguments = String.Format(arguments, keyName, assemblyLocation);
            Process scheduleProcess = new Process();
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

        public static void UnsetAutoStartByScheduler(String keyName)
        {
            String fileName = "schtasks.exe";
            String arguments = "/delete /tn \"{0}\" /f";
            arguments = String.Format(arguments, keyName);
            Process scheduleProcess = new Process();
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

        public static Boolean IsAutoStartByRegisterEnabled(String keyName, String assemblyLocation)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(RUN_LOCATION);
            if (key == null) return false;
            String value = (String)key.GetValue(keyName);
            if (String.IsNullOrEmpty(value)) return false;
            Boolean result = (value == assemblyLocation);
            return result;
        }
    }
}
