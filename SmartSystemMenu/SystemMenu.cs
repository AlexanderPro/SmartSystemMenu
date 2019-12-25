using System;
using System.Runtime.InteropServices;

namespace SmartSystemMenu
{
    class SystemMenu
    {
        #region Fields.Private

        private IntPtr _priorityMenuHandle;
        private IntPtr _alignmentMenuHandle;
        private IntPtr _sizeMenuHandle;
        private IntPtr _transparencyMenuHandle;
        private IntPtr _systemTrayMenuHandle;
        private IntPtr _otherWindowsHandle;

        #endregion


        #region Properties.Public

        public IntPtr WindowHandle { get; private set; }

        public bool Exists
        {
            get
            {
                IntPtr windowMenuHandle = NativeMethods.GetSystemMenu(WindowHandle, false);
                bool existsWindowMenu = windowMenuHandle != IntPtr.Zero;
                return existsWindowMenu;
            }
        }

        #endregion


        #region Constants.Public

        public const int SC_TRANS_100 = 0x4740;
        public const int SC_TRANS_90 = 0x4742;
        public const int SC_TRANS_80 = 0x4744;
        public const int SC_TRANS_70 = 0x4746;
        public const int SC_TRANS_60 = 0x4748;
        public const int SC_TRANS_50 = 0x4750;
        public const int SC_TRANS_40 = 0x4752;
        public const int SC_TRANS_30 = 0x4754;
        public const int SC_TRANS_20 = 0x4756;
        public const int SC_TRANS_10 = 0x4758;
        public const int SC_TRANS_00 = 0x4760;
        public const int SC_TRANS_CUSTOM = 0x4761;
        public const int SC_TRANS_DEFAULT = 0x4762;
        public const int SC_TOPMOST = 0x4763;
        public const int SC_SIZE_640_480 = 0x4765;
        public const int SC_SIZE_720_480 = 0x4766;
        public const int SC_SIZE_720_576 = 0x4767;
        public const int SC_SIZE_800_600 = 0x4768;
        public const int SC_SIZE_1024_768 = 0x4769;
        public const int SC_SIZE_1152_864 = 0x4770;
        public const int SC_SIZE_1280_768 = 0x4771;
        public const int SC_SIZE_1280_800 = 0x4772;
        public const int SC_SIZE_1280_960 = 0x4773;
        public const int SC_SIZE_1280_1024 = 0x4774;
        public const int SC_SIZE_1440_900 = 0x4775;
        public const int SC_SIZE_1600_900 = 0x4776;
        public const int SC_SIZE_1680_1050 = 0x4777;
        public const int SC_SIZE_DEFAULT = 0x4778;
        public const int SC_SIZE_CUSTOM = 0x4779;
        public const int SC_MINIMIZE_TO_SYSTEMTRAY = 0x4780;
        public const int SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY = 0x4781;
        public const int SC_INFORMATION = 0x4782;
        public const int SC_ROLLUP = 0x4783;
        public const int SC_PRIORITY_REAL_TIME = 0x4784;
        public const int SC_PRIORITY_HIGH = 0x4785;
        public const int SC_PRIORITY_ABOVE_NORMAL = 0x4786;
        public const int SC_PRIORITY_NORMAL = 0x4787;
        public const int SC_PRIORITY_BELOW_NORMAL = 0x4788;
        public const int SC_PRIORITY_IDLE = 0x4789;
        public const int SC_ALIGN_TOP_LEFT = 0x4790;
        public const int SC_ALIGN_TOP_CENTER = 0x4791;
        public const int SC_ALIGN_TOP_RIGHT = 0x4792;
        public const int SC_ALIGN_MIDDLE_LEFT = 0x4793;
        public const int SC_ALIGN_MIDDLE_CENTER = 0x4794;
        public const int SC_ALIGN_MIDDLE_RIGHT = 0x4795;
        public const int SC_ALIGN_BOTTOM_LEFT = 0x4796;
        public const int SC_ALIGN_BOTTOM_CENTER = 0x4797;
        public const int SC_ALIGN_BOTTOM_RIGHT = 0x4798;
        public const int SC_ALIGN_DEFAULT = 0x4799;
        public const int SC_ALIGN_CUSTOM = 0x4800;
        public const int SC_ALIGN_MONITOR = 0x4801;
        public const int SC_SAVE_SCREEN_SHOT = 0x4802;
        public const int SC_COPY_TEXT_TO_CLIPBOARD = 0x4803;
        public const int SC_OPEN_FILE_IN_EXPLORER = 0x4804;
        public const int SC_CLOSE_OTHER_WINDOWS = 0x4805;
        public const int SC_MINIMIZE_OTHER_WINDOWS = 0x4806;
        public const int SC_AERO_GLASS = 0x4807;

        #endregion


        #region Methods.Public

        public SystemMenu(IntPtr windowHandle)
        {
            WindowHandle = windowHandle;
        }

        public void Create()
        {
            var windowMenuHandle = NativeMethods.GetSystemMenu(WindowHandle, false);
            var index = NativeMethods.GetMenuItemCount(windowMenuHandle);

            NativeMethods.InsertMenu(windowMenuHandle, index, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, IntPtr.Zero, "");
            NativeMethods.InsertMenu(windowMenuHandle, index + 1, NativeConstants.MF_BYPOSITION, SC_INFORMATION, "Information");
            NativeMethods.InsertMenu(windowMenuHandle, index + 2, NativeConstants.MF_BYPOSITION, SC_AERO_GLASS, "Aero Glass");
            NativeMethods.InsertMenu(windowMenuHandle, index + 3, NativeConstants.MF_BYPOSITION, SC_TOPMOST, "Always On Top");
            NativeMethods.InsertMenu(windowMenuHandle, index + 4, NativeConstants.MF_BYPOSITION, SC_SAVE_SCREEN_SHOT, "Save Screenshot");
            NativeMethods.InsertMenu(windowMenuHandle, index + 5, NativeConstants.MF_BYPOSITION, SC_OPEN_FILE_IN_EXPLORER, "Open File In Explorer");
            NativeMethods.InsertMenu(windowMenuHandle, index + 6, NativeConstants.MF_BYPOSITION, SC_COPY_TEXT_TO_CLIPBOARD, "Copy Text To Clipboard");

            _sizeMenuHandle = NativeMethods.CreateMenu();
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_640_480, "640x480");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_720_480, "720x480");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_720_576, "720x576");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_800_600, "800x600");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_1024_768, "1024x768");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_1152_864, "1152x864");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_1280_768, "1280x768");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_1280_800, "1280x800");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_1280_960, "1280x960");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_1280_1024, "1280x1024");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_1440_900, "1440x900");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_1600_900, "1600x900");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_1680_1050, "1680x1050");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_DEFAULT, "Default");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_SIZE_CUSTOM, "Custom...");
            NativeMethods.InsertMenu(windowMenuHandle, index + 7, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _sizeMenuHandle, "Resize");

            _alignmentMenuHandle = NativeMethods.CreateMenu();
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_MONITOR, "Monitor");
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_TOP_LEFT, "top-left");
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_TOP_CENTER, "top-center");
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_TOP_RIGHT, "top-right");
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_MIDDLE_LEFT, "middle-left");
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_MIDDLE_CENTER, "middle-center");
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_MIDDLE_RIGHT, "middle-right");
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_BOTTOM_LEFT, "bottom-left");
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_BOTTOM_CENTER, "bottom-center");
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_BOTTOM_RIGHT, "bottom-right");
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_DEFAULT, "Default");
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_alignmentMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_ALIGN_CUSTOM, "Custom...");
            NativeMethods.InsertMenu(windowMenuHandle, index + 8, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _alignmentMenuHandle, "Alignment");

            _transparencyMenuHandle = NativeMethods.CreateMenu();
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_00, "0% (opaque)");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_10, "10%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_20, "20%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_30, "30%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_40, "40%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_50, "50%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_60, "60%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_70, "70%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_80, "80%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_90, "90%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_100, "100% (invisible)");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_DEFAULT, "Default");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION | NativeConstants.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_TRANS_CUSTOM, "Custom...");
            NativeMethods.InsertMenu(windowMenuHandle, index + 9, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _transparencyMenuHandle, "Transparency");

            _priorityMenuHandle = NativeMethods.CreateMenu();
            NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_PRIORITY_REAL_TIME, "Real Time: 24");
            NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_PRIORITY_HIGH, "High: 13");
            NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_PRIORITY_ABOVE_NORMAL, "Above Normal: 10");
            NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_PRIORITY_NORMAL, "Normal: 8");
            NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_PRIORITY_BELOW_NORMAL, "Below Normal: 6");
            NativeMethods.InsertMenu(_priorityMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_PRIORITY_IDLE, "Idle: 4");
            NativeMethods.InsertMenu(windowMenuHandle, index + 10, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _priorityMenuHandle, "Priority");

            _otherWindowsHandle = NativeMethods.CreateMenu();
            NativeMethods.InsertMenu(_otherWindowsHandle, -1, NativeConstants.MF_BYPOSITION, SC_MINIMIZE_OTHER_WINDOWS, "Minimize");
            NativeMethods.InsertMenu(_otherWindowsHandle, -1, NativeConstants.MF_BYPOSITION, SC_CLOSE_OTHER_WINDOWS, "Close");
            NativeMethods.InsertMenu(windowMenuHandle, index + 11, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _otherWindowsHandle, "Other Windows");

            _systemTrayMenuHandle = NativeMethods.CreateMenu();
            NativeMethods.InsertMenu(_systemTrayMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_MINIMIZE_TO_SYSTEMTRAY, "Minimize To Tray");
            NativeMethods.InsertMenu(_systemTrayMenuHandle, -1, NativeConstants.MF_BYPOSITION, SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY, "Minimize To Tray Always");
            NativeMethods.InsertMenu(windowMenuHandle, index + 12, NativeConstants.MF_BYPOSITION | NativeConstants.MF_POPUP, _systemTrayMenuHandle, "System Tray");
        }

        public void Destroy()
        {
            var windowMenuHandle = NativeMethods.GetSystemMenu(WindowHandle, false);
            var Index = NativeMethods.GetMenuItemCount(windowMenuHandle);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 1, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 2, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 3, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 4, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 5, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 6, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 7, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 8, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 9, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 10, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 11, NativeConstants.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 12, NativeConstants.MF_BYPOSITION);
            NativeMethods.DestroyMenu(_priorityMenuHandle);
            NativeMethods.DestroyMenu(_alignmentMenuHandle);
            NativeMethods.DestroyMenu(_sizeMenuHandle);
            NativeMethods.DestroyMenu(_transparencyMenuHandle);
            NativeMethods.DestroyMenu(_otherWindowsHandle);
            NativeMethods.DestroyMenu(_systemTrayMenuHandle);
            NativeMethods.GetSystemMenu(WindowHandle, true);
        }

        public void SetMenuItemText(int id, string text)
        {
            var info = new MenuItemInfo();
            info.cbSize = (UInt32)Marshal.SizeOf(info);
            info.fMask = NativeConstants.MIIM_TYPE;
            info.fType = NativeConstants.MFT_STRING;
            info.dwTypeData = text;
            info.cch = (UInt32)text.Length;
            var windowMenuHandle = NativeMethods.GetSystemMenu(WindowHandle, false);
            NativeMethods.SetMenuItemInfo(windowMenuHandle, id, false, ref info);
        }

        public void CheckMenuItem(int id, bool check)
        {
            var windowMenuHandle = NativeMethods.GetSystemMenu(WindowHandle, false);
            NativeMethods.CheckMenuItem(windowMenuHandle, id, check ? NativeConstants.MF_CHECKED : NativeConstants.MF_UNCHECKED);
        }

        public bool IsMenuItemChecked(int id)
        {
            var windowMenuHandle = NativeMethods.GetSystemMenu(WindowHandle, false);
            var flags = NativeMethods.GetMenuState(windowMenuHandle, id, NativeConstants.MF_BYCOMMAND);
            var isChecked = (flags & NativeConstants.MF_CHECKED) != 0;
            return isChecked;
        }

        public void UncheckPriorityMenu()
        {
            CheckMenuItem(SC_PRIORITY_REAL_TIME, false);
            CheckMenuItem(SC_PRIORITY_HIGH, false);
            CheckMenuItem(SC_PRIORITY_ABOVE_NORMAL, false);
            CheckMenuItem(SC_PRIORITY_NORMAL, false);
            CheckMenuItem(SC_PRIORITY_BELOW_NORMAL, false);
            CheckMenuItem(SC_PRIORITY_IDLE, false);
        }

        public void UncheckAlignmentMenu()
        {
            CheckMenuItem(SC_ALIGN_TOP_LEFT, false);
            CheckMenuItem(SC_ALIGN_TOP_CENTER, false);
            CheckMenuItem(SC_ALIGN_TOP_RIGHT, false);
            CheckMenuItem(SC_ALIGN_MIDDLE_LEFT, false);
            CheckMenuItem(SC_ALIGN_MIDDLE_CENTER, false);
            CheckMenuItem(SC_ALIGN_MIDDLE_RIGHT, false);
            CheckMenuItem(SC_ALIGN_BOTTOM_LEFT, false);
            CheckMenuItem(SC_ALIGN_BOTTOM_CENTER, false);
            CheckMenuItem(SC_ALIGN_BOTTOM_RIGHT, false);
            CheckMenuItem(SC_ALIGN_DEFAULT, false);
            CheckMenuItem(SC_ALIGN_CUSTOM, false);
        }

        public void UncheckSizeMenu()
        {
            CheckMenuItem(SC_SIZE_640_480, false);
            CheckMenuItem(SC_SIZE_720_480, false);
            CheckMenuItem(SC_SIZE_720_576, false);
            CheckMenuItem(SC_SIZE_800_600, false);
            CheckMenuItem(SC_SIZE_1024_768, false);
            CheckMenuItem(SC_SIZE_1152_864, false);
            CheckMenuItem(SC_SIZE_1280_768, false);
            CheckMenuItem(SC_SIZE_1280_800, false);
            CheckMenuItem(SC_SIZE_1280_960, false);
            CheckMenuItem(SC_SIZE_1280_1024, false);
            CheckMenuItem(SC_SIZE_1440_900, false);
            CheckMenuItem(SC_SIZE_1600_900, false);
            CheckMenuItem(SC_SIZE_1680_1050, false);
            CheckMenuItem(SC_SIZE_DEFAULT, false);
            CheckMenuItem(SC_SIZE_CUSTOM, false);
        }

        public void UncheckTransparencyMenu()
        {
            CheckMenuItem(SC_TRANS_100, false);
            CheckMenuItem(SC_TRANS_90, false);
            CheckMenuItem(SC_TRANS_80, false);
            CheckMenuItem(SC_TRANS_70, false);
            CheckMenuItem(SC_TRANS_60, false);
            CheckMenuItem(SC_TRANS_50, false);
            CheckMenuItem(SC_TRANS_40, false);
            CheckMenuItem(SC_TRANS_30, false);
            CheckMenuItem(SC_TRANS_20, false);
            CheckMenuItem(SC_TRANS_10, false);
            CheckMenuItem(SC_TRANS_00, false);
            CheckMenuItem(SC_TRANS_CUSTOM, false);
            CheckMenuItem(SC_TRANS_DEFAULT, false);
        }

        #endregion
    }
}