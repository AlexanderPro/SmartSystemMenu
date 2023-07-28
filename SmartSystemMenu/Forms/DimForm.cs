using System;
using System.Drawing;
using System.Windows.Forms;
using SmartSystemMenu.Native.Enums;
using static SmartSystemMenu.Native.User32;
using static SmartSystemMenu.Native.Constants;

namespace SmartSystemMenu.Forms
{
    public partial class DimForm : Form
    {
        public DimForm(Color backColor, double opacity)
        {
            InitializeComponent();
            
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            StartPosition = FormStartPosition.Manual;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            ControlBox = false;
            BackColor = backColor;
            Opacity = opacity;
            Width = 1;
            Height = 1;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var exStyle = GetWindowLong(Handle, GWL_EXSTYLE);
            SetWindowLong(Handle, GWL_EXSTYLE, exStyle | WS_EX_NOACTIVATE);
            ShowWindow(Handle, (int)WindowShowStyle.ShowMaximized);
        }
    }
}
