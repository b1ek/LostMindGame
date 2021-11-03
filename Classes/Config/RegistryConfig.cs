using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Classes.Config
{
    static class RegistryConfig
    {
        public static RegistryKey root = Registry.CurrentUser.CreateSubKey("SOFTWARE").CreateSubKey("blek").CreateSubKey("LostMind");
        //                               HKEY_CURRENT_USER/SOFTWARE/blek/LostMind

        public static bool startGameWithoutBootloader {
            get {
                var val = root.GetValue("startGameWithoutBootload", false);
                if (val == null) return false;
                return val.ToString().ToLower() == "true";
            }
        }
        public static bool noStartupLogoAnim {
            get {
                var val = root.GetValue("noStartupLogoAnim", false);
                if (val == null) return false;
                return val.ToString().ToLower() == "true";
            }
        }
        public static bool AllowBSODStyleException {
            get {
                var val = root.GetValue("AllowBSODStyleException", false);
                if (val == null) return false;
                return val.ToString().ToLower() == "true";
            }
        }
        public static string customGameTitle {
            get {
                var val = root.GetValue("customGameTitle", null);
                if (val == null) return null;
                return Convert.ToString(val);
            }
        }
    }
}
