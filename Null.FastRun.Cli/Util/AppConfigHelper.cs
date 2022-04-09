using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using NullLib.FastRun;

namespace Null.FastRun.Cli.Util
{
    public static class AppConfigHelper
    {
        public static ISerializer ConfigSerializer { get; } = new SerializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();
        public static IDeserializer ConfigDeserializer { get; } = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();

        public static bool TryLoadConfig(string filename, out AppConfig config)
        {
            try
            {
                using StreamReader sr = new StreamReader(filename);
                config = ConfigDeserializer.Deserialize<AppConfig>(sr);
                return true;
            }
            catch
            {
                config = null;
                return false;
            }
        }

        public static bool TrySaveConfig(string filename, AppConfig config)
        {
            try
            {
                using StreamWriter sw = new StreamWriter(filename);
                ConfigSerializer.Serialize(sw, config);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
