﻿using Null.Faststart.WinForm.Util;
using Null.Faststart.WinForm.ViewModule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Null.Faststart.WinForm.View
{
    public partial class EditLinkDialog : Form
    {
        public EditLinkDialog() : this(new LinkInfo())
        {

        }
        public EditLinkDialog(LinkInfo module)
        {
            InitializeComponent();

            viewModule = module;
            txb_name.DataBindings.DefaultDataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
            txb_target.DataBindings.DefaultDataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;

            txb_name.DataBindings.Add(new Binding(nameof(Text), ViewModule, nameof(ViewModule.Name)));
            txb_target.DataBindings.Add(new Binding(nameof(Text), ViewModule, nameof(ViewModule.Target)));
        }

        private LinkInfo viewModule;
        public LinkInfo ViewModule => viewModule;
        private void btn_ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        static FolderBrowserDialog StaticFolderBrowserDialog { get; } = new FolderBrowserDialog()
        {
            Description = "Choose a folder or file",
            ShowNewFolderButton = true,
        };

        private void lklb_open_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (StaticFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                viewModule.Target = StaticFolderBrowserDialog.SelectedPath;
            }
        }
    }
}
