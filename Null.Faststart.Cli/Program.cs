using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using Null.Faststart.Module;
using System.IO;
using Null.Faststart.Cli.Util;
using NullLib.Faststart;
using CommandLine;

namespace Null.Faststart.Cli
{
    internal class Program
    {

        [Verb("new", HelpText = "Create new config file with default content")]
        internal class NewOption
        {
            [Value(0, HelpText = "Create new config file with default content")]
            public string Filename { get; set; }
        }

        [Verb("apply", HelpText = "Apply a config file")]
        internal class ApplyOption
        {
            [Value(0, HelpText = "Filename of config file")]
            public string Filename { get; set; }
        }

        [Verb("uninstall", HelpText = "Uninstall a config from this pc")]
        internal class UninstallOption
        {
            [Value(0, HelpText = "Filename of config file for uninstalling")]
            public string Filename { get; set; }
        }

        public static ISerializer ConfigSerializer { get; } = new SerializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();
        public static IDeserializer ConfigDeserializer { get; } = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();

        static bool TryLoadConfig(string filename, out AppConfig config)
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

        static bool TrySaveConfig(string filename, AppConfig config)
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

        static void Main(string[] args)
        {
            Environment.ExitCode = Parser.Default.ParseArguments<ApplyOption, NewOption, UninstallOption>(args)
                .MapResult(
                    (ApplyOption opt) =>
                    {
                        string filename = opt.Filename;
                        if (TryLoadConfig(filename, out AppConfig config))
                        {
                            CoreManager.ApplyAll(config);
                            return 0;
                        }
                        else
                        {
                            Log.Error("Cannot load config");
                            return 1;
                        }
                    },
                    (NewOption opt) =>
                    {
                        string filename = opt.Filename;
                        if (TrySaveConfig($"{filename}.yaml", AppConfig.Default))
                        {
                            Log.Info("New config file created");
                            return 0;
                        }
                        else
                        {
                            Log.Error("Cannot write config");
                            return 1;
                        }
                    },
                    (UninstallOption opt) =>
                    {
                        string filename = opt.Filename;
                        if (TryLoadConfig(filename, out AppConfig config))
                        {
                            if (CoreManager.UninstallAll(config) == 0)
                            {
                                Log.Info("Config uninstalled");
                                return 0;
                            }
                            else
                            {
                                Log.Error("Connot uninstall config, please retry");
                                return 1;
                            }
                        }
                        else
                        {
                            Log.Error("Cannot load config");
                            return 1;
                        }
                    },
                    errs => -1);
        }
    }
}
