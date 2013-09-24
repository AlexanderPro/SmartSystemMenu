using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartSystemMenu.App_Code.Common
{
    class SystemMenu
    {
        #region Fields.Private

        private IntPtr _transparencyMenuHandle;
        private IntPtr _sizeMenuHandle;
        private IntPtr _systemTrayMenuHandle;

        #endregion


        #region Properties.Public

        public IntPtr WindowHandle { get; private set; }

        public Boolean Exists
        {
            get
            {
                IntPtr windowMenuHandle = NativeMethods.GetSystemMenu(WindowHandle, false);
                Boolean existsWindowMenu = windowMenuHandle != IntPtr.Zero;
                return existsWindowMenu;
            }
        }

        #endregion


        #region Constants.Public

        public const Int32 SC_TRANS100 = 0x4740;
        public const Int32 SC_TRANS90 = 0x4742;
        public const Int32 SC_TRANS80 = 0x4744;
        public const Int32 SC_TRANS70 = 0x4746;
        public const Int32 SC_TRANS60 = 0x4748;
        public const Int32 SC_TRANS50 = 0x4750;
        public const Int32 SC_TRANS40 = 0x4752;
        public const Int32 SC_TRANS30 = 0x4754;
        public const Int32 SC_TRANS20 = 0x4756;
        public const Int32 SC_TRANS10 = 0x4758;
        public const Int32 SC_TRANS00 = 0x4760;
        public const Int32 SC_TRANS_CUSTOM = 0x4761;
        public const Int32 SC_TRANS_CURRENT = 0x4762;
        public const Int32 SC_TOPMOST = 0x4763;
        public const Int32 SC_SIZE_640_480 = 0x4765;
        public const Int32 SC_SIZE_720_480 = 0x4766;
        public const Int32 SC_SIZE_720_576 = 0x4767;
        public const Int32 SC_SIZE_800_600 = 0x4768;
        public const Int32 SC_SIZE_1024_768 = 0x4769;
        public const Int32 SC_SIZE_1152_864 = 0x4770;
        public const Int32 SC_SIZE_1280_768 = 0x4771;
        public const Int32 SC_SIZE_1280_800 = 0x4772;
        public const Int32 SC_SIZE_1280_960 = 0x4773;
        public const Int32 SC_SIZE_1280_1024 = 0x4774;
        public const Int32 SC_SIZE_1440_900 = 0x4775;
        public const Int32 SC_SIZE_1600_900 = 0x4776;
        public const Int32 SC_SIZE_1680_1050 = 0x4777;
        public const Int32 SC_SIZE_CURRENT = 0x4778;
        public const Int32 SC_SIZE_CUSTOM = 0x4779;
        public const Int32 SC_MINIMIZE_TO_SYSTEMTRAY = 0x4780;
        public const Int32 SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY = 0x4781;
        public const Int32 SC_INFORMATION = 0x4782;

        #endregion


        #region Methods.Public

        public SystemMenu(IntPtr windowHandle)
        {
            WindowHandle = windowHandle;
        }

        public void Create()
        {
            IntPtr windowMenuHandle = NativeMethods.GetSystemMenu(WindowHandle, false);
            Int32 index = NativeMethods.GetMenuItemCount(windowMenuHandle);

            NativeMethods.InsertMenu(windowMenuHandle, index, NativeMethods.MF_BYPOSITION | NativeMethods.MF_SEPARATOR, IntPtr.Zero, "");
            NativeMethods.InsertMenu(windowMenuHandle, index + 1, NativeMethods.MF_BYPOSITION, SC_INFORMATION, "Information");
            NativeMethods.InsertMenu(windowMenuHandle, index + 2, NativeMethods.MF_BYPOSITION, SC_TOPMOST, "Always On Top");

            _sizeMenuHandle = NativeMethods.CreateMenu();
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_SIZE_640_480, "640x480");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_SIZE_720_480, "720x480");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_SIZE_720_576, "720x576");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_SIZE_800_600, "800x600");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_SIZE_1024_768, "1024x768");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_SIZE_1152_864, "1152x864");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_SIZE_1280_768, "1280x768");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_SIZE_1280_800, "1280x800");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_SIZE_1280_960, "1280x960");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_SIZE_1280_1024, "1280x1024");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_SIZE_1440_900, "1440x900");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_SIZE_1600_900, "1600x900");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_SIZE_1680_1050, "1680x1050");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeMethods.MF_BYPOSITION | NativeMethods.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_SIZE_CURRENT, "Default");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeMethods.MF_BYPOSITION | NativeMethods.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_sizeMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_SIZE_CUSTOM, "Custom...");
            NativeMethods.InsertMenu(windowMenuHandle, index + 3, NativeMethods.MF_BYPOSITION | NativeMethods.MF_POPUP, _sizeMenuHandle, "Resize");

            _transparencyMenuHandle = NativeMethods.CreateMenu();
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_TRANS100, "100%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_TRANS90, "90%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_TRANS80, "80%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_TRANS70, "70%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_TRANS60, "60%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_TRANS50, "50%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_TRANS40, "40%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_TRANS30, "30%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_TRANS20, "20%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_TRANS10, "10%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_TRANS00, "0%");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeMethods.MF_BYPOSITION | NativeMethods.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_TRANS_CURRENT, "Default");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeMethods.MF_BYPOSITION | NativeMethods.MF_SEPARATOR, 0, "");
            NativeMethods.InsertMenu(_transparencyMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_TRANS_CUSTOM, "Custom...");
            NativeMethods.InsertMenu(windowMenuHandle, index + 4, NativeMethods.MF_BYPOSITION | NativeMethods.MF_POPUP, _transparencyMenuHandle, "Transparency");

            _systemTrayMenuHandle = NativeMethods.CreateMenu();
            NativeMethods.InsertMenu(_systemTrayMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_MINIMIZE_TO_SYSTEMTRAY, "&Mimimize To Tray\tCtrl+Shift+Arrow Down");
            NativeMethods.InsertMenu(_systemTrayMenuHandle, -1, NativeMethods.MF_BYPOSITION, SC_MINIMIZE_ALWAYS_TO_SYSTEMTRAY, "Mimimize To Tray Always");
            NativeMethods.InsertMenu(windowMenuHandle, index + 5, NativeMethods.MF_BYPOSITION | NativeMethods.MF_POPUP, _systemTrayMenuHandle, "System Tray");
        }

        public void Destroy()
        {
            IntPtr windowMenuHandle = NativeMethods.GetSystemMenu(WindowHandle, false);
            Int32 Index = NativeMethods.GetMenuItemCount(windowMenuHandle);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 1, NativeMethods.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 2, NativeMethods.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 3, NativeMethods.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 4, NativeMethods.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 5, NativeMethods.MF_BYPOSITION);
            NativeMethods.DeleteMenu(windowMenuHandle, Index - 6, NativeMethods.MF_BYPOSITION);
            NativeMethods.DestroyMenu(_sizeMenuHandle);
            NativeMethods.DestroyMenu(_transparencyMenuHandle);
            NativeMethods.DestroyMenu(_systemTrayMenuHandle);
            NativeMethods.GetSystemMenu(WindowHandle, true);
        }

        public void CheckOnTopMenuItem(Int32 id, Boolean check)
        {
            IntPtr windowMenuHandle = NativeMethods.GetSystemMenu(WindowHandle, false);
            NativeMethods.CheckMenuItem(windowMenuHandle, id, check ? NativeMethods.MF_CHECKED : NativeMethods.MF_UNCHECKED);
        }

        public void CheckSizeMenuItem(Int32 id, Boolean check)
        {
            NativeMethods.CheckMenuItem(_sizeMenuHandle, id, check ? NativeMethods.MF_CHECKED : NativeMethods.MF_UNCHECKED);
        }

        public void CheckTransparencyMenuItem(Int32 id, Boolean check)
        {
            NativeMethods.CheckMenuItem(_transparencyMenuHandle, id, check ? NativeMethods.MF_CHECKED : NativeMethods.MF_UNCHECKED);
        }

        public void CheckSystemTrayMenuItem(Int32 id, Boolean check)
        {
            NativeMethods.CheckMenuItem(_systemTrayMenuHandle, id, check ? NativeMethods.MF_CHECKED : NativeMethods.MF_UNCHECKED);
        }

        public Boolean IsOnTopMenuItemChecked(Int32 id)
        {
            IntPtr windowMenuHandle = NativeMethods.GetSystemMenu(WindowHandle, false);
            UInt32 flags = NativeMethods.GetMenuState(windowMenuHandle, id, NativeMethods.MF_BYCOMMAND);
            Boolean isChecked = (flags & NativeMethods.MF_CHECKED) != 0;
            return isChecked;
        }

        public Boolean IsSystemTrayMenuItemChecked(Int32 id)
        {
            UInt32 flags = NativeMethods.GetMenuState(_systemTrayMenuHandle, id, NativeMethods.MF_BYCOMMAND);
            Boolean isChecked = (flags & NativeMethods.MF_CHECKED) != 0;
            return isChecked;
        }

        public void UncheckTransparencyMenu()
        {
            CheckTransparencyMenuItem(SC_TRANS100, false);
            CheckTransparencyMenuItem(SC_TRANS90, false);
            CheckTransparencyMenuItem(SC_TRANS80, false);
            CheckTransparencyMenuItem(SC_TRANS70, false);
            CheckTransparencyMenuItem(SC_TRANS60, false);
            CheckTransparencyMenuItem(SC_TRANS50, false);
            CheckTransparencyMenuItem(SC_TRANS40, false);
            CheckTransparencyMenuItem(SC_TRANS30, false);
            CheckTransparencyMenuItem(SC_TRANS20, false);
            CheckTransparencyMenuItem(SC_TRANS10, false);
            CheckTransparencyMenuItem(SC_TRANS00, false);
            CheckTransparencyMenuItem(SC_TRANS_CUSTOM, false);
            CheckTransparencyMenuItem(SC_TRANS_CURRENT, false);
        }

        public void UncheckSizeMenu()
        {
            CheckSizeMenuItem(SC_SIZE_640_480, false);
            CheckSizeMenuItem(SC_SIZE_720_480, false);
            CheckSizeMenuItem(SC_SIZE_720_576, false);
            CheckSizeMenuItem(SC_SIZE_800_600, false);
            CheckSizeMenuItem(SC_SIZE_1024_768, false);
            CheckSizeMenuItem(SC_SIZE_1152_864, false);
            CheckSizeMenuItem(SC_SIZE_1280_768, false);
            CheckSizeMenuItem(SC_SIZE_1280_800, false);
            CheckSizeMenuItem(SC_SIZE_1280_960, false);
            CheckSizeMenuItem(SC_SIZE_1280_1024, false);
            CheckSizeMenuItem(SC_SIZE_1440_900, false);
            CheckSizeMenuItem(SC_SIZE_1600_900, false);
            CheckSizeMenuItem(SC_SIZE_1680_1050, false);
            CheckSizeMenuItem(SC_SIZE_CURRENT, false);
            CheckSizeMenuItem(SC_SIZE_CUSTOM, false);
        }

        #endregion
    }
}