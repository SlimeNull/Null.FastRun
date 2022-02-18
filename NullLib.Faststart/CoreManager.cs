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
        public static int ApplyAll(AppConfig config)
        {
            return
                ApplyLinks(config) +
                ApplySysPath(config);
        }

        public static int UninstallAll(AppConfig config)
        {
            return
                UninstallLinks(config) +
                UninstallSysPath(config);
        }

        public static int ApplyLinks(AppConfig config)
        {
            int errors = 0;

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
                        if (!WinAPI.CreateSymbolicLink(linkFileName, file.FullName, 0))
                            errors++;
                    }
                    else
                    {
                        DirectoryInfo dir = new DirectoryInfo(linkTarget);
                        if (dir.Exists)
                        {
                            if (!WinAPI.CreateSymbolicLink(linkFileName, file.FullName, WinAPI.SYMBOLIC_LINK_FLAG_DIRECTORY))
                                errors++;
                        }
                        else
                        {
                            errors++;
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
                        if (!WinAPI.CreateHardLink(linkFileName, file.FullName, IntPtr.Zero))
                            errors++;
                    }
                    else
                    {
                        errors++;
                    }
                }
            }
            else
            {
                errors++;
            }

            return errors;
        }

        public static int UninstallLinks(AppConfig config)
        {
            int errors = 0;

            if (config.LinksPath != null)
            {
                DirectoryInfo dir = new(config.LinksPath);
                if (dir.Exists)
                    dir.Delete(true);
            }
            else
            {
                errors++;
            }

            return errors;
        }

        public static int ApplySysPath(AppConfig config)
        {
            int errors = 0;
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
            else
            {
                errors++;
            }

            return errors;
        }

        public static int UninstallSysPath(AppConfig config)
        {
            int errors = 0;

            if (SysUtil.CanAccessSystemEnvironmentVariables())
            {
                string pathStr = SysUtil.GetSystemEnvironmentVariable("Path");
                string[] paths = pathStr.Split(';');
                string newPathStr = string.Join(";", paths.Where(s => s != config.LinksPath));
                SysUtil.SetSystemEnvironmentVariable("Path", newPathStr);
            }
            else
            {
                errors++;
            }

            return errors;
        }
    }
}