namespace Null.FastRun.View
{
    partial class AboutDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
            this.lb_title = new System.Windows.Forms.Label();
            this.lb_description = new System.Windows.Forms.Label();
            this.btn_ok = new System.Windows.Forms.Button();
            this.lnlb_github = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            resources.ApplyResources(this.lb_title, "lb_title");
            this.lb_title.Name = "lb_title";
            // 
            // lb_description
            // 
            resources.ApplyResources(this.lb_description, "lb_description");
            this.lb_description.Name = "lb_description";
            // 
            // btn_ok
            // 
            resources.ApplyResources(this.btn_ok, "btn_ok");
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // lnlb_github
            // 
            resources.ApplyResources(this.lnlb_github, "lnlb_github");
            this.lnlb_github.Name = "lnlb_github";
            this.lnlb_github.TabStop = true;
            this.lnlb_github.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnlb_github_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Null.FastRun.Properties.Resources.null_faststart_128x128;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // AboutDialog
            // 
            this.AcceptButton = this.btn_ok;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lnlb_github);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.lb_description);
            this.Controls.Add(this.lb_title);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutDialog";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lb_title;
        private System.Windows.Forms.Label lb_description;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.LinkLabel lnlb_github;
    }
}