using Null.Faststart.Module;
using Null.Faststart.Util;
using Null.Faststart.WinForm.Util;
using Null.Faststart.WinForm.ViewModule;
using NullLib.Faststart;
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

namespace Null.Faststart.WinForm.View
{
    public partial class ConfigDialog : Form
    {
        public BindingAppConfig ViewModule { get; }
        public ConfigDialog() : this(new BindingAppConfig(AppConfig.Default)) { }
        public ConfigDialog(BindingAppConfig config)
        {
            InitializeComponent();

            ViewModule = config;

            comb_linkmode.DataSource = new AppConfig.LinkMode[]
            {
                AppConfig.LinkMode.Symbolic,
                AppConfig.LinkMode.Hard
            };

            txb_linkpath.DataBindings.Add(new Binding(nameof(Text), config, nameof(AppConfig.LinksPath)));
            comb_linkmode.DataBindings.Add(new Binding(nameof(ComboBox.SelectedItem), config, nameof(AppConfig.LinksMode)));
        }

        private void closeDialog(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_uninstall_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This acction will remove registry key, link folder, continue?", "Tips", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Exception ex = null;
                try
                {
                    if (CliCall.UninstallConfig(MainForm.AppConfigPath) == 0)
                    {
                        MessageBox.Show("All done", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                catch (Exception _ex) { ex = _ex; }

                MessageBox.Show($"Something went wrong, please retry, {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
