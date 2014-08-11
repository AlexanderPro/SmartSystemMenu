using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using SmartSystemMenu.App_Code.Common.Extensions;

namespace SmartSystemMenu.App_Code.Common
{
    static class PlatformUtility
    {
        public static Boolean IsWow64Process(Int32 pId)
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) || Environment.OSVersion.Version.Major >= 6)
            {
                using (Process p = Process.GetProcessById(pId))
                {
                    Boolean retVal;
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
