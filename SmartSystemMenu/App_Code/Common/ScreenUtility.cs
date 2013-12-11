using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartSystemMenu.App_Code.Common
{
    static class ScreenUtility
    {
        public static Int32 PrimaryScreenId
        {
            get
            {
                Int32 screenId = 0;
                for (Int32 i = 0; i < Screen.AllScreens.Length; i++)
                {
                    if (Screen.AllScreens[i].Primary)
                    {
                        screenId = i;
                        break;
                    }
                }
                return screenId;
            }
        }
    }
}
