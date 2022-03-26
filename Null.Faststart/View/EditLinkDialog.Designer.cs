namespace Null.Faststart.View
{
    partial class EditLinkDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditLinkDialog));
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.txb_name = new System.Windows.Forms.TextBox();
            this.lb_name = new System.Windows.Forms.Label();
            this.txb_target = new System.Windows.Forms.TextBox();
            this.lb_target = new System.Windows.Forms.Label();
            this.lklb_openfolder = new System.Windows.Forms.LinkLabel();
            this.lb_tips = new System.Windows.Forms.Label();
            this.lnlb_openfile = new System.Windows.Forms.LinkLabel();
            this.lb_open = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_ok
            // 
            resources.ApplyResources(this.btn_ok, "btn_ok");
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancel
            // 
            resources.ApplyResources(this.btn_cancel, "btn_cancel");
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // txb_name
            // 
            resources.ApplyResources(this.txb_name, "txb_name");
            this.txb_name.Name = "txb_name";
            // 
            // lb_name
            // 
            resources.ApplyResources(this.lb_name, "lb_name");
            this.lb_name.Name = "lb_name";
            // 
            // txb_target
            // 
            resources.ApplyResources(this.txb_target, "txb_target");
            this.txb_target.Name = "txb_target";
            // 
            // lb_target
            // 
            resources.ApplyResources(this.lb_target, "lb_target");
            this.lb_target.Name = "lb_target";
            // 
            // lklb_openfolder
            // 
            resources.ApplyResources(this.lklb_openfolder, "lklb_openfolder");
            this.lklb_openfolder.Name = "lklb_openfolder";
            this.lklb_openfolder.TabStop = true;
            this.lklb_openfolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklb_openfolder_LinkClicked);
            // 
            // lb_tips
            // 
            resources.ApplyResources(this.lb_tips, "lb_tips");
            this.lb_tips.Name = "lb_tips";
            // 
            // lnlb_openfile
            // 
            resources.ApplyResources(this.lnlb_openfile, "lnlb_openfile");
            this.lnlb_openfile.Name = "lnlb_openfile";
            this.lnlb_openfile.TabStop = true;
            this.lnlb_openfile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnlb_openfile_LinkClicked);
            // 
            // lb_open
            // 
            resources.ApplyResources(this.lb_open, "lb_open");
            this.lb_open.Name = "lb_open";
            // 
            // EditLinkDialog
            // 
            this.AcceptButton = this.btn_ok;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.Controls.Add(this.lb_open);
            this.Controls.Add(this.lb_tips);
            this.Controls.Add(this.lnlb_openfile);
            this.Controls.Add(this.lklb_openfolder);
            this.Controls.Add(this.lb_target);
            this.Controls.Add(this.txb_target);
            this.Controls.Add(this.lb_name);
            this.Controls.Add(this.txb_name);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditLinkDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.TextBox txb_name;
        private System.Windows.Forms.Label lb_name;
        private System.Windows.Forms.TextBox txb_target;
        private System.Windows.Forms.Label lb_target;
        private System.Windows.Forms.LinkLabel lklb_openfolder;
        private System.Windows.Forms.Label lb_tips;
        private System.Windows.Forms.LinkLabel lnlb_openfile;
        private System.Windows.Forms.Label lb_open;
    }
}