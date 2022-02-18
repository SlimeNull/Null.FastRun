using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YamlDotNet.Serialization;
using Null.Faststart.WinForm.View;
using Null.Faststart.WinForm.Util;
using Microsoft.Win32;
using Null.Faststart.Module;
using Null.Faststart.Util;
using NullLib.Faststart;
using YamlDotNet.Serialization.NamingConventions;
using Null.Faststart.WinForm.ViewModule;

namespace Null.Faststart.WinForm
{
    public partial class MainForm : Form
    {
        public static string AppConfigPath { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.yaml");

        public AppConfig Config { get; }
        public BindingList<LinkInfo> LinkList { get; }

        public ISerializer ConfigSerializer { get; } = new SerializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();
        public IDeserializer ConfigDeserializer { get; } = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();

        public MainForm()
        {
            InitializeComponent();

            if (File.Exists(AppConfigPath))
            {
                using StreamReader sr = new StreamReader(AppConfigPath);
                Config = ConfigDeserializer.Deserialize<AppConfig>(sr);
            }
            else
            {
                Config = AppConfig.Default;
                using StreamWriter sw = new StreamWriter(AppConfigPath);
                ConfigSerializer.Serialize(sw, Config);
            }

            LinkList = Config.Links == default ?
                new BindingList<LinkInfo>() :
                new BindingList<LinkInfo>(Config.Links.Select(ln => new LinkInfo(ln.Key, ln.Value)).ToList());
            lv_links.DataSource = LinkList;
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
            ApplyConfig();
            if (CliCall.ApplyConfig(AppConfigPath) == 0)
            {
                MessageBox.Show("All done", "Succeed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Something went wrong, please retry", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ApplyConfig()
        {
            Config.Links = LinkList
                .ToDictionary(ln => ln.Name, ln => ln.Target);
            using (StreamWriter sw = new StreamWriter(AppConfigPath))
            {
                ConfigSerializer.Serialize(sw, Config);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
#if DEBUG
            return;
#endif
            if (!SysUtil.IsAdministrator())
            {
                MessageBox.Show("Please run this application with Admistrator permission.", "Error - Null.Faststart", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
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
    }
}
