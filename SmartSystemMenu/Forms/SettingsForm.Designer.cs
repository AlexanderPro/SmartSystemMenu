using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabpGeneral = new System.Windows.Forms.TabPage();
            this.grpbProcessExclusions = new System.Windows.Forms.GroupBox();
            this.btnProcessExclusionDown = new System.Windows.Forms.Button();
            this.btnProcessExclusionUp = new System.Windows.Forms.Button();
            this.btnAddProcessExclusion = new System.Windows.Forms.Button();
            this.gvProcessExclusions = new System.Windows.Forms.DataGridView();
            this.clmProcessExclusionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmProcessExclusionEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.clmProcessExcusionDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabpMenu = new System.Windows.Forms.TabPage();
            this.grpbStartProgram = new System.Windows.Forms.GroupBox();
            this.btnAddStartProgram = new System.Windows.Forms.Button();
            this.gvStartProgram = new System.Windows.Forms.DataGridView();
            this.clmStartProgramTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmStartProgramPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmStartProgramArguments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmStartProgramEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.clmStartProgramDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTipAddProcessName = new System.Windows.Forms.ToolTip(this.components);
            this.btnStartProgramDown = new System.Windows.Forms.Button();
            this.btnStartProgramUp = new System.Windows.Forms.Button();
            this.tabMain.SuspendLayout();
            this.tabpGeneral.SuspendLayout();
            this.grpbProcessExclusions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProcessExclusions)).BeginInit();
            this.tabpMenu.SuspendLayout();
            this.grpbStartProgram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvStartProgram)).BeginInit();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabpGeneral);
            this.tabMain.Controls.Add(this.tabpMenu);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(521, 416);
            this.tabMain.TabIndex = 0;
            // 
            // tabpGeneral
            // 
            this.tabpGeneral.Controls.Add(this.grpbProcessExclusions);
            this.tabpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabpGeneral.Name = "tabpGeneral";
            this.tabpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabpGeneral.Size = new System.Drawing.Size(513, 390);
            this.tabpGeneral.TabIndex = 0;
            this.tabpGeneral.Text = _menuLanguage.GetStringValue("tab_settings_general");
            this.tabpGeneral.UseVisualStyleBackColor = true;
            // 
            // grpbProcessExclusions
            // 
            this.grpbProcessExclusions.Controls.Add(this.btnProcessExclusionDown);
            this.grpbProcessExclusions.Controls.Add(this.btnProcessExclusionUp);
            this.grpbProcessExclusions.Controls.Add(this.btnAddProcessExclusion);
            this.grpbProcessExclusions.Controls.Add(this.gvProcessExclusions);
            this.grpbProcessExclusions.Location = new System.Drawing.Point(8, 16);
            this.grpbProcessExclusions.Name = "grpbProcessExclusions";
            this.grpbProcessExclusions.Size = new System.Drawing.Size(497, 368);
            this.grpbProcessExclusions.TabIndex = 0;
            this.grpbProcessExclusions.TabStop = false;
            this.grpbProcessExclusions.Text = _menuLanguage.GetStringValue("grpb_process_exclusions");
            // 
            // btnProcessExclusionDown
            // 
            this.btnProcessExclusionDown.Image = global::SmartSystemMenu.Properties.Resources.ArrowDown;
            this.btnProcessExclusionDown.Location = new System.Drawing.Point(406, 339);
            this.btnProcessExclusionDown.Name = "btnProcessExclusionDown";
            this.btnProcessExclusionDown.Size = new System.Drawing.Size(31, 23);
            this.btnProcessExclusionDown.TabIndex = 2;
            this.toolTipAddProcessName.SetToolTip(this.btnProcessExclusionDown, _menuLanguage.GetStringValue("btn_process_exclusion_down"));
            this.btnProcessExclusionDown.UseVisualStyleBackColor = true;
            this.btnProcessExclusionDown.Click += new System.EventHandler(this.ButtonArrowDownClick);
            // 
            // btnProcessExclusionUp
            // 
            this.btnProcessExclusionUp.Image = global::SmartSystemMenu.Properties.Resources.ArrowUp;
            this.btnProcessExclusionUp.Location = new System.Drawing.Point(369, 339);
            this.btnProcessExclusionUp.Name = "btnProcessExclusionUp";
            this.btnProcessExclusionUp.Size = new System.Drawing.Size(31, 23);
            this.btnProcessExclusionUp.TabIndex = 1;
            this.toolTipAddProcessName.SetToolTip(this.btnProcessExclusionUp, _menuLanguage.GetStringValue("btn_process_exclusion_up"));
            this.btnProcessExclusionUp.UseVisualStyleBackColor = true;
            this.btnProcessExclusionUp.Click += new System.EventHandler(this.ButtonArrowUpClick);
            // 
            // btnAddProcessExclusion
            // 
            this.btnAddProcessExclusion.Location = new System.Drawing.Point(460, 339);
            this.btnAddProcessExclusion.Name = "btnAddProcessExclusion";
            this.btnAddProcessExclusion.Size = new System.Drawing.Size(31, 23);
            this.btnAddProcessExclusion.TabIndex = 3;
            this.btnAddProcessExclusion.Text = "+";
            this.toolTipAddProcessName.SetToolTip(this.btnAddProcessExclusion, _menuLanguage.GetStringValue("btn_add_process_exclusion"));
            this.btnAddProcessExclusion.UseVisualStyleBackColor = true;
            this.btnAddProcessExclusion.Click += new System.EventHandler(this.ButtonAddProcessExclusionClick);
            // 
            // gvProcessExclusions
            // 
            this.gvProcessExclusions.AllowUserToAddRows = false;
            this.gvProcessExclusions.AllowUserToDeleteRows = false;
            this.gvProcessExclusions.AllowUserToResizeColumns = false;
            this.gvProcessExclusions.AllowUserToResizeRows = false;
            this.gvProcessExclusions.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.gvProcessExclusions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvProcessExclusions.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gvProcessExclusions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProcessExclusions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmProcessExclusionName,
            this.clmProcessExclusionEdit,
            this.clmProcessExcusionDelete});
            this.gvProcessExclusions.GridColor = System.Drawing.SystemColors.Control;
            this.gvProcessExclusions.Location = new System.Drawing.Point(6, 19);
            this.gvProcessExclusions.MultiSelect = false;
            this.gvProcessExclusions.Name = "gvProcessExclusions";
            this.gvProcessExclusions.RowHeadersVisible = false;
            this.gvProcessExclusions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gvProcessExclusions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvProcessExclusions.Size = new System.Drawing.Size(485, 314);
            this.gvProcessExclusions.TabIndex = 0;
            this.gvProcessExclusions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewProcessExclusionsCellContentClick);
            this.gvProcessExclusions.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewProcessExclusionsCellDoubleClick);
            // 
            // clmProcessExclusionName
            // 
            this.clmProcessExclusionName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmProcessExclusionName.HeaderText = _menuLanguage.GetStringValue("clm_process_exclusion_name");
            this.clmProcessExclusionName.Name = "clmProcessExclusionName";
            this.clmProcessExclusionName.ReadOnly = true;
            this.clmProcessExclusionName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmProcessExclusionName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmProcessExclusionEdit
            // 
            this.clmProcessExclusionEdit.HeaderText = "";
            this.clmProcessExclusionEdit.Name = "clmProcessExclusionEdit";
            this.clmProcessExclusionEdit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmProcessExclusionEdit.Text = "...";
            this.clmProcessExclusionEdit.ToolTipText = _menuLanguage.GetStringValue("clm_process_exclusion_edit");
            this.clmProcessExclusionEdit.UseColumnTextForButtonValue = true;
            this.clmProcessExclusionEdit.Width = 30;
            // 
            // clmProcessExcusionDelete
            // 
            this.clmProcessExcusionDelete.HeaderText = "";
            this.clmProcessExcusionDelete.Name = "clmProcessExcusionDelete";
            this.clmProcessExcusionDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmProcessExcusionDelete.Text = "-";
            this.clmProcessExcusionDelete.ToolTipText = _menuLanguage.GetStringValue("clm_process_exclusion_delete");
            this.clmProcessExcusionDelete.UseColumnTextForButtonValue = true;
            this.clmProcessExcusionDelete.Width = 30;
            // 
            // tabpMenu
            // 
            this.tabpMenu.Controls.Add(this.grpbStartProgram);
            this.tabpMenu.Location = new System.Drawing.Point(4, 22);
            this.tabpMenu.Name = "tabpMenu";
            this.tabpMenu.Padding = new System.Windows.Forms.Padding(3);
            this.tabpMenu.Size = new System.Drawing.Size(513, 390);
            this.tabpMenu.TabIndex = 1;
            this.tabpMenu.Text = _menuLanguage.GetStringValue("tab_settings_menu");
            this.tabpMenu.UseVisualStyleBackColor = true;
            // 
            // grpbStartProgram
            // 
            this.grpbStartProgram.Controls.Add(this.btnStartProgramDown);
            this.grpbStartProgram.Controls.Add(this.btnStartProgramUp);
            this.grpbStartProgram.Controls.Add(this.btnAddStartProgram);
            this.grpbStartProgram.Controls.Add(this.gvStartProgram);
            this.grpbStartProgram.Location = new System.Drawing.Point(8, 16);
            this.grpbStartProgram.Name = "grpbStartProgram";
            this.grpbStartProgram.Size = new System.Drawing.Size(497, 368);
            this.grpbStartProgram.TabIndex = 0;
            this.grpbStartProgram.TabStop = false;
            this.grpbStartProgram.Text = _menuLanguage.GetStringValue("grpb_start_program");
            // 
            // btnAddStartProgram
            // 
            this.btnAddStartProgram.Location = new System.Drawing.Point(460, 339);
            this.btnAddStartProgram.Name = "btnAddStartProgram";
            this.btnAddStartProgram.Size = new System.Drawing.Size(31, 23);
            this.btnAddStartProgram.TabIndex = 3;
            this.btnAddStartProgram.Text = "+";
            this.toolTipAddProcessName.SetToolTip(this.btnAddStartProgram, _menuLanguage.GetStringValue("btn_add_start_program"));
            this.btnAddStartProgram.UseVisualStyleBackColor = true;
            this.btnAddStartProgram.Click += new System.EventHandler(this.ButtonAddStartProgramClick);
            // 
            // gvStartProgram
            // 
            this.gvStartProgram.AllowUserToAddRows = false;
            this.gvStartProgram.AllowUserToDeleteRows = false;
            this.gvStartProgram.AllowUserToResizeColumns = false;
            this.gvStartProgram.AllowUserToResizeRows = false;
            this.gvStartProgram.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.gvStartProgram.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvStartProgram.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gvStartProgram.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvStartProgram.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmStartProgramTitle,
            this.clmStartProgramPath,
            this.clmStartProgramArguments,
            this.clmStartProgramEdit,
            this.clmStartProgramDelete});
            this.gvStartProgram.GridColor = System.Drawing.SystemColors.Control;
            this.gvStartProgram.Location = new System.Drawing.Point(6, 19);
            this.gvStartProgram.MultiSelect = false;
            this.gvStartProgram.Name = "gvStartProgram";
            this.gvStartProgram.RowHeadersVisible = false;
            this.gvStartProgram.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gvStartProgram.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvStartProgram.Size = new System.Drawing.Size(485, 314);
            this.gvStartProgram.TabIndex = 0;
            this.gvStartProgram.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewStartProgramCellContentClick);
            this.gvStartProgram.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewStartProgramCellDoubleClick);
            // 
            // clmStartProgramTitle
            // 
            this.clmStartProgramTitle.HeaderText = _menuLanguage.GetStringValue("clm_start_program_title");
            this.clmStartProgramTitle.Name = "clmStartProgramTitle";
            this.clmStartProgramTitle.ReadOnly = true;
            this.clmStartProgramTitle.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmStartProgramTitle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmStartProgramPath
            // 
            this.clmStartProgramPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmStartProgramPath.HeaderText = _menuLanguage.GetStringValue("clm_start_program_path");
            this.clmStartProgramPath.Name = "clmStartProgramPath";
            this.clmStartProgramPath.ReadOnly = true;
            this.clmStartProgramPath.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmStartProgramPath.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmStartProgramArguments
            // 
            this.clmStartProgramArguments.HeaderText = _menuLanguage.GetStringValue("clm_start_program_arguments");
            this.clmStartProgramArguments.Name = "clmStartProgramArguments";
            this.clmStartProgramArguments.ReadOnly = true;
            this.clmStartProgramArguments.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmStartProgramArguments.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmStartProgramArguments.Width = 72;
            // 
            // clmStartProgramEdit
            // 
            this.clmStartProgramEdit.HeaderText = "";
            this.clmStartProgramEdit.Name = "clmStartProgramEdit";
            this.clmStartProgramEdit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmStartProgramEdit.Text = "...";
            this.clmStartProgramEdit.ToolTipText = _menuLanguage.GetStringValue("clm_start_program_edit");
            this.clmStartProgramEdit.UseColumnTextForButtonValue = true;
            this.clmStartProgramEdit.Width = 30;
            // 
            // clmStartProgramDelete
            // 
            this.clmStartProgramDelete.HeaderText = "";
            this.clmStartProgramDelete.Name = "clmStartProgramDelete";
            this.clmStartProgramDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmStartProgramDelete.Text = "-";
            this.clmStartProgramDelete.ToolTipText = _menuLanguage.GetStringValue("clm_start_program_delete");
            this.clmStartProgramDelete.UseColumnTextForButtonValue = true;
            this.clmStartProgramDelete.Width = 30;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(340, 422);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(81, 35);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = _menuLanguage.GetStringValue("settings_btn_apply");
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.ButtonApplyClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(429, 422);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 35);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = _menuLanguage.GetStringValue("settings_btn_cancel");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // btnStartProgramDown
            // 
            this.btnStartProgramDown.Image = global::SmartSystemMenu.Properties.Resources.ArrowDown;
            this.btnStartProgramDown.Location = new System.Drawing.Point(406, 339);
            this.btnStartProgramDown.Name = "btnStartProgramDown";
            this.btnStartProgramDown.Size = new System.Drawing.Size(31, 23);
            this.btnStartProgramDown.TabIndex = 2;
            this.toolTipAddProcessName.SetToolTip(this.btnStartProgramDown, _menuLanguage.GetStringValue("btn_start_program_down"));
            this.btnStartProgramDown.UseVisualStyleBackColor = true;
            this.btnStartProgramDown.Click += new System.EventHandler(this.ButtonArrowDownClick);
            // 
            // btnStartProgramUp
            // 
            this.btnStartProgramUp.Image = global::SmartSystemMenu.Properties.Resources.ArrowUp;
            this.btnStartProgramUp.Location = new System.Drawing.Point(369, 339);
            this.btnStartProgramUp.Name = "btnStartProgramUp";
            this.btnStartProgramUp.Size = new System.Drawing.Size(31, 23);
            this.btnStartProgramUp.TabIndex = 1;
            this.toolTipAddProcessName.SetToolTip(this.btnStartProgramUp, _menuLanguage.GetStringValue("btn_start_program_up"));
            this.btnStartProgramUp.UseVisualStyleBackColor = true;
            this.btnStartProgramUp.Click += new System.EventHandler(this.ButtonArrowUpClick);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 469);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.tabMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = _menuLanguage.GetStringValue("settings_form");
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownClick);
            this.tabMain.ResumeLayout(false);
            this.tabpGeneral.ResumeLayout(false);
            this.grpbProcessExclusions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvProcessExclusions)).EndInit();
            this.tabpMenu.ResumeLayout(false);
            this.grpbStartProgram.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvStartProgram)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabpGeneral;
        private System.Windows.Forms.TabPage tabpMenu;
        private System.Windows.Forms.GroupBox grpbProcessExclusions;
        private System.Windows.Forms.DataGridView gvProcessExclusions;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAddProcessExclusion;
        private System.Windows.Forms.ToolTip toolTipAddProcessName;
        private System.Windows.Forms.GroupBox grpbStartProgram;
        private System.Windows.Forms.Button btnAddStartProgram;
        private System.Windows.Forms.DataGridView gvStartProgram;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmStartProgramTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmStartProgramPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmStartProgramArguments;
        private System.Windows.Forms.DataGridViewButtonColumn clmStartProgramEdit;
        private System.Windows.Forms.DataGridViewButtonColumn clmStartProgramDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmProcessExclusionName;
        private System.Windows.Forms.DataGridViewButtonColumn clmProcessExclusionEdit;
        private System.Windows.Forms.DataGridViewButtonColumn clmProcessExcusionDelete;
        private System.Windows.Forms.Button btnProcessExclusionUp;
        private System.Windows.Forms.Button btnProcessExclusionDown;
        private System.Windows.Forms.Button btnStartProgramDown;
        private System.Windows.Forms.Button btnStartProgramUp;
    }
}