using Null.Faststart.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Null.Faststart.ViewModule
{
    public class BindingAppConfig : AppConfig, INotifyPropertyChanged
    {
        public BindingAppConfig() { }
        public BindingAppConfig(AppConfig config)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            (LinksPath, LinksMode, Links) = (config.LinksPath, config.LinksMode, config.Links);
        }

        public AppConfig GetValue() => new()
        {
            Links = Links,
            LinksMode = LinksMode,
            LinksPath = LinksPath,
        };

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
