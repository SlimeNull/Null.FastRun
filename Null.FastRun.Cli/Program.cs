using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using System.IO;
using Null.FastRun.Cli.Util;
using NullLib.FastRun;
using CommandLine;

namespace Null.FastRun.Cli
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

        static void Main(string[] args)
        {
            Environment.ExitCode = Parser.Default.ParseArguments<ApplyOption, NewOption, UninstallOption>(args)
                .MapResult(
                    (ApplyOption opt) =>
                    {
                        string filename = opt.Filename;
                        string configdir = Path.GetDirectoryName(filename);
                        if (AppConfigHelper.TryLoadConfig(filename, out AppConfig config))
                        {
                            if (config.Links != null)
                                foreach (string key in config.Links?.Keys.ToArray())
                                {
                                    config.Links[key] = Path.Combine(configdir, config.Links[key]);
#if DEBUG
                                    Console.WriteLine(config.Links[key]);
                                    //Console.ReadKey();
#endif
                                }
                            return CoreManager.ApplyAll(config);
                        }
                        else
                        {
                            Log.Error("Cannot load config");
                            return 1;
                        }
                    },
                    (NewOption opt) =>
                    {
                        string filename = opt.Filename.EndsWith(".yaml", StringComparison.OrdinalIgnoreCase) ? opt.Filename : $"{opt.Filename}.yaml";
                        AppConfig config = AppConfig.Default;
                        config.LinksPath = Path.Combine(config.LinksPath, Path.GetFileName(filename));
                        if (AppConfigHelper.TrySaveConfig(filename, config))
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
                        if (AppConfigHelper.TryLoadConfig(filename, out AppConfig config))
                        {
                            int errc = CoreManager.UninstallAll(config);
                            if (errc == 0)
                            {
                                Log.Info("Config uninstalled");
                                return 0;
                            }
                            else
                            {
                                Log.Error("Connot uninstall config, please retry");
                                return errc;
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
