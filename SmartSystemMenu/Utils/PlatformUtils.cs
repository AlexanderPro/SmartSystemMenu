using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using SmartSystemMenu.Extensions;

namespace SmartSystemMenu
{
    static class PlatformUtils
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
    }
}
