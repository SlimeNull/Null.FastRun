using IWshRuntimeLibrary;
using NullLib.FastRun.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace NullLib.FastRun
{
    public static class CoreManager
    {
#if DEBUG
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);
#endif
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
#if DEBUG
            MessageBox(IntPtr.Zero, "Applying links", "Faststart", 0);
#endif

            DirectoryInfo basedir = new DirectoryInfo(config.LinksPath);
            if (!basedir.Exists)
                basedir.Create();
            foreach (FileInfo file in basedir.EnumerateFiles())
                file.Delete();
            foreach (DirectoryInfo dir in basedir.EnumerateDirectories())
                dir.Delete();

            Dictionary<string, string> links = config.Links ?? new Dictionary<string, string>(0);
            if (config.LinksMode == AppConfig.LinkMode.Shortcut)
            {
                IWshShell wsh = new WshShell();
                foreach (string linkName in links.Keys)
                {
                    string linkTarget = links[linkName];
                    string linkFileName = Path.Combine(config.LinksPath, linkName);
                    if (!linkFileName.EndsWith(".lnk", StringComparison.OrdinalIgnoreCase))
                        linkFileName += ".lnk";
                    FileInfo file = new FileInfo(linkTarget);
                    if (file.Exists)
                    {
                        try
                        {
                            IWshShortcut? shortcut = wsh.CreateShortcut(linkFileName) as IWshShortcut;
                            if (shortcut != null)
                            {
                                shortcut.TargetPath = file.FullName;
                                shortcut.Save();
#if DEBUG
                                MessageBox(IntPtr.Zero, $"Created shortcut {linkFileName}", "Faststart", 0);
#endif
                            }
                            else
                            {
#if DEBUG
                                MessageBox(IntPtr.Zero, "Failed to create shortcut: " + linkFileName, "Error", 0);
#endif
                                errors++;
                            }
                        }
                        catch
                        {
#if DEBUG
                            MessageBox(IntPtr.Zero, "Failed to create shortcut: " + linkFileName, "Error", 0);
#endif
                            errors++;
                        }
                    }
                    else
                    {
                        DirectoryInfo dir = new DirectoryInfo(linkTarget);
                        if (dir.Exists)
                        {
                            try
                            {
                                IWshShortcut? shortcut = wsh.CreateShortcut(linkFileName) as IWshShortcut;
                                if (shortcut != null)
                                {
                                    shortcut.TargetPath = dir.FullName;
                                    shortcut.Save();
#if DEBUG
                                    MessageBox(IntPtr.Zero, $"Created shortcut {linkFileName}", "Faststart", 0);
#endif
                                }
                                else
                                {
#if DEBUG
                                    MessageBox(IntPtr.Zero, "Failed to create shortcut: " + linkFileName, "Error", 0);
#endif
                                    errors++;
                                }
                            }
                            catch
                            {
#if DEBUG
                                MessageBox(IntPtr.Zero, "Failed to create shortcut: " + linkFileName, "Error", 0);
#endif
                                errors++;
                            }
                        }
                        else
                        {
                            errors++;
                        }
                    }
                }
            }
            else if (config.LinksMode == AppConfig.LinkMode.Symbolic)
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