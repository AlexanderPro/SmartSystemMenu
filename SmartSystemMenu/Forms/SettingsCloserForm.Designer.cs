namespace SmartSystemMenu.Forms
{
    partial class SettingsCloserForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.cmbKey1 = new System.Windows.Forms.ComboBox();
            this.cmbKey2 = new System.Windows.Forms.ComboBox();
            this.cmMouseButton = new System.Windows.Forms.ComboBox();
            this.lblKey1 = new System.Windows.Forms.Label();
            this.lblKey2 = new System.Windows.Forms.Label();
            this.lblMouseButton = new System.Windows.Forms.Label();
            this.cmbAction = new System.Windows.Forms.ComboBox();
            this.lblAction = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(315, 139);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 35);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(228, 139);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(81, 35);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "Ok";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.ButtonApplyClick);
            // 
            // cmbKey1
            // 
            this.cmbKey1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKey1.FormattingEnabled = true;
            this.cmbKey1.Location = new System.Drawing.Point(16, 36);
            this.cmbKey1.Name = "cmbKey1";
            this.cmbKey1.Size = new System.Drawing.Size(110, 21);
            this.cmbKey1.TabIndex = 1;
            // 
            // cmbKey2
            // 
            this.cmbKey2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKey2.FormattingEnabled = true;
            this.cmbKey2.Location = new System.Drawing.Point(150, 36);
            this.cmbKey2.Name = "cmbKey2";
            this.cmbKey2.Size = new System.Drawing.Size(110, 21);
            this.cmbKey2.TabIndex = 3;
            // 
            // cmMouseButton
            // 
            this.cmMouseButton.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmMouseButton.FormattingEnabled = true;
            this.cmMouseButton.Location = new System.Drawing.Point(285, 36);
            this.cmMouseButton.Name = "cmMouseButton";
            this.cmMouseButton.Size = new System.Drawing.Size(110, 21);
            this.cmMouseButton.TabIndex = 5;
            // 
            // lblKey1
            // 
            this.lblKey1.AutoSize = true;
            this.lblKey1.Location = new System.Drawing.Point(13, 20);
            this.lblKey1.Name = "lblKey1";
            this.lblKey1.Size = new System.Drawing.Size(34, 13);
            this.lblKey1.TabIndex = 0;
            this.lblKey1.Text = "Key 1";
            // 
            // lblKey2
            // 
            this.lblKey2.AutoSize = true;
            this.lblKey2.Location = new System.Drawing.Point(147, 20);
            this.lblKey2.Name = "lblKey2";
            this.lblKey2.Size = new System.Drawing.Size(34, 13);
            this.lblKey2.TabIndex = 2;
            this.lblKey2.Text = "Key 2";
            // 
            // lblMouseButton
            // 
            this.lblMouseButton.AutoSize = true;
            this.lblMouseButton.Location = new System.Drawing.Point(282, 20);
            this.lblMouseButton.Name = "lblMouseButton";
            this.lblMouseButton.Size = new System.Drawing.Size(73, 13);
            this.lblMouseButton.TabIndex = 4;
            this.lblMouseButton.Text = "Mouse Button";
            // 
            // cmbAction
            // 
            this.cmbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAction.FormattingEnabled = true;
            this.cmbAction.Location = new System.Drawing.Point(16, 92);
            this.cmbAction.Name = "cmbAction";
            this.cmbAction.Size = new System.Drawing.Size(379, 21);
            this.cmbAction.TabIndex = 7;
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Location = new System.Drawing.Point(13, 76);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(37, 13);
            this.lblAction.TabIndex = 6;
            this.lblAction.Text = "Action";
            // 
            // WindowCloserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 192);
            this.Controls.Add(this.lblAction);
            this.Controls.Add(this.cmbAction);
            this.Controls.Add(this.lblMouseButton);
            this.Controls.Add(this.lblKey2);
            this.Controls.Add(this.lblKey1);
            this.Controls.Add(this.cmMouseButton);
            this.Controls.Add(this.cmbKey2);
            this.Controls.Add(this.cmbKey1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WindowCloserForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ComboBox cmbKey1;
        private System.Windows.Forms.ComboBox cmbKey2;
        private System.Windows.Forms.ComboBox cmMouseButton;
        private System.Windows.Forms.Label lblKey1;
        private System.Windows.Forms.Label lblKey2;
        private System.Windows.Forms.Label lblMouseButton;
        private System.Windows.Forms.ComboBox cmbAction;
        private System.Windows.Forms.Label lblAction;
    }
}