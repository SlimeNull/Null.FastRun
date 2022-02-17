using Null.Faststart.Module;
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
    }
}
