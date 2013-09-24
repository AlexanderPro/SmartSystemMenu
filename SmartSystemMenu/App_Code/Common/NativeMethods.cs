using System;
using System.Text;
using System.Runtime.InteropServices;

namespace SmartSystemMenu.App_Code.Common
{
    class NativeMethods
    {
        public delegate Boolean EnumWindowDelegate(IntPtr hwnd, Int32 lParam);

        [DllImport("user32.dll")]
        public static extern Int32 EnumWindows(EnumWindowDelegate enumFunc, Int32 lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean EnumThreadWindows(Int32 threadId, EnumWindowDelegate enumFunc, Int32 lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr handle, Int32 uCmd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern Int32 GetWindowText(IntPtr handle, StringBuilder title, Int32 size);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern Int32 GetClassName(IntPtr handle, StringBuilder className, Int32 size);

        [DllImport("user32.dll")]
        public static extern Boolean IsWindowVisible(IntPtr handle);

        [DllImport("user32.dll")]
        public static extern Int32 RegisterWindowMessage(String lpString);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr handle, Boolean revert);

        [DllImport("user32.dll")]
        public static extern IntPtr GetMenu(IntPtr handle);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSubMenu(IntPtr handle, Int32 pos);

        [DllImport("user32.dll", EntryPoint = "InsertMenuW", CharSet = CharSet.Unicode)]
        public static extern Boolean InsertMenu(IntPtr hMenu, Int32 uPosition, Int32 uFlags, IntPtr uIDNewItem, String lpNewItem);

        [DllImport("user32.dll", EntryPoint = "InsertMenuW", CharSet = CharSet.Unicode)]
        public static extern Boolean InsertMenu(IntPtr hMenu, Int32 uPosition, Int32 uFlags, Int32 uIDNewItem, String lpNewItem);

        [DllImport("user32.dll")]
        public static extern Boolean RemoveMenu(IntPtr hMenu, Int32 uPosition, Int32 uFlags);

        [DllImport("user32.dll")]
        public static extern Boolean DeleteMenu(IntPtr hMenu, Int32 uPosition, Int32 uFlags);

        [DllImport("user32.dll")]
        public static extern Int32 CheckMenuItem(IntPtr hMenu, Int32 uIDCheckItem, Int32 uFlags);

        [DllImport("user32.dll")]
        public static extern Int32 GetMenuItemCount(IntPtr hMenu);

        [DllImport("user32.dll")]
        public static extern UInt32 GetMenuState(IntPtr hMenu, Int32 uIDItem, Int32 uFlags);

        [DllImport("user32.dll")]
        public static extern Boolean DrawMenuBar(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern Int32 GetAsyncKeyState(Int32 key);

        [DllImport("user32.dll")]
        public static extern Boolean SetLayeredWindowAttributes(IntPtr hwnd, UInt32 crKey, Byte bAlpha, Int32 dwFlags);

        [DllImport("user32.dll")]
        public static extern Int32 SetWindowLong(IntPtr handle, Int32 nIndex, Int32 dwNewLong);

        [DllImport("user32.dll")]
        public static extern Int32 GetWindowLong(IntPtr handle, Int32 nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr CreateMenu();

        [DllImport("user32.dll")]
        public static extern Boolean DestroyMenu(IntPtr hMenu);

        [DllImport("user32.dll")]
        public static extern IntPtr GetProp(IntPtr handle, String lpString);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean SetWindowPos(IntPtr handle, IntPtr hWndInsertAfter, Int32 x, Int32 y, Int32 cx, Int32 cy, UInt32 uFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean GetWindowRect(IntPtr handle, out RECT lpRect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean GetClientRect(IntPtr handle, out RECT lpRect);

        [DllImport("user32.dll")]
        public static extern Boolean MoveWindow(IntPtr handle, Int32 x, Int32 y, Int32 nWidth, Int32 nHeight, Boolean bRepaint);

        [DllImport("user32.dll")]
        public static extern Boolean ChangeWindowMessageFilter(Int32 msg, Int32 flag);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, Int32 msg, UInt32 wParam, UInt32 lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, Int32 msg, UInt32 wParam, UInt32 lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, Int32 msg, UInt64 wParam, UInt64 lParam);

        [DllImport("user32.dll")]
        public static extern Int32 SendMessageTimeout(IntPtr handle, Int32 uMsg, UInt32 wParam, UInt32 lParam, SendMessageTimeoutFlags fuFlags, Int32 uTimeout, out UInt32 lpdwResult);

        [DllImport("user32.dll")]
        public static extern IntPtr LoadIcon(IntPtr hInstance, String lpIconName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean ShowWindow(IntPtr hWnd, Int32 nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean ShowWindowAsync(IntPtr hWnd, Int32 nCmdShow);

        [DllImport("user32.dll")]
        public static extern Boolean SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern UInt32 GetWindowThreadProcessId(IntPtr hWnd, out Int32 lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern UInt32 GetWindowThreadProcessId(IntPtr hWnd, IntPtr processId);

        [DllImport("kernel32.dll")]
        public static extern UInt32 GetCurrentThreadId();

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern Boolean AttachThreadInput(UInt32 idAttach, UInt32 idAttachTo, Boolean fAttach);

        [DllImport("user32.dll")]
        public static extern Boolean BringWindowToTop(IntPtr hWnd);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean IsWow64Process(IntPtr hProcess, out Boolean wow64Process);

        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        public static extern UInt32 GetClassLongPtr32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
        public static extern IntPtr GetClassLongPtr64(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr GetThreadDesktop(Int32 threadId);

        public static IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex)
        {
            return IntPtr.Size > 4 ? GetClassLongPtr64(hWnd, nIndex) : new IntPtr(GetClassLongPtr32(hWnd, nIndex));
        }

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
        public const Int32 GWL_WNDPROC = (-4);
        public const Int32 GWL_HINSTANCE = (-6);
        public const Int32 GWL_HWNDPARENT = (-8);
        public const Int32 GWL_STYLE = (-16);
        public const Int32 GWL_EXSTYLE = (-20);
        public const Int32 GWL_USERDATA = (-21);
        public const Int32 GWL_ID = (-12);

        // WindowStyle
        public const Int32 WS_EX_LAYERED = 0x00080000;

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
    }
}