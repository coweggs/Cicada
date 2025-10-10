using Gma.System.MouseKeyHook;

namespace Cicada.Services
{
    internal class HotkeyManager
    {
        private readonly IKeyboardMouseEvents GlobalHook;
        private readonly AudioManager AudioMan;

        public HotkeyManager(AudioManager _audioManager)
        {
            GlobalHook = Hook.GlobalEvents();
            AudioMan = _audioManager;

            // register event
            GlobalHook.KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyData == Settings.VolumeUpKey)
            {
                AudioMan.IncreaseVolume(Settings.increment / 100);
            }
            else if (e.KeyData == Settings.VolumeDownKey)
            {
                AudioMan.DecreaseVolume(Settings.increment / 100);
            }
            else if (e.KeyData == Settings.MuteKey)
            {
                AudioMan.ToggleSessionMute();
            }
            else if (e.KeyData == Settings.IsolateKey)
            {
                AudioMan.IsolateSession();
            }
        }

        public void Dispose()
        {
            GlobalHook.Dispose();
        }
    }
}
