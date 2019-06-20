using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartSystemMenu
{
    static class NativeConstants
    {
        // Menus
        public const Int32 MF_UNCHECKED = 0x00000000;
        public const Int32 MF_STRING = 0x00000000;
        public const Int32 MF_GRAYED = 0x00000001;
        public const Int32 MF_DISABLED = 0x00000002;
        public const Int32 MF_CHECKED = 0x00000008;
        public const Int32 MF_POPUP = 0x00000010;
        public const Int32 MF_BYCOMMAND = 0x00000000;
        public const Int32 MF_BYPOSITION = 0x00000400;
        public const Int32 MF_SEPARATOR = 0x00000800;

        // GetWindow
        public const Int32 GW_HWNDFIRST = 0;
        public const Int32 GW_OWNER = 4;
        public const Int32 GW_CHILD = 5;

        // LayeredWindowAttributes
        public const Int32 LWA_COLORKEY = 0x00000001;
        public const Int32 LWA_ALPHA = 0x00000002;

        // WindowLong
        public const Int32 GWL_WNDPROC = -4;
        public const Int32 GWL_HINSTANCE = -6;
        public const Int32 GWL_HWNDPARENT = -8;
        public const Int32 GWL_STYLE = -16;
        public const Int32 GWL_EXSTYLE = -20;
        public const Int32 GWL_USERDATA = -21;
        public const Int32 GWL_ID = -12;

        // WindowStyle
        public const Int32 WS_EX_LAYERED = 0x00080000;
        public const Int32 WS_EX_TOPMOST = 0x00000008;

        // Window Messages
        public const Int32 WM_CREATE = 0x0001;
        public const Int32 WM_DESTROY = 0x0002;
        public const Int32 WM_MOVE = 0x0003;
        public const Int32 WM_SIZE = 0x0005;
        public const Int32 WM_ACTIVATE = 0x0006;
        public const Int32 WM_COMMAND = 0x0111;
        public const Int32 WM_SYSCOMMAND = 0x0112;
        public const Int32 WM_MENUCOMMAND = 0x0126;
        public const Int32 WM_MENUSELECT = 0x011F;
        public const Int32 WM_GETICON = 0x7F;
        public const Int32 WM_CLOSE = 0x0010;
        public const Int32 WM_NULL = 0x0000;

        public const UInt32 SWP_NOSIZE = 0x0001;
        public const UInt32 SWP_NOMOVE = 0x0002;
        public const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        public const Int32 ICON_SMALL = 0;
        public const Int32 ICON_BIG = 1;
        public const Int32 ICON_SMALL2 = 2;
        public const Int32 GCLP_HICON = -14;
        public const Int32 GCLP_HICONSM = -34;
        public const String IDI_APPLICATION = "#32512";

        public const Int32 SC_MINIMIZE = 0xF020;
        public const Int32 SC_MAXIMIZE = 0xF030;
        public const Int32 SC_RESTORE = 0xF120;
        public const Int32 SW_MAXIMIZE = 0x3;
        public const Int32 SW_MINIMIZE = 0x6;
        public const Int32 SW_Maxim = 0x6;

        public const Int32 MSGFLT_ADD = 0x1;

        public const Int32 HWND_BROADCAST = 0xffff;

        public const Int32 VK_SHIFT = 0x10;
        public const Int32 VK_CONTROL = 0x11;
        public const Int32 VK_DOWN = 0x28;

        public const Int32 MIIM_TYPE = 0x00000010;
        public const Int32 MFT_STRING = 0x00000000;

        public const Int32 PROCESS_SET_INFORMATION = 0x0200;
        public const Int32 PROCESS_QUERY_INFORMATION = 0x0400;
        public const Int32 PROCESS_QUERY_LIMITED_INFORMATION = 0x1000;
    }
}
