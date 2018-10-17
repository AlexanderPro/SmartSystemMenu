using System;
using System.Text;
using System.Runtime.InteropServices;

namespace SmartSystemMenu.Code.Common
{
    static class NativeMethods
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
        public static extern UInt32 GetMenuState(IntPtr hMenu, Int32 uIdItem, Int32 uFlags);

        [DllImport("User32.dll")]
        public static extern Boolean SetMenuItemInfo(IntPtr hMenu, Int32 uIdItem, Boolean fByPosition, ref MenuItemInfo lpmii);

        [DllImport("user32.dll")]
        public static extern Boolean DrawMenuBar(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern Int32 GetAsyncKeyState(Int32 key);

        [DllImport("user32.dll")]
        public static extern Boolean SetLayeredWindowAttributes(IntPtr hwnd, UInt32 crKey, Byte bAlpha, UInt32 dwFlags);

        [DllImport("user32.dll")]
        public static extern Boolean GetLayeredWindowAttributes(IntPtr hwnd, out UInt32 crKey, out Byte bAlpha, out UInt32 dwFlags);

        [DllImport("user32.dll")]
        public static extern Int32 SetWindowLong(IntPtr handle, Int32 nIndex, Int32 dwNewLong);

        [DllImport("user32.dll")]
        public static extern Int32 GetWindowLong(IntPtr handle, Int32 nIndex);

        [DllImport("kernel32.dll")]
        public static extern Boolean SetPriorityClass(IntPtr hProcess, PriorityClass priorityClass);

        [DllImport("kernel32.dll")]
        public static extern PriorityClass GetPriorityClass(IntPtr hProcess);

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
        public static extern Boolean GetWindowRect(IntPtr handle, out Rect lpRect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean PrintWindow(IntPtr handle, IntPtr hdc, Int32 nFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean GetClientRect(IntPtr handle, out Rect lpRect);

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

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(Int32 dwDesiredAccess, Boolean bInheritHandle, Int32 dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern Boolean CloseHandle(IntPtr hObject);

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
    }
}