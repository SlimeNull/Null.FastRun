using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullLib.FastRun
{
    public class AppConfig : ICloneable
    {
        public enum LinkMode
        {
            Shortcut,
            Symbolic,
            Hard,
        }
        public static AppConfig Default { get; } = new AppConfig()
        {
            LinksPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "NFastTool")
        };

        public string? LinksPath { get; set; }
        public LinkMode LinksMode { get; set; }
        public Dictionary<string, string>? Links { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
