namespace SmartSystemMenu.Forms
{
    partial class InfoForm
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
            this.grpWindow = new System.Windows.Forms.GroupBox();
            this.tblWindowBottom = new System.Windows.Forms.TableLayoutPanel();
            this.lblDwlDlgProc = new System.Windows.Forms.Label();
            this.txtWindowHandle = new System.Windows.Forms.TextBox();
            this.txtGwlStyle = new System.Windows.Forms.TextBox();
            this.txtParentWindowHandle = new System.Windows.Forms.TextBox();
            this.txtGclStyle = new System.Windows.Forms.TextBox();
            this.txtWindowSize = new System.Windows.Forms.TextBox();
            this.txtGwlExStyle = new System.Windows.Forms.TextBox();
            this.txtInstance = new System.Windows.Forms.TextBox();
            this.txtWindowInfoExStyle = new System.Windows.Forms.TextBox();
            this.txtProcessId = new System.Windows.Forms.TextBox();
            this.txtLwaAlpha = new System.Windows.Forms.TextBox();
            this.txtThreadId = new System.Windows.Forms.TextBox();
            this.txtLwaColorKey = new System.Windows.Forms.TextBox();
            this.txtGclWndProc = new System.Windows.Forms.TextBox();
            this.txtGwlUserData = new System.Windows.Forms.TextBox();
            this.txtDwlDlgProc = new System.Windows.Forms.TextBox();
            this.txtDwlUser = new System.Windows.Forms.TextBox();
            this.lblWindowHandle = new System.Windows.Forms.Label();
            this.lblParentWindowHandle = new System.Windows.Forms.Label();
            this.lblWindowSize = new System.Windows.Forms.Label();
            this.lblInstance = new System.Windows.Forms.Label();
            this.lblProcessId = new System.Windows.Forms.Label();
            this.lblThreadId = new System.Windows.Forms.Label();
            this.lblGclWndProc = new System.Windows.Forms.Label();
            this.lblGwlStyle = new System.Windows.Forms.Label();
            this.lblGclStyle = new System.Windows.Forms.Label();
            this.lblGwlExStyle = new System.Windows.Forms.Label();
            this.lblWindowInfoExStyle = new System.Windows.Forms.Label();
            this.lblLwaAlpha = new System.Windows.Forms.Label();
            this.lblLwaColorKey = new System.Windows.Forms.Label();
            this.lblGwlUserData = new System.Windows.Forms.Label();
            this.lblDwlUser = new System.Windows.Forms.Label();
            this.tblWindowTop = new System.Windows.Forms.TableLayoutPanel();
            this.lblFontFace = new System.Windows.Forms.Label();
            this.lblRealGetWindowClass = new System.Windows.Forms.Label();
            this.lblGetClassName = new System.Windows.Forms.Label();
            this.lblWmGetText = new System.Windows.Forms.Label();
            this.lblGetWindowText = new System.Windows.Forms.Label();
            this.txtGetWindowText = new System.Windows.Forms.TextBox();
            this.txtWmGetText = new System.Windows.Forms.TextBox();
            this.txtGetClassName = new System.Windows.Forms.TextBox();
            this.txtRealGetWindowClass = new System.Windows.Forms.TextBox();
            this.txtFontFace = new System.Windows.Forms.TextBox();
            this.grpProcess = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtFileVersion = new System.Windows.Forms.TextBox();
            this.lblFileVersion = new System.Windows.Forms.Label();
            this.lblProductVersion = new System.Windows.Forms.Label();
            this.txtProductVersion = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.txtCopyright = new System.Windows.Forms.TextBox();
            this.tblProcessBottom = new System.Windows.Forms.TableLayoutPanel();
            this.txtOwner = new System.Windows.Forms.TextBox();
            this.lblOwner = new System.Windows.Forms.Label();
            this.lblStartedAt = new System.Windows.Forms.Label();
            this.lblParent = new System.Windows.Forms.Label();
            this.lblPriority = new System.Windows.Forms.Label();
            this.txtStartedAt = new System.Windows.Forms.TextBox();
            this.txtParent = new System.Windows.Forms.TextBox();
            this.txtPriority = new System.Windows.Forms.TextBox();
            this.tblProcessTop = new System.Windows.Forms.TableLayoutPanel();
            this.lblFullPath = new System.Windows.Forms.Label();
            this.lblCommandLine = new System.Windows.Forms.Label();
            this.txtFullPath = new System.Windows.Forms.TextBox();
            this.txtCommandLine = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblThreads = new System.Windows.Forms.Label();
            this.lblHandles = new System.Windows.Forms.Label();
            this.lblVirtualSize = new System.Windows.Forms.Label();
            this.lblWorkingSetSize = new System.Windows.Forms.Label();
            this.txtHandles = new System.Windows.Forms.TextBox();
            this.txtThreads = new System.Windows.Forms.TextBox();
            this.txtWorkingSetSize = new System.Windows.Forms.TextBox();
            this.txtVirtualSize = new System.Windows.Forms.TextBox();
            this.grpWindow.SuspendLayout();
            this.tblWindowBottom.SuspendLayout();
            this.tblWindowTop.SuspendLayout();
            this.grpProcess.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tblProcessBottom.SuspendLayout();
            this.tblProcessTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpWindow
            // 
            this.grpWindow.Controls.Add(this.tblWindowBottom);
            this.grpWindow.Controls.Add(this.tblWindowTop);
            this.grpWindow.Location = new System.Drawing.Point(7, 2);
            this.grpWindow.Name = "grpWindow";
            this.grpWindow.Size = new System.Drawing.Size(715, 354);
            this.grpWindow.TabIndex = 0;
            this.grpWindow.TabStop = false;
            this.grpWindow.Text = "Window";
            // 
            // tblWindowBottom
            // 
            this.tblWindowBottom.ColumnCount = 4;
            this.tblWindowBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.49288F));
            this.tblWindowBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.21367F));
            this.tblWindowBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.32859F));
            this.tblWindowBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.03983F));
            this.tblWindowBottom.Controls.Add(this.lblDwlDlgProc, 0, 7);
            this.tblWindowBottom.Controls.Add(this.txtWindowHandle, 1, 0);
            this.tblWindowBottom.Controls.Add(this.txtGwlStyle, 3, 0);
            this.tblWindowBottom.Controls.Add(this.txtParentWindowHandle, 1, 1);
            this.tblWindowBottom.Controls.Add(this.txtGclStyle, 3, 1);
            this.tblWindowBottom.Controls.Add(this.txtWindowSize, 1, 2);
            this.tblWindowBottom.Controls.Add(this.txtGwlExStyle, 3, 2);
            this.tblWindowBottom.Controls.Add(this.txtInstance, 1, 3);
            this.tblWindowBottom.Controls.Add(this.txtWindowInfoExStyle, 3, 3);
            this.tblWindowBottom.Controls.Add(this.txtProcessId, 1, 4);
            this.tblWindowBottom.Controls.Add(this.txtLwaAlpha, 3, 4);
            this.tblWindowBottom.Controls.Add(this.txtThreadId, 1, 5);
            this.tblWindowBottom.Controls.Add(this.txtLwaColorKey, 3, 5);
            this.tblWindowBottom.Controls.Add(this.txtGclWndProc, 1, 6);
            this.tblWindowBottom.Controls.Add(this.txtGwlUserData, 3, 6);
            this.tblWindowBottom.Controls.Add(this.txtDwlDlgProc, 1, 7);
            this.tblWindowBottom.Controls.Add(this.txtDwlUser, 3, 7);
            this.tblWindowBottom.Controls.Add(this.lblWindowHandle, 0, 0);
            this.tblWindowBottom.Controls.Add(this.lblParentWindowHandle, 0, 1);
            this.tblWindowBottom.Controls.Add(this.lblWindowSize, 0, 2);
            this.tblWindowBottom.Controls.Add(this.lblInstance, 0, 3);
            this.tblWindowBottom.Controls.Add(this.lblProcessId, 0, 4);
            this.tblWindowBottom.Controls.Add(this.lblThreadId, 0, 5);
            this.tblWindowBottom.Controls.Add(this.lblGclWndProc, 0, 6);
            this.tblWindowBottom.Controls.Add(this.lblGwlStyle, 2, 0);
            this.tblWindowBottom.Controls.Add(this.lblGclStyle, 2, 1);
            this.tblWindowBottom.Controls.Add(this.lblGwlExStyle, 2, 2);
            this.tblWindowBottom.Controls.Add(this.lblWindowInfoExStyle, 2, 3);
            this.tblWindowBottom.Controls.Add(this.lblLwaAlpha, 2, 4);
            this.tblWindowBottom.Controls.Add(this.lblLwaColorKey, 2, 5);
            this.tblWindowBottom.Controls.Add(this.lblGwlUserData, 2, 6);
            this.tblWindowBottom.Controls.Add(this.lblDwlUser, 2, 7);
            this.tblWindowBottom.Location = new System.Drawing.Point(6, 142);
            this.tblWindowBottom.Name = "tblWindowBottom";
            this.tblWindowBottom.RowCount = 8;
            this.tblWindowBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblWindowBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblWindowBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblWindowBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblWindowBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblWindowBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblWindowBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblWindowBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblWindowBottom.Size = new System.Drawing.Size(703, 205);
            this.tblWindowBottom.TabIndex = 1;
            // 
            // lblDwlDlgProc
            // 
            this.lblDwlDlgProc.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDwlDlgProc.AutoSize = true;
            this.lblDwlDlgProc.Location = new System.Drawing.Point(97, 183);
            this.lblDwlDlgProc.Name = "lblDwlDlgProc";
            this.lblDwlDlgProc.Size = new System.Drawing.Size(93, 13);
            this.lblDwlDlgProc.TabIndex = 28;
            this.lblDwlDlgProc.Text = "DWL_DLGPROC:";
            // 
            // txtWindowHandle
            // 
            this.txtWindowHandle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWindowHandle.Location = new System.Drawing.Point(196, 3);
            this.txtWindowHandle.Name = "txtWindowHandle";
            this.txtWindowHandle.ReadOnly = true;
            this.txtWindowHandle.Size = new System.Drawing.Size(171, 20);
            this.txtWindowHandle.TabIndex = 1;
            this.txtWindowHandle.TabStop = false;
            this.txtWindowHandle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtGwlStyle
            // 
            this.txtGwlStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGwlStyle.Location = new System.Drawing.Point(536, 3);
            this.txtGwlStyle.Name = "txtGwlStyle";
            this.txtGwlStyle.ReadOnly = true;
            this.txtGwlStyle.Size = new System.Drawing.Size(164, 20);
            this.txtGwlStyle.TabIndex = 3;
            this.txtGwlStyle.TabStop = false;
            this.txtGwlStyle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtParentWindowHandle
            // 
            this.txtParentWindowHandle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParentWindowHandle.Location = new System.Drawing.Point(196, 28);
            this.txtParentWindowHandle.Name = "txtParentWindowHandle";
            this.txtParentWindowHandle.ReadOnly = true;
            this.txtParentWindowHandle.Size = new System.Drawing.Size(171, 20);
            this.txtParentWindowHandle.TabIndex = 5;
            this.txtParentWindowHandle.TabStop = false;
            this.txtParentWindowHandle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtGclStyle
            // 
            this.txtGclStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGclStyle.Location = new System.Drawing.Point(536, 28);
            this.txtGclStyle.Name = "txtGclStyle";
            this.txtGclStyle.ReadOnly = true;
            this.txtGclStyle.Size = new System.Drawing.Size(164, 20);
            this.txtGclStyle.TabIndex = 7;
            this.txtGclStyle.TabStop = false;
            this.txtGclStyle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtWindowSize
            // 
            this.txtWindowSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWindowSize.Location = new System.Drawing.Point(196, 53);
            this.txtWindowSize.Name = "txtWindowSize";
            this.txtWindowSize.ReadOnly = true;
            this.txtWindowSize.Size = new System.Drawing.Size(171, 20);
            this.txtWindowSize.TabIndex = 9;
            this.txtWindowSize.TabStop = false;
            this.txtWindowSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtGwlExStyle
            // 
            this.txtGwlExStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGwlExStyle.Location = new System.Drawing.Point(536, 53);
            this.txtGwlExStyle.Name = "txtGwlExStyle";
            this.txtGwlExStyle.ReadOnly = true;
            this.txtGwlExStyle.Size = new System.Drawing.Size(164, 20);
            this.txtGwlExStyle.TabIndex = 11;
            this.txtGwlExStyle.TabStop = false;
            this.txtGwlExStyle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtInstance
            // 
            this.txtInstance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInstance.Location = new System.Drawing.Point(196, 78);
            this.txtInstance.Name = "txtInstance";
            this.txtInstance.ReadOnly = true;
            this.txtInstance.Size = new System.Drawing.Size(171, 20);
            this.txtInstance.TabIndex = 13;
            this.txtInstance.TabStop = false;
            this.txtInstance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtWindowInfoExStyle
            // 
            this.txtWindowInfoExStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWindowInfoExStyle.Location = new System.Drawing.Point(536, 78);
            this.txtWindowInfoExStyle.Name = "txtWindowInfoExStyle";
            this.txtWindowInfoExStyle.ReadOnly = true;
            this.txtWindowInfoExStyle.Size = new System.Drawing.Size(164, 20);
            this.txtWindowInfoExStyle.TabIndex = 15;
            this.txtWindowInfoExStyle.TabStop = false;
            this.txtWindowInfoExStyle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtProcessId
            // 
            this.txtProcessId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProcessId.Location = new System.Drawing.Point(196, 103);
            this.txtProcessId.Name = "txtProcessId";
            this.txtProcessId.ReadOnly = true;
            this.txtProcessId.Size = new System.Drawing.Size(171, 20);
            this.txtProcessId.TabIndex = 17;
            this.txtProcessId.TabStop = false;
            this.txtProcessId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtLwaAlpha
            // 
            this.txtLwaAlpha.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLwaAlpha.Location = new System.Drawing.Point(536, 103);
            this.txtLwaAlpha.Name = "txtLwaAlpha";
            this.txtLwaAlpha.ReadOnly = true;
            this.txtLwaAlpha.Size = new System.Drawing.Size(164, 20);
            this.txtLwaAlpha.TabIndex = 19;
            this.txtLwaAlpha.TabStop = false;
            this.txtLwaAlpha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtThreadId
            // 
            this.txtThreadId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtThreadId.Location = new System.Drawing.Point(196, 128);
            this.txtThreadId.Name = "txtThreadId";
            this.txtThreadId.ReadOnly = true;
            this.txtThreadId.Size = new System.Drawing.Size(171, 20);
            this.txtThreadId.TabIndex = 21;
            this.txtThreadId.TabStop = false;
            this.txtThreadId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtLwaColorKey
            // 
            this.txtLwaColorKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLwaColorKey.Location = new System.Drawing.Point(536, 128);
            this.txtLwaColorKey.Name = "txtLwaColorKey";
            this.txtLwaColorKey.ReadOnly = true;
            this.txtLwaColorKey.Size = new System.Drawing.Size(164, 20);
            this.txtLwaColorKey.TabIndex = 23;
            this.txtLwaColorKey.TabStop = false;
            this.txtLwaColorKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtGclWndProc
            // 
            this.txtGclWndProc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGclWndProc.Location = new System.Drawing.Point(196, 153);
            this.txtGclWndProc.Name = "txtGclWndProc";
            this.txtGclWndProc.ReadOnly = true;
            this.txtGclWndProc.Size = new System.Drawing.Size(171, 20);
            this.txtGclWndProc.TabIndex = 25;
            this.txtGclWndProc.TabStop = false;
            this.txtGclWndProc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtGwlUserData
            // 
            this.txtGwlUserData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGwlUserData.Location = new System.Drawing.Point(536, 153);
            this.txtGwlUserData.Name = "txtGwlUserData";
            this.txtGwlUserData.ReadOnly = true;
            this.txtGwlUserData.Size = new System.Drawing.Size(164, 20);
            this.txtGwlUserData.TabIndex = 27;
            this.txtGwlUserData.TabStop = false;
            this.txtGwlUserData.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDwlDlgProc
            // 
            this.txtDwlDlgProc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDwlDlgProc.Location = new System.Drawing.Point(196, 178);
            this.txtDwlDlgProc.Name = "txtDwlDlgProc";
            this.txtDwlDlgProc.ReadOnly = true;
            this.txtDwlDlgProc.Size = new System.Drawing.Size(171, 20);
            this.txtDwlDlgProc.TabIndex = 29;
            this.txtDwlDlgProc.TabStop = false;
            this.txtDwlDlgProc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDwlUser
            // 
            this.txtDwlUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDwlUser.Location = new System.Drawing.Point(536, 178);
            this.txtDwlUser.Name = "txtDwlUser";
            this.txtDwlUser.ReadOnly = true;
            this.txtDwlUser.Size = new System.Drawing.Size(164, 20);
            this.txtDwlUser.TabIndex = 31;
            this.txtDwlUser.TabStop = false;
            this.txtDwlUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblWindowHandle
            // 
            this.lblWindowHandle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblWindowHandle.AutoSize = true;
            this.lblWindowHandle.Location = new System.Drawing.Point(104, 6);
            this.lblWindowHandle.Name = "lblWindowHandle";
            this.lblWindowHandle.Size = new System.Drawing.Size(86, 13);
            this.lblWindowHandle.TabIndex = 0;
            this.lblWindowHandle.Text = "Window Handle:";
            // 
            // lblParentWindowHandle
            // 
            this.lblParentWindowHandle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblParentWindowHandle.AutoSize = true;
            this.lblParentWindowHandle.Location = new System.Drawing.Point(70, 31);
            this.lblParentWindowHandle.Name = "lblParentWindowHandle";
            this.lblParentWindowHandle.Size = new System.Drawing.Size(120, 13);
            this.lblParentWindowHandle.TabIndex = 4;
            this.lblParentWindowHandle.Text = "Parent Window Handle:";
            // 
            // lblWindowSize
            // 
            this.lblWindowSize.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblWindowSize.AutoSize = true;
            this.lblWindowSize.Location = new System.Drawing.Point(118, 56);
            this.lblWindowSize.Name = "lblWindowSize";
            this.lblWindowSize.Size = new System.Drawing.Size(72, 13);
            this.lblWindowSize.TabIndex = 8;
            this.lblWindowSize.Text = "Window Size:";
            // 
            // lblInstance
            // 
            this.lblInstance.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblInstance.AutoSize = true;
            this.lblInstance.Location = new System.Drawing.Point(139, 81);
            this.lblInstance.Name = "lblInstance";
            this.lblInstance.Size = new System.Drawing.Size(51, 13);
            this.lblInstance.TabIndex = 12;
            this.lblInstance.Text = "Instance:";
            // 
            // lblProcessId
            // 
            this.lblProcessId.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblProcessId.AutoSize = true;
            this.lblProcessId.Location = new System.Drawing.Point(131, 106);
            this.lblProcessId.Name = "lblProcessId";
            this.lblProcessId.Size = new System.Drawing.Size(59, 13);
            this.lblProcessId.TabIndex = 16;
            this.lblProcessId.Text = "ProcessID:";
            // 
            // lblThreadId
            // 
            this.lblThreadId.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblThreadId.AutoSize = true;
            this.lblThreadId.Location = new System.Drawing.Point(135, 131);
            this.lblThreadId.Name = "lblThreadId";
            this.lblThreadId.Size = new System.Drawing.Size(55, 13);
            this.lblThreadId.TabIndex = 20;
            this.lblThreadId.Text = "ThreadID:";
            // 
            // lblGclWndProc
            // 
            this.lblGclWndProc.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblGclWndProc.AutoSize = true;
            this.lblGclWndProc.Location = new System.Drawing.Point(96, 156);
            this.lblGclWndProc.Name = "lblGclWndProc";
            this.lblGclWndProc.Size = new System.Drawing.Size(94, 13);
            this.lblGclWndProc.TabIndex = 24;
            this.lblGclWndProc.Text = "GCL_WNDPROC:";
            // 
            // lblGwlStyle
            // 
            this.lblGwlStyle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblGwlStyle.AutoSize = true;
            this.lblGwlStyle.Location = new System.Drawing.Point(455, 6);
            this.lblGwlStyle.Name = "lblGwlStyle";
            this.lblGwlStyle.Size = new System.Drawing.Size(75, 13);
            this.lblGwlStyle.TabIndex = 2;
            this.lblGwlStyle.Text = "GWL_STYLE:";
            // 
            // lblGclStyle
            // 
            this.lblGclStyle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblGclStyle.AutoSize = true;
            this.lblGclStyle.Location = new System.Drawing.Point(459, 31);
            this.lblGclStyle.Name = "lblGclStyle";
            this.lblGclStyle.Size = new System.Drawing.Size(71, 13);
            this.lblGclStyle.TabIndex = 6;
            this.lblGclStyle.Text = "GCL_STYLE:";
            // 
            // lblGwlExStyle
            // 
            this.lblGwlExStyle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblGwlExStyle.AutoSize = true;
            this.lblGwlExStyle.Location = new System.Drawing.Point(441, 56);
            this.lblGwlExStyle.Name = "lblGwlExStyle";
            this.lblGwlExStyle.Size = new System.Drawing.Size(89, 13);
            this.lblGwlExStyle.TabIndex = 10;
            this.lblGwlExStyle.Text = "GWL_EXSTYLE:";
            // 
            // lblWindowInfoExStyle
            // 
            this.lblWindowInfoExStyle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblWindowInfoExStyle.AutoSize = true;
            this.lblWindowInfoExStyle.Location = new System.Drawing.Point(425, 81);
            this.lblWindowInfoExStyle.Name = "lblWindowInfoExStyle";
            this.lblWindowInfoExStyle.Size = new System.Drawing.Size(105, 13);
            this.lblWindowInfoExStyle.TabIndex = 14;
            this.lblWindowInfoExStyle.Text = "WindowInfo.ExStyle:";
            // 
            // lblLwaAlpha
            // 
            this.lblLwaAlpha.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLwaAlpha.AutoSize = true;
            this.lblLwaAlpha.Location = new System.Drawing.Point(455, 106);
            this.lblLwaAlpha.Name = "lblLwaAlpha";
            this.lblLwaAlpha.Size = new System.Drawing.Size(75, 13);
            this.lblLwaAlpha.TabIndex = 18;
            this.lblLwaAlpha.Text = "LWA_ALPHA:";
            // 
            // lblLwaColorKey
            // 
            this.lblLwaColorKey.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLwaColorKey.AutoSize = true;
            this.lblLwaColorKey.Location = new System.Drawing.Point(432, 131);
            this.lblLwaColorKey.Name = "lblLwaColorKey";
            this.lblLwaColorKey.Size = new System.Drawing.Size(98, 13);
            this.lblLwaColorKey.TabIndex = 22;
            this.lblLwaColorKey.Text = "LWA_COLORKEY:";
            // 
            // lblGwlUserData
            // 
            this.lblGwlUserData.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblGwlUserData.AutoSize = true;
            this.lblGwlUserData.Location = new System.Drawing.Point(430, 156);
            this.lblGwlUserData.Name = "lblGwlUserData";
            this.lblGwlUserData.Size = new System.Drawing.Size(100, 13);
            this.lblGwlUserData.TabIndex = 26;
            this.lblGwlUserData.Text = "GWL_USERDATA:";
            // 
            // lblDwlUser
            // 
            this.lblDwlUser.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDwlUser.AutoSize = true;
            this.lblDwlUser.Location = new System.Drawing.Point(459, 183);
            this.lblDwlUser.Name = "lblDwlUser";
            this.lblDwlUser.Size = new System.Drawing.Size(71, 13);
            this.lblDwlUser.TabIndex = 30;
            this.lblDwlUser.Text = "DWL_USER:";
            // 
            // tblWindowTop
            // 
            this.tblWindowTop.ColumnCount = 2;
            this.tblWindowTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.45377F));
            this.tblWindowTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.54623F));
            this.tblWindowTop.Controls.Add(this.lblFontFace, 0, 4);
            this.tblWindowTop.Controls.Add(this.lblRealGetWindowClass, 0, 3);
            this.tblWindowTop.Controls.Add(this.lblGetClassName, 0, 2);
            this.tblWindowTop.Controls.Add(this.lblWmGetText, 0, 1);
            this.tblWindowTop.Controls.Add(this.lblGetWindowText, 0, 0);
            this.tblWindowTop.Controls.Add(this.txtGetWindowText, 1, 0);
            this.tblWindowTop.Controls.Add(this.txtWmGetText, 1, 1);
            this.tblWindowTop.Controls.Add(this.txtGetClassName, 1, 2);
            this.tblWindowTop.Controls.Add(this.txtRealGetWindowClass, 1, 3);
            this.tblWindowTop.Controls.Add(this.txtFontFace, 1, 4);
            this.tblWindowTop.Location = new System.Drawing.Point(6, 16);
            this.tblWindowTop.Name = "tblWindowTop";
            this.tblWindowTop.RowCount = 5;
            this.tblWindowTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblWindowTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblWindowTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblWindowTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblWindowTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblWindowTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblWindowTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblWindowTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblWindowTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblWindowTop.Size = new System.Drawing.Size(703, 126);
            this.tblWindowTop.TabIndex = 0;
            // 
            // lblFontFace
            // 
            this.lblFontFace.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblFontFace.AutoSize = true;
            this.lblFontFace.Location = new System.Drawing.Point(132, 106);
            this.lblFontFace.Name = "lblFontFace";
            this.lblFontFace.Size = new System.Drawing.Size(58, 13);
            this.lblFontFace.TabIndex = 8;
            this.lblFontFace.Text = "Font Face:";
            // 
            // lblRealGetWindowClass
            // 
            this.lblRealGetWindowClass.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRealGetWindowClass.AutoSize = true;
            this.lblRealGetWindowClass.Location = new System.Drawing.Point(77, 81);
            this.lblRealGetWindowClass.Name = "lblRealGetWindowClass";
            this.lblRealGetWindowClass.Size = new System.Drawing.Size(113, 13);
            this.lblRealGetWindowClass.TabIndex = 6;
            this.lblRealGetWindowClass.Text = "RealGetWindowClass:";
            // 
            // lblGetClassName
            // 
            this.lblGetClassName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblGetClassName.AutoSize = true;
            this.lblGetClassName.Location = new System.Drawing.Point(110, 56);
            this.lblGetClassName.Name = "lblGetClassName";
            this.lblGetClassName.Size = new System.Drawing.Size(80, 13);
            this.lblGetClassName.TabIndex = 4;
            this.lblGetClassName.Text = "GetClassName:";
            // 
            // lblWmGetText
            // 
            this.lblWmGetText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblWmGetText.AutoSize = true;
            this.lblWmGetText.Location = new System.Drawing.Point(104, 31);
            this.lblWmGetText.Name = "lblWmGetText";
            this.lblWmGetText.Size = new System.Drawing.Size(86, 13);
            this.lblWmGetText.TabIndex = 2;
            this.lblWmGetText.Text = "WM_GETTEXT:";
            // 
            // lblGetWindowText
            // 
            this.lblGetWindowText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblGetWindowText.AutoSize = true;
            this.lblGetWindowText.Location = new System.Drawing.Point(103, 6);
            this.lblGetWindowText.Name = "lblGetWindowText";
            this.lblGetWindowText.Size = new System.Drawing.Size(87, 13);
            this.lblGetWindowText.TabIndex = 0;
            this.lblGetWindowText.Text = "GetWindowText:";
            // 
            // txtGetWindowText
            // 
            this.txtGetWindowText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGetWindowText.Location = new System.Drawing.Point(196, 3);
            this.txtGetWindowText.Name = "txtGetWindowText";
            this.txtGetWindowText.ReadOnly = true;
            this.txtGetWindowText.Size = new System.Drawing.Size(504, 20);
            this.txtGetWindowText.TabIndex = 1;
            this.txtGetWindowText.TabStop = false;
            // 
            // txtWmGetText
            // 
            this.txtWmGetText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWmGetText.Location = new System.Drawing.Point(196, 28);
            this.txtWmGetText.Name = "txtWmGetText";
            this.txtWmGetText.ReadOnly = true;
            this.txtWmGetText.Size = new System.Drawing.Size(504, 20);
            this.txtWmGetText.TabIndex = 3;
            this.txtWmGetText.TabStop = false;
            // 
            // txtGetClassName
            // 
            this.txtGetClassName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGetClassName.Location = new System.Drawing.Point(196, 53);
            this.txtGetClassName.Name = "txtGetClassName";
            this.txtGetClassName.ReadOnly = true;
            this.txtGetClassName.Size = new System.Drawing.Size(504, 20);
            this.txtGetClassName.TabIndex = 5;
            this.txtGetClassName.TabStop = false;
            // 
            // txtRealGetWindowClass
            // 
            this.txtRealGetWindowClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRealGetWindowClass.Location = new System.Drawing.Point(196, 78);
            this.txtRealGetWindowClass.Name = "txtRealGetWindowClass";
            this.txtRealGetWindowClass.ReadOnly = true;
            this.txtRealGetWindowClass.Size = new System.Drawing.Size(504, 20);
            this.txtRealGetWindowClass.TabIndex = 7;
            this.txtRealGetWindowClass.TabStop = false;
            // 
            // txtFontFace
            // 
            this.txtFontFace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFontFace.Location = new System.Drawing.Point(196, 103);
            this.txtFontFace.Name = "txtFontFace";
            this.txtFontFace.ReadOnly = true;
            this.txtFontFace.Size = new System.Drawing.Size(504, 20);
            this.txtFontFace.TabIndex = 9;
            this.txtFontFace.TabStop = false;
            // 
            // grpProcess
            // 
            this.grpProcess.Controls.Add(this.tableLayoutPanel2);
            this.grpProcess.Controls.Add(this.tableLayoutPanel1);
            this.grpProcess.Controls.Add(this.tblProcessBottom);
            this.grpProcess.Controls.Add(this.tblProcessTop);
            this.grpProcess.Location = new System.Drawing.Point(7, 362);
            this.grpProcess.Name = "grpProcess";
            this.grpProcess.Size = new System.Drawing.Size(715, 264);
            this.grpProcess.TabIndex = 1;
            this.grpProcess.TabStop = false;
            this.grpProcess.Text = "Process";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.73826F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.00142F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.93599F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.60882F));
            this.tableLayoutPanel2.Controls.Add(this.txtFileVersion, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblFileVersion, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblProductVersion, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtProductVersion, 3, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 229);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(703, 27);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // txtFileVersion
            // 
            this.txtFileVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileVersion.Location = new System.Drawing.Point(197, 3);
            this.txtFileVersion.Name = "txtFileVersion";
            this.txtFileVersion.ReadOnly = true;
            this.txtFileVersion.Size = new System.Drawing.Size(225, 20);
            this.txtFileVersion.TabIndex = 1;
            this.txtFileVersion.TabStop = false;
            // 
            // lblFileVersion
            // 
            this.lblFileVersion.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblFileVersion.AutoSize = true;
            this.lblFileVersion.Location = new System.Drawing.Point(127, 7);
            this.lblFileVersion.Name = "lblFileVersion";
            this.lblFileVersion.Size = new System.Drawing.Size(64, 13);
            this.lblFileVersion.TabIndex = 0;
            this.lblFileVersion.Text = "File Version:";
            // 
            // lblProductVersion
            // 
            this.lblProductVersion.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblProductVersion.AutoSize = true;
            this.lblProductVersion.Location = new System.Drawing.Point(441, 7);
            this.lblProductVersion.Name = "lblProductVersion";
            this.lblProductVersion.Size = new System.Drawing.Size(85, 13);
            this.lblProductVersion.TabIndex = 2;
            this.lblProductVersion.Text = "Product Version:";
            // 
            // txtProductVersion
            // 
            this.txtProductVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProductVersion.Location = new System.Drawing.Point(532, 3);
            this.txtProductVersion.Name = "txtProductVersion";
            this.txtProductVersion.ReadOnly = true;
            this.txtProductVersion.Size = new System.Drawing.Size(168, 20);
            this.txtProductVersion.TabIndex = 3;
            this.txtProductVersion.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.59602F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.40398F));
            this.tableLayoutPanel1.Controls.Add(this.lblProductName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtProductName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtCopyright, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCopyright, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 176);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(703, 53);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // lblCopyright
            // 
            this.lblCopyright.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Location = new System.Drawing.Point(137, 32);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(54, 13);
            this.lblCopyright.TabIndex = 2;
            this.lblCopyright.Text = "Copyright:";
            // 
            // lblProductName
            // 
            this.lblProductName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblProductName.AutoSize = true;
            this.lblProductName.Location = new System.Drawing.Point(113, 6);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(78, 13);
            this.lblProductName.TabIndex = 0;
            this.lblProductName.Text = "Product Name:";
            // 
            // txtProductName
            // 
            this.txtProductName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProductName.Location = new System.Drawing.Point(197, 3);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.ReadOnly = true;
            this.txtProductName.Size = new System.Drawing.Size(503, 20);
            this.txtProductName.TabIndex = 1;
            this.txtProductName.TabStop = false;
            // 
            // txtCopyright
            // 
            this.txtCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCopyright.Location = new System.Drawing.Point(197, 28);
            this.txtCopyright.Name = "txtCopyright";
            this.txtCopyright.ReadOnly = true;
            this.txtCopyright.Size = new System.Drawing.Size(503, 20);
            this.txtCopyright.TabIndex = 3;
            this.txtCopyright.TabStop = false;
            // 
            // tblProcessBottom
            // 
            this.tblProcessBottom.ColumnCount = 4;
            this.tblProcessBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.59602F));
            this.tblProcessBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.71693F));
            this.tblProcessBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.07824F));
            this.tblProcessBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.60882F));
            this.tblProcessBottom.Controls.Add(this.txtWorkingSetSize, 0, 3);
            this.tblProcessBottom.Controls.Add(this.lblWorkingSetSize, 0, 3);
            this.tblProcessBottom.Controls.Add(this.lblStartedAt, 0, 0);
            this.tblProcessBottom.Controls.Add(this.lblParent, 2, 0);
            this.tblProcessBottom.Controls.Add(this.lblPriority, 2, 1);
            this.tblProcessBottom.Controls.Add(this.txtStartedAt, 1, 0);
            this.tblProcessBottom.Controls.Add(this.txtParent, 3, 0);
            this.tblProcessBottom.Controls.Add(this.txtPriority, 3, 1);
            this.tblProcessBottom.Controls.Add(this.lblThreads, 0, 2);
            this.tblProcessBottom.Controls.Add(this.txtOwner, 1, 1);
            this.tblProcessBottom.Controls.Add(this.lblOwner, 0, 1);
            this.tblProcessBottom.Controls.Add(this.lblHandles, 2, 2);
            this.tblProcessBottom.Controls.Add(this.lblVirtualSize, 2, 3);
            this.tblProcessBottom.Controls.Add(this.txtThreads, 1, 2);
            this.tblProcessBottom.Controls.Add(this.txtHandles, 3, 2);
            this.tblProcessBottom.Controls.Add(this.txtVirtualSize, 3, 3);
            this.tblProcessBottom.Location = new System.Drawing.Point(6, 72);
            this.tblProcessBottom.Name = "tblProcessBottom";
            this.tblProcessBottom.RowCount = 4;
            this.tblProcessBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblProcessBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblProcessBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblProcessBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblProcessBottom.Size = new System.Drawing.Size(703, 104);
            this.tblProcessBottom.TabIndex = 1;
            // 
            // txtOwner
            // 
            this.txtOwner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOwner.Location = new System.Drawing.Point(197, 28);
            this.txtOwner.Name = "txtOwner";
            this.txtOwner.ReadOnly = true;
            this.txtOwner.Size = new System.Drawing.Size(223, 20);
            this.txtOwner.TabIndex = 5;
            this.txtOwner.TabStop = false;
            // 
            // lblOwner
            // 
            this.lblOwner.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblOwner.AutoSize = true;
            this.lblOwner.Location = new System.Drawing.Point(150, 31);
            this.lblOwner.Name = "lblOwner";
            this.lblOwner.Size = new System.Drawing.Size(41, 13);
            this.lblOwner.TabIndex = 4;
            this.lblOwner.Text = "Owner:";
            // 
            // lblStartedAt
            // 
            this.lblStartedAt.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblStartedAt.AutoSize = true;
            this.lblStartedAt.Location = new System.Drawing.Point(135, 6);
            this.lblStartedAt.Name = "lblStartedAt";
            this.lblStartedAt.Size = new System.Drawing.Size(56, 13);
            this.lblStartedAt.TabIndex = 0;
            this.lblStartedAt.Text = "Started at:";
            // 
            // lblParent
            // 
            this.lblParent.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblParent.AutoSize = true;
            this.lblParent.Location = new System.Drawing.Point(485, 6);
            this.lblParent.Name = "lblParent";
            this.lblParent.Size = new System.Drawing.Size(41, 13);
            this.lblParent.TabIndex = 2;
            this.lblParent.Text = "Parent:";
            // 
            // lblPriority
            // 
            this.lblPriority.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPriority.AutoSize = true;
            this.lblPriority.Location = new System.Drawing.Point(485, 31);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(41, 13);
            this.lblPriority.TabIndex = 6;
            this.lblPriority.Text = "Priority:";
            // 
            // txtStartedAt
            // 
            this.txtStartedAt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStartedAt.Location = new System.Drawing.Point(197, 3);
            this.txtStartedAt.Name = "txtStartedAt";
            this.txtStartedAt.ReadOnly = true;
            this.txtStartedAt.Size = new System.Drawing.Size(223, 20);
            this.txtStartedAt.TabIndex = 1;
            this.txtStartedAt.TabStop = false;
            // 
            // txtParent
            // 
            this.txtParent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParent.Location = new System.Drawing.Point(532, 3);
            this.txtParent.Name = "txtParent";
            this.txtParent.ReadOnly = true;
            this.txtParent.Size = new System.Drawing.Size(168, 20);
            this.txtParent.TabIndex = 3;
            this.txtParent.TabStop = false;
            // 
            // txtPriority
            // 
            this.txtPriority.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPriority.Location = new System.Drawing.Point(532, 28);
            this.txtPriority.Name = "txtPriority";
            this.txtPriority.ReadOnly = true;
            this.txtPriority.Size = new System.Drawing.Size(168, 20);
            this.txtPriority.TabIndex = 7;
            this.txtPriority.TabStop = false;
            // 
            // tblProcessTop
            // 
            this.tblProcessTop.ColumnCount = 2;
            this.tblProcessTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.71428F));
            this.tblProcessTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.28572F));
            this.tblProcessTop.Controls.Add(this.lblFullPath, 0, 0);
            this.tblProcessTop.Controls.Add(this.lblCommandLine, 0, 1);
            this.tblProcessTop.Controls.Add(this.txtFullPath, 1, 0);
            this.tblProcessTop.Controls.Add(this.txtCommandLine, 1, 1);
            this.tblProcessTop.Location = new System.Drawing.Point(6, 19);
            this.tblProcessTop.Name = "tblProcessTop";
            this.tblProcessTop.RowCount = 2;
            this.tblProcessTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblProcessTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblProcessTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblProcessTop.Size = new System.Drawing.Size(703, 53);
            this.tblProcessTop.TabIndex = 0;
            // 
            // lblFullPath
            // 
            this.lblFullPath.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblFullPath.AutoSize = true;
            this.lblFullPath.Location = new System.Drawing.Point(140, 6);
            this.lblFullPath.Name = "lblFullPath";
            this.lblFullPath.Size = new System.Drawing.Size(51, 13);
            this.lblFullPath.TabIndex = 0;
            this.lblFullPath.Text = "Full Path:";
            // 
            // lblCommandLine
            // 
            this.lblCommandLine.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCommandLine.AutoSize = true;
            this.lblCommandLine.Location = new System.Drawing.Point(111, 32);
            this.lblCommandLine.Name = "lblCommandLine";
            this.lblCommandLine.Size = new System.Drawing.Size(80, 13);
            this.lblCommandLine.TabIndex = 2;
            this.lblCommandLine.Text = "Command Line:";
            // 
            // txtFullPath
            // 
            this.txtFullPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFullPath.Location = new System.Drawing.Point(197, 3);
            this.txtFullPath.Name = "txtFullPath";
            this.txtFullPath.ReadOnly = true;
            this.txtFullPath.Size = new System.Drawing.Size(503, 20);
            this.txtFullPath.TabIndex = 1;
            this.txtFullPath.TabStop = false;
            // 
            // txtCommandLine
            // 
            this.txtCommandLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommandLine.Location = new System.Drawing.Point(197, 28);
            this.txtCommandLine.Name = "txtCommandLine";
            this.txtCommandLine.ReadOnly = true;
            this.txtCommandLine.Size = new System.Drawing.Size(503, 20);
            this.txtCommandLine.TabIndex = 3;
            this.txtCommandLine.TabStop = false;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(599, 642);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(114, 33);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.CloseClick);
            // 
            // lblThreads
            // 
            this.lblThreads.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblThreads.AutoSize = true;
            this.lblThreads.Location = new System.Drawing.Point(142, 56);
            this.lblThreads.Name = "lblThreads";
            this.lblThreads.Size = new System.Drawing.Size(49, 13);
            this.lblThreads.TabIndex = 8;
            this.lblThreads.Text = "Threads:";
            // 
            // lblHandles
            // 
            this.lblHandles.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblHandles.AutoSize = true;
            this.lblHandles.Location = new System.Drawing.Point(477, 56);
            this.lblHandles.Name = "lblHandles";
            this.lblHandles.Size = new System.Drawing.Size(49, 13);
            this.lblHandles.TabIndex = 10;
            this.lblHandles.Text = "Handles:";
            // 
            // lblVirtualSize
            // 
            this.lblVirtualSize.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblVirtualSize.AutoSize = true;
            this.lblVirtualSize.Location = new System.Drawing.Point(464, 83);
            this.lblVirtualSize.Name = "lblVirtualSize";
            this.lblVirtualSize.Size = new System.Drawing.Size(62, 13);
            this.lblVirtualSize.TabIndex = 14;
            this.lblVirtualSize.Text = "Virtual Size:";
            // 
            // lblWorkingSetSize
            // 
            this.lblWorkingSetSize.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblWorkingSetSize.AutoSize = true;
            this.lblWorkingSetSize.Location = new System.Drawing.Point(99, 83);
            this.lblWorkingSetSize.Name = "lblWorkingSetSize";
            this.lblWorkingSetSize.Size = new System.Drawing.Size(92, 13);
            this.lblWorkingSetSize.TabIndex = 12;
            this.lblWorkingSetSize.Text = "Working Set Size:";
            // 
            // txtHandles
            // 
            this.txtHandles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHandles.Location = new System.Drawing.Point(532, 53);
            this.txtHandles.Name = "txtHandles";
            this.txtHandles.ReadOnly = true;
            this.txtHandles.Size = new System.Drawing.Size(168, 20);
            this.txtHandles.TabIndex = 11;
            this.txtHandles.TabStop = false;
            // 
            // txtThreads
            // 
            this.txtThreads.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtThreads.Location = new System.Drawing.Point(197, 53);
            this.txtThreads.Name = "txtThreads";
            this.txtThreads.ReadOnly = true;
            this.txtThreads.Size = new System.Drawing.Size(223, 20);
            this.txtThreads.TabIndex = 9;
            this.txtThreads.TabStop = false;
            // 
            // txtWorkingSetSize
            // 
            this.txtWorkingSetSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWorkingSetSize.Location = new System.Drawing.Point(197, 78);
            this.txtWorkingSetSize.Name = "txtWorkingSetSize";
            this.txtWorkingSetSize.ReadOnly = true;
            this.txtWorkingSetSize.Size = new System.Drawing.Size(223, 20);
            this.txtWorkingSetSize.TabIndex = 13;
            this.txtWorkingSetSize.TabStop = false;
            // 
            // txtVirtualSize
            // 
            this.txtVirtualSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVirtualSize.Location = new System.Drawing.Point(532, 78);
            this.txtVirtualSize.Name = "txtVirtualSize";
            this.txtVirtualSize.ReadOnly = true;
            this.txtVirtualSize.Size = new System.Drawing.Size(168, 20);
            this.txtVirtualSize.TabIndex = 15;
            this.txtVirtualSize.TabStop = false;
            // 
            // InfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 686);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grpProcess);
            this.Controls.Add(this.grpWindow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
            this.grpWindow.ResumeLayout(false);
            this.tblWindowBottom.ResumeLayout(false);
            this.tblWindowBottom.PerformLayout();
            this.tblWindowTop.ResumeLayout(false);
            this.tblWindowTop.PerformLayout();
            this.grpProcess.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tblProcessBottom.ResumeLayout(false);
            this.tblProcessBottom.PerformLayout();
            this.tblProcessTop.ResumeLayout(false);
            this.tblProcessTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpWindow;
        private System.Windows.Forms.TableLayoutPanel tblWindowBottom;
        private System.Windows.Forms.Label lblDwlDlgProc;
        private System.Windows.Forms.TextBox txtWindowHandle;
        private System.Windows.Forms.TextBox txtGwlStyle;
        private System.Windows.Forms.TextBox txtParentWindowHandle;
        private System.Windows.Forms.TextBox txtGclStyle;
        private System.Windows.Forms.TextBox txtWindowSize;
        private System.Windows.Forms.TextBox txtGwlExStyle;
        private System.Windows.Forms.TextBox txtInstance;
        private System.Windows.Forms.TextBox txtWindowInfoExStyle;
        private System.Windows.Forms.TextBox txtProcessId;
        private System.Windows.Forms.TextBox txtLwaAlpha;
        private System.Windows.Forms.TextBox txtThreadId;
        private System.Windows.Forms.TextBox txtLwaColorKey;
        private System.Windows.Forms.TextBox txtGclWndProc;
        private System.Windows.Forms.TextBox txtGwlUserData;
        private System.Windows.Forms.TextBox txtDwlDlgProc;
        private System.Windows.Forms.TextBox txtDwlUser;
        private System.Windows.Forms.Label lblWindowHandle;
        private System.Windows.Forms.Label lblParentWindowHandle;
        private System.Windows.Forms.Label lblWindowSize;
        private System.Windows.Forms.Label lblInstance;
        private System.Windows.Forms.Label lblProcessId;
        private System.Windows.Forms.Label lblThreadId;
        private System.Windows.Forms.Label lblGclWndProc;
        private System.Windows.Forms.Label lblGwlStyle;
        private System.Windows.Forms.Label lblGclStyle;
        private System.Windows.Forms.Label lblGwlExStyle;
        private System.Windows.Forms.Label lblWindowInfoExStyle;
        private System.Windows.Forms.Label lblLwaAlpha;
        private System.Windows.Forms.Label lblLwaColorKey;
        private System.Windows.Forms.Label lblGwlUserData;
        private System.Windows.Forms.Label lblDwlUser;
        private System.Windows.Forms.TableLayoutPanel tblWindowTop;
        private System.Windows.Forms.Label lblFontFace;
        private System.Windows.Forms.Label lblRealGetWindowClass;
        private System.Windows.Forms.Label lblGetClassName;
        private System.Windows.Forms.Label lblWmGetText;
        private System.Windows.Forms.Label lblGetWindowText;
        private System.Windows.Forms.TextBox txtGetWindowText;
        private System.Windows.Forms.TextBox txtWmGetText;
        private System.Windows.Forms.TextBox txtGetClassName;
        private System.Windows.Forms.TextBox txtRealGetWindowClass;
        private System.Windows.Forms.TextBox txtFontFace;
        private System.Windows.Forms.GroupBox grpProcess;
        private System.Windows.Forms.TableLayoutPanel tblProcessTop;
        private System.Windows.Forms.Label lblFullPath;
        private System.Windows.Forms.Label lblCommandLine;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtFileVersion;
        private System.Windows.Forms.Label lblFileVersion;
        private System.Windows.Forms.Label lblProductVersion;
        private System.Windows.Forms.TextBox txtProductVersion;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.TextBox txtCopyright;
        private System.Windows.Forms.TableLayoutPanel tblProcessBottom;
        private System.Windows.Forms.TextBox txtOwner;
        private System.Windows.Forms.Label lblOwner;
        private System.Windows.Forms.Label lblStartedAt;
        private System.Windows.Forms.Label lblParent;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.TextBox txtStartedAt;
        private System.Windows.Forms.TextBox txtParent;
        private System.Windows.Forms.TextBox txtPriority;
        private System.Windows.Forms.TextBox txtFullPath;
        private System.Windows.Forms.TextBox txtCommandLine;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtWorkingSetSize;
        private System.Windows.Forms.Label lblWorkingSetSize;
        private System.Windows.Forms.Label lblThreads;
        private System.Windows.Forms.Label lblHandles;
        private System.Windows.Forms.Label lblVirtualSize;
        private System.Windows.Forms.TextBox txtThreads;
        private System.Windows.Forms.TextBox txtHandles;
        private System.Windows.Forms.TextBox txtVirtualSize;
    }
}