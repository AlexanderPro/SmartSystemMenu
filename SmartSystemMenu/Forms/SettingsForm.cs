using System;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using SmartSystemMenu.Settings;

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
            foreach (var processExclusion in settings.ProcessExclusions)
            {
                var index = gvProcessExclusions.Rows.Add();
                var row = gvProcessExclusions.Rows[index];
                row.Cells[0].Value = processExclusion;
                row.Cells[1].ToolTipText = "Edit";
                row.Cells[2].ToolTipText = "Delete";
            }

            foreach (var item in settings.MenuItems.StartProgramItems)
            {
                var index = gvStartProgram.Rows.Add();
                var row = gvStartProgram.Rows[index];
                row.Cells[0].Value = item.Title;
                row.Cells[1].Value = item.FileName;
                row.Cells[2].Value = item.Arguments;
                row.Cells[3].ToolTipText = "Edit";
                row.Cells[4].ToolTipText = "Delete";
            }
        }

        private void GridViewProcessExclusionsCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;

            if (grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 1)
                {
                    var cell = grid.Rows[e.RowIndex].Cells[0];
                    var dialog = new ProcessExclusionForm(cell.Value.ToString());
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

                    var dialog = new StartProgramForm(cellTitle.Value.ToString(), cellFileName.Value.ToString(), cellArguments.Value.ToString());
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
                var dialog = new ProcessExclusionForm(cell.Value.ToString());
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    cell.Value = dialog.ProcessName;
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

                var dialog = new StartProgramForm(cellTitle.Value.ToString(), cellFileName.Value.ToString(), cellArguments.Value.ToString());
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
            var dialog = new ProcessExclusionForm("");
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var index = gvProcessExclusions.Rows.Add();
                var row = gvProcessExclusions.Rows[index];
                row.Cells[0].Value = dialog.ProcessName;
                row.Cells[1].ToolTipText = "Edit";
                row.Cells[2].ToolTipText = "Delete";
            }
        }

        private void ButtonAddStartProgramClick(object sender, EventArgs e)
        {
            var dialog = new StartProgramForm("", "", "");
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var index = gvStartProgram.Rows.Add();
                var row = gvStartProgram.Rows[index];
                row.Cells[0].Value = dialog.Title;
                row.Cells[1].Value = dialog.FileName;
                row.Cells[2].Value = dialog.Arguments;
                row.Cells[3].ToolTipText = "Edit";
                row.Cells[4].ToolTipText = "Delete";
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
                settings.MenuItems.StartProgramItems.Add(new StartProgramItem { Title = row.Cells[0].Value.ToString(), FileName = row.Cells[1].Value.ToString(), Arguments = row.Cells[2].Value.ToString() });
            }

            if (!settings.Equals(_settings))
            {
                MessageBox.Show("You should restart the application manually to apply the settings.", "Attention", MessageBoxButtons.OK);

                try
                {
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