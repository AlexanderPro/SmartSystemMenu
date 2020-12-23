using System;
using SmartSystemMenu.Native;

namespace SmartSystemMenu
{
    class WindowInfo
    {
        public string GetWindowText { get; set; }

        public string WM_GETTEXT { get; set; }

        public string GetClassName { get; set; }

        public string RealGetWindowClass { get; set; }

        public string FontFace { get; set; }

        public IntPtr Handle { get; set; }

        public IntPtr ParentHandle { get; set; }

        public Rect Size { get; set; }

        public IntPtr Instance { get; set; }

        public int ProcessId { get; set; }

        public uint ThreadId { get; set; }

        public int GCL_WNDPROC { get; set; }

        public int DWL_DLGPROC { get; set; }

        public int GWL_STYLE { get; set; }

        public int GCL_STYLE { get; set; }

        public int GWL_EXSTYLE { get; set; }

        public uint WindowInfoExStyle { get; set; }

        public bool LWA_ALPHA { get; set; }

        public bool LWA_COLORKEY { get; set; }

        public int GWL_ID { get; set; }

        public int GWL_USERDATA { get; set; }

        public int DWL_USER { get; set; }

        public string AccessibleName { get; set; }

        public string AccessibleValue { get; set; }

        public string AccessibleRole { get; set; }

        public string AccessibleDescription { get; set; }

        public string FullPath { get; set; }

        public string CommandLine { get; set; }

        public string WorkingDirectory { get; set; }

        public string Owner { get; set; }

        public DateTime? StartTime { get; set; }

        public string Parent { get; set; }

        public Priority Priority { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public string FileVersion { get; set; }

        public string ProductVersion { get; set; }

        public string Copyright { get; set; }

        public WindowInfo()
        {
            GetWindowText = "";
            WM_GETTEXT = "";
            GetClassName = "";
            RealGetWindowClass = "";
            FontFace = "";
            Handle = IntPtr.Zero;
            ParentHandle = IntPtr.Zero;
            Size = new Rect();
            Instance = IntPtr.Zero;
            ProcessId = 0;
            ThreadId = 0;
            GCL_WNDPROC = 0;
            DWL_DLGPROC = 0;
            GWL_STYLE = 0;
            GCL_STYLE = 0;
            GWL_EXSTYLE = 0;
            WindowInfoExStyle = 0;
            LWA_ALPHA = false;
            LWA_COLORKEY = false;
            GWL_ID = 0;
            GWL_USERDATA = 0;
            DWL_USER = 0;
            AccessibleName = "";
            AccessibleValue = "";
            AccessibleRole = "";
            AccessibleDescription = "";
            FullPath = "";
            CommandLine = "";
            WorkingDirectory = "";
            Owner = "";
            StartTime = null;
            Parent = "";
            Priority = 0;
            ProductName = "";
            ProductDescription = "";
            FileVersion = "";
            ProductVersion = "";
            Copyright = "";
        }
    }
}