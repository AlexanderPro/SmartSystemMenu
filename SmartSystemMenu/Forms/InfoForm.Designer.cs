using SmartSystemMenu.Settings;

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
        /// <param name="menuLanguage">Contains language strings.</param>
        private void InitializeComponent(MenuLanguage menuLanguage)
        {
            this.tabInfo = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.txtClassValue = new System.Windows.Forms.TextBox();
            this.lblRectangleValue = new System.Windows.Forms.Label();
            this.lblStyleValue = new System.Windows.Forms.Label();
            this.txtCaptionValue = new System.Windows.Forms.TextBox();
            this.txtHandleValue = new System.Windows.Forms.TextBox();
            this.lblRectangle = new System.Windows.Forms.Label();
            this.lblStyle = new System.Windows.Forms.Label();
            this.lblClass = new System.Windows.Forms.Label();
            this.lblCaption = new System.Windows.Forms.Label();
            this.lblHandle = new System.Windows.Forms.Label();
            this.tabProcess = new System.Windows.Forms.TabPage();
            this.lblThreadIdValue = new System.Windows.Forms.Label();
            this.lblThreadId = new System.Windows.Forms.Label();
            this.lblProcessIdValue = new System.Windows.Forms.Label();
            this.lblProcessId = new System.Windows.Forms.Label();
            this.txtModulePathValue = new System.Windows.Forms.TextBox();
            this.lblModulePath = new System.Windows.Forms.Label();
            this.txtModuleNameValue = new System.Windows.Forms.TextBox();
            this.lblModuleName = new System.Windows.Forms.Label();
            this.tabInfo.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabProcess.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.tabGeneral);
            this.tabInfo.Controls.Add(this.tabProcess);
            this.tabInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabInfo.Location = new System.Drawing.Point(0, 0);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.SelectedIndex = 0;
            this.tabInfo.Size = new System.Drawing.Size(388, 216);
            this.tabInfo.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.txtClassValue);
            this.tabGeneral.Controls.Add(this.lblRectangleValue);
            this.tabGeneral.Controls.Add(this.lblStyleValue);
            this.tabGeneral.Controls.Add(this.txtCaptionValue);
            this.tabGeneral.Controls.Add(this.txtHandleValue);
            this.tabGeneral.Controls.Add(this.lblRectangle);
            this.tabGeneral.Controls.Add(this.lblStyle);
            this.tabGeneral.Controls.Add(this.lblClass);
            this.tabGeneral.Controls.Add(this.lblCaption);
            this.tabGeneral.Controls.Add(this.lblHandle);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(380, 190);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = menuLanguage.GetStringValue("tab_general");
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // txtClassValue
            // 
            this.txtClassValue.Location = new System.Drawing.Point(101, 83);
            this.txtClassValue.Name = "txtClassValue";
            this.txtClassValue.Size = new System.Drawing.Size(262, 20);
            this.txtClassValue.TabIndex = 5;
            // 
            // lblRectangleValue
            // 
            this.lblRectangleValue.AutoSize = true;
            this.lblRectangleValue.Location = new System.Drawing.Point(98, 149);
            this.lblRectangleValue.Name = "lblRectangleValue";
            this.lblRectangleValue.Size = new System.Drawing.Size(86, 13);
            this.lblRectangleValue.TabIndex = 9;
            this.lblRectangleValue.Text = "Rectangle Value";
            // 
            // lblStyleValue
            // 
            this.lblStyleValue.AutoSize = true;
            this.lblStyleValue.Location = new System.Drawing.Point(98, 118);
            this.lblStyleValue.Name = "lblStyleValue";
            this.lblStyleValue.Size = new System.Drawing.Size(60, 13);
            this.lblStyleValue.TabIndex = 7;
            this.lblStyleValue.Text = "Style Value";
            // 
            // txtCaptionValue
            // 
            this.txtCaptionValue.Location = new System.Drawing.Point(101, 51);
            this.txtCaptionValue.Name = "txtCaptionValue";
            this.txtCaptionValue.Size = new System.Drawing.Size(262, 20);
            this.txtCaptionValue.TabIndex = 3;
            // 
            // txtHandleValue
            // 
            this.txtHandleValue.Location = new System.Drawing.Point(101, 20);
            this.txtHandleValue.Name = "txtHandleValue";
            this.txtHandleValue.Size = new System.Drawing.Size(262, 20);
            this.txtHandleValue.TabIndex = 1;
            // 
            // lblRectangle
            // 
            this.lblRectangle.AutoSize = true;
            this.lblRectangle.Location = new System.Drawing.Point(21, 149);
            this.lblRectangle.Name = "lblRectangle";
            this.lblRectangle.Size = new System.Drawing.Size(59, 13);
            this.lblRectangle.TabIndex = 8;
            this.lblRectangle.Text = menuLanguage.GetStringValue("lbl_rectangle");
            // 
            // lblStyle
            // 
            this.lblStyle.AutoSize = true;
            this.lblStyle.Location = new System.Drawing.Point(21, 118);
            this.lblStyle.Name = "lblStyle";
            this.lblStyle.Size = new System.Drawing.Size(33, 13);
            this.lblStyle.TabIndex = 6;
            this.lblStyle.Text = menuLanguage.GetStringValue("lbl_style");
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Location = new System.Drawing.Point(21, 86);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(35, 13);
            this.lblClass.TabIndex = 4;
            this.lblClass.Text = menuLanguage.GetStringValue("lbl_class");
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Location = new System.Drawing.Point(21, 54);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(46, 13);
            this.lblCaption.TabIndex = 2;
            this.lblCaption.Text = menuLanguage.GetStringValue("lbl_caption");
            // 
            // lblHandle
            // 
            this.lblHandle.AutoSize = true;
            this.lblHandle.Location = new System.Drawing.Point(21, 23);
            this.lblHandle.Name = "lblHandle";
            this.lblHandle.Size = new System.Drawing.Size(44, 13);
            this.lblHandle.TabIndex = 0;
            this.lblHandle.Text = menuLanguage.GetStringValue("lbl_handle");
            // 
            // tabProcess
            // 
            this.tabProcess.Controls.Add(this.lblThreadIdValue);
            this.tabProcess.Controls.Add(this.lblThreadId);
            this.tabProcess.Controls.Add(this.lblProcessIdValue);
            this.tabProcess.Controls.Add(this.lblProcessId);
            this.tabProcess.Controls.Add(this.txtModulePathValue);
            this.tabProcess.Controls.Add(this.lblModulePath);
            this.tabProcess.Controls.Add(this.txtModuleNameValue);
            this.tabProcess.Controls.Add(this.lblModuleName);
            this.tabProcess.Location = new System.Drawing.Point(4, 22);
            this.tabProcess.Name = "tabProcess";
            this.tabProcess.Padding = new System.Windows.Forms.Padding(3);
            this.tabProcess.Size = new System.Drawing.Size(380, 190);
            this.tabProcess.TabIndex = 1;
            this.tabProcess.Text = menuLanguage.GetStringValue("tab_process");
            this.tabProcess.UseVisualStyleBackColor = true;
            // 
            // lblThreadIdValue
            // 
            this.lblThreadIdValue.AutoSize = true;
            this.lblThreadIdValue.Location = new System.Drawing.Point(98, 118);
            this.lblThreadIdValue.Name = "lblThreadIdValue";
            this.lblThreadIdValue.Size = new System.Drawing.Size(83, 13);
            this.lblThreadIdValue.TabIndex = 7;
            this.lblThreadIdValue.Text = "Thread Id Value";
            // 
            // lblThreadId
            // 
            this.lblThreadId.AutoSize = true;
            this.lblThreadId.Location = new System.Drawing.Point(21, 118);
            this.lblThreadId.Name = "lblThreadId";
            this.lblThreadId.Size = new System.Drawing.Size(56, 13);
            this.lblThreadId.TabIndex = 6;
            this.lblThreadId.Text = menuLanguage.GetStringValue("lbl_thread_id");
            // 
            // lblProcessIdValue
            // 
            this.lblProcessIdValue.AutoSize = true;
            this.lblProcessIdValue.Location = new System.Drawing.Point(98, 86);
            this.lblProcessIdValue.Name = "lblProcessIdValue";
            this.lblProcessIdValue.Size = new System.Drawing.Size(87, 13);
            this.lblProcessIdValue.TabIndex = 5;
            this.lblProcessIdValue.Text = "Process Id Value";
            // 
            // lblProcessId
            // 
            this.lblProcessId.AutoSize = true;
            this.lblProcessId.Location = new System.Drawing.Point(21, 86);
            this.lblProcessId.Name = "lblProcessId";
            this.lblProcessId.Size = new System.Drawing.Size(60, 13);
            this.lblProcessId.TabIndex = 4;
            this.lblProcessId.Text = menuLanguage.GetStringValue("lbl_process_id");
            // 
            // txtModulePathValue
            // 
            this.txtModulePathValue.Location = new System.Drawing.Point(101, 51);
            this.txtModulePathValue.Name = "txtModulePathValue";
            this.txtModulePathValue.Size = new System.Drawing.Size(262, 20);
            this.txtModulePathValue.TabIndex = 3;
            // 
            // lblModulePath
            // 
            this.lblModulePath.AutoSize = true;
            this.lblModulePath.Location = new System.Drawing.Point(21, 54);
            this.lblModulePath.Name = "lblModulePath";
            this.lblModulePath.Size = new System.Drawing.Size(70, 13);
            this.lblModulePath.TabIndex = 2;
            this.lblModulePath.Text = menuLanguage.GetStringValue("lbl_module_path");
            // 
            // txtModuleNameValue
            // 
            this.txtModuleNameValue.Location = new System.Drawing.Point(101, 20);
            this.txtModuleNameValue.Name = "txtModuleNameValue";
            this.txtModuleNameValue.Size = new System.Drawing.Size(262, 20);
            this.txtModuleNameValue.TabIndex = 1;
            // 
            // lblModuleName
            // 
            this.lblModuleName.AutoSize = true;
            this.lblModuleName.Location = new System.Drawing.Point(21, 23);
            this.lblModuleName.Name = "lblModuleName";
            this.lblModuleName.Size = new System.Drawing.Size(76, 13);
            this.lblModuleName.TabIndex = 0;
            this.lblModuleName.Text = menuLanguage.GetStringValue("lbl_module_name");
            // 
            // InfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 216);
            this.Controls.Add(this.tabInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = menuLanguage.GetStringValue("information");
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
            this.tabInfo.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.tabProcess.ResumeLayout(false);
            this.tabProcess.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabInfo;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabProcess;
        private System.Windows.Forms.Label lblHandle;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.Label lblStyle;
        private System.Windows.Forms.Label lblRectangle;
        private System.Windows.Forms.Label lblRectangleValue;
        private System.Windows.Forms.Label lblStyleValue;
        private System.Windows.Forms.TextBox txtCaptionValue;
        private System.Windows.Forms.TextBox txtHandleValue;
        private System.Windows.Forms.Label lblThreadIdValue;
        private System.Windows.Forms.Label lblThreadId;
        private System.Windows.Forms.Label lblProcessIdValue;
        private System.Windows.Forms.Label lblProcessId;
        private System.Windows.Forms.TextBox txtModulePathValue;
        private System.Windows.Forms.Label lblModulePath;
        private System.Windows.Forms.TextBox txtModuleNameValue;
        private System.Windows.Forms.Label lblModuleName;
        private System.Windows.Forms.TextBox txtClassValue;
    }
}