using System;
using System.Collections.Generic;
using System.Diagnostics;
using SmartSystemMenu.Extensions;

namespace SmartSystemMenu
{
    static class SystemUtils
    {
        public static bool IsWow64Process(int pId)
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) || Environment.OSVersion.Version.Major >= 6)
            {
                using (Process p = Process.GetProcessById(pId))
                {
                    bool retVal;
                    if (!NativeMethods.IsWow64Process(p.GetHandle(), out retVal))
                    {
                        return false;
                    }
                    return retVal;
                }
            }
            else
            {
                return false;
            }
        }

        public static IList<IntPtr> GetMonitors()
        {
            var monitors = new List<IntPtr>();
            NativeMethods.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, (IntPtr hMonitor, IntPtr hdcMonitor, ref Rect rect, IntPtr data) =>
            {
                monitors.Add(hMonitor);
                return true;
            }, IntPtr.Zero);
            return monitors;
        }
    }
}
