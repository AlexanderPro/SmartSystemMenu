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
        private readonly ContextMenuStrip _systemTrayMenu;
        private readonly ToolStripMenuItem _menuItemAutoStart;
        private readonly ToolStripMenuItem _menuItemRestore;
        private readonly ToolStripMenuItem _menuItemSettings;
        private readonly ToolStripMenuItem _menuItemAbout;
        private readonly ToolStripMenuItem _menuItemExit;
        private readonly ToolStripSeparator _menuItemSeparator1;
        private readonly ToolStripSeparator _menuItemSeparator2;
        private readonly NotifyIcon _icon;
        private readonly ApplicationSettings _settings;
        private bool _created;

        public event EventHandler MenuItemAutoStartClick;
        public event EventHandler MenuItemSettingsClick;
        public event EventHandler MenuItemAboutClick;
        public event EventHandler MenuItemExitClick;
        public event EventHandler<EventArgs<long>> MenuItemRestoreClick;

        public SystemTrayMenu(ApplicationSettings settings)
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
                _menuItemAutoStart.Click += ItemAutoStartClick;

                _menuItemSettings.Name = "miSettings";
                _menuItemSettings.Size = new Size(175, 22);
                _menuItemSettings.Font = new Font(_menuItemSettings.Font.Name, _menuItemSettings.Font.Size, FontStyle.Bold);
                _menuItemSettings.Text = _settings.Language.GetValue("mi_settings");
                _menuItemSettings.Click += ItemSettingsClick;

                _menuItemAbout.Name = "miAbout";
                _menuItemAbout.Size = new Size(175, 22);
                _menuItemAbout.Text = _settings.Language.GetValue("mi_about");
                _menuItemAbout.Click += ItemAboutClick;

                _menuItemSeparator1.Name = "miSeparator1";
                _menuItemSeparator1.Size = new Size(172, 6);

                _menuItemSeparator2.Name = "miSeparator2";
                _menuItemSeparator2.Size = new Size(172, 6);

                _menuItemExit.Name = "miExit";
                _menuItemExit.Size = new Size(175, 22);
                _menuItemExit.Text = _settings.Language.GetValue("mi_exit");
                _menuItemExit.Click += ItemExitClick;

                var hideItemName = MenuItemId.GetName(MenuItemId.SC_HIDE);
                var clickThroughItemName = MenuItemId.GetName(MenuItemId.SC_CLICK_THROUGH);
                var transparencyItemName = MenuItemId.GetName(MenuItemId.SC_TRANS);
                var dimmerItemName = MenuItemId.GetName(MenuItemId.SC_DIMMER);
                var menuItems = _settings.MenuItems.Items.Flatten(x => x.Items);
                var hideAny = menuItems.Any(x => x.Type == MenuItemType.Item && x.Name == hideItemName && x.Show);
                var clickThroughAny = menuItems.Any(x => x.Type == MenuItemType.Item && x.Name == clickThroughItemName && x.Show);
                var transparencyAny = menuItems.Any(x => x.Type == MenuItemType.Group && x.Name == transparencyItemName && x.Show);
                var dimmerAny = menuItems.Any(x => x.Type == MenuItemType.Group && x.Name == dimmerItemName && x.Show);

                if (hideAny || clickThroughAny || transparencyAny || dimmerAny)
                {
                    _menuItemRestore.Name = "miRestore";
                    _menuItemRestore.Size = new Size(175, 22);
                    _menuItemRestore.Text = _settings.Language.GetValue("mi_restore_windows");

                    if (hideAny)
                    {
                        var subMenuItem = new ToolStripMenuItem();
                        subMenuItem.Name = "miHide";
                        subMenuItem.Size = new Size(175, 22);
                        subMenuItem.Text = _settings.Language.GetValue("hide");
                        subMenuItem.Click += ItemRestoreClick;
                        _menuItemRestore.DropDownItems.Add(subMenuItem);
                    }

                    if (clickThroughAny)
                    {
                        var subMenuItem = new ToolStripMenuItem();
                        subMenuItem.Name = "miClickThrough";
                        subMenuItem.Size = new Size(175, 22);
                        subMenuItem.Text = _settings.Language.GetValue("click_through");
                        subMenuItem.Click += ItemRestoreClick;
                        _menuItemRestore.DropDownItems.Add(subMenuItem);
                    }

                    if (transparencyAny)
                    {
                        var subMenuItem = new ToolStripMenuItem();
                        subMenuItem.Name = "miTransparency";
                        subMenuItem.Size = new Size(175, 22);
                        subMenuItem.Text = _settings.Language.GetValue("transparency");
                        subMenuItem.Click += ItemRestoreClick;
                        _menuItemRestore.DropDownItems.Add(subMenuItem);
                    }

                    if (dimmerAny)
                    {
                        var subMenuItem = new ToolStripMenuItem();
                        subMenuItem.Name = "miDimmer";
                        subMenuItem.Size = new Size(175, 22);
                        subMenuItem.Text = _settings.Language.GetValue("dimmer");
                        subMenuItem.Click += ItemRestoreClick;
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
                _icon.DoubleClick += ItemSettingsClick;

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

        private void ItemAutoStartClick(object sender, EventArgs e)
        {
            var handler = MenuItemAutoStartClick;
            handler?.Invoke(sender, e);
        }

        private void ItemRestoreClick(object sender, EventArgs e)
        {
            var handler = MenuItemRestoreClick;
            if (handler != null && sender is ToolStripMenuItem menuItem)
            {
                var menuItemId = menuItem.Name == "miHide" ? MenuItemId.SC_HIDE : menuItem.Name == "miClickThrough" ? MenuItemId.SC_CLICK_THROUGH : menuItem.Name == "miTransparency" ? MenuItemId.SC_TRANS_DEFAULT : MenuItemId.SC_DIMMER_OFF;
                handler.Invoke(sender, new EventArgs<long>(menuItemId));
            }
        }

        private void ItemSettingsClick(object sender, EventArgs e)
        {
            var handler = MenuItemSettingsClick;
            handler?.Invoke(sender, e);
        }

        private void ItemAboutClick(object sender, EventArgs e)
        {
            var handler = MenuItemAboutClick;
            handler?.Invoke(sender, e);
        }

        private void ItemExitClick(object sender, EventArgs e)
        {
            var handler = MenuItemExitClick;
            handler?.Invoke(sender, e);
        }
    }
}