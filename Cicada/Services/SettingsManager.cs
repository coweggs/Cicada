namespace Cicada.Services
{
    internal class SettingsManager
    {
        private IniFile ConfigIni;

        public SettingsManager()
        {
            ConfigIni = new IniFile("config.ini");
            if (!ConfigIni.KeyExists("Increment"))
                ConfigIni.Write("Increment", "2");
            if (!ConfigIni.KeyExists("RunOnStartup"))
                ConfigIni.Write("RunOnStartup", "False");

            if (!ConfigIni.KeyExists("ModKey"))
                ConfigIni.Write("ModKey", "Alt");

            if (!ConfigIni.KeyExists("VolUpKey"))
                ConfigIni.Write("VolUpKey", "F7");
            if (!ConfigIni.KeyExists("VolDownKey"))
                ConfigIni.Write("VolDownKey", "F6");
            if (!ConfigIni.KeyExists("VolMuteKey"))
                ConfigIni.Write("VolMuteKey", "F5");
            if (!ConfigIni.KeyExists("IsolateKey"))
                ConfigIni.Write("IsolateKey", "M");
        }

        public float GetIncrement()
            => float.TryParse(ConfigIni.Read("Increment"), out var x) ? x : 2f;
        public bool GetRunOnStartup()
            => bool.TryParse(ConfigIni.Read("RunOnStartup"), out var x) && x;

        private Keys ReadKey(string name, Keys fallback)
        {
            if (Enum.TryParse(ConfigIni.Read(name), out Keys k))
            {
                return k;
            }
            else
            {
                MessageBox.Show($"Invalid {name} in config.ini. Defaulting to fallback {fallback.ToString()}");
                return fallback;
            }
        }
        //
        public Keys GetModKey() => ReadKey("ModKey", Keys.Alt);
        public Keys GetVolUpKey() => ReadKey("VolUpKey", Keys.F7);
        public Keys GetVolDownKey() => ReadKey("VolDownKey", Keys.F6);
        public Keys GetVolMuteKey() => ReadKey("VolMuteKey", Keys.F5);
        public Keys GetIsolateKey() => ReadKey("IsolateKey", Keys.F8);

    }
}
