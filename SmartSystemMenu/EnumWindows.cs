using System;
using System.Collections.Generic;
using System.Linq;
using SmartSystemMenu.Settings;
using SmartSystemMenu.Native;

namespace SmartSystemMenu
{
    static class EnumWindows
    {
        private static string[] _filterTitles;
        private static IList<Window> _windows;
        private static SmartSystemMenuSettings _settings;
        private static WindowSettings _windowSettings;

        public static IList<Window> EnumAllWindows(SmartSystemMenuSettings settings, WindowSettings windowSettings, params string[] filterTitles)
        {
            _filterTitles = filterTitles ?? new string[0];
            _windows = new List<Window>();
            _settings = settings;
            _windowSettings = windowSettings;
            NativeMethods.EnumWindows(EnumWindowCallback, 0);
            return _windows;
        }

        private static bool EnumWindowCallback(IntPtr hwnd, int lParam)
        {
            if (_windows.Any(w => w.Handle == hwnd))
            {
                return true;
            }

            int pid;
            bool isAdd;
            NativeMethods.GetWindowThreadProcessId(hwnd, out pid);

#if WIN32
            isAdd = !Environment.Is64BitOperatingSystem || SystemUtils.IsWow64Process(pid);
#else
            isAdd = Environment.Is64BitOperatingSystem && !SystemUtils.IsWow64Process(pid);
#endif

            if (!isAdd)
            {
                return true;
            }

            var window = new Window(hwnd, _settings.MenuItems, _settings.Language);

            if (!window.Menu.Exists)
            {
                isAdd = false;
            }

            if (_filterTitles.Any(s => window.GetWindowText() == s))
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