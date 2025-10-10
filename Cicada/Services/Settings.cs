using System.IO;

namespace Cicada.Services
{
    public class Settings
    {
        private static IniFile ConfigIni;

        public static Keys VolumeUpKey;
        public static Keys VolumeDownKey;
        public static Keys MuteKey;
        public static Keys IsolateKey;
        public static float increment;

        static Settings()
        {
            var exeDir = AppDomain.CurrentDomain.BaseDirectory;

            ConfigIni = new IniFile(Path.Combine(exeDir, "config.ini"));
            if (!ConfigIni.KeyExists("Increment"))
                ConfigIni.Write("Increment", "2");

            if (!ConfigIni.KeyExists("VolUpKey"))
                ConfigIni.Write("VolUpKey", "Alt + F7");
            if (!ConfigIni.KeyExists("VolDownKey"))
                ConfigIni.Write("VolDownKey", "Alt + F6");
            if (!ConfigIni.KeyExists("VolMuteKey"))
                ConfigIni.Write("VolMuteKey", "Alt + F5");
            if (!ConfigIni.KeyExists("IsolateKey"))
                ConfigIni.Write("IsolateKey", "Alt + M");

            UpdateIncrement();
            UpdateHotkeys();
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
            MessageBox.Show($"Invalid {name} in config.ini. Defaulting to fallback {fallback.ToString()}");
            return fallback;
        }

        public static void UpdateHotkeys()
        {
            VolumeUpKey = ReadKey("VolUpKey", Keys.Alt | Keys.F7);
            VolumeDownKey = ReadKey("VolDownKey", Keys.Alt | Keys.F6);
            MuteKey = ReadKey("VolMuteKey", Keys.Alt | Keys.F5);
            IsolateKey = ReadKey("IsolateKey", Keys.Alt | Keys.M);
        }
    }
}
