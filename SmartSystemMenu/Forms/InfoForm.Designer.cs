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
            this.grpProcess = new System.Windows.Forms.GroupBox();
            this.tblWindowTop = new System.Windows.Forms.TableLayoutPanel();
            this.txtGetWindowText = new System.Windows.Forms.TextBox();
            this.txtWmGetText = new System.Windows.Forms.TextBox();
            this.txtGetClassName = new System.Windows.Forms.TextBox();
            this.txtRealGetWindowClass = new System.Windows.Forms.TextBox();
            this.txtFontFace = new System.Windows.Forms.TextBox();
            this.txtAccessibleName = new System.Windows.Forms.TextBox();
            this.txtAccessibleValue = new System.Windows.Forms.TextBox();
            this.txtAccessibleRole = new System.Windows.Forms.TextBox();
            this.lblGetWindowText = new System.Windows.Forms.Label();
            this.lblWmGetText = new System.Windows.Forms.Label();
            this.lblGetClassName = new System.Windows.Forms.Label();
            this.lblRealGetWindowClass = new System.Windows.Forms.Label();
            this.lblFontFace = new System.Windows.Forms.Label();
            this.lblAccessibleName = new System.Windows.Forms.Label();
            this.lblAccessibleValue = new System.Windows.Forms.Label();
            this.lblAccessibleRole = new System.Windows.Forms.Label();
            this.txtAccessibleDescription = new System.Windows.Forms.TextBox();
            this.lblAccessibleDescription = new System.Windows.Forms.Label();
            this.tblWindowBottom = new System.Windows.Forms.TableLayoutPanel();
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
            this.lblDwlDlgProc = new System.Windows.Forms.Label();
            this.lblGwlStyle = new System.Windows.Forms.Label();
            this.lblGclStyle = new System.Windows.Forms.Label();
            this.lblGwlExStyle = new System.Windows.Forms.Label();
            this.lblWindowInfoExStyle = new System.Windows.Forms.Label();
            this.lblLwaAlpha = new System.Windows.Forms.Label();
            this.lblLwaColorKey = new System.Windows.Forms.Label();
            this.lblGwlUserData = new System.Windows.Forms.Label();
            this.lblDwlUser = new System.Windows.Forms.Label();
            this.tblProcessTop = new System.Windows.Forms.TableLayoutPanel();
            this.lblFullPath = new System.Windows.Forms.Label();
            this.lblCommandLine = new System.Windows.Forms.Label();
            this.lblWorkingDirectory = new System.Windows.Forms.Label();
            this.txtFullPath = new System.Windows.Forms.TextBox();
            this.txtCommandLine = new System.Windows.Forms.TextBox();
            this.txtWorkingDirectory = new System.Windows.Forms.TextBox();
            this.tblProcessBottom = new System.Windows.Forms.TableLayoutPanel();
            this.lblStartedAt = new System.Windows.Forms.Label();
            this.lblParent = new System.Windows.Forms.Label();
            this.lblPriority = new System.Windows.Forms.Label();
            this.lblOwner = new System.Windows.Forms.Label();
            this.txtStartedAt = new System.Windows.Forms.TextBox();
            this.txtParent = new System.Windows.Forms.TextBox();
            this.txtPriority = new System.Windows.Forms.TextBox();
            this.txtOwner = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtCopyright = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblFileVersion = new System.Windows.Forms.Label();
            this.lblProductVersion = new System.Windows.Forms.Label();
            this.txtFileVersion = new System.Windows.Forms.TextBox();
            this.txtProductVersion = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.grpWindow.SuspendLayout();
            this.grpProcess.SuspendLayout();
            this.tblWindowTop.SuspendLayout();
            this.tblWindowBottom.SuspendLayout();
            this.tblProcessTop.SuspendLayout();
            this.tblProcessBottom.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpWindow
            // 
            this.grpWindow.Controls.Add(this.tblWindowBottom);
            this.grpWindow.Controls.Add(this.tblWindowTop);
            this.grpWindow.Location = new System.Drawing.Point(7, 2);
            this.grpWindow.Name = "grpWindow";
            this.grpWindow.Size = new System.Drawing.Size(715, 456);
            this.grpWindow.TabIndex = 0;
            this.grpWindow.TabStop = false;
            this.grpWindow.Text = "Window";
            // 
            // grpProcess
            // 
            this.grpProcess.Controls.Add(this.tableLayoutPanel2);
            this.grpProcess.Controls.Add(this.tableLayoutPanel1);
            this.grpProcess.Controls.Add(this.tblProcessBottom);
            this.grpProcess.Controls.Add(this.tblProcessTop);
            this.grpProcess.Location = new System.Drawing.Point(7, 464);
            this.grpProcess.Name = "grpProcess";
            this.grpProcess.Size = new System.Drawing.Size(715, 265);
            this.grpProcess.TabIndex = 1;
            this.grpProcess.TabStop = false;
            this.grpProcess.Text = "Process";
            // 
            // tblWindowTop
            // 
            this.tblWindowTop.ColumnCount = 2;
            this.tblWindowTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.45377F));
            this.tblWindowTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.54623F));
            this.tblWindowTop.Controls.Add(this.lblAccessibleDescription, 0, 8);
            this.tblWindowTop.Controls.Add(this.lblAccessibleRole, 0, 7);
            this.tblWindowTop.Controls.Add(this.lblAccessibleValue, 0, 6);
            this.tblWindowTop.Controls.Add(this.lblAccessibleName, 0, 5);
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
            this.tblWindowTop.Controls.Add(this.txtAccessibleName, 1, 5);
            this.tblWindowTop.Controls.Add(this.txtAccessibleValue, 1, 6);
            this.tblWindowTop.Controls.Add(this.txtAccessibleRole, 1, 7);
            this.tblWindowTop.Controls.Add(this.txtAccessibleDescription, 1, 8);
            this.tblWindowTop.Location = new System.Drawing.Point(6, 16);
            this.tblWindowTop.Name = "tblWindowTop";
            this.tblWindowTop.RowCount = 9;
            this.tblWindowTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblWindowTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblWindowTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblWindowTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblWindowTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblWindowTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblWindowTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblWindowTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblWindowTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblWindowTop.Size = new System.Drawing.Size(703, 230);
            this.tblWindowTop.TabIndex = 0;
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
            // 
            // txtAccessibleName
            // 
            this.txtAccessibleName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAccessibleName.Location = new System.Drawing.Point(196, 128);
            this.txtAccessibleName.Name = "txtAccessibleName";
            this.txtAccessibleName.ReadOnly = true;
            this.txtAccessibleName.Size = new System.Drawing.Size(504, 20);
            this.txtAccessibleName.TabIndex = 11;
            // 
            // txtAccessibleValue
            // 
            this.txtAccessibleValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAccessibleValue.Location = new System.Drawing.Point(196, 153);
            this.txtAccessibleValue.Name = "txtAccessibleValue";
            this.txtAccessibleValue.ReadOnly = true;
            this.txtAccessibleValue.Size = new System.Drawing.Size(504, 20);
            this.txtAccessibleValue.TabIndex = 13;
            // 
            // txtAccessibleRole
            // 
            this.txtAccessibleRole.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAccessibleRole.Location = new System.Drawing.Point(196, 178);
            this.txtAccessibleRole.Name = "txtAccessibleRole";
            this.txtAccessibleRole.ReadOnly = true;
            this.txtAccessibleRole.Size = new System.Drawing.Size(504, 20);
            this.txtAccessibleRole.TabIndex = 15;
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
            // lblAccessibleName
            // 
            this.lblAccessibleName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblAccessibleName.AutoSize = true;
            this.lblAccessibleName.Location = new System.Drawing.Point(98, 131);
            this.lblAccessibleName.Name = "lblAccessibleName";
            this.lblAccessibleName.Size = new System.Drawing.Size(92, 13);
            this.lblAccessibleName.TabIndex = 10;
            this.lblAccessibleName.Text = "Accessible Name:";
            // 
            // lblAccessibleValue
            // 
            this.lblAccessibleValue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblAccessibleValue.AutoSize = true;
            this.lblAccessibleValue.Location = new System.Drawing.Point(99, 156);
            this.lblAccessibleValue.Name = "lblAccessibleValue";
            this.lblAccessibleValue.Size = new System.Drawing.Size(91, 13);
            this.lblAccessibleValue.TabIndex = 12;
            this.lblAccessibleValue.Text = "Accessible Value:";
            // 
            // lblAccessibleRole
            // 
            this.lblAccessibleRole.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblAccessibleRole.AutoSize = true;
            this.lblAccessibleRole.Location = new System.Drawing.Point(104, 181);
            this.lblAccessibleRole.Name = "lblAccessibleRole";
            this.lblAccessibleRole.Size = new System.Drawing.Size(86, 13);
            this.lblAccessibleRole.TabIndex = 14;
            this.lblAccessibleRole.Text = "Accessible Role:";
            // 
            // txtAccessibleDescription
            // 
            this.txtAccessibleDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAccessibleDescription.Location = new System.Drawing.Point(196, 203);
            this.txtAccessibleDescription.Name = "txtAccessibleDescription";
            this.txtAccessibleDescription.ReadOnly = true;
            this.txtAccessibleDescription.Size = new System.Drawing.Size(504, 20);
            this.txtAccessibleDescription.TabIndex = 17;
            // 
            // lblAccessibleDescription
            // 
            this.lblAccessibleDescription.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblAccessibleDescription.AutoSize = true;
            this.lblAccessibleDescription.Location = new System.Drawing.Point(73, 208);
            this.lblAccessibleDescription.Name = "lblAccessibleDescription";
            this.lblAccessibleDescription.Size = new System.Drawing.Size(117, 13);
            this.lblAccessibleDescription.TabIndex = 16;
            this.lblAccessibleDescription.Text = "Accessible Description:";
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
            this.tblWindowBottom.Location = new System.Drawing.Point(6, 246);
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
            // txtWindowHandle
            // 
            this.txtWindowHandle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWindowHandle.Location = new System.Drawing.Point(196, 3);
            this.txtWindowHandle.Name = "txtWindowHandle";
            this.txtWindowHandle.ReadOnly = true;
            this.txtWindowHandle.Size = new System.Drawing.Size(171, 20);
            this.txtWindowHandle.TabIndex = 1;
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
            // tblProcessTop
            // 
            this.tblProcessTop.ColumnCount = 2;
            this.tblProcessTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.71428F));
            this.tblProcessTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.28571F));
            this.tblProcessTop.Controls.Add(this.lblWorkingDirectory, 0, 2);
            this.tblProcessTop.Controls.Add(this.lblFullPath, 0, 0);
            this.tblProcessTop.Controls.Add(this.lblCommandLine, 0, 1);
            this.tblProcessTop.Controls.Add(this.txtFullPath, 1, 0);
            this.tblProcessTop.Controls.Add(this.txtCommandLine, 1, 1);
            this.tblProcessTop.Controls.Add(this.txtWorkingDirectory, 1, 2);
            this.tblProcessTop.Location = new System.Drawing.Point(6, 19);
            this.tblProcessTop.Name = "tblProcessTop";
            this.tblProcessTop.RowCount = 3;
            this.tblProcessTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblProcessTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblProcessTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblProcessTop.Size = new System.Drawing.Size(703, 80);
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
            this.lblCommandLine.Location = new System.Drawing.Point(111, 31);
            this.lblCommandLine.Name = "lblCommandLine";
            this.lblCommandLine.Size = new System.Drawing.Size(80, 13);
            this.lblCommandLine.TabIndex = 2;
            this.lblCommandLine.Text = "Command Line:";
            // 
            // lblWorkingDirectory
            // 
            this.lblWorkingDirectory.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblWorkingDirectory.AutoSize = true;
            this.lblWorkingDirectory.Location = new System.Drawing.Point(96, 58);
            this.lblWorkingDirectory.Name = "lblWorkingDirectory";
            this.lblWorkingDirectory.Size = new System.Drawing.Size(95, 13);
            this.lblWorkingDirectory.TabIndex = 4;
            this.lblWorkingDirectory.Text = "Working Directory:";
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
            // 
            // txtWorkingDirectory
            // 
            this.txtWorkingDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWorkingDirectory.Location = new System.Drawing.Point(197, 53);
            this.txtWorkingDirectory.Name = "txtWorkingDirectory";
            this.txtWorkingDirectory.ReadOnly = true;
            this.txtWorkingDirectory.Size = new System.Drawing.Size(503, 20);
            this.txtWorkingDirectory.TabIndex = 5;
            // 
            // tblProcessBottom
            // 
            this.tblProcessBottom.ColumnCount = 4;
            this.tblProcessBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.61087F));
            this.tblProcessBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tblProcessBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.85714F));
            this.tblProcessBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.46209F));
            this.tblProcessBottom.Controls.Add(this.txtOwner, 0, 1);
            this.tblProcessBottom.Controls.Add(this.lblOwner, 0, 1);
            this.tblProcessBottom.Controls.Add(this.lblStartedAt, 0, 0);
            this.tblProcessBottom.Controls.Add(this.lblParent, 2, 0);
            this.tblProcessBottom.Controls.Add(this.lblPriority, 2, 1);
            this.tblProcessBottom.Controls.Add(this.txtStartedAt, 1, 0);
            this.tblProcessBottom.Controls.Add(this.txtParent, 3, 0);
            this.tblProcessBottom.Controls.Add(this.txtPriority, 3, 1);
            this.tblProcessBottom.Location = new System.Drawing.Point(6, 99);
            this.tblProcessBottom.Name = "tblProcessBottom";
            this.tblProcessBottom.RowCount = 2;
            this.tblProcessBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblProcessBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblProcessBottom.Size = new System.Drawing.Size(703, 56);
            this.tblProcessBottom.TabIndex = 1;
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
            this.lblParent.Location = new System.Drawing.Point(493, 6);
            this.lblParent.Name = "lblParent";
            this.lblParent.Size = new System.Drawing.Size(41, 13);
            this.lblParent.TabIndex = 2;
            this.lblParent.Text = "Parent:";
            // 
            // lblPriority
            // 
            this.lblPriority.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPriority.AutoSize = true;
            this.lblPriority.Location = new System.Drawing.Point(493, 34);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(41, 13);
            this.lblPriority.TabIndex = 7;
            this.lblPriority.Text = "Priority:";
            // 
            // lblOwner
            // 
            this.lblOwner.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblOwner.AutoSize = true;
            this.lblOwner.Location = new System.Drawing.Point(150, 34);
            this.lblOwner.Name = "lblOwner";
            this.lblOwner.Size = new System.Drawing.Size(41, 13);
            this.lblOwner.TabIndex = 5;
            this.lblOwner.Text = "Owner:";
            // 
            // txtStartedAt
            // 
            this.txtStartedAt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStartedAt.Location = new System.Drawing.Point(197, 3);
            this.txtStartedAt.Name = "txtStartedAt";
            this.txtStartedAt.ReadOnly = true;
            this.txtStartedAt.Size = new System.Drawing.Size(226, 20);
            this.txtStartedAt.TabIndex = 1;
            // 
            // txtParent
            // 
            this.txtParent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParent.Location = new System.Drawing.Point(540, 3);
            this.txtParent.Name = "txtParent";
            this.txtParent.ReadOnly = true;
            this.txtParent.Size = new System.Drawing.Size(160, 20);
            this.txtParent.TabIndex = 3;
            // 
            // txtPriority
            // 
            this.txtPriority.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPriority.Location = new System.Drawing.Point(540, 28);
            this.txtPriority.Name = "txtPriority";
            this.txtPriority.ReadOnly = true;
            this.txtPriority.Size = new System.Drawing.Size(160, 20);
            this.txtPriority.TabIndex = 0;
            // 
            // txtOwner
            // 
            this.txtOwner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOwner.Location = new System.Drawing.Point(197, 28);
            this.txtOwner.Name = "txtOwner";
            this.txtOwner.ReadOnly = true;
            this.txtOwner.Size = new System.Drawing.Size(226, 20);
            this.txtOwner.TabIndex = 6;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.59602F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.40398F));
            this.tableLayoutPanel1.Controls.Add(this.lblCopyright, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblProductName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblDescription, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtProductName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtDescription, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtCopyright, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 153);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(703, 78);
            this.tableLayoutPanel1.TabIndex = 2;
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
            // lblDescription
            // 
            this.lblDescription.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(128, 31);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(63, 13);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description:";
            // 
            // lblCopyright
            // 
            this.lblCopyright.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Location = new System.Drawing.Point(137, 57);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(54, 13);
            this.lblCopyright.TabIndex = 4;
            this.lblCopyright.Text = "Copyright:";
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
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(197, 28);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(503, 20);
            this.txtDescription.TabIndex = 3;
            // 
            // txtCopyright
            // 
            this.txtCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCopyright.Location = new System.Drawing.Point(197, 53);
            this.txtCopyright.Name = "txtCopyright";
            this.txtCopyright.ReadOnly = true;
            this.txtCopyright.Size = new System.Drawing.Size(503, 20);
            this.txtCopyright.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.73826F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.00142F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.78947F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.32859F));
            this.tableLayoutPanel2.Controls.Add(this.txtFileVersion, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblFileVersion, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblProductVersion, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtProductVersion, 3, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 231);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(703, 27);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // lblFileVersion
            // 
            this.lblFileVersion.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblFileVersion.AutoSize = true;
            this.lblFileVersion.Location = new System.Drawing.Point(128, 7);
            this.lblFileVersion.Name = "lblFileVersion";
            this.lblFileVersion.Size = new System.Drawing.Size(64, 13);
            this.lblFileVersion.TabIndex = 0;
            this.lblFileVersion.Text = "File Version:";
            // 
            // lblProductVersion
            // 
            this.lblProductVersion.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblProductVersion.AutoSize = true;
            this.lblProductVersion.Location = new System.Drawing.Point(450, 7);
            this.lblProductVersion.Name = "lblProductVersion";
            this.lblProductVersion.Size = new System.Drawing.Size(85, 13);
            this.lblProductVersion.TabIndex = 2;
            this.lblProductVersion.Text = "Product Version:";
            // 
            // txtFileVersion
            // 
            this.txtFileVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileVersion.Location = new System.Drawing.Point(198, 3);
            this.txtFileVersion.Name = "txtFileVersion";
            this.txtFileVersion.ReadOnly = true;
            this.txtFileVersion.Size = new System.Drawing.Size(226, 20);
            this.txtFileVersion.TabIndex = 1;
            // 
            // txtProductVersion
            // 
            this.txtProductVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProductVersion.Location = new System.Drawing.Point(541, 3);
            this.txtProductVersion.Name = "txtProductVersion";
            this.txtProductVersion.ReadOnly = true;
            this.txtProductVersion.Size = new System.Drawing.Size(159, 20);
            this.txtProductVersion.TabIndex = 3;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(602, 735);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(114, 33);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.CloseClick);
            // 
            // InfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 777);
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
            this.grpProcess.ResumeLayout(false);
            this.tblWindowTop.ResumeLayout(false);
            this.tblWindowTop.PerformLayout();
            this.tblWindowBottom.ResumeLayout(false);
            this.tblWindowBottom.PerformLayout();
            this.tblProcessTop.ResumeLayout(false);
            this.tblProcessTop.PerformLayout();
            this.tblProcessBottom.ResumeLayout(false);
            this.tblProcessBottom.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
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
        private System.Windows.Forms.Label lblAccessibleDescription;
        private System.Windows.Forms.Label lblAccessibleRole;
        private System.Windows.Forms.Label lblAccessibleValue;
        private System.Windows.Forms.Label lblAccessibleName;
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
        private System.Windows.Forms.TextBox txtAccessibleName;
        private System.Windows.Forms.TextBox txtAccessibleValue;
        private System.Windows.Forms.TextBox txtAccessibleRole;
        private System.Windows.Forms.TextBox txtAccessibleDescription;
        private System.Windows.Forms.GroupBox grpProcess;
        private System.Windows.Forms.TableLayoutPanel tblProcessTop;
        private System.Windows.Forms.Label lblWorkingDirectory;
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
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.TextBox txtDescription;
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
        private System.Windows.Forms.TextBox txtWorkingDirectory;
        private System.Windows.Forms.Button btnOk;
    }
}