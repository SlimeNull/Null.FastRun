using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Null.FastRun.Util
{
    internal static class CliCall
    {
        public const string CliAppName = "Null.Faststart.Cli.exe";
        public static int ApplyConfig(string filename)
        {
            if (!File.Exists(filename))
                return 1;
            Process newproc = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = Path.Combine(AppContext.BaseDirectory, CliAppName),
                    Arguments = $"apply \"{filename}\"",
                    UseShellExecute = true,
                    CreateNoWindow = true,
                }
            };

            if (!newproc.Start())
                return 1;
            newproc.WaitForExit();
            return newproc.ExitCode;
        }
        public static int UninstallConfig(string filename)
        {
            if (!File.Exists(filename))
                return 1;
            Process newproc = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = Path.Combine(AppContext.BaseDirectory, CliAppName),
                    Arguments = $"uninstall \"{filename}\"",
                    UseShellExecute = true,
                    CreateNoWindow = true,
                }
            };
            if (!newproc.Start())
                return 1;
            newproc.WaitForExit();
            return newproc.ExitCode;
        }
    }
}
