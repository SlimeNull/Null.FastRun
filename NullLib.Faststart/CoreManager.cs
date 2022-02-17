using Null.Faststart.Module;
using Null.Faststart.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NullLib.Faststart
{
    public static class CoreManager
    {
        public static void ApplyAll(AppConfig config)
        {
            ApplyLinks(config);
            ApplySysPath(config);
        }

        public static void ApplyLinks(AppConfig config)
        {
            DirectoryInfo basedir = new DirectoryInfo(config.LinksPath);
            if (!basedir.Exists)
                basedir.Create();
            foreach (FileInfo file in basedir.EnumerateFiles())
                file.Delete();
            foreach (DirectoryInfo dir in basedir.EnumerateDirectories())
                dir.Delete();

            Dictionary<string, string> links = config.Links ?? new Dictionary<string, string>(0);
            if (config.LinksMode == AppConfig.LinkMode.Symbolic)
            {
                foreach (string linkName in links.Keys)
                {
                    string linkTarget = links[linkName];
                    string linkFileName = Path.Combine(config.LinksPath, linkName);
                    FileInfo file = new FileInfo(linkTarget);
                    if (file.Exists)
                    {
                        WinAPI.CreateSymbolicLink(linkFileName, file.FullName, 0);
                    }
                    else
                    {
                        DirectoryInfo dir = new DirectoryInfo(linkTarget);
                        if (dir.Exists)
                        {
                            WinAPI.CreateSymbolicLink(linkFileName, file.FullName, WinAPI.SYMBOLIC_LINK_FLAG_DIRECTORY);
                        }
                    }
                }
            }
            else if (config.LinksMode != AppConfig.LinkMode.Hard)
            {
                foreach (string linkName in links.Keys)
                {
                    string linkTarget = links[linkName];
                    string linkFileName = Path.Combine(config.LinksPath, linkName);
                    FileInfo file = new FileInfo(linkTarget);
                    if (file.Exists)
                    {
                        WinAPI.CreateHardLink(linkFileName, file.FullName, IntPtr.Zero);
                    }
                }
            }
        }

        public static void ApplySysPath(AppConfig config)
        {
            if (SysUtil.CanAccessSystemEnvironmentVariables())
            {
                string pathEnv = SysUtil.GetSystemEnvironmentVariable("Path");
                string[] pathDirs = pathEnv.Split(';');
                if (!pathDirs.Contains(config.LinksPath))
                {
                    SysUtil.SetSystemEnvironmentVariable("_PATH", pathEnv);
                    SysUtil.SetSystemEnvironmentVariable("Path", string.Join(";", pathDirs.Append(config.LinksPath)));
                }
            }
        }
    }
}