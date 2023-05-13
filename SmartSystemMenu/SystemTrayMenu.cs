using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartSystemMenu.Settings;
using SmartSystemMenu.Extensions;

namespace SmartSystemMenu
{
    class SystemTrayMenu : IDisposable
    {
        private ContextMenuStrip _systemTrayMenu;
        private ToolStripMenuItem _menuItemAutoStart;
        private ToolStripMenuItem _menuItemRestore;
        private ToolStripMenuItem _menuItemSettings;
        private ToolStripMenuItem _menuItemAbout;
        private ToolStripMenuItem _menuItemExit;
        private ToolStripSeparator _menuItemSeparator1;
        private ToolStripSeparator _menuItemSeparator2;
        private NotifyIcon _icon;
        private SmartSystemMenuSettings _settings;
        private bool _created;

        public event EventHandler MenuItemAutoStartClick;
        public event EventHandler MenuItemSettingsClick;
        public event EventHandler MenuItemAboutClick;
        public event EventHandler MenuItemExitClick;
        public event EventHandler<EventArgs<long>> MenuItemRestoreClick;

        public SystemTrayMenu(SmartSystemMenuSettings settings)
        {
            _menuItemAutoStart = new ToolStripMenuItem();
            _menuItemRestore = new ToolStripMenuItem();
            _menuItemSettings = new ToolStripMenuItem();
            _menuItemAbout = new ToolStripMenuItem();
            _menuItemSeparator1 = new ToolStripSeparator();
            _menuItemSeparator2 = new ToolStripSeparator();
            _menuItemExit = new ToolStripMenuItem();

            var components = new Container();
            _systemTrayMenu = new ContextMenuStrip(components);
            _icon = new NotifyIcon(components);

            _settings = settings;
            _created = false;
        }

        public void Create()
        {
            if (!_created)
            {
                _menuItemAutoStart.Name = "miAutoStart";
                _menuItemAutoStart.Size = new Size(175, 22);
                _menuItemAutoStart.Text = _settings.Language.GetValue("mi_auto_start");
                _menuItemAutoStart.Click += _menuItemAutoStart_Click;

                _menuItemSettings.Name = "miSettings";
                _menuItemSettings.Size = new Size(175, 22);
                _menuItemSettings.Font = new Font(_menuItemSettings.Font.Name, _menuItemSettings.Font.Size, FontStyle.Bold);
                _menuItemSettings.Text = _settings.Language.GetValue("mi_settings");
                _menuItemSettings.Click += _menuItemSettings_Click;

                _menuItemAbout.Name = "miAbout";
                _menuItemAbout.Size = new Size(175, 22);
                _menuItemAbout.Text = _settings.Language.GetValue("mi_about");
                _menuItemAbout.Click += _menuItemAbout_Click;

                _menuItemSeparator1.Name = "miSeparator1";
                _menuItemSeparator1.Size = new Size(172, 6);

                _menuItemSeparator2.Name = "miSeparator2";
                _menuItemSeparator2.Size = new Size(172, 6);

                _menuItemExit.Name = "miExit";
                _menuItemExit.Size = new Size(175, 22);
                _menuItemExit.Text = _settings.Language.GetValue("mi_exit");
                _menuItemExit.Click += _menuItemExit_Click;

                var clickThroughItemName = MenuItemId.GetName(MenuItemId.SC_CLICK_THROUGH);
                var transparencyItemName = MenuItemId.GetName(MenuItemId.SC_TRANS);
                var menuItems = _settings.MenuItems.Items.Flatten(x => x.Items);
                var clickThroughAny = menuItems.Any(x => x.Type == MenuItemType.Item && x.Name == clickThroughItemName && x.Show);
                var transparencyAny = menuItems.Any(x => x.Type == MenuItemType.Group && x.Name == transparencyItemName && x.Show);

                if (clickThroughAny || transparencyAny)
                {
                    _menuItemRestore.Name = "miRestore";
                    _menuItemRestore.Size = new Size(175, 22);
                    _menuItemRestore.Text = _settings.Language.GetValue("mi_restore_windows");
                    
                    if (clickThroughAny)
                    {
                        var subMenuItem = new ToolStripMenuItem();
                        subMenuItem.Name = "miClickThrough";
                        subMenuItem.Size = new Size(175, 22);
                        subMenuItem.Text = _settings.Language.GetValue("click_through");
                        subMenuItem.Click += _menuItemRestore_Click;
                        _menuItemRestore.DropDownItems.Add(subMenuItem);
                    }

                    if (transparencyAny)
                    {
                        var subMenuItem = new ToolStripMenuItem();
                        subMenuItem.Name = "miTransparency";
                        subMenuItem.Size = new Size(175, 22);
                        subMenuItem.Text = _settings.Language.GetValue("transparency");
                        subMenuItem.Click += _menuItemRestore_Click;
                        _menuItemRestore.DropDownItems.Add(subMenuItem);
                    }

                    _systemTrayMenu.Items.AddRange(new ToolStripItem[] { _menuItemAutoStart, _menuItemSeparator1, _menuItemRestore, _menuItemSettings, _menuItemAbout, _menuItemSeparator2, _menuItemExit });
                }
                else
                {
                    _systemTrayMenu.Items.AddRange(new ToolStripItem[] { _menuItemAutoStart, _menuItemSeparator1, _menuItemSettings, _menuItemAbout, _menuItemSeparator2, _menuItemExit });
                }

                _systemTrayMenu.Name = "systemTrayMenu";
                _systemTrayMenu.Size = new Size(176, 80);

                _icon.ContextMenuStrip = _systemTrayMenu;
                _icon.Icon = Properties.Resources.SmartSystemMenu;
                _icon.Text = AssemblyUtils.AssemblyTitle;
                _icon.Visible = true;

                _created = true;
            }
        }

        public void CheckMenuItemAutoStart(bool check)
        {
            _menuItemAutoStart.Checked = check;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _menuItemAutoStart?.Dispose();
                _menuItemRestore?.Dispose();
                _menuItemSettings?.Dispose();
                _menuItemAbout?.Dispose();
                _menuItemExit?.Dispose();
                _menuItemSeparator1?.Dispose();
                _menuItemSeparator2?.Dispose();
                _systemTrayMenu?.Dispose();
                _icon.Visible = false;
                _icon.Dispose();
            }
        }

        ~SystemTrayMenu()
        {
            Dispose(false);
        }

        private void _menuItemAutoStart_Click(object sender, EventArgs e)
        {
            var handler = MenuItemAutoStartClick;
            handler?.Invoke(sender, e);
        }

        private void _menuItemRestore_Click(object sender, EventArgs e)
        {
            var handler = MenuItemRestoreClick;
            if (handler != null && sender is ToolStripMenuItem menuItem)
            {
                var menuItemId = menuItem.Name == "miClickThrough" ? MenuItemId.SC_CLICK_THROUGH : MenuItemId.SC_TRANS_DEFAULT;
                handler.Invoke(sender, new EventArgs<long>(menuItemId));
            }
        }

        private void _menuItemSettings_Click(object sender, EventArgs e)
        {
            var handler = MenuItemSettingsClick;
            handler?.Invoke(sender, e);
        }

        private void _menuItemAbout_Click(object sender, EventArgs e)
        {
            var handler = MenuItemAboutClick;
            handler?.Invoke(sender, e);
        }

        private void _menuItemExit_Click(object sender, EventArgs e)
        {
            var handler = MenuItemExitClick;
            handler?.Invoke(sender, e);
        }
    }
}