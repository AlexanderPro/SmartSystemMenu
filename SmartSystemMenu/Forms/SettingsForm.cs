using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using SmartSystemMenu.Settings;
using SmartSystemMenu.Controls;

namespace SmartSystemMenu.Forms
{
    public partial class SettingsForm : Form
    {
        private SmartSystemMenuSettings _settings;
        private CloserSettings _closerSettings;

        public event EventHandler<EventArgs<SmartSystemMenuSettings>> OkClick;

        public SettingsForm(SmartSystemMenuSettings settings)
        {            
            InitializeComponent();

            try
            {
                _settings = settings;
                _closerSettings = (CloserSettings)settings.Closer.Clone();
                InitializeControls(settings);
            }
            catch
            {
                tabMain.Enabled = false;
                btnApply.Enabled = false;
                MessageBox.Show("Failed to read the settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeControls(SmartSystemMenuSettings settings)
        {
            tabpGeneral.Text = settings.Language.GetValue("tab_settings_general");
            tabpMenuStart.Text = settings.Language.GetValue("tab_settings_menu_start");
            tabpMenuSize.Text = settings.Language.GetValue("tab_settings_menu_size");
            tabpMenu.Text = settings.Language.GetValue("tab_settings_menu");
            tabpMenuSaveSelectedItems.Text = settings.Language.GetValue("tab_settings_menu_save");
            grpbLanguage.Text = settings.Language.GetValue("grpb_language");
            grpbProcessExclusions.Text = settings.Language.GetValue("grpb_process_exclusions");
            grpbStartProgram.Text = settings.Language.GetValue("grpb_start_program");
            grpbWindowSize.Text = settings.Language.GetValue("grpb_window_size");
            grpbCloser.Text = settings.Language.GetValue("grpb_closer");
            grpbSizer.Text = settings.Language.GetValue("grpb_sizer");
            grpbDisplay.Text = settings.Language.GetValue("grpb_display");
            chkEnableHighDPI.Text = settings.Language.GetValue("chk_enable_high_dpi");
            clmProcessExclusionName.HeaderText = settings.Language.GetValue("clm_process_exclusion_name");
            clmProcessExclusionEdit.ToolTipText = settings.Language.GetValue("clm_process_exclusion_edit");
            clmProcessExcusionDelete.ToolTipText = settings.Language.GetValue("clm_process_exclusion_delete");
            clmStartProgramTitle.HeaderText = settings.Language.GetValue("clm_start_program_title");
            clmStartProgramPath.HeaderText = settings.Language.GetValue("clm_start_program_path");
            clmStartProgramArguments.HeaderText = settings.Language.GetValue("clm_start_program_arguments");
            clmStartProgramEdit.ToolTipText = settings.Language.GetValue("clm_start_program_edit");
            clmStartProgramDelete.ToolTipText = settings.Language.GetValue("clm_start_program_delete");
            clmWindowSizeTitle.HeaderText = settings.Language.GetValue("clm_window_size_title");
            clmWindowSizeLeft.HeaderText = settings.Language.GetValue("clm_window_size_left");
            clmWindowSizeTop.HeaderText = settings.Language.GetValue("clm_window_size_top");
            clmWindowSizeWidth.HeaderText = settings.Language.GetValue("clm_window_size_width");
            clmWindowSizeHeight.HeaderText = settings.Language.GetValue("clm_window_size_height");
            clmWindowSizeEdit.ToolTipText = settings.Language.GetValue("clm_window_size_edit");
            clmWindowSizeDelete.ToolTipText = settings.Language.GetValue("clm_window_size_delete");
            clmnMenuItemName.HeaderText = settings.Language.GetValue("clm_hotkeys_name");
            clmnHotkeys.HeaderText = settings.Language.GetValue("clm_hotkeys_keys");
            toolTipAddProcessName.SetToolTip(btnProcessExclusionDown, settings.Language.GetValue("btn_process_exclusion_down"));
            toolTipAddProcessName.SetToolTip(btnProcessExclusionUp, settings.Language.GetValue("btn_process_exclusion_up"));
            toolTipAddProcessName.SetToolTip(btnAddProcessExclusion, settings.Language.GetValue("btn_add_process_exclusion"));
            toolTipAddProcessName.SetToolTip(btnAddStartProgram, settings.Language.GetValue("btn_add_start_program"));
            toolTipAddProcessName.SetToolTip(btnStartProgramDown, settings.Language.GetValue("btn_start_program_down"));
            toolTipAddProcessName.SetToolTip(btnStartProgramUp, settings.Language.GetValue("btn_start_program_up"));
            toolTipAddProcessName.SetToolTip(btnAddWindowSize, settings.Language.GetValue("btn_add_window_size"));
            toolTipAddProcessName.SetToolTip(btnWindowSizeDown, settings.Language.GetValue("btn_window_size_down"));
            toolTipAddProcessName.SetToolTip(btnWindowSizeUp, settings.Language.GetValue("btn_window_size_up"));
            toolTipAddProcessName.SetToolTip(btnMenuItemDown, settings.Language.GetValue("btn_menu_item_down"));
            toolTipAddProcessName.SetToolTip(btnMenuItemUp, settings.Language.GetValue("btn_menu_item_up"));
            chkAeroGlass.Text = settings.Language.GetValue("aero_glass");
            chkAlwaysOnTop.Text = settings.Language.GetValue("always_on_top");
            chkAlignment.Text = settings.Language.GetValue("alignment");
            chkHideForAltTab.Text = settings.Language.GetValue("hide_for_alt_tab");
            chkTransparency.Text = settings.Language.GetValue("transparency");
            chkPriority.Text = settings.Language.GetValue("priority");
            chkMinimizeToTrayAlways.Text = settings.Language.GetValue("minimize_always_to_systemtray");
            chkButtons.Text = settings.Language.GetValue("buttons");
            btnCloser.Text = settings.Language.GetValue("closer_button_name");
            btnApply.Text = settings.Language.GetValue("settings_btn_apply");
            btnCancel.Text = settings.Language.GetValue("settings_btn_cancel");
            Text = settings.Language.GetValue("settings_form");

            foreach (var processExclusion in settings.ProcessExclusions)
            {
                var index = gvProcessExclusions.Rows.Add();
                var row = gvProcessExclusions.Rows[index];
                row.Cells[0].Value = processExclusion;
                row.Cells[1].ToolTipText = settings.Language.GetValue("clm_process_exclusion_edit");
                row.Cells[2].ToolTipText = settings.Language.GetValue("clm_process_exclusion_delete");
            }

            foreach (var item in settings.MenuItems.WindowSizeItems)
            {
                var index = gvWindowSize.Rows.Add();
                var row = gvWindowSize.Rows[index];
                row.Tag = (WindowSizeMenuItem)item.Clone();
                row.Cells[0].Value = item.Title;
                row.Cells[1].Value = item.Left.HasValue ? item.Left.ToString() : string.Empty;
                row.Cells[2].Value = item.Top.HasValue ? item.Top.ToString() : string.Empty;
                row.Cells[3].Value = item.Width.ToString();
                row.Cells[4].Value = item.Height.ToString();
                row.Cells[5].Value = item.ToString();
                row.Cells[6].ToolTipText = settings.Language.GetValue("clm_window_size_edit");
                row.Cells[7].ToolTipText = settings.Language.GetValue("clm_window_size_delete");
            }

            foreach (var item in settings.MenuItems.StartProgramItems)
            {
                var cloneItem = (StartProgramMenuItem)item.Clone();
                var index = gvStartProgram.Rows.Add();
                var row = gvStartProgram.Rows[index];
                row.Cells[0].Value = cloneItem.Title;
                row.Cells[1].Value = cloneItem.FileName;
                row.Cells[2].Value = cloneItem.Arguments;
                row.Cells[3].ToolTipText = settings.Language.GetValue("clm_start_program_edit");
                row.Cells[4].ToolTipText = settings.Language.GetValue("clm_start_program_delete");
                row.Tag = cloneItem;
            }

            cmbLanguage.DisplayMember = "Text";
            cmbLanguage.ValueMember = "Value";

            var languageItems = new[] {
                new { Text = "", Value = "" },
                new { Text = "English", Value = "en" },
                new { Text = "Deutsch", Value = "de" },
                new { Text = "Français", Value = "fr" },
                new { Text = "Italiano", Value = "it" },
                new { Text = "Magyar", Value = "hu" },
                new { Text = "Português", Value = "pt" },
                new { Text = "Русский", Value = "ru" },
                new { Text = "Српски", Value = "sr" },
                new { Text = "简体中文", Value = "zh_cn" },
                new { Text = "繁體中文", Value = "zh_tw"},
                new { Text = "日本語", Value = "ja" },
                new { Text = "한국어", Value = "ko" }
            };

            cmbLanguage.DataSource = languageItems;
            cmbLanguage.SelectedValue = settings.LanguageName;

            cmbSizer.Items.Add(settings.Language.GetValue("sizer_window_with_margins"));
            cmbSizer.Items.Add(settings.Language.GetValue("sizer_window_without_margins"));
            cmbSizer.Items.Add(settings.Language.GetValue("sizer_window_client_area"));
            cmbSizer.SelectedIndex = (int)settings.Sizer;
            chkEnableHighDPI.Checked = settings.EnableHighDPI;
            chkAeroGlass.Checked = settings.SaveSelectedItems.AeroGlass;
            chkAlwaysOnTop.Checked = settings.SaveSelectedItems.AlwaysOnTop;
            chkHideForAltTab.Checked = settings.SaveSelectedItems.HideForAltTab;
            chkAlignment.Checked = settings.SaveSelectedItems.Alignment;
            chkTransparency.Checked = settings.SaveSelectedItems.Transparency;
            chkPriority.Checked = settings.SaveSelectedItems.Priority;
            chkMinimizeToTrayAlways.Checked = settings.SaveSelectedItems.MinimizeToTrayAlways;
            chkButtons.Checked = settings.SaveSelectedItems.Buttons;

            var items = new List<Settings.MenuItem>();
            foreach(var item in settings.MenuItems.Items)
            {
                items.Add((Settings.MenuItem)item.Clone());
            }
            FillGridViewHotKeys(gvHotkeys, items, settings.Language);
        }

        private void GridViewProcessExclusionsCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;

            if (grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 1)
                {
                    var cell = grid.Rows[e.RowIndex].Cells[0];
                    var dialog = new ProcessExclusionForm(cell.Value.ToString(), _settings.Language);
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        cell.Value = dialog.ProcessName;
                    }
                }

                if (e.ColumnIndex == 2)
                {
                    grid.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void GridViewStartProgramCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;

            if (grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                var row = grid.Rows[e.RowIndex];
                if (e.ColumnIndex == 3 && row.Tag is StartProgramMenuItem menuItem)
                {
                    var dialog = new StartProgramForm(menuItem, _settings.Language);
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        row.Cells[0].Value = dialog.MenuItem.Title;
                        row.Cells[1].Value = dialog.MenuItem.FileName;
                        row.Cells[2].Value = dialog.MenuItem.Arguments;
                        row.Tag = dialog.MenuItem;
                    }
                }

                if (e.ColumnIndex == 4)
                {
                    grid.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void GridViewWindowSizeCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;

            if (grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 6 && grid.Rows[e.RowIndex].Tag is WindowSizeMenuItem menuItem)
                {
                    var dialog = new SettingsSizeForm(_settings.Language, menuItem);
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        var row = grid.Rows[e.RowIndex];
                        row.Cells[0].Value = dialog.MenuItem.Title;
                        row.Cells[1].Value = dialog.MenuItem.Left.HasValue ? dialog.MenuItem.Left.ToString() : string.Empty;
                        row.Cells[2].Value = dialog.MenuItem.Top.HasValue ? dialog.MenuItem.Top.ToString() : string.Empty;
                        row.Cells[3].Value = dialog.MenuItem.Width.ToString();
                        row.Cells[4].Value = dialog.MenuItem.Height.ToString();
                        row.Cells[5].Value = dialog.MenuItem.ToString();

                        menuItem.Title = dialog.MenuItem.Title;
                        menuItem.Left = dialog.MenuItem.Left;
                        menuItem.Top = dialog.MenuItem.Top;
                        menuItem.Width = dialog.MenuItem.Width;
                        menuItem.Height = dialog.MenuItem.Height;
                        menuItem.Key1 = dialog.MenuItem.Key1;
                        menuItem.Key2 = dialog.MenuItem.Key2;
                        menuItem.Key3 = dialog.MenuItem.Key3;
                    }
                }

                if (e.ColumnIndex == 7)
                {
                    grid.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void GridViewProcessExclusionsCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                var cell = grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var dialog = new ProcessExclusionForm(cell.Value.ToString(), _settings.Language);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    cell.Value = dialog.ProcessName;
                }
            }
        }

        private void GridViewHotkeysCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;
            if (grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                var row = grid.Rows[e.RowIndex];
                if (!row.ReadOnly)
                {
                    ShowHotkeysForm(row);
                }
            }

            if (grid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
            {
                var row = grid.Rows[e.RowIndex];
                var cell = (DataGridViewCheckBoxCell)row.Cells[e.ColumnIndex];
                cell.Value = !(bool)cell.Value;
                var menuItem = (Settings.MenuItem)row.Tag;
                menuItem.Show = (bool)cell.Value;
            }
        }

        private void GridViewHotkeysCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;
            if ((e.ColumnIndex == 0 || e.ColumnIndex == 1) && e.RowIndex >= 0)
            {
                var row = grid.Rows[e.RowIndex];
                if (!row.ReadOnly)
                {
                    ShowHotkeysForm(row);
                }
            }
        }

        private void GridViewStartProgramCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;
            var row = grid.Rows[e.RowIndex];
            if ((e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2) && e.RowIndex >= 0 && row.Tag is StartProgramMenuItem menuItem)
            {
                var dialog = new StartProgramForm(menuItem, _settings.Language);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    row.Cells[0].Value = dialog.MenuItem.Title;
                    row.Cells[1].Value = dialog.MenuItem.FileName;
                    row.Cells[2].Value = dialog.MenuItem.Arguments;
                    row.Tag = dialog.MenuItem;
                }
            }
        }

        private void GridViewWindowSizeCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;
            if ((e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5) && e.RowIndex >= 0 && grid.Rows[e.RowIndex].Tag is WindowSizeMenuItem menuItem)
            {
                var dialog = new SettingsSizeForm(_settings.Language, menuItem);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    var row = grid.Rows[e.RowIndex];
                    row.Cells[0].Value = dialog.MenuItem.Title;
                    row.Cells[1].Value = dialog.MenuItem.Left.HasValue ? dialog.MenuItem.Left.ToString() : string.Empty;
                    row.Cells[2].Value = dialog.MenuItem.Top.HasValue ? dialog.MenuItem.Top.ToString() : string.Empty;
                    row.Cells[3].Value = dialog.MenuItem.Width.ToString();
                    row.Cells[4].Value = dialog.MenuItem.Height.ToString();
                    row.Cells[5].Value = dialog.MenuItem.ToString();

                    menuItem.Title = dialog.MenuItem.Title;
                    menuItem.Left = dialog.MenuItem.Left;
                    menuItem.Top = dialog.MenuItem.Top;
                    menuItem.Width = dialog.MenuItem.Width;
                    menuItem.Height = dialog.MenuItem.Height;
                    menuItem.Key1 = dialog.MenuItem.Key1;
                    menuItem.Key2 = dialog.MenuItem.Key2;
                    menuItem.Key3 = dialog.MenuItem.Key3;
                }
            }
        }

        private void ButtonAddProcessExclusionClick(object sender, EventArgs e)
        {
            var dialog = new ProcessExclusionForm("", _settings.Language);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var index = gvProcessExclusions.Rows.Add();
                var row = gvProcessExclusions.Rows[index];
                row.Cells[0].Value = dialog.ProcessName;
                row.Cells[1].ToolTipText = _settings.Language.GetValue("clm_process_exclusion_edit");
                row.Cells[2].ToolTipText = _settings.Language.GetValue("clm_process_exclusion_delete");
            }
        }

        private void ButtonAddStartProgramClick(object sender, EventArgs e)
        {
            var dialog = new StartProgramForm(null, _settings.Language);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var index = gvStartProgram.Rows.Add();
                var row = gvStartProgram.Rows[index];
                row.Cells[0].Value = dialog.MenuItem.Title;
                row.Cells[1].Value = dialog.MenuItem.FileName;
                row.Cells[2].Value = dialog.MenuItem.Arguments;
                row.Cells[3].ToolTipText = _settings.Language.GetValue("clm_start_program_edit");
                row.Cells[4].ToolTipText = _settings.Language.GetValue("clm_start_program_delete");
                row.Tag = dialog.MenuItem;
            }
        }

        private void ButtonAddWindowSizeClick(object sender, EventArgs e)
        {
            var dialog = new SettingsSizeForm(_settings.Language, new WindowSizeMenuItem { Width = 1, Height = 1 });
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var index = gvWindowSize.Rows.Add();
                var row = gvWindowSize.Rows[index];
                row.Cells[0].Value = dialog.MenuItem.Title;
                row.Cells[1].Value = dialog.MenuItem.Left.HasValue ? dialog.MenuItem.Left.ToString() : string.Empty;
                row.Cells[2].Value = dialog.MenuItem.Top.HasValue ? dialog.MenuItem.Top.ToString() : string.Empty;
                row.Cells[3].Value = dialog.MenuItem.Width.ToString();
                row.Cells[4].Value = dialog.MenuItem.Height.ToString();
                row.Cells[5].Value = dialog.MenuItem.ToString();
                row.Cells[6].ToolTipText = _settings.Language.GetValue("clm_window_size_edit");
                row.Cells[7].ToolTipText = _settings.Language.GetValue("clm_window_size_delete");
                row.Tag = dialog.MenuItem;
            }
        }

        private void ButtonArrowUpClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var grid = button.Name == "btnProcessExclusionUp" ? gvProcessExclusions : button.Name == "btnWindowSizeUp" ? gvWindowSize : gvStartProgram;
            if (grid.SelectedRows.Count > 0)
            {
                var index = grid.SelectedRows[0].Index;
                var newIndex = index > 0 ? index - 1 : 0;
                var selectedRow = grid.SelectedRows[0];
                grid.Rows.RemoveAt(index);
                grid.Rows.Insert(newIndex, selectedRow);
                grid.Rows[newIndex].Selected = true;
                grid.CurrentCell = grid.Rows[newIndex].Cells[0];
            }
        }

        private void ButtonArrowDownClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var grid = button.Name == "btnProcessExclusionDown" ? gvProcessExclusions : button.Name == "btnWindowSizeDown" ? gvWindowSize : gvStartProgram;
            if (grid.SelectedRows.Count > 0)
            {
                var index = grid.SelectedRows[0].Index;
                var newIndex = index < grid.Rows.Count - 1 ? index + 1 : grid.Rows.Count - 1;
                var selectedRow = grid.SelectedRows[0];
                grid.Rows.RemoveAt(index);
                grid.Rows.Insert(newIndex, selectedRow);
                grid.Rows[newIndex].Selected = true;
                grid.CurrentCell = grid.Rows[newIndex].Cells[0];
            }
        }

        private void ButtonWindowCloserClick(object sender, EventArgs e)
        {
            var dialog = new SettingsCloserForm(_settings.Language, _closerSettings.Key1, _closerSettings.Key2, _closerSettings.MouseButton, _closerSettings.Type);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _closerSettings.Key1 = dialog.Key1;
                _closerSettings.Key2 = dialog.Key2;
                _closerSettings.MouseButton = dialog.MouseButton;
                _closerSettings.Type = dialog.CloserType;
            }
        }

        private void ButtonMenuItemUpClick(object sender, EventArgs e)
        {
            if (gvHotkeys.SelectedRows.Count > 0)
            {
                var items = (IList<Settings.MenuItem>)gvHotkeys.Tag;
                var item = (Settings.MenuItem)gvHotkeys.SelectedRows[0].Tag;
                var list = FindList(items, item);
                if (list != null && list.Count > 1)
                {
                    var index = list.IndexOf(item);
                    if (index > 0)
                    {
                        ((List<Settings.MenuItem>)list).Reverse(index - 1, 2);
                        gvHotkeys.Rows.Clear();
                        FillGridViewHotKeys(gvHotkeys, items, _settings.Language);
                        foreach (DataGridViewRow row in gvHotkeys.Rows)
                        {
                            if (row.Tag == item)
                            {
                                row.Selected = true;
                                gvHotkeys.CurrentCell = row.Cells[0];
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void ButtonMenuItemDownClick(object sender, EventArgs e)
        {
            if (gvHotkeys.SelectedRows.Count > 0)
            {
                var items = (IList<Settings.MenuItem>)gvHotkeys.Tag;
                var item = (Settings.MenuItem)gvHotkeys.SelectedRows[0].Tag;
                var list = FindList(items, item);
                if (list != null && list.Count > 1)
                {
                    var index = list.IndexOf(item);
                    if (index < list.Count - 1)
                    {
                        ((List<Settings.MenuItem>)list).Reverse(index, 2);
                        gvHotkeys.Rows.Clear();
                        FillGridViewHotKeys(gvHotkeys, items, _settings.Language);
                        foreach (DataGridViewRow row in gvHotkeys.Rows)
                        {
                            if (row.Tag == item)
                            {
                                row.Selected = true;
                                gvHotkeys.CurrentCell = row.Cells[0];
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            var settings = new SmartSystemMenuSettings();

            foreach (DataGridViewRow row in gvProcessExclusions.Rows)
            {
                settings.ProcessExclusions.Add(row.Cells[0].Value.ToString());
            }

            foreach (DataGridViewRow row in gvWindowSize.Rows)
            {
                if (row.Tag is WindowSizeMenuItem item)
                {
                    settings.MenuItems.WindowSizeItems.Add(new WindowSizeMenuItem 
                    { 
                        Title = item.Title,
                        Left = item.Left,
                        Top = item.Top,
                        Width = item.Width, 
                        Height = item.Height,
                        Key1 = item.Key1,
                        Key2 = item.Key2,
                        Key3 = item.Key3
                    });
                }
            }

            foreach (DataGridViewRow row in gvStartProgram.Rows)
            {
                settings.MenuItems.StartProgramItems.Add((StartProgramMenuItem)row.Tag);
            }

            settings.MenuItems.Items = (IList<Settings.MenuItem>)gvHotkeys.Tag;
            settings.Closer.Key1 = _closerSettings.Key1;
            settings.Closer.Key2 = _closerSettings.Key2;
            settings.Closer.MouseButton = _closerSettings.MouseButton;
            settings.Closer.Type = _closerSettings.Type;
            settings.SaveSelectedItems.AeroGlass = chkAeroGlass.Checked;
            settings.SaveSelectedItems.AlwaysOnTop = chkAlwaysOnTop.Checked;
            settings.SaveSelectedItems.HideForAltTab = chkHideForAltTab.Checked;
            settings.SaveSelectedItems.Alignment = chkAlignment.Checked;
            settings.SaveSelectedItems.Transparency = chkTransparency.Checked;
            settings.SaveSelectedItems.Priority = chkPriority.Checked;
            settings.SaveSelectedItems.MinimizeToTrayAlways = chkMinimizeToTrayAlways.Checked;
            settings.SaveSelectedItems.Buttons = chkButtons.Checked;
            settings.Sizer = (WindowSizerType)cmbSizer.SelectedIndex;
            settings.EnableHighDPI = chkEnableHighDPI.Checked;
            settings.LanguageName = cmbLanguage.SelectedValue == null ? "" : cmbLanguage.SelectedValue.ToString();

            if (!settings.Equals(_settings))
            {
                MessageBox.Show(_settings.Language.GetValue("message_box_attention_content"), _settings.Language.GetValue("message_box_attention_title"), MessageBoxButtons.OK);

                try
                {
                    settings.Language = _settings.Language;

                    var settingsFileName = Path.Combine(AssemblyUtils.AssemblyDirectory, "SmartSystemMenu.xml");
                    SmartSystemMenuSettings.Save(settingsFileName, settings);
                    if (OkClick != null)
                    {
                        OkClick.Invoke(this, new EventArgs<SmartSystemMenuSettings>(settings));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to save the settings." + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            Close();
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        private void KeyDownClick(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ButtonApplyClick(sender, e);
            }

            if (e.KeyValue == 27)
            {
                Close();
            }
        }

        private void ShowHotkeysForm(DataGridViewRow row)
        {
            var menuItem = (Settings.MenuItem)row.Tag;
            var form = new HotkeysForm(_settings.Language, menuItem);
            var result = form.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                menuItem.Key1 = form.MenuItem.Key1;
                menuItem.Key2 = form.MenuItem.Key2;
                menuItem.Key3 = form.MenuItem.Key3;
                row.Cells[1].Value = menuItem.ToString();
                row.Tag = menuItem;
            }
        }

        private void FillGridViewHotKeys(DataGridView gridView, IList<Settings.MenuItem> items, LanguageSettings languageSettings)
        {
            gridView.Tag = items;
            foreach (var item in items)
            {
                if (item.Type == MenuItemType.Item)
                {
                    var index = gridView.Rows.Add();
                    var row = gridView.Rows[index];
                    var id = MenuItemId.GetId(item.Name);
                    var title = GetTransparencyTitle(id, languageSettings);
                    title = title != null ? title : languageSettings.GetValue(item.Name);
                    row.Tag = item;
                    row.Cells[0].Value = title;
                    row.Cells[1].Value = item == null ? "" : item.ToString();
                    ((DataGridViewCheckBoxCell)row.Cells[2]).Value = item.Show;
                    ((DataGridViewCheckBoxCell)row.Cells[2]).ToolTipText = languageSettings.GetValue("clm_hotkeys_show_tooltip");
                }

                if (item.Type == MenuItemType.Separator)
                {
                    var index = gridView.Rows.Add();
                    var row = gridView.Rows[index];
                    var title = languageSettings.GetValue("separator");
                    row.Tag = item;
                    row.Cells[0].Value = title;
                    row.Cells[1].Value = item == null ? "" : item.ToString();
                    ((DataGridViewCheckBoxCell)row.Cells[2]).Value = item.Show;
                    ((DataGridViewCheckBoxCell)row.Cells[2]).ToolTipText = languageSettings.GetValue("clm_hotkeys_show_tooltip");
                }

                if (item.Type == MenuItemType.Group)
                {
                    var index = gridView.Rows.Add();
                    var row = gridView.Rows[index];
                    row.Tag = item;
                    row.Cells[0].Value = languageSettings.GetValue(item.Name);
                    row.ReadOnly = true;
                    ((DataGridViewCheckBoxCell)row.Cells[2]).Value = item.Show;
                    ((DataGridViewCheckBoxCell)row.Cells[2]).ToolTipText = languageSettings.GetValue("clm_hotkeys_show_tooltip");
                    ((DataGridViewDisableButtonCell)row.Cells[3]).Enabled = false;

                    foreach (var subItem in item.Items)
                    {
                        if (subItem.Type == MenuItemType.Item)
                        {
                            var subItemIndex = gridView.Rows.Add();
                            var subItemRow = gridView.Rows[subItemIndex];
                            var id = MenuItemId.GetId(subItem.Name);
                            var title = GetTransparencyTitle(id, languageSettings);
                            title = title != null ? title : languageSettings.GetValue(subItem.Name);
                            subItemRow.Tag = subItem;
                            subItemRow.Cells[0].Value = title;
                            subItemRow.Cells[1].Value = subItem == null ? "" : subItem.ToString();
                            ((DataGridViewCheckBoxCell)subItemRow.Cells[2]).Value = subItem.Show;
                            ((DataGridViewCheckBoxCell)subItemRow.Cells[2]).ToolTipText = languageSettings.GetValue("clm_hotkeys_show_tooltip");
                            var padding = subItemRow.Cells[0].Style.Padding;
                            subItemRow.Cells[0].Style.Padding = new Padding(20, padding.Top, padding.Right, padding.Bottom);
                        }

                        if (subItem.Type == MenuItemType.Separator)
                        {
                            var subItemIndex = gridView.Rows.Add();
                            var subItemRow = gridView.Rows[subItemIndex];
                            var title = languageSettings.GetValue("separator");
                            subItemRow.Tag = subItem;
                            subItemRow.Cells[0].Value = title;
                            subItemRow.Cells[1].Value = subItem == null ? "" : subItem.ToString();
                            ((DataGridViewCheckBoxCell)subItemRow.Cells[2]).Value = subItem.Show;
                            ((DataGridViewCheckBoxCell)subItemRow.Cells[2]).ToolTipText = languageSettings.GetValue("clm_hotkeys_show_tooltip");
                            var padding = subItemRow.Cells[0].Style.Padding;
                            subItemRow.Cells[0].Style.Padding = new Padding(20, padding.Top, padding.Right, padding.Bottom);
                        }
                    }
                }
            }
        }

        private string GetTransparencyTitle(int id, LanguageSettings languageSettings)
        {
            switch (id)
            {
                case MenuItemId.SC_TRANS_00: return "0%" + languageSettings.GetValue("trans_opaque");
                case MenuItemId.SC_TRANS_10: return "10%";
                case MenuItemId.SC_TRANS_20: return "20%";
                case MenuItemId.SC_TRANS_30: return "30%";
                case MenuItemId.SC_TRANS_40: return "40%";
                case MenuItemId.SC_TRANS_50: return "50%";
                case MenuItemId.SC_TRANS_60: return "60%";
                case MenuItemId.SC_TRANS_70: return "70%";
                case MenuItemId.SC_TRANS_80: return "80%";
                case MenuItemId.SC_TRANS_90: return "90%";
                case MenuItemId.SC_TRANS_100: return "100%" + languageSettings.GetValue("trans_invisible");
                default: return null;
            }
        }

        private IList<Settings.MenuItem> FindList(IList<Settings.MenuItem> list, Settings.MenuItem element)
        {
            foreach (var item in list)
            {
                if (item == element)
                {
                    return list;
                }

                if (item.Items.Any(x => x == element))
                {
                    return item.Items;
                }
            }
            return null;
        }
    }
}
