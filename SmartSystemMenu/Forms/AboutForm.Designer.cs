namespace SmartSystemMenu.Forms
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.linkUrl = new System.Windows.Forms.LinkLabel();
            this.btnOk = new System.Windows.Forms.Button();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.lblSpecialThanks = new System.Windows.Forms.Label();
            this.linkLightAPIs = new System.Windows.Forms.LinkLabel();
            this.linkJaehyungLee = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Location = new System.Drawing.Point(114, 30);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(75, 13);
            this.lblProductName.TabIndex = 1;
            this.lblProductName.Text = "Product Name";
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Location = new System.Drawing.Point(114, 52);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(51, 13);
            this.lblCopyright.TabIndex = 2;
            this.lblCopyright.Text = "Copyright";
            // 
            // linkUrl
            // 
            this.linkUrl.AutoSize = true;
            this.linkUrl.Location = new System.Drawing.Point(114, 75);
            this.linkUrl.Name = "linkUrl";
            this.linkUrl.Size = new System.Drawing.Size(29, 13);
            this.linkUrl.TabIndex = 3;
            this.linkUrl.TabStop = true;
            this.linkUrl.Text = "URL";
            this.linkUrl.Click += new System.EventHandler(this.LinkClick);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(358, 30);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(86, 30);
            this.btnOk.TabIndex = 0;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.CloseClick);
            // 
            // pbImage
            // 
            this.pbImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbImage.BackgroundImage")));
            this.pbImage.InitialImage = null;
            this.pbImage.Location = new System.Drawing.Point(4, 21);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(104, 101);
            this.pbImage.TabIndex = 4;
            this.pbImage.TabStop = false;
            // 
            // lblSpecialThanks
            // 
            this.lblSpecialThanks.AutoSize = true;
            this.lblSpecialThanks.Location = new System.Drawing.Point(114, 109);
            this.lblSpecialThanks.Name = "lblSpecialThanks";
            this.lblSpecialThanks.Size = new System.Drawing.Size(92, 13);
            this.lblSpecialThanks.TabIndex = 4;
            this.lblSpecialThanks.Text = "Special thanks to:";
            // 
            // linkLightAPIs
            // 
            this.linkLightAPIs.AutoSize = true;
            this.linkLightAPIs.Location = new System.Drawing.Point(116, 131);
            this.linkLightAPIs.Name = "linkLightAPIs";
            this.linkLightAPIs.Size = new System.Drawing.Size(52, 13);
            this.linkLightAPIs.TabIndex = 5;
            this.linkLightAPIs.TabStop = true;
            this.linkLightAPIs.Text = "LightAPIs";
            this.linkLightAPIs.Click += new System.EventHandler(this.LinkClick);
            // 
            // linkJaehyungLee
            // 
            this.linkJaehyungLee.AutoSize = true;
            this.linkJaehyungLee.Location = new System.Drawing.Point(116, 153);
            this.linkJaehyungLee.Name = "linkJaehyungLee";
            this.linkJaehyungLee.Size = new System.Drawing.Size(74, 13);
            this.linkJaehyungLee.TabIndex = 6;
            this.linkJaehyungLee.TabStop = true;
            this.linkJaehyungLee.Text = "Jaehyung Lee";
            this.linkJaehyungLee.Click += new System.EventHandler(this.LinkClick);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 185);
            this.Controls.Add(this.linkJaehyungLee);
            this.Controls.Add(this.linkLightAPIs);
            this.Controls.Add(this.lblSpecialThanks);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.linkUrl);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblProductName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownClick);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.LinkLabel linkUrl;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Label lblSpecialThanks;
        private System.Windows.Forms.LinkLabel linkLightAPIs;
        private System.Windows.Forms.LinkLabel linkJaehyungLee;
    }
}