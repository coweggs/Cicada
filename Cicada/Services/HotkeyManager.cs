using Gma.System.MouseKeyHook;

namespace Cicada.Services
{
    internal class HotkeyManager
    {
        private readonly IKeyboardMouseEvents GlobalHook;
        private readonly AudioManager AudioMan;

        private Keys Mod;
        private Keys VolumeUpKey;
        private Keys VolumeDownKey;
        private Keys MuteKey;
        private Keys IsolateKey;
        private float increment;

        public HotkeyManager(AudioManager audioManager, SettingsManager settingsManager)
        {
            GlobalHook = Hook.GlobalEvents();
            AudioMan = audioManager;

            // get settings
            Mod = settingsManager.GetModKey();
            VolumeUpKey = settingsManager.GetVolUpKey();
            VolumeDownKey = settingsManager.GetVolDownKey();
            MuteKey = settingsManager.GetVolMuteKey();
            IsolateKey = settingsManager.GetIsolateKey();
            increment = settingsManager.GetIncrement() / 100;

            // register event
            GlobalHook.KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Modifiers == Mod)
            {
                if (e.KeyCode == VolumeUpKey)
                {
                    AudioMan.IncreaseVolume(increment);
                }
                else if (e.KeyCode == VolumeDownKey)
                {
                    AudioMan.DecreaseVolume(increment);
                }
                else if (e.KeyCode == MuteKey)
                {
                    AudioMan.ToggleSessionMute();
                }
                else if (e.KeyCode == IsolateKey)
                {
                    AudioMan.IsolateSession();
                }
            }
        }

        public void Dispose()
        {
            GlobalHook.Dispose();
        }
    }
}
