using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace SmartSystemMenu
{
    static class EnumWindows
    {
        private static string[] _filterTitles;
        private static IntPtr[] _filterHandles;
        private static IList<Window> _windows;

        public static IList<Window> EnumAllWindows(params string[] filterTitles)
        {
            _filterTitles = filterTitles ?? new string[0];
            _filterHandles = new IntPtr[0];
            _windows = new List<Window>();
            NativeMethods.EnumWindows(EnumWindowCallback, 0);
            return _windows;
        }

        public static IList<Window> EnumProcessWindows(int processId, IntPtr[] filterHandles, params string[] filterTitles)
        {
            _filterTitles = filterTitles ?? new string[0];
            _filterHandles = filterHandles ?? new IntPtr[0];
            _windows = new List<Window>();
            foreach (ProcessThread thread in Process.GetProcessById(processId).Threads)
            {
                NativeMethods.EnumThreadWindows(thread.Id, EnumWindowCallback, 0);
            }
            return _windows;
        }

        private static bool EnumWindowCallback(IntPtr hwnd, int lParam)
        {
            if (_filterHandles.Any(h => h == hwnd)) return true;
            if (_windows.Any(w => w.Handle == hwnd)) return true;

            int pid;
            bool isAdd;
            NativeMethods.GetWindowThreadProcessId(hwnd, out pid);

#if WIN32
            isAdd = !Environment.Is64BitOperatingSystem || PlatformUtils.IsWow64Process(pid);
#else
            isAdd = Environment.Is64BitOperatingSystem && !PlatformUtils.IsWow64Process(pid);
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