using System;
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

        public event EventHandler<SmartSystemMenuSettingsEventArgs> OkClick;

        public SettingsForm(SmartSystemMenuSettings settings)
        {
            InitializeComponent();

            try
            {
                _settings = settings;
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
            lblLanguage.Text = settings.LanguageSettings.GetValue("lbl_language");
            tabpGeneral.Text = settings.LanguageSettings.GetValue("tab_settings_general");
            tabpMenu.Text = settings.LanguageSettings.GetValue("tab_settings_menu");
            tabpHotkeys.Text = settings.LanguageSettings.GetValue("tab_settings_hotkeys");
            grpbProcessExclusions.Text = settings.LanguageSettings.GetValue("grpb_process_exclusions");
            grpbStartProgram.Text = settings.LanguageSettings.GetValue("grpb_start_program");
            clmProcessExclusionName.HeaderText = settings.LanguageSettings.GetValue("clm_process_exclusion_name");
            clmProcessExclusionEdit.ToolTipText = settings.LanguageSettings.GetValue("clm_process_exclusion_edit");
            clmProcessExcusionDelete.ToolTipText = settings.LanguageSettings.GetValue("clm_process_exclusion_delete");
            clmStartProgramTitle.HeaderText = settings.LanguageSettings.GetValue("clm_start_program_title");
            clmStartProgramPath.HeaderText = settings.LanguageSettings.GetValue("clm_start_program_path");
            clmStartProgramArguments.HeaderText = settings.LanguageSettings.GetValue("clm_start_program_arguments");
            clmStartProgramEdit.ToolTipText = settings.LanguageSettings.GetValue("clm_start_program_edit");
            clmStartProgramDelete.ToolTipText = settings.LanguageSettings.GetValue("clm_start_program_delete");
            clmnMenuItemName.HeaderText = settings.LanguageSettings.GetValue("clm_hotkeys_name");
            clmnHotkeys.HeaderText = settings.LanguageSettings.GetValue("clm_hotkeys_keys");
            toolTipAddProcessName.SetToolTip(btnProcessExclusionDown, settings.LanguageSettings.GetValue("btn_process_exclusion_down"));
            toolTipAddProcessName.SetToolTip(btnProcessExclusionUp, settings.LanguageSettings.GetValue("btn_process_exclusion_up"));
            toolTipAddProcessName.SetToolTip(btnAddProcessExclusion, settings.LanguageSettings.GetValue("btn_add_process_exclusion"));
            toolTipAddProcessName.SetToolTip(btnAddStartProgram, settings.LanguageSettings.GetValue("btn_add_start_program"));
            toolTipAddProcessName.SetToolTip(btnStartProgramDown, settings.LanguageSettings.GetValue("btn_start_program_down"));
            toolTipAddProcessName.SetToolTip(btnStartProgramUp, settings.LanguageSettings.GetValue("btn_start_program_up"));
            btnApply.Text = settings.LanguageSettings.GetValue("settings_btn_apply");
            btnCancel.Text = settings.LanguageSettings.GetValue("settings_btn_cancel");
            Text = settings.LanguageSettings.GetValue("settings_form");

            foreach (var processExclusion in settings.ProcessExclusions)
            {
                var index = gvProcessExclusions.Rows.Add();
                var row = gvProcessExclusions.Rows[index];
                row.Cells[0].Value = processExclusion;
                row.Cells[1].ToolTipText = settings.LanguageSettings.GetValue("clm_process_exclusion_edit");
                row.Cells[2].ToolTipText = settings.LanguageSettings.GetValue("clm_process_exclusion_delete");
            }

            foreach (var item in settings.MenuItems.StartProgramItems)
            {
                var index = gvStartProgram.Rows.Add();
                var row = gvStartProgram.Rows[index];
                row.Cells[0].Value = item.Title;
                row.Cells[1].Value = item.FileName;
                row.Cells[2].Value = item.Arguments;
                row.Cells[3].ToolTipText = settings.LanguageSettings.GetValue("clm_start_program_edit");
                row.Cells[4].ToolTipText = settings.LanguageSettings.GetValue("clm_start_program_delete");
            }

            cmbLanguage.DisplayMember = "Text";
            cmbLanguage.ValueMember = "Value";

            var languageItems = new[] {
                new { Text = "", Value = "" },
                new { Text = "English", Value = "en" },
                new { Text = "中文", Value = "cn" },
                new { Text = "日本語", Value = "ja" },
                new { Text = "한국어", Value = "ko" },
                new { Text = "Русский", Value = "ru" }
            };

            cmbLanguage.DataSource = languageItems;
            cmbLanguage.SelectedValue = settings.LanguageName;

            FillGridViewRowHotkey(gvHotkeys, settings, "information");
            FillGridViewRowHotkey(gvHotkeys, settings, "roll_up");
            FillGridViewRowHotkey(gvHotkeys, settings, "aero_glass");
            FillGridViewRowHotkey(gvHotkeys, settings, "always_on_top");
            FillGridViewRowHotkey(gvHotkeys, settings, "send_to_bottom");
            FillGridViewRowHotkey(gvHotkeys, settings, "save_screenshot");
            FillGridViewRowHotkey(gvHotkeys, settings, "open_file_in_explorer");
            FillGridViewRowHotkey(gvHotkeys, settings, "copy_text_to_clipboard");
            FillGridViewRowHotkey(gvHotkeys, settings, "drag_by_mouse");
            FillGridViewGroupHotkey(gvHotkeys, settings, "size");
            FillGridViewRowHotkey(gvHotkeys, settings, "640_480", "640x480", true);
            FillGridViewRowHotkey(gvHotkeys, settings, "720_480", "720x480", true);
            FillGridViewRowHotkey(gvHotkeys, settings, "720_576", "720x576", true);
            FillGridViewRowHotkey(gvHotkeys, settings, "800_600", "800x600", true);
            FillGridViewRowHotkey(gvHotkeys, settings, "1024_768", "1024x768", true);
            FillGridViewRowHotkey(gvHotkeys, settings, "1152_864", "1024x768", true);
            FillGridViewRowHotkey(gvHotkeys, settings, "1280_800", "1280x800", true);
            FillGridViewRowHotkey(gvHotkeys, settings, "1280_960", "1280x960", true);
            FillGridViewRowHotkey(gvHotkeys, settings, "1280_1024", "1280x1024", true);
            FillGridViewRowHotkey(gvHotkeys, settings, "1440_900", "1440x900", true);
            FillGridViewRowHotkey(gvHotkeys, settings, "1600_900", "1600x900", true);
            FillGridViewRowHotkey(gvHotkeys, settings, "1680_1050", "1680x1050", true);
            FillGridViewRowHotkey(gvHotkeys, settings, "size_default", null, true);
            FillGridViewRowHotkey(gvHotkeys, settings, "size_custom", null, true);
            FillGridViewGroupHotkey(gvHotkeys, settings, "move_to");
            FillGridViewGroupHotkey(gvHotkeys, settings, "alignment");
            FillGridViewRowHotkey(gvHotkeys, settings, "align_top_left", null, true);
            FillGridViewRowHotkey(gvHotkeys, settings, "align_top_center", null, true);
            FillGridViewRowHotkey(gvHotkeys, settings, "align_top_right", null, true);
            FillGridViewRowHotkey(gvHotkeys, settings, "align_middle_left", null, true);
            FillGridViewRowHotkey(gvHotkeys, settings, "align_middle_center", null, true);
            FillGridViewRowHotkey(gvHotkeys, settings, "align_middle_right", null, true);
            FillGridViewRowHotkey(gvHotkeys, settings, "align_bottom_left", null, true);
            FillGridViewRowHotkey(gvHotkeys, settings, "align_bottom_center", null, true);
            FillGridViewRowHotkey(gvHotkeys, settings, "align_bottom_right", null, true);
            FillGridViewRowHotkey(gvHotkeys, settings, "align_default", null, true);
            FillGridViewRowHotkey(gvHotkeys, settings, "align_custom", null, true);
            FillGridViewGroupHotkey(gvHotkeys, settings, "transparency");
            FillGridViewRowHotkey(gvHotkeys, settings, "trans_opaque", "0%" + settings.LanguageSettings.GetValue("trans_opaque"), true);
            FillGridViewRowHotkey(gvHotkeys, settings, "10%", "10%", true);
            FillGridViewRowHotkey(gvHotkeys, settings, "20%", "20%", true);
            FillGridViewRowHotkey(gvHotkeys, settings, "30%", "30%", true);
            FillGridViewRowHotkey(gvHotkeys, settings, "40%", "40%", true);
            FillGridViewRowHotkey(gvHotkeys, settings, "50%", "50%", true);
            FillGridViewRowHotkey(gvHotkeys, settings, "60%", "60%", true);
            FillGridViewRowHotkey(gvHotkeys, settings, "70%", "70%", true);
            FillGridViewRowHotkey(gvHotkeys, settings, "80%", "80%", true);
            FillGridViewRowHotkey(gvHotkeys, settings, "90%", "90%", true);
            FillGridViewRowHotkey(gvHotkeys, settings, "trans_invisible", "100%" + settings.LanguageSettings.GetValue("trans_invisible"), true);
            FillGridViewRowHotkey(gvHotkeys, settings, "trans_default", null, true);
            FillGridViewRowHotkey(gvHotkeys, settings, "trans_custom", null, true);
            FillGridViewGroupHotkey(gvHotkeys, settings, "priority");
            FillGridViewRowHotkey(gvHotkeys, settings, "priority_real_time", null, true);
            FillGridViewRowHotkey(gvHotkeys, settings, "priority_high", null, true);
            FillGridViewRowHotkey(gvHotkeys, settings, "priority_above_normal", null, true);
            FillGridViewRowHotkey(gvHotkeys, settings, "priority_normal", null, true);
            FillGridViewRowHotkey(gvHotkeys, settings, "priority_below_normal", null, true);
            FillGridViewRowHotkey(gvHotkeys, settings, "priority_idle", null, true);
            FillGridViewGroupHotkey(gvHotkeys, settings, "system_tray");
            FillGridViewRowHotkey(gvHotkeys, settings, "minimize_to_systemtray", null, true);
            FillGridViewRowHotkey(gvHotkeys, settings, "minimize_always_to_systemtray", null, true);
            FillGridViewGroupHotkey(gvHotkeys, settings, "other_windows");
            FillGridViewRowHotkey(gvHotkeys, settings, "minimize_other_windows", null, true);
            FillGridViewRowHotkey(gvHotkeys, settings, "close_other_windows", null, true);
        }

        private void GridViewProcessExclusionsCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;

            if (grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 1)
                {
                    var cell = grid.Rows[e.RowIndex].Cells[0];
                    var dialog = new ProcessExclusionForm(cell.Value.ToString(), _settings);
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
                if (e.ColumnIndex == 3)
                {
                    var cellTitle = grid.Rows[e.RowIndex].Cells[0];
                    var cellFileName = grid.Rows[e.RowIndex].Cells[1];
                    var cellArguments = grid.Rows[e.RowIndex].Cells[2];

                    var dialog = new StartProgramForm(cellTitle.Value.ToString(), cellFileName.Value.ToString(), cellArguments.Value.ToString(), _settings);
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        cellTitle.Value = dialog.Title;
                        cellFileName.Value = dialog.FileName;
                        cellArguments.Value = dialog.Arguments;
                    }
                }

                if (e.ColumnIndex == 4)
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
                var dialog = new ProcessExclusionForm(cell.Value.ToString(), _settings);
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
                if (row.Tag != null)
                {
                    ShowHotkeysForm(row);
                }
            }
        }

        private void GridViewHotkeysCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;
            if ((e.ColumnIndex == 0 || e.ColumnIndex == 1) && e.RowIndex >= 0)
            {
                var row = grid.Rows[e.RowIndex];
                if (row.Tag != null)
                {
                    ShowHotkeysForm(row);
                }
            }
        }

        private void GridViewStartProgramCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;
            if ((e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2) && e.RowIndex >= 0)
            {
                var cellTitle = grid.Rows[e.RowIndex].Cells[0];
                var cellFileName = grid.Rows[e.RowIndex].Cells[1];
                var cellArguments = grid.Rows[e.RowIndex].Cells[2];

                var dialog = new StartProgramForm(cellTitle.Value.ToString(), cellFileName.Value.ToString(), cellArguments.Value.ToString(), _settings);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    cellTitle.Value = dialog.Title;
                    cellFileName.Value = dialog.FileName;
                    cellArguments.Value = dialog.Arguments;
                }
            }
        }

        private void ButtonAddProcessExclusionClick(object sender, EventArgs e)
        {
            var dialog = new ProcessExclusionForm("", _settings);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var index = gvProcessExclusions.Rows.Add();
                var row = gvProcessExclusions.Rows[index];
                row.Cells[0].Value = dialog.ProcessName;
                row.Cells[1].ToolTipText = _settings.LanguageSettings.GetValue("clm_process_exclusion_edit");
                row.Cells[2].ToolTipText = _settings.LanguageSettings.GetValue("clm_process_exclusion_delete");
            }
        }

        private void ButtonAddStartProgramClick(object sender, EventArgs e)
        {
            var dialog = new StartProgramForm("", "", "", _settings);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var index = gvStartProgram.Rows.Add();
                var row = gvStartProgram.Rows[index];
                row.Cells[0].Value = dialog.Title;
                row.Cells[1].Value = dialog.FileName;
                row.Cells[2].Value = dialog.Arguments;
                row.Cells[3].ToolTipText = _settings.LanguageSettings.GetValue("clm_start_program_edit");
                row.Cells[4].ToolTipText = _settings.LanguageSettings.GetValue("clm_start_program_delete");
            }
        }

        private void ButtonArrowUpClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var grid = button.Name == "btnProcessExclusionUp" ? gvProcessExclusions : gvStartProgram;
            if (grid.SelectedRows.Count > 0)
            {
                var index = grid.SelectedRows[0].Index;
                var newIndex = index > 0 ? index - 1 : 0;
                var selectedRow = grid.SelectedRows[0];
                grid.Rows.RemoveAt(index);
                grid.Rows.Insert(newIndex, selectedRow);
                grid.Rows[newIndex].Selected = true;
            }
        }

        private void ButtonArrowDownClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var grid = button.Name == "btnProcessExclusionDown" ? gvProcessExclusions : gvStartProgram;
            if (grid.SelectedRows.Count > 0)
            {
                var index = grid.SelectedRows[0].Index;
                var newIndex = index < grid.Rows.Count - 1 ? index + 1 : grid.Rows.Count - 1;
                var selectedRow = grid.SelectedRows[0];
                grid.Rows.RemoveAt(index);
                grid.Rows.Insert(newIndex, selectedRow);
                grid.Rows[newIndex].Selected = true;
            }
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            var settings = new SmartSystemMenuSettings();

            foreach (DataGridViewRow row in gvProcessExclusions.Rows)
            {
                settings.ProcessExclusions.Add(row.Cells[0].Value.ToString());
            }

            foreach (DataGridViewRow row in gvStartProgram.Rows)
            {
                settings.MenuItems.StartProgramItems.Add(new StartProgramMenuItem { Title = row.Cells[0].Value.ToString(), FileName = row.Cells[1].Value.ToString(), Arguments = row.Cells[2].Value.ToString() });
            }

            foreach (DataGridViewRow row in gvHotkeys.Rows.OfType<DataGridViewRow>().Where(x => x.Tag is Settings.MenuItem))
            {
                var menuItem = (Settings.MenuItem)row.Tag;
                settings.MenuItems.Items.Add(menuItem);
            }

            settings.LanguageName = cmbLanguage.SelectedValue == null ? "" : cmbLanguage.SelectedValue.ToString();

            if (!settings.Equals(_settings))
            {
                MessageBox.Show(_settings.LanguageSettings.GetValue("message_box_attention_content"), _settings.LanguageSettings.GetValue("message_box_attention_title"), MessageBoxButtons.OK);

                try
                {
                    settings.LanguageSettings = _settings.LanguageSettings;

                    var settingsFileName = Path.Combine(AssemblyUtils.AssemblyDirectory, "SmartSystemMenu.xml");
                    SmartSystemMenuSettings.Save(settingsFileName, settings);
                    if (OkClick != null)
                    {
                        OkClick.Invoke(this, new SmartSystemMenuSettingsEventArgs(settings));
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
            var form = new HotkeysForm(_settings, menuItem);
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

        private void FillGridViewRowHotkey(DataGridView gridView, SmartSystemMenuSettings settings, string itemName, string title = null, bool isPadding = false)
        {
            var index = gridView.Rows.Add();
            var row = gridView.Rows[index];
            var menuItem = settings.MenuItems.Items.FirstOrDefault(x => x.Name == itemName);
            title = title != null ? title : settings.LanguageSettings.GetValue(itemName);
            row.Tag = (Settings.MenuItem)menuItem.Clone();
            row.Cells[0].Value = title;
            row.Cells[1].Value = menuItem == null ? "" : menuItem.ToString();
            if (isPadding)
            {
                var padding = row.Cells[0].Style.Padding;
                row.Cells[0].Style.Padding = new Padding(20, padding.Top, padding.Right, padding.Bottom);
            }
        }

        private void FillGridViewGroupHotkey(DataGridView gridView, SmartSystemMenuSettings settings, string itemName)
        {
            var index = gridView.Rows.Add();
            var row = gridView.Rows[index];
            row.Cells[0].Value = settings.LanguageSettings.GetValue(itemName);
            row.ReadOnly = true;
            ((DataGridViewDisableButtonCell)row.Cells[2]).Enabled = false;
        }
    }

    public class SmartSystemMenuSettingsEventArgs : EventArgs
    {
        public SmartSystemMenuSettings Settings { get; private set; }

        public SmartSystemMenuSettingsEventArgs(SmartSystemMenuSettings settings)
        {
            Settings = settings;
        }
    }
}