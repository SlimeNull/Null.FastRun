using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Null.FastRun.Cli.Util;
using Null.FastRun.View;
using Null.FastRun.Util;
using Null.FastRun.ViewModule;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using NullLib.FastRun;

namespace Null.FastRun
{
    public partial class MainForm : Form
    {
        private AppConfig config;

        public static string AppConfigPath { get; private set; }

        static MainForm()
        {
            AppConfigPath = Program.Args.Length == 0 || !Program.Args[0].EndsWith(".yaml") || !File.Exists(Program.Args[0]) ?
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.yaml") :
                Program.Args[0];
        }

        public AppConfig Config
        {
            get => config;
            private set
            {
                config = value;
                if (LinkList is BindingList<LinkInfo> linklist && config.Links is Dictionary<string, string>)
                {
                    linklist.Clear();
                    foreach (var link in config.Links)
                    {
                        linklist.Add(new LinkInfo(link.Key, link.Value));
                    }
                }
            }
        }
        public BindingList<LinkInfo> LinkList { get; private set; }

        public ISerializer ConfigSerializer { get; } = new SerializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();
        public IDeserializer ConfigDeserializer { get; } = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();

        public MainForm()
        {
            InitializeComponent();

            if (AppConfigHelper.TryLoadConfig(AppConfigPath, out AppConfig config))
            {
                Config = config;
            }
            else
            {
                Config = AppConfig.Default;
                if (!AppConfigHelper.TrySaveConfig(AppConfigPath, Config))
                {
                    MessageBox.Show("Cannot load config or create config", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(-1);
                }
            }

            LoadModel();
        }

        private void LoadModel()
        {
            if (Config.Links == default)
            {
                Config.Links = new Dictionary<string, string>();
            }

            LinkList = new BindingList<LinkInfo>(Config.Links.Select(ln => new LinkInfo(ln.Key, ln.Value)).ToList());
            lv_links.DataSource = LinkList;
        }
        private void UpdateModel()
        {
            Config.Links = LinkList.ToDictionary(ln => ln.Name, ln => ln.Target);
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            EditLinkDialog dlg = new EditLinkDialog();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                LinkList.Add(dlg.ViewModule);
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            foreach (int i in lv_links.SelectedRows.Cast<DataGridViewRow>().Select(v => v.Index))
            {
                EditLinkDialog dlg = new EditLinkDialog(LinkList[i]);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    LinkList[i] = dlg.ViewModule;
                }
            }
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = lv_links.SelectedRows;
            if (selectedRows.Count > 0)
            {
                LinkList.RemoveAt(selectedRows[0].Index);
            }
        }

        private void btn_apply_all_Click(object sender, EventArgs e)
        {
            UpdateModel();
            if (AppConfigHelper.TrySaveConfig(AppConfigPath, Config))
            {
                if (CliCall.ApplyConfig(AppConfigPath) == 0)
                {
                    MessageBox.Show("All done", "Succeed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Something went wrong, please retry", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Cannot save config", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_about_Click(object sender, EventArgs e)
        {
            new AboutDialog().ShowDialog(this);
        }

        private void lv_links_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ?
                DragDropEffects.Link :
                DragDropEffects.None;
        }

        private void lv_links_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
                foreach (string file in files)
                {
                    LinkList.Add(new LinkInfo(Path.GetFileNameWithoutExtension(file), Path.GetFullPath(file)));
                }
            }
        }

        private void lv_links_DoubleClick(object sender, EventArgs e)
        {
            if (lv_links.SelectedRows.Count == 0 || lv_links.SelectedColumns.Count == 0)
                return;
            int selectedRowIndex = lv_links.SelectedRows[0].Index;
            if (selectedRowIndex >= LinkList.Count)
                return;
            int selectedColumnIndex = lv_links.SelectedColumns[0].Index;
            if (selectedColumnIndex != 1)
                return;
            using FolderBrowserDialog dlg = new()
            {
                Description = "Choose folder or file",
                ShowNewFolderButton = true,
            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                LinkList[selectedRowIndex].Target = dlg.SelectedPath;
            }
        }

        private void btn_config_Click(object sender, EventArgs e)
        {
            ConfigDialog dlg = new ConfigDialog(new BindingAppConfig(Config));
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Config.LinksPath = dlg.ViewModule.LinksPath;
                Config.LinksMode = dlg.ViewModule.LinksMode;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            UpdateModel();
            if (!AppConfigHelper.TrySaveConfig(AppConfigPath, Config))
                MessageBox.Show("Cannot save config", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if (e.Data.GetData(DataFormats.FileDrop) is string[] files && files.Length > 0)
                {
                    if (files[0].EndsWith(".yaml"))
                    {
                        e.Effect = DragDropEffects.Copy;
                    }
                }
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if (e.Data.GetData(DataFormats.FileDrop) is string[] files && files.Length > 0)
                {
                    string filename = files[0];
                    if (AppConfigHelper.TryLoadConfig(filename, out AppConfig config))
                    {
                        AppConfigPath = filename;
                        Config = config;
                    }
                    else
                    {
                        MessageBox.Show("Invalid format of config file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Open config";
            openFileDialog.Filter = "FTool config|*.yaml";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (AppConfigHelper.TryLoadConfig(openFileDialog.FileName, out var loadedConfig))
                {
                    Config = loadedConfig;

                }
            }
        }
    }
}
