using System;
using System.Text;
using System.Runtime.InteropServices;
using SmartSystemMenu.Native.Enums;
using SmartSystemMenu.Native.Structs;

namespace SmartSystemMenu.Native
{
    static class User32
    {
        public delegate bool EnumWindowDelegate(IntPtr hwnd, int lParam);

        public delegate bool EnumMonitorProc(IntPtr hMonitor, IntPtr hdcMonitor, ref Rect rcMonitor, IntPtr data);

        [DllImport("user32.dll")]
        public static extern int EnumWindows(EnumWindowDelegate enumFunc, int lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumThreadWindows(int threadId, EnumWindowDelegate enumFunc, int lParam);

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumDisplayMonitors(IntPtr hDC, IntPtr clipRect, EnumMonitorProc proc, IntPtr data);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr handle, int uCmd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetParent(IntPtr handle);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr handle, StringBuilder title, int size);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr handle, StringBuilder className, int size);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint RealGetWindowClass(IntPtr handle, [Out] StringBuilder className, int size);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr handle);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr handle, bool revert);

        [DllImport("user32.dll")]
        public static extern IntPtr GetMenu(IntPtr handle);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSubMenu(IntPtr handle, int pos);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll", EntryPoint = "InsertMenuW", CharSet = CharSet.Unicode)]
        public static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, IntPtr uIDNewItem, string lpNewItem);

        [DllImport("user32.dll", EntryPoint = "InsertMenuW", CharSet = CharSet.Unicode)]
        public static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll")]
        public static extern bool RemoveMenu(IntPtr hMenu, int uPosition, int uFlags);

        [DllImport("user32.dll")]
        public static extern bool DeleteMenu(IntPtr hMenu, int uPosition, int uFlags);

        [DllImport("user32.dll")]
        public static extern bool DeleteMenu(IntPtr hMenu, IntPtr uIDNewItem, int uFlags);

        [DllImport("user32.dll")]
        public static extern int CheckMenuItem(IntPtr hMenu, int uIDCheckItem, int uFlags);

        [DllImport("user32.dll")]
        public static extern int GetMenuItemCount(IntPtr hMenu);

        [DllImport("user32.dll")]
        public static extern int GetMenuState(IntPtr hMenu, int uIdItem, int uFlags);

        [DllImport("user32.dll")]
        public static extern bool GetMenuItemInfo(IntPtr hMenu, int uIdItem, bool fByPosition, ref MenuItemInfo lpmii);

        [DllImport("user32.dll")]
        public static extern bool SetMenuItemInfo(IntPtr hMenu, int uIdItem, bool fByPosition, ref MenuItemInfo lpmii);

        [DllImport("user32.dll")]
        public static extern int GetMenuItemID(IntPtr hMenu, int uPosition);

        [DllImport("User32.dll")]
        public static extern uint EnableMenuItem(IntPtr hMenu, int itemId, int uEnable);

        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(int key);

        [DllImport("user32.dll")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, Byte bAlpha, uint dwFlags);

        [DllImport("user32.dll")]
        public static extern bool GetLayeredWindowAttributes(IntPtr hwnd, out uint crKey, out Byte bAlpha, out uint dwFlags);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr handle, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr handle, int nIndex);

        [DllImport("user32.dll")]
        public static extern int GetClassLong(IntPtr handle, int nIndex);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowInfo([In] IntPtr hWnd, [In, Out] ref WINDOW_INFO rect);

        [DllImport("user32.dll")]
        public static extern IntPtr CreateMenu();

        [DllImport("user32.dll")]
        public static extern bool DestroyMenu(IntPtr hMenu);

        [DllImport("user32.dll")]
        public static extern IntPtr GetProp(IntPtr handle, string lpString);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr handle, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr handle, out Rect lpRect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PrintWindow(IntPtr handle, IntPtr hdc, int nFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetClientRect(IntPtr handle, out Rect lpRect);

        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr handle, int x, int y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        public static extern bool ChangeWindowMessageFilter(int msg, int flag);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, uint wParam, uint lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, int msg, uint wParam, uint lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, int msg, UInt64 wParam, UInt64 lParam);

        [DllImport("user32.dll")]
        public extern static int SendNotifyMessage(IntPtr hWnd, int msg, uint wParam, uint lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessageTimeout(IntPtr handle, int uMsg, uint wParam, uint lParam, SendMessageTimeoutFlags fuFlags, int uTimeout, out uint lpdwResult);

        [DllImport("user32.dll")]
        public static extern IntPtr LoadIcon(IntPtr hInstance, string lpIconName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr processId);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        [DllImport("user32.dll")]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        public static extern uint GetClassLongPtr32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
        public static extern IntPtr GetClassLongPtr64(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr GetThreadDesktop(int threadId);

        [DllImport("user32.dll")]
        public static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        [DllImport("user32")]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MonitorInfo info);

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point p);

        [DllImport("user32.dll")]
        public static extern IntPtr GetShellWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookProc callback, IntPtr hModule, uint threadId);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, MouseHookProc callback, IntPtr hModule, uint dwThreadId);

        [DllImport("user32.dll")]
        public static extern bool UnhookWindowsHookEx(IntPtr handleHook);

        [DllImport("user32.dll")]
        public static extern int CallNextHookEx(IntPtr handleHook, int nCode, IntPtr wParam, ref KeyboardLLHookStruct lParam);

        [DllImport("user32.dll")]
        public static extern int CallNextHookEx(IntPtr handleHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetProcessDpiAwarenessContext(int dpiFlag);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetProcessDPIAware();

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr GetAncestor(IntPtr hwnd, GetAncestorFlags flags);

        [DllImport("user32.dll")]
        public static extern IntPtr GetLastActivePopup(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetTitleBarInfo(IntPtr hwnd, ref TITLEBARINFO pti);

        public static IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex) => IntPtr.Size > 4 ? GetClassLongPtr64(hWnd, nIndex) : new IntPtr(GetClassLongPtr32(hWnd, nIndex));

        public static readonly IntPtr HWND_TOP = new (0);
        public static readonly IntPtr HWND_TOPMOST = new (-1);
        public static readonly IntPtr HWND_NOTOPMOST = new (-2);
    }
}
