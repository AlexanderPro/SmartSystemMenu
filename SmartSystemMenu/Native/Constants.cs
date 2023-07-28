namespace SmartSystemMenu.Native
{
    static class Constants
    {
        // Menus
        public const int MF_UNCHECKED = 0x00000000;
        public const int MF_STRING = 0x00000000;
        public const int MF_ENABLED = 0x00000000;
        public const int MF_GRAYED = 0x00000001;
        public const int MF_DISABLED = 0x00000002;
        public const int MF_CHECKED = 0x00000008;
        public const int MF_POPUP = 0x00000010;
        public const int MF_BYCOMMAND = 0x00000000;
        public const int MF_BYPOSITION = 0x00000400;
        public const int MF_SEPARATOR = 0x00000800;

        // GetWindow
        public const int GW_HWNDFIRST = 0;
        public const int GW_OWNER = 4;
        public const int GW_CHILD = 5;

        // LayeredWindowAttributes
        public const int LWA_COLORKEY = 0x00000001;
        public const int LWA_ALPHA = 0x00000002;

        // WindowLong
        public const int GWL_WNDPROC = -4;
        public const int GWL_HINSTANCE = -6;
        public const int GWL_HWNDPARENT = -8;
        public const int GWL_STYLE = -16;
        public const int GWL_EXSTYLE = -20;
        public const int GWL_USERDATA = -21;
        public const int GWL_ID = -12;
        
        // ClassLong
        public const int GCL_STYLE = -26;
        public const int GCL_WNDPROC = -24;
        public const int DWL_DLGPROC = 4;
        public const int DWL_USER = 8;

        // WindowStyle
        public const int WS_MAXIMIZEBOX = 0x00010000;
        public const int WS_MINIMIZEBOX = 0x00020000;
        public const int WS_EX_LAYERED = 0x00080000;
        public const int WS_EX_TOPMOST = 0x00000008;
        public const int WS_EX_TOOLWINDOW = 0x00000080;
        public const int WS_EX_TRANSPARENT = 0x20;
        public const int WS_EX_NOACTIVATE = 0x08000000;

        // Window Messages
        public const int WM_CREATE = 0x0001;
        public const int WM_DESTROY = 0x0002;
        public const int WM_MOVE = 0x0003;
        public const int WM_SIZE = 0x0005;
        public const int WM_ACTIVATE = 0x0006;
        public const int WM_COMMAND = 0x0111;
        public const int WM_SYSCOMMAND = 0x0112;
        public const int WM_MENUCOMMAND = 0x0126;
        public const int WM_MENUSELECT = 0x011F;
        public const int WM_GETICON = 0x7F;
        public const int WM_CLOSE = 0x0010;
        public const int WM_NULL = 0x0000;
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_SYSKEYDOWN = 0x0104;
        public const int WM_GETTEXT = 0x000D;
        public const int WM_GETTEXTLENGTH = 0x000E;
        public const int WM_GETFONT = 0x0031;
        public const int WM_COPYDATA = 0x004A;       

        public const int WM_MOUSEMOVE = 0x0200;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_RBUTTONDOWN = 0x0204;
        public const int WM_RBUTTONUP = 0x0205;
        public const int WM_MBUTTONDOWN = 0x0207;
        public const int WM_MBUTTONUP = 0x0208;

        // SetWindowPos
        public const uint SWP_NOSIZE = 0x0001;
        public const uint SWP_NOMOVE = 0x0002;
        public const uint SWP_NOACTIVATE = 0x0010;

        // MonitorFromWindow
        public const uint MONITOR_DEFAULTTONULL = 0;
        public const uint MONITOR_DEFAULTTOPRIMARY = 1;
        public const uint MONITOR_DEFAULTTONEAREST = 2;

        public const int ICON_SMALL = 0;
        public const int ICON_BIG = 1;
        public const int ICON_SMALL2 = 2;
        public const int GCLP_HICON = -14;
        public const int GCLP_HICONSM = -34;
        public const string IDI_APPLICATION = "#32512";

        public const int SW_HIDE = 0x0;
        public const int SW_MAXIMIZE = 0x3;
        public const int SW_MINIMIZE = 0x6;

        public const int MSGFLT_ADD = 0x1;

        public const int HWND_BROADCAST = 0xffff;

        public const int VK_SHIFT = 0x10;
        public const int VK_CONTROL = 0x11;
        public const int VK_DOWN = 0x28;

        public const int MIIM_TYPE = 0x00000010;
        public const int MFT_STRING = 0x00000000;

        public const int PROCESS_TERMINATE = 0x0001;
        public const int PROCESS_SET_INFORMATION = 0x0200;
        public const int PROCESS_QUERY_INFORMATION = 0x0400;
        public const int PROCESS_QUERY_LIMITED_INFORMATION = 0x1000;
        public const int PROCESS_VM_READ = 0x10;

        public const int STD_OUTPUT_HANDLE = -11;

        public const int WH_KEYBOARD_LL = 0x0D;
        public const int WH_MOUSE_LL = 0x0E;
        public const uint HC_ACTION = 0;

        public const int DWMWA_EXTENDED_FRAME_BOUNDS = 9;
        public const int CREATE_NO_WINDOW = 0x08000000;
        public const int STARTF_USESHOWWINDOW = 0x00000001;

        public const int STATE_SYSTEM_INVISIBLE = 0x00008000;

        public const int SE_PRIVILEGE_ENABLED = 0x02;

        public const int SEND_CHILD_HANDLE = 1;
    }
}
