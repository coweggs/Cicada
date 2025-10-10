using Microsoft.Win32;
using System.IO;

namespace Cicada.Services
{
    public class Settings
    {
        private static IniFile ConfigIni;

        private const string CONFIG_INI = "config.ini";
        private const string REG_KEY_NAME = "Cicada";

        public static string INCREMENT = "Increment";
        public static string VOLUME_UP = "VolUpKey";
        public static string VOLUME_DOWN = "VolDownKey";
        public static string VOLUME_MUTE = "VolMuteKey";
        public static string ISOLATE = "IsolateKey";

        public static Keys VolumeUpKey;
        public static Keys VolumeDownKey;
        public static Keys MuteKey;
        public static Keys IsolateKey;
        public static float increment;

        static Settings()
        {
            UpdateStartupEntry(IsRunOnStartupEnabled()); // refresh registry path on launch

            // setup config.ini
            string exeDir = AppDomain.CurrentDomain.BaseDirectory;
            ConfigIni = new IniFile(Path.Combine(exeDir, CONFIG_INI));
            if (!ConfigIni.KeyExists(INCREMENT))
                ConfigIni.Write(INCREMENT, "2");
            if (!ConfigIni.KeyExists(VOLUME_UP))
                ConfigIni.Write(VOLUME_UP, "Alt + F7");
            if (!ConfigIni.KeyExists(VOLUME_DOWN))
                ConfigIni.Write(VOLUME_DOWN, "Alt + F6");
            if (!ConfigIni.KeyExists(VOLUME_MUTE))
                ConfigIni.Write(VOLUME_MUTE, "Alt + F5");
            if (!ConfigIni.KeyExists(ISOLATE))
                ConfigIni.Write(ISOLATE, "Alt + M");

            // setup global variables
            UpdateIncrement();
            UpdateHotkeys();
        }

        public static string ReadField(string field)
        {
            return ConfigIni.Read(field);
        }

        public static void UpdateField(string field, string? value)
        {
            if (value == null)
                return;
            ConfigIni.Write(field, value);
        }

        public static bool IsRunOnStartupEnabled()
        {
            RegistryKey? key = Registry.CurrentUser.OpenSubKey
                (@"Software\Microsoft\Windows\CurrentVersion\Run", false);
            return key?.GetValue(REG_KEY_NAME) != null;
        }

        public static void UpdateStartupEntry(bool? enabled)
        {
            if (enabled == null)
                return;
            RegistryKey? key = Registry.CurrentUser.OpenSubKey
                (@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            if (enabled == true) // nullable bool needs the == true check
            {
                key?.SetValue(REG_KEY_NAME, Application.ExecutablePath);
            }
            else
            {
                key?.DeleteValue(REG_KEY_NAME, false);
            }
        }

        public static void UpdateIncrement()
        {
            increment = float.TryParse(ConfigIni.Read("Increment"), out var x) ? x : 2f;
        }

        private static Keys ReadKey(string name, Keys fallback)
        {
            Keys result = Keys.None;
            string raw = ConfigIni.Read(name).Replace(" ", "");
            string[] split = raw.Split("+");
            if (split.Length > 0)
            {
                foreach (string s in split)
                {
                    if (Enum.TryParse(s, out Keys k))
                    {
                        result |= k;
                    }
                }
                if (result != Keys.None)
                    return result;
            }
            MessageBox.Show($"Invalid {name} in {CONFIG_INI}. Defaulting to fallback {fallback.ToString()}");
            return fallback;
        }

        public static void UpdateHotkeys()
        {
            VolumeUpKey = ReadKey(VOLUME_UP, Keys.Alt | Keys.F7);
            VolumeDownKey = ReadKey(VOLUME_DOWN, Keys.Alt | Keys.F6);
            MuteKey = ReadKey(VOLUME_MUTE, Keys.Alt | Keys.F5);
            IsolateKey = ReadKey(ISOLATE, Keys.Alt | Keys.M);
        }
    }
}
