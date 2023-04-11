![SmartSystemMenu](https://user-images.githubusercontent.com/8102586/68280906-8e86b800-0087-11ea-9762-f9eb028bb8fe.png) SmartSystemMenu
=============

- English
- [Русский](/README_RU.md)
- [中文版](/README_CN.md)

---

SmartSystemMenu extends system menu of all windows in the system. It appends next custom items to menu:

* **Information.** Shows a dialog with information of the current window and process: the window handle, the window caption, the window style, the window class, the process name, the process id, the path to the process.
* **Roll Up.** Allows to roll up and down the current window.
* **Aero Glass.** Allows to add the "Aero Glass" blur to the current window. (Windows Vista and higher. Mostly for console windows.)
* **Always On Top.** Allows the current window to stay on top of all other windows.
* **Send To Bottom.** Allows to send to bottom the current window.
* **Save Screenshot.** Allows to save the current window screenshot in a file.
* **Open File In Explorer.** Allows to open a process file in a File Explorer.
* **Drag By Mouse.** Allows to drag by mouse the current window.
* **Hide For Alt+Tab.** Allows to hide the current window for the Taskbar and Alt+Tab switch.
* **Resize.** Allows to change the size of the current window.
* **Move To.** Allows to move the current window to another monitor.
* **Alignment.** Allows the current window to be aligned with any of the 9 positions on the desktop.
* **Transparency.** Allows to change the transparency of the current window.
* **Priority.** Allows to change the current window's program priority.
* **Clipboard.** Allows to copy all window texts (including console, ms office products, etc.) to clipboard and clear clipboard.
* **Buttons.** Allows to disable "Minimize", "Maximize" and "Close" button.
* **System Tray.** Allows to minimize or suspend the current window to the system tray.
* **Other Windows.** Allows to close and minimize all windows in the system except the current.
* **Start Program.** Allows to start programs which is in the settings.

Screenshots
------------------

![alt tag](https://user-images.githubusercontent.com/8102586/102021339-3349ed80-3d90-11eb-835f-7780a2caae69.jpg)
![alt tag](https://user-images.githubusercontent.com/8102586/102021342-380ea180-3d90-11eb-9dd3-537aab1b17f5.jpg)
![alt tag](https://user-images.githubusercontent.com/8102586/102021344-3b099200-3d90-11eb-99fd-a021ee54043f.jpg)
![alt tag](https://user-images.githubusercontent.com/8102586/103346844-566de000-4aa6-11eb-9f15-f0ca2ea80ee9.png)

Command Line Interface
--------------------

```bash
   --help             The help
   --title            Title
   --titleBegins      Title begins 
   --titleEnds        Title ends
   --titleContains    Title contains
   --handle           Handle (1234567890) (0xFFFFFF)
   --processId        PID (1234567890)
-d --delay            Delay in milliseconds
-l --left             Left
-t --top              Top
-w --width            Width
-h --height           Height
-i --information      Information dialog
-s --savescreenshot   Save Screenshot
-m --monitor          [0, 1, 2, 3, ...]
-a --alignment        [topleft,
                       topcenter,
                       topright,
                       middleleft,
                       middlecenter,
                       middleright,
                       bottomleft,
                       bottomcenter,
                       bottomright,
                       centerhorizontally,
                       centervertically]
-p --priority         [realtime,
                       high,
                       abovenormal,
                       normal,
                       belownormal,
                       idle]
   --transparency     [0 ... 100]
   --alwaysontop      [on, off]
-g --aeroglass        [on, off]
   --hidealttab       [on, off]
   --minimizebutton   [on, off]
   --maximizebutton   [on, off]
   --sendtobottom     Send To Bottom
-o --openinexplorer   Open File In Explorer
-c --copytoclipboard  Copy Window Text To Clipboard
   --copyscreenshot   Copy Screenshot To Clipboard
   --clearclipboard   Clear Clipboard
   --trustedinstaller Sets TrustedInstaller owner for SmartSystemMenuHook.dll and SmartSystemMenuHook64.dll
-n --nogui            No GUI

Example:
SmartSystemMenu.exe --title "Untitled - Notepad" -a topleft -p high --alwaysontop on --nogui
```

Install
--------------------

* Download the [SmartSystemMenu](https://github.com/AlexanderPro/SmartSystemMenu/releases) in the zip file
* [Chocolatey](https://chocolatey.org/): `choco install smartsystemmenu`
* [Scoop](https://scoop.sh/): `scoop install smartsystemmenu`

Requirements
--------------------

* OS Windows XP SP3 and later. Supports x86 and x64 systems.
* .NET Framework 4.0

Files
--------------------

* SmartSystemMenu.exe
* SmartSystemMenu64.exe (located in resources of SmartSystemMenu.exe module)
* SmartSystemMenuHook.dll
* SmartSystemMenuHook64.dll
* SmartSystemMenu.xml
* Language.xml

This program has SmartSystemMenu.exe and SmartSystemMenuHook.dll modules for x86 processes, SmartSystemMenu64.exe and SmartSystemMenuHook64.dll modules for x64 processes. When you run SmartSystemMenu.exe, it also runs SmartSystemMenu64.exe. These two executable modules load hooks (SmartSystemMenuHook.dll and SmartSystemMenuHook64.dll) to all processes. When you select an item in the system menu, the hook sends a message to the executable module. After that, the module performs the selected action: changes the transparency of the window, changes the size of the window, etc.

Tips
--------------------

Run SmartSystemMenu.exe process. If your OS has enabled UAC, the system will display a UAC dialog. You do not need to worry because the program needs elevated privileges. After the program has been executed, in all system menus of all windows you can see custom items.