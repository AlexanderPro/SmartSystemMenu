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
            this.lblJaehyungLee = new System.Windows.Forms.Label();
            this.lblLightAPIs = new System.Windows.Forms.Label();
            this.lblOzzii = new System.Windows.Forms.Label();
            this.lblOzziiAction = new System.Windows.Forms.Label();
            this.lblWengh = new System.Windows.Forms.Label();
            this.linkWengh = new System.Windows.Forms.LinkLabel();
            this.lblMarocco2 = new System.Windows.Forms.Label();
            this.linkMarocco2 = new System.Windows.Forms.LinkLabel();
            this.lblSaiyajinK = new System.Windows.Forms.Label();
            this.linkSaiyajinK = new System.Windows.Forms.LinkLabel();
            this.lblKissAction = new System.Windows.Forms.Label();
            this.lblKiss = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Location = new System.Drawing.Point(114, 30);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(75, 13);
            this.lblProductName.TabIndex = 0;
            this.lblProductName.Text = "Product Name";
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Location = new System.Drawing.Point(114, 52);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(51, 13);
            this.lblCopyright.TabIndex = 1;
            this.lblCopyright.Text = "Copyright";
            // 
            // linkUrl
            // 
            this.linkUrl.AutoSize = true;
            this.linkUrl.Location = new System.Drawing.Point(114, 75);
            this.linkUrl.Name = "linkUrl";
            this.linkUrl.Size = new System.Drawing.Size(29, 13);
            this.linkUrl.TabIndex = 2;
            this.linkUrl.TabStop = true;
            this.linkUrl.Text = "URL";
            this.linkUrl.Click += new System.EventHandler(this.LinkClick);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(358, 30);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(86, 30);
            this.btnOk.TabIndex = 18;
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
            this.lblSpecialThanks.TabIndex = 3;
            this.lblSpecialThanks.Text = "Special thanks to:";
            // 
            // linkLightAPIs
            // 
            this.linkLightAPIs.AutoSize = true;
            this.linkLightAPIs.Location = new System.Drawing.Point(116, 131);
            this.linkLightAPIs.Name = "linkLightAPIs";
            this.linkLightAPIs.Size = new System.Drawing.Size(52, 13);
            this.linkLightAPIs.TabIndex = 4;
            this.linkLightAPIs.TabStop = true;
            this.linkLightAPIs.Text = "LightAPIs";
            this.linkLightAPIs.Click += new System.EventHandler(this.LinkClick);
            // 
            // linkJaehyungLee
            // 
            this.linkJaehyungLee.AutoSize = true;
            this.linkJaehyungLee.Location = new System.Drawing.Point(116, 175);
            this.linkJaehyungLee.Name = "linkJaehyungLee";
            this.linkJaehyungLee.Size = new System.Drawing.Size(74, 13);
            this.linkJaehyungLee.TabIndex = 8;
            this.linkJaehyungLee.TabStop = true;
            this.linkJaehyungLee.Text = "Jaehyung Lee";
            this.linkJaehyungLee.Click += new System.EventHandler(this.LinkClick);
            // 
            // lblJaehyungLee
            // 
            this.lblJaehyungLee.AutoSize = true;
            this.lblJaehyungLee.Location = new System.Drawing.Point(215, 175);
            this.lblJaehyungLee.Name = "lblJaehyungLee";
            this.lblJaehyungLee.Size = new System.Drawing.Size(92, 13);
            this.lblJaehyungLee.TabIndex = 9;
            this.lblJaehyungLee.Text = "Korean translation";
            // 
            // lblLightAPIs
            // 
            this.lblLightAPIs.AutoSize = true;
            this.lblLightAPIs.Location = new System.Drawing.Point(215, 130);
            this.lblLightAPIs.Name = "lblLightAPIs";
            this.lblLightAPIs.Size = new System.Drawing.Size(114, 13);
            this.lblLightAPIs.TabIndex = 5;
            this.lblLightAPIs.Text = "Multi language support";
            // 
            // lblOzzii
            // 
            this.lblOzzii.AutoSize = true;
            this.lblOzzii.Location = new System.Drawing.Point(116, 197);
            this.lblOzzii.Name = "lblOzzii";
            this.lblOzzii.Size = new System.Drawing.Size(27, 13);
            this.lblOzzii.TabIndex = 10;
            this.lblOzzii.Text = "ozzii";
            // 
            // lblOzziiAction
            // 
            this.lblOzziiAction.AutoSize = true;
            this.lblOzziiAction.Location = new System.Drawing.Point(215, 197);
            this.lblOzziiAction.Name = "lblOzziiAction";
            this.lblOzziiAction.Size = new System.Drawing.Size(94, 13);
            this.lblOzziiAction.TabIndex = 11;
            this.lblOzziiAction.Text = "Serbian translation";
            // 
            // lblWengh
            // 
            this.lblWengh.AutoSize = true;
            this.lblWengh.Location = new System.Drawing.Point(215, 152);
            this.lblWengh.Name = "lblWengh";
            this.lblWengh.Size = new System.Drawing.Size(226, 13);
            this.lblWengh.TabIndex = 7;
            this.lblWengh.Text = "Menu item Suspend and Minimize, Refactoring";
            // 
            // linkWengh
            // 
            this.linkWengh.AutoSize = true;
            this.linkWengh.Location = new System.Drawing.Point(116, 153);
            this.linkWengh.Name = "linkWengh";
            this.linkWengh.Size = new System.Drawing.Size(70, 13);
            this.linkWengh.TabIndex = 6;
            this.linkWengh.TabStop = true;
            this.linkWengh.Text = "Weng Haoyu";
            this.linkWengh.Click += new System.EventHandler(this.LinkClick);
            // 
            // lblMarocco2
            // 
            this.lblMarocco2.AutoSize = true;
            this.lblMarocco2.Location = new System.Drawing.Point(215, 219);
            this.lblMarocco2.Name = "lblMarocco2";
            this.lblMarocco2.Size = new System.Drawing.Size(86, 13);
            this.lblMarocco2.TabIndex = 13;
            this.lblMarocco2.Text = "Italian translation";
            // 
            // linkMarocco2
            // 
            this.linkMarocco2.AutoSize = true;
            this.linkMarocco2.Location = new System.Drawing.Point(116, 219);
            this.linkMarocco2.Name = "linkMarocco2";
            this.linkMarocco2.Size = new System.Drawing.Size(55, 13);
            this.linkMarocco2.TabIndex = 12;
            this.linkMarocco2.TabStop = true;
            this.linkMarocco2.Text = "Marocco2";
            this.linkMarocco2.Click += new System.EventHandler(this.LinkClick);
            // 
            // lblSaiyajinK
            // 
            this.lblSaiyajinK.AutoSize = true;
            this.lblSaiyajinK.Location = new System.Drawing.Point(215, 241);
            this.lblSaiyajinK.Name = "lblSaiyajinK";
            this.lblSaiyajinK.Size = new System.Drawing.Size(91, 13);
            this.lblSaiyajinK.TabIndex = 15;
            this.lblSaiyajinK.Text = "French translation";
            // 
            // linkSaiyajinK
            // 
            this.linkSaiyajinK.AutoSize = true;
            this.linkSaiyajinK.Location = new System.Drawing.Point(116, 241);
            this.linkSaiyajinK.Name = "linkSaiyajinK";
            this.linkSaiyajinK.Size = new System.Drawing.Size(50, 13);
            this.linkSaiyajinK.TabIndex = 14;
            this.linkSaiyajinK.TabStop = true;
            this.linkSaiyajinK.Text = "SaiyajinK";
            this.linkSaiyajinK.Click += new System.EventHandler(this.LinkClick);
            // 
            // lblKissAction
            // 
            this.lblKissAction.AutoSize = true;
            this.lblKissAction.Location = new System.Drawing.Point(215, 263);
            this.lblKissAction.Name = "lblKissAction";
            this.lblKissAction.Size = new System.Drawing.Size(107, 13);
            this.lblKissAction.TabIndex = 17;
            this.lblKissAction.Text = "Hungarian translation";
            // 
            // lblKiss
            // 
            this.lblKiss.AutoSize = true;
            this.lblKiss.Location = new System.Drawing.Point(116, 263);
            this.lblKiss.Name = "lblKiss";
            this.lblKiss.Size = new System.Drawing.Size(93, 13);
            this.lblKiss.TabIndex = 16;
            this.lblKiss.Text = "Kiss Dénes László";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 320);
            this.Controls.Add(this.lblKiss);
            this.Controls.Add(this.lblKissAction);
            this.Controls.Add(this.lblSaiyajinK);
            this.Controls.Add(this.linkSaiyajinK);
            this.Controls.Add(this.lblMarocco2);
            this.Controls.Add(this.linkMarocco2);
            this.Controls.Add(this.lblWengh);
            this.Controls.Add(this.linkWengh);
            this.Controls.Add(this.lblOzziiAction);
            this.Controls.Add(this.lblOzzii);
            this.Controls.Add(this.lblLightAPIs);
            this.Controls.Add(this.lblJaehyungLee);
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
            this.ShowIcon = false;
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
        private System.Windows.Forms.Label lblJaehyungLee;
        private System.Windows.Forms.Label lblLightAPIs;
        private System.Windows.Forms.Label lblOzzii;
        private System.Windows.Forms.Label lblOzziiAction;
        private System.Windows.Forms.Label lblWengh;
        private System.Windows.Forms.LinkLabel linkWengh;
        private System.Windows.Forms.Label lblMarocco2;
        private System.Windows.Forms.LinkLabel linkMarocco2;
        private System.Windows.Forms.Label lblSaiyajinK;
        private System.Windows.Forms.LinkLabel linkSaiyajinK;
        private System.Windows.Forms.Label lblKissAction;
        private System.Windows.Forms.Label lblKiss;
    }
}