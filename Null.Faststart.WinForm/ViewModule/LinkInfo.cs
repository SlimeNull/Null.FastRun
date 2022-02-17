using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Null.Faststart.WinForm.ViewModule
{
    public class LinkInfo : INotifyPropertyChanged, ICloneable
    {
        public LinkInfo() { }
        public LinkInfo(string name, string target)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            if (string.IsNullOrEmpty(target))
            {
                throw new ArgumentException($"'{nameof(target)}' cannot be null or empty.", nameof(target));
            }

            (Name, Target) = (name, target);
        }
        public string Name { get; set; }
        public string Target { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
