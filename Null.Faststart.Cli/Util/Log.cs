using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Null.Faststart.Cli.Util
{
    internal static class Log
    {
        private static void Str(string prefix, object content)
        {
            Console.WriteLine($"[{prefix}] {content}");
        }
        public static void Info(object content) => Str("INFO", content);
        public static void Warn(object content) => Str("WARN", content);
        public static void Error(object content) => Str("ERROR", content);
    }
}
