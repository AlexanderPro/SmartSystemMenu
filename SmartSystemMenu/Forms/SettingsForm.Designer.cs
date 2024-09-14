using SmartSystemMenu.Controls;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabpGeneral = new System.Windows.Forms.TabPage();
            this.grpbDisplay = new System.Windows.Forms.GroupBox();
            this.chkEnableHighDPI = new System.Windows.Forms.CheckBox();
            this.grpbCloser = new System.Windows.Forms.GroupBox();
            this.btnCloser = new System.Windows.Forms.Button();
            this.grpbLanguage = new System.Windows.Forms.GroupBox();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.grpbProcessExclusions = new System.Windows.Forms.GroupBox();
            this.btnProcessExclusionDown = new System.Windows.Forms.Button();
            this.btnProcessExclusionUp = new System.Windows.Forms.Button();
            this.btnAddProcessExclusion = new System.Windows.Forms.Button();
            this.gvProcessExclusions = new System.Windows.Forms.DataGridView();
            this.clmProcessExclusionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmProcessExclusionEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.clmProcessExcusionDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabpMenu = new System.Windows.Forms.TabPage();
            this.grpbHotkeys = new System.Windows.Forms.GroupBox();
            this.btnMenuItemDown = new System.Windows.Forms.Button();
            this.btnMenuItemUp = new System.Windows.Forms.Button();
            this.gvHotkeys = new System.Windows.Forms.DataGridView();
            this.tabpMenuSize = new System.Windows.Forms.TabPage();
            this.grpbSizer = new System.Windows.Forms.GroupBox();
            this.cmbSizer = new System.Windows.Forms.ComboBox();
            this.grpbWindowSize = new System.Windows.Forms.GroupBox();
            this.btnWindowSizeDown = new System.Windows.Forms.Button();
            this.btnWindowSizeUp = new System.Windows.Forms.Button();
            this.btnAddWindowSize = new System.Windows.Forms.Button();
            this.gvWindowSize = new System.Windows.Forms.DataGridView();
            this.tabpMenuStart = new System.Windows.Forms.TabPage();
            this.grpbStartProgram = new System.Windows.Forms.GroupBox();
            this.btnStartProgramDown = new System.Windows.Forms.Button();
            this.btnStartProgramUp = new System.Windows.Forms.Button();
            this.btnAddStartProgram = new System.Windows.Forms.Button();
            this.gvStartProgram = new System.Windows.Forms.DataGridView();
            this.tabpMenuDimmer = new System.Windows.Forms.TabPage();
            this.grpbDimmerTransparency = new System.Windows.Forms.GroupBox();
            this.lblTransparencyValue = new System.Windows.Forms.Label();
            this.lblTransparencyToValue = new System.Windows.Forms.Label();
            this.lblTransparencyFromValue = new System.Windows.Forms.Label();
            this.trackbDimmerTransparency = new System.Windows.Forms.TrackBar();
            this.grpbDimmerColor = new System.Windows.Forms.GroupBox();
            this.btnChooseDimmerColor = new System.Windows.Forms.Button();
            this.txtDimmerColor = new System.Windows.Forms.TextBox();
            this.tabpMenuSaveSelectedItems = new System.Windows.Forms.TabPage();
            this.grpbSaveSelectedItems = new System.Windows.Forms.GroupBox();
            this.chkButtons = new System.Windows.Forms.CheckBox();
            this.chkHideForAltTab = new System.Windows.Forms.CheckBox();
            this.chkMinimizeToTrayAlways = new System.Windows.Forms.CheckBox();
            this.chkPriority = new System.Windows.Forms.CheckBox();
            this.chkTransparency = new System.Windows.Forms.CheckBox();
            this.chkAlignment = new System.Windows.Forms.CheckBox();
            this.chkAlwaysOnTop = new System.Windows.Forms.CheckBox();
            this.chkAeroGlass = new System.Windows.Forms.CheckBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTipAddProcessName = new System.Windows.Forms.ToolTip(this.components);
            this.clmWindowSizeTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWindowSizeLeft = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWindowSizeTop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWindowSizeWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWindowSizeHeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWindowSizeHotKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmWindowSizeEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.clmWindowSizeDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.clmStartProgramTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmStartProgramPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmStartProgramArguments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmStartProgramEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.clmStartProgramDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.clmnMenuItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnHotkeys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnShow = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmnChangeHotkey = new SmartSystemMenu.Controls.DataGridViewDisableButtonColumn();
            this.tabMain.SuspendLayout();
            this.tabpGeneral.SuspendLayout();
            this.grpbDisplay.SuspendLayout();
            this.grpbCloser.SuspendLayout();
            this.grpbLanguage.SuspendLayout();
            this.grpbProcessExclusions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProcessExclusions)).BeginInit();
            this.tabpMenu.SuspendLayout();
            this.grpbHotkeys.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvHotkeys)).BeginInit();
            this.tabpMenuSize.SuspendLayout();
            this.grpbSizer.SuspendLayout();
            this.grpbWindowSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvWindowSize)).BeginInit();
            this.tabpMenuStart.SuspendLayout();
            this.grpbStartProgram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvStartProgram)).BeginInit();
            this.tabpMenuDimmer.SuspendLayout();
            this.grpbDimmerTransparency.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackbDimmerTransparency)).BeginInit();
            this.grpbDimmerColor.SuspendLayout();
            this.tabpMenuSaveSelectedItems.SuspendLayout();
            this.grpbSaveSelectedItems.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabpGeneral);
            this.tabMain.Controls.Add(this.tabpMenu);
            this.tabMain.Controls.Add(this.tabpMenuSize);
            this.tabMain.Controls.Add(this.tabpMenuStart);
            this.tabMain.Controls.Add(this.tabpMenuDimmer);
            this.tabMain.Controls.Add(this.tabpMenuSaveSelectedItems);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(565, 416);
            this.tabMain.TabIndex = 0;
            // 
            // tabpGeneral
            // 
            this.tabpGeneral.Controls.Add(this.grpbDisplay);
            this.tabpGeneral.Controls.Add(this.grpbCloser);
            this.tabpGeneral.Controls.Add(this.grpbLanguage);
            this.tabpGeneral.Controls.Add(this.grpbProcessExclusions);
            this.tabpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabpGeneral.Name = "tabpGeneral";
            this.tabpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabpGeneral.Size = new System.Drawing.Size(557, 390);
            this.tabpGeneral.TabIndex = 0;
            this.tabpGeneral.UseVisualStyleBackColor = true;
            // 
            // grpbDisplay
            // 
            this.grpbDisplay.Controls.Add(this.chkEnableHighDPI);
            this.grpbDisplay.Location = new System.Drawing.Point(309, 87);
            this.grpbDisplay.Name = "grpbDisplay";
            this.grpbDisplay.Size = new System.Drawing.Size(240, 69);
            this.grpbDisplay.TabIndex = 2;
            this.grpbDisplay.TabStop = false;
            // 
            // chkEnableHighDPI
            // 
            this.chkEnableHighDPI.AutoSize = true;
            this.chkEnableHighDPI.Location = new System.Drawing.Point(6, 28);
            this.chkEnableHighDPI.Name = "chkEnableHighDPI";
            this.chkEnableHighDPI.Size = new System.Drawing.Size(15, 14);
            this.chkEnableHighDPI.TabIndex = 0;
            this.chkEnableHighDPI.UseVisualStyleBackColor = true;
            // 
            // grpbCloser
            // 
            this.grpbCloser.Controls.Add(this.btnCloser);
            this.grpbCloser.Location = new System.Drawing.Point(8, 87);
            this.grpbCloser.Name = "grpbCloser";
            this.grpbCloser.Size = new System.Drawing.Size(295, 69);
            this.grpbCloser.TabIndex = 1;
            this.grpbCloser.TabStop = false;
            // 
            // btnCloser
            // 
            this.btnCloser.Location = new System.Drawing.Point(6, 26);
            this.btnCloser.Name = "btnCloser";
            this.btnCloser.Size = new System.Drawing.Size(166, 23);
            this.btnCloser.TabIndex = 0;
            this.btnCloser.UseVisualStyleBackColor = true;
            this.btnCloser.Click += new System.EventHandler(this.ButtonWindowCloserClick);
            // 
            // grpbLanguage
            // 
            this.grpbLanguage.Controls.Add(this.cmbLanguage);
            this.grpbLanguage.Location = new System.Drawing.Point(8, 16);
            this.grpbLanguage.Name = "grpbLanguage";
            this.grpbLanguage.Size = new System.Drawing.Size(541, 68);
            this.grpbLanguage.TabIndex = 0;
            this.grpbLanguage.TabStop = false;
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Location = new System.Drawing.Point(6, 28);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(166, 21);
            this.cmbLanguage.TabIndex = 0;
            // 
            // grpbProcessExclusions
            // 
            this.grpbProcessExclusions.Controls.Add(this.btnProcessExclusionDown);
            this.grpbProcessExclusions.Controls.Add(this.btnProcessExclusionUp);
            this.grpbProcessExclusions.Controls.Add(this.btnAddProcessExclusion);
            this.grpbProcessExclusions.Controls.Add(this.gvProcessExclusions);
            this.grpbProcessExclusions.Location = new System.Drawing.Point(8, 160);
            this.grpbProcessExclusions.Name = "grpbProcessExclusions";
            this.grpbProcessExclusions.Size = new System.Drawing.Size(541, 224);
            this.grpbProcessExclusions.TabIndex = 3;
            this.grpbProcessExclusions.TabStop = false;
            // 
            // btnProcessExclusionDown
            // 
            this.btnProcessExclusionDown.Image = ((System.Drawing.Image)(resources.GetObject("btnProcessExclusionDown.Image")));
            this.btnProcessExclusionDown.Location = new System.Drawing.Point(450, 195);
            this.btnProcessExclusionDown.Name = "btnProcessExclusionDown";
            this.btnProcessExclusionDown.Size = new System.Drawing.Size(31, 23);
            this.btnProcessExclusionDown.TabIndex = 2;
            this.btnProcessExclusionDown.UseVisualStyleBackColor = true;
            this.btnProcessExclusionDown.Click += new System.EventHandler(this.ButtonArrowDownClick);
            // 
            // btnProcessExclusionUp
            // 
            this.btnProcessExclusionUp.Image = ((System.Drawing.Image)(resources.GetObject("btnProcessExclusionUp.Image")));
            this.btnProcessExclusionUp.Location = new System.Drawing.Point(413, 195);
            this.btnProcessExclusionUp.Name = "btnProcessExclusionUp";
            this.btnProcessExclusionUp.Size = new System.Drawing.Size(31, 23);
            this.btnProcessExclusionUp.TabIndex = 1;
            this.btnProcessExclusionUp.UseVisualStyleBackColor = true;
            this.btnProcessExclusionUp.Click += new System.EventHandler(this.ButtonArrowUpClick);
            // 
            // btnAddProcessExclusion
            // 
            this.btnAddProcessExclusion.Location = new System.Drawing.Point(504, 195);
            this.btnAddProcessExclusion.Name = "btnAddProcessExclusion";
            this.btnAddProcessExclusion.Size = new System.Drawing.Size(31, 23);
            this.btnAddProcessExclusion.TabIndex = 3;
            this.btnAddProcessExclusion.Text = "+";
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
            this.gvProcessExclusions.Size = new System.Drawing.Size(529, 170);
            this.gvProcessExclusions.TabIndex = 0;
            this.gvProcessExclusions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewProcessExclusionsCellContentClick);
            this.gvProcessExclusions.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewProcessExclusionsCellDoubleClick);
            // 
            // clmProcessExclusionName
            // 
            this.clmProcessExclusionName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
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
            this.clmProcessExclusionEdit.UseColumnTextForButtonValue = true;
            this.clmProcessExclusionEdit.Width = 30;
            // 
            // clmProcessExcusionDelete
            // 
            this.clmProcessExcusionDelete.HeaderText = "";
            this.clmProcessExcusionDelete.Name = "clmProcessExcusionDelete";
            this.clmProcessExcusionDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmProcessExcusionDelete.Text = "-";
            this.clmProcessExcusionDelete.UseColumnTextForButtonValue = true;
            this.clmProcessExcusionDelete.Width = 30;
            // 
            // tabpMenu
            // 
            this.tabpMenu.Controls.Add(this.grpbHotkeys);
            this.tabpMenu.Location = new System.Drawing.Point(4, 22);
            this.tabpMenu.Name = "tabpMenu";
            this.tabpMenu.Padding = new System.Windows.Forms.Padding(3);
            this.tabpMenu.Size = new System.Drawing.Size(557, 390);
            this.tabpMenu.TabIndex = 2;
            this.tabpMenu.UseVisualStyleBackColor = true;
            // 
            // grpbHotkeys
            // 
            this.grpbHotkeys.Controls.Add(this.btnMenuItemDown);
            this.grpbHotkeys.Controls.Add(this.btnMenuItemUp);
            this.grpbHotkeys.Controls.Add(this.gvHotkeys);
            this.grpbHotkeys.Location = new System.Drawing.Point(8, 16);
            this.grpbHotkeys.Name = "grpbHotkeys";
            this.grpbHotkeys.Size = new System.Drawing.Size(541, 368);
            this.grpbHotkeys.TabIndex = 3;
            this.grpbHotkeys.TabStop = false;
            // 
            // btnMenuItemDown
            // 
            this.btnMenuItemDown.Image = ((System.Drawing.Image)(resources.GetObject("btnMenuItemDown.Image")));
            this.btnMenuItemDown.Location = new System.Drawing.Point(504, 339);
            this.btnMenuItemDown.Name = "btnMenuItemDown";
            this.btnMenuItemDown.Size = new System.Drawing.Size(31, 23);
            this.btnMenuItemDown.TabIndex = 4;
            this.btnMenuItemDown.UseVisualStyleBackColor = true;
            this.btnMenuItemDown.Click += new System.EventHandler(this.ButtonMenuItemDownClick);
            // 
            // btnMenuItemUp
            // 
            this.btnMenuItemUp.Image = ((System.Drawing.Image)(resources.GetObject("btnMenuItemUp.Image")));
            this.btnMenuItemUp.Location = new System.Drawing.Point(467, 339);
            this.btnMenuItemUp.Name = "btnMenuItemUp";
            this.btnMenuItemUp.Size = new System.Drawing.Size(31, 23);
            this.btnMenuItemUp.TabIndex = 3;
            this.btnMenuItemUp.UseVisualStyleBackColor = true;
            this.btnMenuItemUp.Click += new System.EventHandler(this.ButtonMenuItemUpClick);
            // 
            // gvHotkeys
            // 
            this.gvHotkeys.AllowUserToAddRows = false;
            this.gvHotkeys.AllowUserToDeleteRows = false;
            this.gvHotkeys.AllowUserToResizeColumns = false;
            this.gvHotkeys.AllowUserToResizeRows = false;
            this.gvHotkeys.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.gvHotkeys.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvHotkeys.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gvHotkeys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvHotkeys.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmnMenuItemName,
            this.clmnHotkeys,
            this.clmnShow,
            this.clmnChangeHotkey});
            this.gvHotkeys.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gvHotkeys.GridColor = System.Drawing.SystemColors.Control;
            this.gvHotkeys.Location = new System.Drawing.Point(6, 19);
            this.gvHotkeys.MultiSelect = false;
            this.gvHotkeys.Name = "gvHotkeys";
            this.gvHotkeys.RowHeadersVisible = false;
            this.gvHotkeys.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gvHotkeys.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvHotkeys.Size = new System.Drawing.Size(529, 314);
            this.gvHotkeys.TabIndex = 0;
            this.gvHotkeys.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewHotkeysCellContentClick);
            this.gvHotkeys.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewHotkeysCellDoubleClick);
            // 
            // tabpMenuSize
            // 
            this.tabpMenuSize.Controls.Add(this.grpbSizer);
            this.tabpMenuSize.Controls.Add(this.grpbWindowSize);
            this.tabpMenuSize.Location = new System.Drawing.Point(4, 22);
            this.tabpMenuSize.Name = "tabpMenuSize";
            this.tabpMenuSize.Padding = new System.Windows.Forms.Padding(3);
            this.tabpMenuSize.Size = new System.Drawing.Size(557, 390);
            this.tabpMenuSize.TabIndex = 3;
            this.tabpMenuSize.UseVisualStyleBackColor = true;
            // 
            // grpbSizer
            // 
            this.grpbSizer.Controls.Add(this.cmbSizer);
            this.grpbSizer.Location = new System.Drawing.Point(8, 16);
            this.grpbSizer.Name = "grpbSizer";
            this.grpbSizer.Size = new System.Drawing.Size(541, 68);
            this.grpbSizer.TabIndex = 0;
            this.grpbSizer.TabStop = false;
            // 
            // cmbSizer
            // 
            this.cmbSizer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSizer.FormattingEnabled = true;
            this.cmbSizer.Location = new System.Drawing.Point(6, 28);
            this.cmbSizer.Name = "cmbSizer";
            this.cmbSizer.Size = new System.Drawing.Size(166, 21);
            this.cmbSizer.TabIndex = 0;
            // 
            // grpbWindowSize
            // 
            this.grpbWindowSize.Controls.Add(this.btnWindowSizeDown);
            this.grpbWindowSize.Controls.Add(this.btnWindowSizeUp);
            this.grpbWindowSize.Controls.Add(this.btnAddWindowSize);
            this.grpbWindowSize.Controls.Add(this.gvWindowSize);
            this.grpbWindowSize.Location = new System.Drawing.Point(8, 87);
            this.grpbWindowSize.Name = "grpbWindowSize";
            this.grpbWindowSize.Size = new System.Drawing.Size(541, 297);
            this.grpbWindowSize.TabIndex = 1;
            this.grpbWindowSize.TabStop = false;
            // 
            // btnWindowSizeDown
            // 
            this.btnWindowSizeDown.Image = ((System.Drawing.Image)(resources.GetObject("btnWindowSizeDown.Image")));
            this.btnWindowSizeDown.Location = new System.Drawing.Point(450, 268);
            this.btnWindowSizeDown.Name = "btnWindowSizeDown";
            this.btnWindowSizeDown.Size = new System.Drawing.Size(31, 23);
            this.btnWindowSizeDown.TabIndex = 2;
            this.btnWindowSizeDown.UseVisualStyleBackColor = true;
            this.btnWindowSizeDown.Click += new System.EventHandler(this.ButtonArrowDownClick);
            // 
            // btnWindowSizeUp
            // 
            this.btnWindowSizeUp.Image = ((System.Drawing.Image)(resources.GetObject("btnWindowSizeUp.Image")));
            this.btnWindowSizeUp.Location = new System.Drawing.Point(413, 268);
            this.btnWindowSizeUp.Name = "btnWindowSizeUp";
            this.btnWindowSizeUp.Size = new System.Drawing.Size(31, 23);
            this.btnWindowSizeUp.TabIndex = 1;
            this.btnWindowSizeUp.UseVisualStyleBackColor = true;
            this.btnWindowSizeUp.Click += new System.EventHandler(this.ButtonArrowUpClick);
            // 
            // btnAddWindowSize
            // 
            this.btnAddWindowSize.Location = new System.Drawing.Point(504, 268);
            this.btnAddWindowSize.Name = "btnAddWindowSize";
            this.btnAddWindowSize.Size = new System.Drawing.Size(31, 23);
            this.btnAddWindowSize.TabIndex = 3;
            this.btnAddWindowSize.Text = "+";
            this.btnAddWindowSize.UseVisualStyleBackColor = true;
            this.btnAddWindowSize.Click += new System.EventHandler(this.ButtonAddWindowSizeClick);
            // 
            // gvWindowSize
            // 
            this.gvWindowSize.AllowUserToAddRows = false;
            this.gvWindowSize.AllowUserToDeleteRows = false;
            this.gvWindowSize.AllowUserToResizeColumns = false;
            this.gvWindowSize.AllowUserToResizeRows = false;
            this.gvWindowSize.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.gvWindowSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvWindowSize.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gvWindowSize.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvWindowSize.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmWindowSizeTitle,
            this.clmWindowSizeLeft,
            this.clmWindowSizeTop,
            this.clmWindowSizeWidth,
            this.clmWindowSizeHeight,
            this.clmWindowSizeHotKey,
            this.clmWindowSizeEdit,
            this.clmWindowSizeDelete});
            this.gvWindowSize.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gvWindowSize.GridColor = System.Drawing.SystemColors.Control;
            this.gvWindowSize.Location = new System.Drawing.Point(6, 19);
            this.gvWindowSize.MultiSelect = false;
            this.gvWindowSize.Name = "gvWindowSize";
            this.gvWindowSize.RowHeadersVisible = false;
            this.gvWindowSize.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gvWindowSize.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvWindowSize.Size = new System.Drawing.Size(529, 243);
            this.gvWindowSize.TabIndex = 0;
            this.gvWindowSize.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewWindowSizeCellContentClick);
            this.gvWindowSize.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewWindowSizeCellDoubleClick);
            // 
            // tabpMenuStart
            // 
            this.tabpMenuStart.Controls.Add(this.grpbStartProgram);
            this.tabpMenuStart.Location = new System.Drawing.Point(4, 22);
            this.tabpMenuStart.Name = "tabpMenuStart";
            this.tabpMenuStart.Padding = new System.Windows.Forms.Padding(3);
            this.tabpMenuStart.Size = new System.Drawing.Size(557, 390);
            this.tabpMenuStart.TabIndex = 1;
            this.tabpMenuStart.UseVisualStyleBackColor = true;
            // 
            // grpbStartProgram
            // 
            this.grpbStartProgram.Controls.Add(this.btnStartProgramDown);
            this.grpbStartProgram.Controls.Add(this.btnStartProgramUp);
            this.grpbStartProgram.Controls.Add(this.btnAddStartProgram);
            this.grpbStartProgram.Controls.Add(this.gvStartProgram);
            this.grpbStartProgram.Location = new System.Drawing.Point(8, 16);
            this.grpbStartProgram.Name = "grpbStartProgram";
            this.grpbStartProgram.Size = new System.Drawing.Size(541, 368);
            this.grpbStartProgram.TabIndex = 0;
            this.grpbStartProgram.TabStop = false;
            // 
            // btnStartProgramDown
            // 
            this.btnStartProgramDown.Image = ((System.Drawing.Image)(resources.GetObject("btnStartProgramDown.Image")));
            this.btnStartProgramDown.Location = new System.Drawing.Point(450, 339);
            this.btnStartProgramDown.Name = "btnStartProgramDown";
            this.btnStartProgramDown.Size = new System.Drawing.Size(31, 23);
            this.btnStartProgramDown.TabIndex = 2;
            this.btnStartProgramDown.UseVisualStyleBackColor = true;
            this.btnStartProgramDown.Click += new System.EventHandler(this.ButtonArrowDownClick);
            // 
            // btnStartProgramUp
            // 
            this.btnStartProgramUp.Image = ((System.Drawing.Image)(resources.GetObject("btnStartProgramUp.Image")));
            this.btnStartProgramUp.Location = new System.Drawing.Point(413, 339);
            this.btnStartProgramUp.Name = "btnStartProgramUp";
            this.btnStartProgramUp.Size = new System.Drawing.Size(31, 23);
            this.btnStartProgramUp.TabIndex = 1;
            this.btnStartProgramUp.UseVisualStyleBackColor = true;
            this.btnStartProgramUp.Click += new System.EventHandler(this.ButtonArrowUpClick);
            // 
            // btnAddStartProgram
            // 
            this.btnAddStartProgram.Location = new System.Drawing.Point(504, 339);
            this.btnAddStartProgram.Name = "btnAddStartProgram";
            this.btnAddStartProgram.Size = new System.Drawing.Size(31, 23);
            this.btnAddStartProgram.TabIndex = 3;
            this.btnAddStartProgram.Text = "+";
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
            this.gvStartProgram.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gvStartProgram.GridColor = System.Drawing.SystemColors.Control;
            this.gvStartProgram.Location = new System.Drawing.Point(6, 19);
            this.gvStartProgram.MultiSelect = false;
            this.gvStartProgram.Name = "gvStartProgram";
            this.gvStartProgram.RowHeadersVisible = false;
            this.gvStartProgram.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gvStartProgram.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvStartProgram.Size = new System.Drawing.Size(529, 314);
            this.gvStartProgram.TabIndex = 0;
            this.gvStartProgram.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewStartProgramCellContentClick);
            this.gvStartProgram.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewStartProgramCellDoubleClick);
            // 
            // tabpMenuDimmer
            // 
            this.tabpMenuDimmer.Controls.Add(this.grpbDimmerTransparency);
            this.tabpMenuDimmer.Controls.Add(this.grpbDimmerColor);
            this.tabpMenuDimmer.Location = new System.Drawing.Point(4, 22);
            this.tabpMenuDimmer.Name = "tabpMenuDimmer";
            this.tabpMenuDimmer.Size = new System.Drawing.Size(557, 390);
            this.tabpMenuDimmer.TabIndex = 5;
            this.tabpMenuDimmer.UseVisualStyleBackColor = true;
            // 
            // grpbDimmerTransparency
            // 
            this.grpbDimmerTransparency.Controls.Add(this.lblTransparencyValue);
            this.grpbDimmerTransparency.Controls.Add(this.lblTransparencyToValue);
            this.grpbDimmerTransparency.Controls.Add(this.lblTransparencyFromValue);
            this.grpbDimmerTransparency.Controls.Add(this.trackbDimmerTransparency);
            this.grpbDimmerTransparency.Location = new System.Drawing.Point(8, 90);
            this.grpbDimmerTransparency.Name = "grpbDimmerTransparency";
            this.grpbDimmerTransparency.Size = new System.Drawing.Size(541, 100);
            this.grpbDimmerTransparency.TabIndex = 1;
            this.grpbDimmerTransparency.TabStop = false;
            // 
            // lblTransparencyValue
            // 
            this.lblTransparencyValue.AutoSize = true;
            this.lblTransparencyValue.Location = new System.Drawing.Point(259, 23);
            this.lblTransparencyValue.Name = "lblTransparencyValue";
            this.lblTransparencyValue.Size = new System.Drawing.Size(0, 13);
            this.lblTransparencyValue.TabIndex = 1;
            // 
            // lblTransparencyToValue
            // 
            this.lblTransparencyToValue.AutoSize = true;
            this.lblTransparencyToValue.Location = new System.Drawing.Point(502, 23);
            this.lblTransparencyToValue.Name = "lblTransparencyToValue";
            this.lblTransparencyToValue.Size = new System.Drawing.Size(33, 13);
            this.lblTransparencyToValue.TabIndex = 2;
            this.lblTransparencyToValue.Text = "100%";
            // 
            // lblTransparencyFromValue
            // 
            this.lblTransparencyFromValue.AutoSize = true;
            this.lblTransparencyFromValue.Location = new System.Drawing.Point(6, 23);
            this.lblTransparencyFromValue.Name = "lblTransparencyFromValue";
            this.lblTransparencyFromValue.Size = new System.Drawing.Size(21, 13);
            this.lblTransparencyFromValue.TabIndex = 0;
            this.lblTransparencyFromValue.Text = "0%";
            // 
            // trackbDimmerTransparency
            // 
            this.trackbDimmerTransparency.Location = new System.Drawing.Point(6, 39);
            this.trackbDimmerTransparency.Maximum = 100;
            this.trackbDimmerTransparency.Name = "trackbDimmerTransparency";
            this.trackbDimmerTransparency.Size = new System.Drawing.Size(529, 45);
            this.trackbDimmerTransparency.TabIndex = 3;
            this.trackbDimmerTransparency.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackbDimmerTransparency.ValueChanged += new System.EventHandler(this.TrackbDimmerTransparencyValueChanged);
            // 
            // grpbDimmerColor
            // 
            this.grpbDimmerColor.Controls.Add(this.btnChooseDimmerColor);
            this.grpbDimmerColor.Controls.Add(this.txtDimmerColor);
            this.grpbDimmerColor.Location = new System.Drawing.Point(8, 16);
            this.grpbDimmerColor.Name = "grpbDimmerColor";
            this.grpbDimmerColor.Size = new System.Drawing.Size(541, 68);
            this.grpbDimmerColor.TabIndex = 0;
            this.grpbDimmerColor.TabStop = false;
            // 
            // btnChooseDimmerColor
            // 
            this.btnChooseDimmerColor.Location = new System.Drawing.Point(155, 26);
            this.btnChooseDimmerColor.Name = "btnChooseDimmerColor";
            this.btnChooseDimmerColor.Size = new System.Drawing.Size(54, 23);
            this.btnChooseDimmerColor.TabIndex = 1;
            this.btnChooseDimmerColor.Text = "...";
            this.btnChooseDimmerColor.UseVisualStyleBackColor = true;
            this.btnChooseDimmerColor.Click += new System.EventHandler(this.ButtonChooseDimmerColorClick);
            // 
            // txtDimmerColor
            // 
            this.txtDimmerColor.Location = new System.Drawing.Point(6, 28);
            this.txtDimmerColor.Name = "txtDimmerColor";
            this.txtDimmerColor.Size = new System.Drawing.Size(143, 20);
            this.txtDimmerColor.TabIndex = 0;
            // 
            // tabpMenuSaveSelectedItems
            // 
            this.tabpMenuSaveSelectedItems.Controls.Add(this.grpbSaveSelectedItems);
            this.tabpMenuSaveSelectedItems.Location = new System.Drawing.Point(4, 22);
            this.tabpMenuSaveSelectedItems.Name = "tabpMenuSaveSelectedItems";
            this.tabpMenuSaveSelectedItems.Padding = new System.Windows.Forms.Padding(3);
            this.tabpMenuSaveSelectedItems.Size = new System.Drawing.Size(557, 390);
            this.tabpMenuSaveSelectedItems.TabIndex = 4;
            this.tabpMenuSaveSelectedItems.UseVisualStyleBackColor = true;
            // 
            // grpbSaveSelectedItems
            // 
            this.grpbSaveSelectedItems.Controls.Add(this.chkButtons);
            this.grpbSaveSelectedItems.Controls.Add(this.chkHideForAltTab);
            this.grpbSaveSelectedItems.Controls.Add(this.chkMinimizeToTrayAlways);
            this.grpbSaveSelectedItems.Controls.Add(this.chkPriority);
            this.grpbSaveSelectedItems.Controls.Add(this.chkTransparency);
            this.grpbSaveSelectedItems.Controls.Add(this.chkAlignment);
            this.grpbSaveSelectedItems.Controls.Add(this.chkAlwaysOnTop);
            this.grpbSaveSelectedItems.Controls.Add(this.chkAeroGlass);
            this.grpbSaveSelectedItems.Location = new System.Drawing.Point(8, 16);
            this.grpbSaveSelectedItems.Name = "grpbSaveSelectedItems";
            this.grpbSaveSelectedItems.Size = new System.Drawing.Size(541, 368);
            this.grpbSaveSelectedItems.TabIndex = 0;
            this.grpbSaveSelectedItems.TabStop = false;
            // 
            // chkButtons
            // 
            this.chkButtons.AutoSize = true;
            this.chkButtons.Location = new System.Drawing.Point(6, 238);
            this.chkButtons.Name = "chkButtons";
            this.chkButtons.Size = new System.Drawing.Size(62, 17);
            this.chkButtons.TabIndex = 7;
            this.chkButtons.Text = "Buttons";
            this.chkButtons.UseVisualStyleBackColor = true;
            // 
            // chkHideForAltTab
            // 
            this.chkHideForAltTab.AutoSize = true;
            this.chkHideForAltTab.Location = new System.Drawing.Point(6, 88);
            this.chkHideForAltTab.Name = "chkHideForAltTab";
            this.chkHideForAltTab.Size = new System.Drawing.Size(103, 17);
            this.chkHideForAltTab.TabIndex = 2;
            this.chkHideForAltTab.Text = "Hide For Alt Tab";
            this.chkHideForAltTab.UseVisualStyleBackColor = true;
            // 
            // chkMinimizeToTrayAlways
            // 
            this.chkMinimizeToTrayAlways.AutoSize = true;
            this.chkMinimizeToTrayAlways.Location = new System.Drawing.Point(6, 208);
            this.chkMinimizeToTrayAlways.Name = "chkMinimizeToTrayAlways";
            this.chkMinimizeToTrayAlways.Size = new System.Drawing.Size(142, 17);
            this.chkMinimizeToTrayAlways.TabIndex = 6;
            this.chkMinimizeToTrayAlways.Text = "Minimize To Tray Always";
            this.chkMinimizeToTrayAlways.UseVisualStyleBackColor = true;
            // 
            // chkPriority
            // 
            this.chkPriority.AutoSize = true;
            this.chkPriority.Location = new System.Drawing.Point(6, 178);
            this.chkPriority.Name = "chkPriority";
            this.chkPriority.Size = new System.Drawing.Size(57, 17);
            this.chkPriority.TabIndex = 5;
            this.chkPriority.Text = "Priority";
            this.chkPriority.UseVisualStyleBackColor = true;
            // 
            // chkTransparency
            // 
            this.chkTransparency.AutoSize = true;
            this.chkTransparency.Location = new System.Drawing.Point(6, 148);
            this.chkTransparency.Name = "chkTransparency";
            this.chkTransparency.Size = new System.Drawing.Size(91, 17);
            this.chkTransparency.TabIndex = 4;
            this.chkTransparency.Text = "Transparency";
            this.chkTransparency.UseVisualStyleBackColor = true;
            // 
            // chkAlignment
            // 
            this.chkAlignment.AutoSize = true;
            this.chkAlignment.Location = new System.Drawing.Point(6, 118);
            this.chkAlignment.Name = "chkAlignment";
            this.chkAlignment.Size = new System.Drawing.Size(72, 17);
            this.chkAlignment.TabIndex = 3;
            this.chkAlignment.Text = "Alignment";
            this.chkAlignment.UseVisualStyleBackColor = true;
            // 
            // chkAlwaysOnTop
            // 
            this.chkAlwaysOnTop.AutoSize = true;
            this.chkAlwaysOnTop.Location = new System.Drawing.Point(6, 58);
            this.chkAlwaysOnTop.Name = "chkAlwaysOnTop";
            this.chkAlwaysOnTop.Size = new System.Drawing.Size(98, 17);
            this.chkAlwaysOnTop.TabIndex = 1;
            this.chkAlwaysOnTop.Text = "Always On Top";
            this.chkAlwaysOnTop.UseVisualStyleBackColor = true;
            // 
            // chkAeroGlass
            // 
            this.chkAeroGlass.AutoSize = true;
            this.chkAeroGlass.Location = new System.Drawing.Point(6, 28);
            this.chkAeroGlass.Name = "chkAeroGlass";
            this.chkAeroGlass.Size = new System.Drawing.Size(77, 17);
            this.chkAeroGlass.TabIndex = 0;
            this.chkAeroGlass.Text = "Aero Glass";
            this.chkAeroGlass.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(384, 432);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(81, 35);
            this.btnApply.TabIndex = 1;
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.ButtonApplyClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(473, 432);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 35);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // clmWindowSizeTitle
            // 
            this.clmWindowSizeTitle.HeaderText = "clmWindowSizeTitle";
            this.clmWindowSizeTitle.Name = "clmWindowSizeTitle";
            this.clmWindowSizeTitle.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // clmWindowSizeLeft
            // 
            this.clmWindowSizeLeft.HeaderText = "clmWindowSizeLeft";
            this.clmWindowSizeLeft.Name = "clmWindowSizeLeft";
            this.clmWindowSizeLeft.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmWindowSizeLeft.Width = 50;
            // 
            // clmWindowSizeTop
            // 
            this.clmWindowSizeTop.HeaderText = "clmWindowSizeTop";
            this.clmWindowSizeTop.Name = "clmWindowSizeTop";
            this.clmWindowSizeTop.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmWindowSizeTop.Width = 50;
            // 
            // clmWindowSizeWidth
            // 
            this.clmWindowSizeWidth.HeaderText = "clmWindowSizeWidth";
            this.clmWindowSizeWidth.Name = "clmWindowSizeWidth";
            this.clmWindowSizeWidth.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmWindowSizeWidth.Width = 50;
            // 
            // clmWindowSizeHeight
            // 
            this.clmWindowSizeHeight.HeaderText = "clmWindowSizeHeight";
            this.clmWindowSizeHeight.Name = "clmWindowSizeHeight";
            this.clmWindowSizeHeight.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmWindowSizeHeight.Width = 50;
            // 
            // clmWindowSizeHotKey
            // 
            this.clmWindowSizeHotKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmWindowSizeHotKey.HeaderText = "";
            this.clmWindowSizeHotKey.MinimumWidth = 30;
            this.clmWindowSizeHotKey.Name = "clmWindowSizeHotKey";
            // 
            // clmWindowSizeEdit
            // 
            this.clmWindowSizeEdit.HeaderText = "";
            this.clmWindowSizeEdit.Name = "clmWindowSizeEdit";
            this.clmWindowSizeEdit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmWindowSizeEdit.Text = "...";
            this.clmWindowSizeEdit.UseColumnTextForButtonValue = true;
            this.clmWindowSizeEdit.Width = 30;
            // 
            // clmWindowSizeDelete
            // 
            this.clmWindowSizeDelete.HeaderText = "";
            this.clmWindowSizeDelete.Name = "clmWindowSizeDelete";
            this.clmWindowSizeDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmWindowSizeDelete.Text = "-";
            this.clmWindowSizeDelete.UseColumnTextForButtonValue = true;
            this.clmWindowSizeDelete.Width = 30;
            // 
            // clmStartProgramTitle
            // 
            this.clmStartProgramTitle.HeaderText = "clmStartProgramTitle";
            this.clmStartProgramTitle.MinimumWidth = 6;
            this.clmStartProgramTitle.Name = "clmStartProgramTitle";
            this.clmStartProgramTitle.ReadOnly = true;
            this.clmStartProgramTitle.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmStartProgramTitle.Width = 160;
            // 
            // clmStartProgramPath
            // 
            this.clmStartProgramPath.HeaderText = "clmStartProgramPath";
            this.clmStartProgramPath.MinimumWidth = 6;
            this.clmStartProgramPath.Name = "clmStartProgramPath";
            this.clmStartProgramPath.ReadOnly = true;
            this.clmStartProgramPath.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmStartProgramPath.Width = 160;
            // 
            // clmStartProgramArguments
            // 
            this.clmStartProgramArguments.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmStartProgramArguments.HeaderText = "clmStartProgramArguments";
            this.clmStartProgramArguments.MinimumWidth = 30;
            this.clmStartProgramArguments.Name = "clmStartProgramArguments";
            this.clmStartProgramArguments.ReadOnly = true;
            this.clmStartProgramArguments.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // clmStartProgramEdit
            // 
            this.clmStartProgramEdit.HeaderText = "";
            this.clmStartProgramEdit.Name = "clmStartProgramEdit";
            this.clmStartProgramEdit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmStartProgramEdit.Text = "...";
            this.clmStartProgramEdit.UseColumnTextForButtonValue = true;
            this.clmStartProgramEdit.Width = 30;
            // 
            // clmStartProgramDelete
            // 
            this.clmStartProgramDelete.HeaderText = "";
            this.clmStartProgramDelete.Name = "clmStartProgramDelete";
            this.clmStartProgramDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmStartProgramDelete.Text = "-";
            this.clmStartProgramDelete.UseColumnTextForButtonValue = true;
            this.clmStartProgramDelete.Width = 30;
            // 
            // clmnMenuItemName
            // 
            this.clmnMenuItemName.HeaderText = "clmnMenuItemName";
            this.clmnMenuItemName.Name = "clmnMenuItemName";
            this.clmnMenuItemName.ReadOnly = true;
            this.clmnMenuItemName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmnMenuItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmnMenuItemName.Width = 260;
            // 
            // clmnHotkeys
            // 
            this.clmnHotkeys.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmnHotkeys.HeaderText = "clmnHotkeys";
            this.clmnHotkeys.MinimumWidth = 30;
            this.clmnHotkeys.Name = "clmnHotkeys";
            this.clmnHotkeys.ReadOnly = true;
            this.clmnHotkeys.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmnShow
            // 
            this.clmnShow.HeaderText = "";
            this.clmnShow.Name = "clmnShow";
            this.clmnShow.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmnShow.Width = 30;
            // 
            // clmnChangeHotkey
            // 
            this.clmnChangeHotkey.HeaderText = "";
            this.clmnChangeHotkey.Name = "clmnChangeHotkey";
            this.clmnChangeHotkey.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmnChangeHotkey.Text = "...";
            this.clmnChangeHotkey.UseColumnTextForButtonValue = true;
            this.clmnChangeHotkey.Width = 30;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 492);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.tabMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownClick);
            this.tabMain.ResumeLayout(false);
            this.tabpGeneral.ResumeLayout(false);
            this.grpbDisplay.ResumeLayout(false);
            this.grpbDisplay.PerformLayout();
            this.grpbCloser.ResumeLayout(false);
            this.grpbLanguage.ResumeLayout(false);
            this.grpbProcessExclusions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvProcessExclusions)).EndInit();
            this.tabpMenu.ResumeLayout(false);
            this.grpbHotkeys.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvHotkeys)).EndInit();
            this.tabpMenuSize.ResumeLayout(false);
            this.grpbSizer.ResumeLayout(false);
            this.grpbWindowSize.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvWindowSize)).EndInit();
            this.tabpMenuStart.ResumeLayout(false);
            this.grpbStartProgram.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvStartProgram)).EndInit();
            this.tabpMenuDimmer.ResumeLayout(false);
            this.grpbDimmerTransparency.ResumeLayout(false);
            this.grpbDimmerTransparency.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackbDimmerTransparency)).EndInit();
            this.grpbDimmerColor.ResumeLayout(false);
            this.grpbDimmerColor.PerformLayout();
            this.tabpMenuSaveSelectedItems.ResumeLayout(false);
            this.grpbSaveSelectedItems.ResumeLayout(false);
            this.grpbSaveSelectedItems.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabpGeneral;
        private System.Windows.Forms.TabPage tabpMenuStart;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip toolTipAddProcessName;
        private System.Windows.Forms.GroupBox grpbStartProgram;
        private System.Windows.Forms.Button btnAddStartProgram;
        private System.Windows.Forms.DataGridView gvStartProgram;
        private System.Windows.Forms.Button btnStartProgramDown;
        private System.Windows.Forms.Button btnStartProgramUp;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.GroupBox grpbProcessExclusions;
        private System.Windows.Forms.Button btnProcessExclusionDown;
        private System.Windows.Forms.Button btnProcessExclusionUp;
        private System.Windows.Forms.Button btnAddProcessExclusion;
        private System.Windows.Forms.DataGridView gvProcessExclusions;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmProcessExclusionName;
        private System.Windows.Forms.DataGridViewButtonColumn clmProcessExclusionEdit;
        private System.Windows.Forms.DataGridViewButtonColumn clmProcessExcusionDelete;
        private System.Windows.Forms.TabPage tabpMenu;
        private System.Windows.Forms.GroupBox grpbHotkeys;
        private System.Windows.Forms.DataGridView gvHotkeys;
        private System.Windows.Forms.TabPage tabpMenuSize;
        private System.Windows.Forms.GroupBox grpbWindowSize;
        private System.Windows.Forms.Button btnWindowSizeDown;
        private System.Windows.Forms.Button btnWindowSizeUp;
        private System.Windows.Forms.Button btnAddWindowSize;
        private System.Windows.Forms.DataGridView gvWindowSize;
        private System.Windows.Forms.GroupBox grpbCloser;
        private System.Windows.Forms.Button btnCloser;
        private System.Windows.Forms.GroupBox grpbLanguage;
        private System.Windows.Forms.GroupBox grpbSizer;
        private System.Windows.Forms.ComboBox cmbSizer;
        private System.Windows.Forms.Button btnMenuItemDown;
        private System.Windows.Forms.Button btnMenuItemUp;
        private System.Windows.Forms.GroupBox grpbDisplay;
        private System.Windows.Forms.CheckBox chkEnableHighDPI;
        private System.Windows.Forms.TabPage tabpMenuSaveSelectedItems;
        private System.Windows.Forms.GroupBox grpbSaveSelectedItems;
        private System.Windows.Forms.CheckBox chkAeroGlass;
        private System.Windows.Forms.CheckBox chkAlwaysOnTop;
        private System.Windows.Forms.CheckBox chkAlignment;
        private System.Windows.Forms.CheckBox chkTransparency;
        private System.Windows.Forms.CheckBox chkPriority;
        private System.Windows.Forms.CheckBox chkMinimizeToTrayAlways;
        private System.Windows.Forms.CheckBox chkHideForAltTab;
        private System.Windows.Forms.CheckBox chkButtons;
        private System.Windows.Forms.TabPage tabpMenuDimmer;
        private System.Windows.Forms.GroupBox grpbDimmerTransparency;
        private System.Windows.Forms.TrackBar trackbDimmerTransparency;
        private System.Windows.Forms.GroupBox grpbDimmerColor;
        private System.Windows.Forms.Button btnChooseDimmerColor;
        private System.Windows.Forms.TextBox txtDimmerColor;
        private System.Windows.Forms.Label lblTransparencyFromValue;
        private System.Windows.Forms.Label lblTransparencyValue;
        private System.Windows.Forms.Label lblTransparencyToValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnMenuItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnHotkeys;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmnShow;
        private DataGridViewDisableButtonColumn clmnChangeHotkey;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmWindowSizeTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmWindowSizeLeft;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmWindowSizeTop;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmWindowSizeWidth;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmWindowSizeHeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmWindowSizeHotKey;
        private System.Windows.Forms.DataGridViewButtonColumn clmWindowSizeEdit;
        private System.Windows.Forms.DataGridViewButtonColumn clmWindowSizeDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmStartProgramTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmStartProgramPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmStartProgramArguments;
        private System.Windows.Forms.DataGridViewButtonColumn clmStartProgramEdit;
        private System.Windows.Forms.DataGridViewButtonColumn clmStartProgramDelete;
    }
}