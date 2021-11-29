using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Engine.Config {
    class RegistryConfig {
        RegistryKey baseKey = Registry.CurrentUser;
        public RegistryKey BaseKey {
            get => baseKey;
            set {
                baseKey = value.CreateSubKey(rootPath);
            }
        }
        string path = "";
        public string Path {
            get => path;
            set {
                path = value;
            }
        }
        string _rootPath;
        public RegistryConfig(string rootPath) {
            _rootPath = rootPath;
            baseKey = baseKey.CreateSubKey(rootPath);
        }
        public object? GetValue(string key, object? defaultValue = null) {
            return baseKey.GetValue(key, defaultValue);
        }
        public string  GetValueString(string key, string def = "{EMPTY}") {
            var val = GetValue(key, null);
            if (val == null) val = def;
            return (string)val;
        }
        public int     GetValueInt(string key, int def = 0) {
            var val = GetValue(key, null);
            if (val == null | int.TryParse((string)val, out _)) val = def;

            return int.Parse((string) val);
        }
        public bool    GetValueBool(string key, bool def = false) {
            var val = GetValue(key, null);
            if (val == null) val = def.ToString();
            return ((string) val).ToLower() == "true";
        }

        public void    SetValue   (string key, string value, RegistryValueKind kind = RegistryValueKind.String) {
            baseKey.SetValue(key, value, kind);
        }
        public bool    TrySetValue(string key, string value, RegistryValueKind kind = RegistryValueKind.String) {
            try {
                baseKey.SetValue(key, value, kind); return true;
            } catch (Exception) { return false; }
        }
    }
}
