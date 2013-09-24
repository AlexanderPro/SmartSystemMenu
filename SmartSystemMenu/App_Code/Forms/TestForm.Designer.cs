namespace SmartSystemMenu.App_Code.Forms
{
    partial class TestForm
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
            this.btnAddMenu = new System.Windows.Forms.Button();
            this.btnRemoveMenu = new System.Windows.Forms.Button();
            this.txtWindowHandle = new System.Windows.Forms.TextBox();
            this.lblWindowHandle = new System.Windows.Forms.Label();
            this.btnAddRemoveMenu = new System.Windows.Forms.Button();
            this.btnShowInfo = new System.Windows.Forms.Button();
            this.btnSchedule = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAddMenu
            // 
            this.btnAddMenu.Location = new System.Drawing.Point(61, 130);
            this.btnAddMenu.Name = "btnAddMenu";
            this.btnAddMenu.Size = new System.Drawing.Size(175, 44);
            this.btnAddMenu.TabIndex = 0;
            this.btnAddMenu.Text = "Add Menu";
            this.btnAddMenu.UseVisualStyleBackColor = true;
            this.btnAddMenu.Click += new System.EventHandler(this.AddMenuClick);
            // 
            // btnRemoveMenu
            // 
            this.btnRemoveMenu.Location = new System.Drawing.Point(61, 213);
            this.btnRemoveMenu.Name = "btnRemoveMenu";
            this.btnRemoveMenu.Size = new System.Drawing.Size(175, 44);
            this.btnRemoveMenu.TabIndex = 1;
            this.btnRemoveMenu.Text = "Remoove Menu";
            this.btnRemoveMenu.UseVisualStyleBackColor = true;
            this.btnRemoveMenu.Click += new System.EventHandler(this.RemoveMenuClick);
            // 
            // txtWindowHandle
            // 
            this.txtWindowHandle.Location = new System.Drawing.Point(61, 70);
            this.txtWindowHandle.Name = "txtWindowHandle";
            this.txtWindowHandle.Size = new System.Drawing.Size(175, 20);
            this.txtWindowHandle.TabIndex = 2;
            // 
            // lblWindowHandle
            // 
            this.lblWindowHandle.AutoSize = true;
            this.lblWindowHandle.Location = new System.Drawing.Point(61, 51);
            this.lblWindowHandle.Name = "lblWindowHandle";
            this.lblWindowHandle.Size = new System.Drawing.Size(114, 13);
            this.lblWindowHandle.TabIndex = 3;
            this.lblWindowHandle.Text = "Window Handle (Hex):";
            // 
            // btnAddRemoveMenu
            // 
            this.btnAddRemoveMenu.Location = new System.Drawing.Point(61, 291);
            this.btnAddRemoveMenu.Name = "btnAddRemoveMenu";
            this.btnAddRemoveMenu.Size = new System.Drawing.Size(175, 44);
            this.btnAddRemoveMenu.TabIndex = 4;
            this.btnAddRemoveMenu.Text = "Add Remove Menu";
            this.btnAddRemoveMenu.UseVisualStyleBackColor = true;
            this.btnAddRemoveMenu.Click += new System.EventHandler(this.AddRemoveMenuClick);
            // 
            // btnShowInfo
            // 
            this.btnShowInfo.Location = new System.Drawing.Point(61, 369);
            this.btnShowInfo.Name = "btnShowInfo";
            this.btnShowInfo.Size = new System.Drawing.Size(175, 44);
            this.btnShowInfo.TabIndex = 5;
            this.btnShowInfo.Text = "Show Info";
            this.btnShowInfo.UseVisualStyleBackColor = true;
            this.btnShowInfo.Click += new System.EventHandler(this.ShowInfoClick);
            // 
            // btnSchedule
            // 
            this.btnSchedule.Location = new System.Drawing.Point(61, 434);
            this.btnSchedule.Name = "btnSchedule";
            this.btnSchedule.Size = new System.Drawing.Size(175, 44);
            this.btnSchedule.TabIndex = 6;
            this.btnSchedule.Text = "Schedule";
            this.btnSchedule.UseVisualStyleBackColor = true;
            this.btnSchedule.Click += new System.EventHandler(this.ScheduleClick);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 520);
            this.Controls.Add(this.btnSchedule);
            this.Controls.Add(this.btnShowInfo);
            this.Controls.Add(this.btnAddRemoveMenu);
            this.Controls.Add(this.lblWindowHandle);
            this.Controls.Add(this.txtWindowHandle);
            this.Controls.Add(this.btnRemoveMenu);
            this.Controls.Add(this.btnAddMenu);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddMenu;
        private System.Windows.Forms.Button btnRemoveMenu;
        private System.Windows.Forms.TextBox txtWindowHandle;
        private System.Windows.Forms.Label lblWindowHandle;
        private System.Windows.Forms.Button btnAddRemoveMenu;
        private System.Windows.Forms.Button btnShowInfo;
        private System.Windows.Forms.Button btnSchedule;
    }
}