namespace SmartSystemMenu.Forms
{
    partial class SizeForm
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
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SizeForm));
            this.btnApply = new System.Windows.Forms.Button();
            this.txtTop = new System.Windows.Forms.TextBox();
            this.txtLeft = new System.Windows.Forms.TextBox();
            this.lblTop = new System.Windows.Forms.Label();
            this.lblLeft = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(321, 129);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(91, 26);
            this.btnApply.TabIndex = 8;
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.ButtonApplyClick);
            // 
            // txtTop
            // 
            this.txtTop.Location = new System.Drawing.Point(216, 34);
            this.txtTop.Name = "txtTop";
            this.txtTop.Size = new System.Drawing.Size(195, 20);
            this.txtTop.TabIndex = 3;
            // 
            // txtLeft
            // 
            this.txtLeft.Location = new System.Drawing.Point(12, 34);
            this.txtLeft.Name = "txtLeft";
            this.txtLeft.Size = new System.Drawing.Size(195, 20);
            this.txtLeft.TabIndex = 1;
            // 
            // lblTop
            // 
            this.lblTop.AutoSize = true;
            this.lblTop.Location = new System.Drawing.Point(216, 18);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(0, 13);
            this.lblTop.TabIndex = 2;
            // 
            // lblLeft
            // 
            this.lblLeft.AutoSize = true;
            this.lblLeft.Location = new System.Drawing.Point(12, 18);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(0, 13);
            this.lblLeft.TabIndex = 0;
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(216, 90);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(195, 20);
            this.txtHeight.TabIndex = 7;
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(12, 90);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(195, 20);
            this.txtWidth.TabIndex = 5;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(216, 74);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(0, 13);
            this.lblHeight.TabIndex = 6;
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(12, 74);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(0, 13);
            this.lblWidth.TabIndex = 4;
            // 
            // SizeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 192);
            this.Controls.Add(this.txtTop);
            this.Controls.Add(this.txtLeft);
            this.Controls.Add(this.lblTop);
            this.Controls.Add(this.lblLeft);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.lblWidth);
            this.Controls.Add(this.btnApply);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SizeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TextBox txtTop;
        private System.Windows.Forms.TextBox txtLeft;
        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.Label lblLeft;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label lblWidth;
    }
}