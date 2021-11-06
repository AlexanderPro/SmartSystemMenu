using System;
using System.Windows.Forms;
using System.Threading;
using System.Linq;
using SmartSystemMenu.Forms;
using SmartSystemMenu.Utils;
using SmartSystemMenu.Native;
using SmartSystemMenu.Extensions;

namespace SmartSystemMenu
{
    static class Program
    {
        private static Mutex _mutex;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Command Line Interface
            var toggleParser = new ToggleParser(args);
            if (toggleParser.HasToggle("h") || toggleParser.HasToggle("help"))
            {
                var dialog = new MessageBoxForm();
                dialog.Message = BuildHelpString();
                dialog.Text = "Help";
                dialog.ShowDialog();
                return;
            }

            // Clear Clipboard
            if (toggleParser.HasToggle("clearclipboard"))
            {
                Clipboard.Clear();
            }

            var windowHandle = IntPtr.Zero;
            if (toggleParser.HasToggle("title") || toggleParser.HasToggle("handle"))
            {
                var windowHandleString = toggleParser.GetToggleValueOrDefault("handle", null);
                var windowTitle = toggleParser.GetToggleValueOrDefault("title", null);
                if (!string.IsNullOrWhiteSpace(windowHandleString))
                {
                    windowHandle = windowHandleString.StartsWith("0x") ? int.TryParse(windowHandleString.Substring(2), System.Globalization.NumberStyles.AllowHexSpecifier, null, out var number) ? new IntPtr(number) :
                        IntPtr.Zero : int.TryParse(windowHandleString, out var number2) ? new IntPtr(number2) : IntPtr.Zero;
                }

                if (windowHandle == IntPtr.Zero && !string.IsNullOrEmpty(windowTitle))
                {
                    var processIdString = toggleParser.GetToggleValueOrDefault("processId", null);
                    var processId = !string.IsNullOrWhiteSpace(processIdString) && int.TryParse(processIdString, out var pid) ? pid : (int?)null;
                    windowHandle = WindowUtils.FindWindowByTitle(windowTitle, processId);
                }

                if (windowHandle != IntPtr.Zero)
                {
                    var window = new Window(windowHandle);

                    // Set a Window monitor
                    if (toggleParser.HasToggle("m") || toggleParser.HasToggle("monitor"))
                    {
                        var monitorString = toggleParser.GetToggleValueOrDefault("m", null) ?? toggleParser.GetToggleValueOrDefault("monitor", null);
                        if (int.TryParse(monitorString, out var monitor))
                        {
                            var monitorItem = SystemUtils.GetMonitors().Select((x, i) => new { Index = i, MonitorHandle = x }).FirstOrDefault(x => x.Index == monitor);
                            if (monitorItem != null)
                            {
                                window.MoveToMonitor(monitorItem.MonitorHandle);
                            }
                        }
                    }

                    // Set a Window width
                    if (toggleParser.HasToggle("width"))
                    {
                        if (int.TryParse(toggleParser.GetToggleValueOrDefault("width", null), out var width))
                        {
                            window.SetWidth(width);
                        }
                    }

                    // Set a Window height
                    if (toggleParser.HasToggle("height"))
                    {
                        if (int.TryParse(toggleParser.GetToggleValueOrDefault("height", null), out var height))
                        {
                            window.SetHeight(height);
                        }
                    }

                    // Set a Window left position
                    if (toggleParser.HasToggle("left"))
                    {
                        if (int.TryParse(toggleParser.GetToggleValueOrDefault("left", null), out var left))
                        {
                            window.SetLeft(left);
                        }
                    }

                    // Set a Window top position
                    if (toggleParser.HasToggle("top"))
                    {
                        if (int.TryParse(toggleParser.GetToggleValueOrDefault("top", null), out var top))
                        {
                            window.SetTop(top);
                        }
                    }

                    // Set a Window position
                    if (toggleParser.HasToggle("a") || toggleParser.HasToggle("alignment"))
                    {
                        var windowAlignmentString = toggleParser.GetToggleValueOrDefault("a", null) ?? toggleParser.GetToggleValueOrDefault("alignment", null);
                        var windowAlignment = Enum.TryParse<WindowAlignment>(windowAlignmentString, true, out var alignment) ? alignment : 0;
                        window.SetAlignment(windowAlignment);
                    }

                    // Set a Window transparency
                    if (toggleParser.HasToggle("t") || toggleParser.HasToggle("transparency"))
                    {
                        var transparencyString = toggleParser.GetToggleValueOrDefault("t", null) ?? toggleParser.GetToggleValueOrDefault("transparency", null);
                        if (byte.TryParse(transparencyString, out var transparency))
                        {
                            transparency = transparency > 100 ? (byte)100 : transparency;
                            window.SetTransparency(transparency);
                        }
                    }

                    // Set a Process priority
                    if (toggleParser.HasToggle("p") || toggleParser.HasToggle("priority"))
                    {
                        var processPriorityString = toggleParser.GetToggleValueOrDefault("p", null) ?? toggleParser.GetToggleValueOrDefault("priority", null);
                        var processPriority = Enum.TryParse<Priority>(processPriorityString, true, out var priority) ? priority : 0;
                        window.SetPriority(processPriority);
                    }

                    // Set a Window AlwaysOnTop
                    if (toggleParser.HasToggle("alwaysontop"))
                    {
                        var alwaysontopString = toggleParser.GetToggleValueOrDefault("alwaysontop", string.Empty).ToLower();

                        if (alwaysontopString == "on")
                        {
                            window.MakeTopMost(true);
                        }

                        if (alwaysontopString == "off")
                        {
                            window.MakeTopMost(false);
                        }
                    }

                    // Set a Window Aero Glass
                    if (toggleParser.HasToggle("g") || toggleParser.HasToggle("aeroglass"))
                    {
                        var aeroglassString = (toggleParser.GetToggleValueOrDefault("g", null) ?? toggleParser.GetToggleValueOrDefault("aeroglass", string.Empty)).ToLower();
                        var enabled = aeroglassString == "on" ? true : aeroglassString == "off" ? false : (bool?)null;

                        if (enabled.HasValue)
                        {
                            var version = Environment.OSVersion.Version;
                            if (version.Major == 6 && (version.Minor == 0 || version.Minor == 1))
                            {
                                window.AeroGlassForVistaAndSeven(enabled.Value);
                            }
                            else if (version.Major >= 6)
                            {
                                window.AeroGlassForEightAndHigher(enabled.Value);
                            }
                        }
                    }

                    // Send To Bottom Window
                    if (toggleParser.HasToggle("sendtobottom"))
                    {
                        window.SendToBottom();
                    }

                    // Open File In Explorer
                    if (toggleParser.HasToggle("o") || toggleParser.HasToggle("openinexplorer"))
                    {
                        try
                        {
                            SystemUtils.RunAsDesktopUser("explorer.exe", "/select, " + window.Process.GetMainModuleFileName());
                        }
                        catch
                        {
                        }
                    }

                    // Copy to clipboard
                    if (toggleParser.HasToggle("c") || toggleParser.HasToggle("copytoclipboard"))
                    {
                        var text = window.ExtractText();
                        if (text != null)
                        {
                            Clipboard.SetText(text);
                        }
                    }
                }
            }

            if (toggleParser.HasToggle("n") || toggleParser.HasToggle("nogui"))
            {
                return;
            }

#if WIN32
            var mutexName = "SmartSystemMenuMutex";
#else
            var mutexName = "SmartSystemMenuMutex64";
#endif
            _mutex = new Mutex(false, mutexName, out var createNew);
            if (!createNew)
            {
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static string BuildHelpString()
        {
            var help =
                @"-h --help             The help
   --title            Title
   --handle           Handle (1234567890) (0xFFFFFF)
   --processId        PID (1234567890)
   --left             Left
   --top              Top
   --width            Width
   --height           Height
-m --monitor          [0, 1, 2, 3, ...]
-a --alignment        [topleft,
                       topcenter,
                       topright,
                       middleleft,
                       middlecenter,
                       middleright,
                       bottomleft,
                       bottomcenter,
                       bottomright]
-p --priority         [realtime,
                       high,
                       abovenormal,
                       normal,
                       belownormal,
                       idle]
-t --transparency     [0 ... 100]
   --alwaysontop      [on, off]
-g --aeroglass        [on, off]
   --sendtobottom     No params
-o --openinexplorer   No params
-c --copytoclipboard  No params
   --clearclipboard   No params
-n --nogui            No GUI

Example:
SmartSystemMenu.exe --title ""Untitled - Notepad"" -a topleft -p high --alwaysontop on --nogui";
            return help;
        }
    }
}
