namespace Null.FastRun.View
{
    partial class ConfigDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigDialog));
            this.lb_linkpath = new System.Windows.Forms.Label();
            this.txb_linkpath = new System.Windows.Forms.TextBox();
            this.lb_linkmode = new System.Windows.Forms.Label();
            this.comb_linkmode = new System.Windows.Forms.ComboBox();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_uninstall = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_linkpath
            // 
            resources.ApplyResources(this.lb_linkpath, "lb_linkpath");
            this.lb_linkpath.Name = "lb_linkpath";
            // 
            // txb_linkpath
            // 
            resources.ApplyResources(this.txb_linkpath, "txb_linkpath");
            this.txb_linkpath.Name = "txb_linkpath";
            // 
            // lb_linkmode
            // 
            resources.ApplyResources(this.lb_linkmode, "lb_linkmode");
            this.lb_linkmode.Name = "lb_linkmode";
            // 
            // comb_linkmode
            // 
            resources.ApplyResources(this.comb_linkmode, "comb_linkmode");
            this.comb_linkmode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_linkmode.FormattingEnabled = true;
            this.comb_linkmode.Name = "comb_linkmode";
            // 
            // btn_ok
            // 
            resources.ApplyResources(this.btn_ok, "btn_ok");
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.closeDialog);
            // 
            // btn_uninstall
            // 
            resources.ApplyResources(this.btn_uninstall, "btn_uninstall");
            this.btn_uninstall.Name = "btn_uninstall";
            this.btn_uninstall.UseVisualStyleBackColor = true;
            this.btn_uninstall.Click += new System.EventHandler(this.btn_uninstall_Click);
            // 
            // btn_cancel
            // 
            resources.ApplyResources(this.btn_cancel, "btn_cancel");
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.closeDialog);
            // 
            // ConfigDialog
            // 
            this.AcceptButton = this.btn_ok;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_uninstall);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.comb_linkmode);
            this.Controls.Add(this.lb_linkmode);
            this.Controls.Add(this.txb_linkpath);
            this.Controls.Add(this.lb_linkpath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_linkpath;
        private System.Windows.Forms.TextBox txb_linkpath;
        private System.Windows.Forms.Label lb_linkmode;
        private System.Windows.Forms.ComboBox comb_linkmode;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_uninstall;
        private System.Windows.Forms.Button btn_cancel;
    }
}