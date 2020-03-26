using SmartSystemMenu.Settings;

namespace SmartSystemMenu.Forms
{
    partial class PositionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Add language string.
        /// </summary>
        private MenuLanguage _menuLanguage;

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
        private void InitializeComponent(MenuLanguage menuLanguage)
        {
            _menuLanguage = menuLanguage;

            this.numericLeft = new System.Windows.Forms.NumericUpDown();
            this.lblLeft = new System.Windows.Forms.Label();
            this.lblTop = new System.Windows.Forms.Label();
            this.numericTop = new System.Windows.Forms.NumericUpDown();
            this.btnApply = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTop)).BeginInit();
            this.SuspendLayout();
            // 
            // numericLeft
            // 
            this.numericLeft.Location = new System.Drawing.Point(53, 12);
            this.numericLeft.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericLeft.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.numericLeft.Name = "numericLeft";
            this.numericLeft.Size = new System.Drawing.Size(72, 20);
            this.numericLeft.TabIndex = 1;
            // 
            // lblLeft
            // 
            this.lblLeft.AutoSize = true;
            this.lblLeft.Location = new System.Drawing.Point(12, 14);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(28, 13);
            this.lblLeft.TabIndex = 0;
            this.lblLeft.Text = _menuLanguage.GetStringValue("lbl_left");
            // 
            // lblTop
            // 
            this.lblTop.AutoSize = true;
            this.lblTop.Location = new System.Drawing.Point(136, 14);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(29, 13);
            this.lblTop.TabIndex = 2;
            this.lblTop.Text = _menuLanguage.GetStringValue("lbl_top");
            // 
            // numericTop
            // 
            this.numericTop.Location = new System.Drawing.Point(183, 12);
            this.numericTop.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericTop.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.numericTop.Name = "numericTop";
            this.numericTop.Size = new System.Drawing.Size(72, 20);
            this.numericTop.TabIndex = 3;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(279, 9);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(91, 26);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = _menuLanguage.GetStringValue("align_btn_apply");
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.ButtonApplyClick);
            // 
            // PositionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 44);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.lblTop);
            this.Controls.Add(this.numericTop);
            this.Controls.Add(this.lblLeft);
            this.Controls.Add(this.numericLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PositionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = _menuLanguage.GetStringValue("align_form");
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.numericLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericLeft;
        private System.Windows.Forms.Label lblLeft;
        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.NumericUpDown numericTop;
        private System.Windows.Forms.Button btnApply;
    }
}