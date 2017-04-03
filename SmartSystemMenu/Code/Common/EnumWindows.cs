using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SmartSystemMenu.Code.Common
{
    static class EnumWindows
    {
        private static String[] _filterTitles;
        private static IntPtr[] _filterHandles;
        private static IList<Window> _windows;

        public static IList<Window> EnumAllWindows(params String[] filterTitles)
        {
            _filterTitles = filterTitles ?? new String[0];
            _filterHandles = new IntPtr[0];
            _windows = new List<Window>();
            NativeMethods.EnumWindows(EnumWindowCallback, 0);
            return _windows;
        }

        public static IList<Window> EnumProcessWindows(Int32 processId, IntPtr[] filterHandles, params String[] filterTitles)
        {
            _filterTitles = filterTitles ?? new String[0];
            _filterHandles = filterHandles ?? new IntPtr[0];
            _windows = new List<Window>();
            foreach (ProcessThread thread in Process.GetProcessById(processId).Threads)
            {
                NativeMethods.EnumThreadWindows(thread.Id, EnumWindowCallback, 0);
            }
            return _windows;
        }

        private static Boolean EnumWindowCallback(IntPtr hwnd, Int32 lParam)
        {
            if (_filterHandles.Any(h => h == hwnd)) return true;
            if (_windows.Any(w => w.Handle == hwnd)) return true;

            Int32 pid;
            Boolean isAdd;
            NativeMethods.GetWindowThreadProcessId(hwnd, out pid);

#if WIN32
            isAdd = !Environment.Is64BitOperatingSystem || PlatformUtility.IsWow64Process(pid);
#else
            isAdd = Environment.Is64BitOperatingSystem && !PlatformUtility.IsWow64Process(pid);
#endif

            if (!isAdd) return true;

            var window = new Window(hwnd);

            if (!window.Menu.Exists)
            {
                isAdd = false;
            }

            if (_filterTitles.Any(s => window.WindowText == s))
            {
                isAdd = false;
            }

            if (isAdd)
            {
                _windows.Add(window);
            }
            return true;
        }
    }
}