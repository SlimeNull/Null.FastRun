﻿using Null.FastRun.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Null.FastRun.View
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
        }

        private void lnlb_github_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(lnlb_github.Text);
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
