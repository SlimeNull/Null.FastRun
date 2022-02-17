using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Null.Faststart.Util
{
    public static class SysUtil
    {
        public static bool IsAdministrator()
        {
            WindowsIdentity current = WindowsIdentity.GetCurrent();
            WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);
            //WindowsBuiltInRole可以枚举出很多权限，例如系统用户、User、Guest等等
            return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private static RegistryKey? OpenSystemEnvironmentVariables()
        {
            return Registry.LocalMachine
                ?.OpenSubKey("SYSTEM")
                ?.OpenSubKey("CurrentControlSet")
                ?.OpenSubKey("Control")
                ?.OpenSubKey("Session Manager")
                ?.OpenSubKey("Environment", true);
        }

        public static bool CanAccessSystemEnvironmentVariables()
        {
            try
            {
                RegistryKey? sysEnvKey = OpenSystemEnvironmentVariables();
                return sysEnvKey != null;
            }
            catch { return false; }
        }
        public static void SetSystemEnvironmentVariable(string name, string value)
        {
            RegistryKey envRegKey = OpenSystemEnvironmentVariables() ?? throw new Exception("Cannot open registry key.");
            envRegKey.SetValue(name, value);
        }
        public static string GetSystemEnvironmentVariable(string name)
        {
            RegistryKey envRegKey = OpenSystemEnvironmentVariables() ?? throw new Exception("Cannot open registry key.");
            return envRegKey.GetValue(name).ToString();
        }
    }
}
