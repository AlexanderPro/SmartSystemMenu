using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace SmartSystemMenu.Code.Common
{
    class SystemTrayMenu
    {
        public ToolStripMenuItem MenuItemAutoStart { get; private set; }
        public ToolStripMenuItem MenuItemAbout { get; private set; }
        public ToolStripMenuItem MenuItemExit { get; private set; }
        public NotifyIcon Icon { get; private set; }

        public SystemTrayMenu()
        {
            MenuItemAutoStart = new ToolStripMenuItem();
            MenuItemAutoStart.Name = "miAutoStart";
            MenuItemAutoStart.Size = new Size(175, 22);
            MenuItemAutoStart.Text = "Auto start program";

            MenuItemAbout = new ToolStripMenuItem();
            MenuItemAbout.Name = "miAbout";
            MenuItemAbout.Size = new Size(175, 22);
            MenuItemAbout.Text = "About";

            var menuItemSeparator = new ToolStripSeparator();
            menuItemSeparator.Name = "miSeparator";
            menuItemSeparator.Size = new Size(172, 6);

            MenuItemExit = new ToolStripMenuItem();
            MenuItemExit.Name = "miExit";
            MenuItemExit.Size = new Size(175, 22);
            MenuItemExit.Text = "Exit";

            var components = new System.ComponentModel.Container();
            var systemTrayMenu = new ContextMenuStrip(components);
            systemTrayMenu.Items.AddRange(new ToolStripItem[] { MenuItemAutoStart, MenuItemAbout, menuItemSeparator, MenuItemExit });
            systemTrayMenu.Name = "systemTrayMenu";
            systemTrayMenu.Size = new Size(176, 80);

            Icon = new NotifyIcon(components);
            Icon.ContextMenuStrip = systemTrayMenu;
            Icon.Icon = Properties.Resources.SmartSystemMenu;
            Icon.Text = AssemblyUtility.AssemblyTitle;
            Icon.Visible = true;
        }
    }
}