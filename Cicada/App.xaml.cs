using Cicada.Flyouts;
using Cicada.Services;
using System.Windows;

namespace Cicada
{
    public partial class App : System.Windows.Application
    {
        private AudioManager AudioMan;
        private TrayManager TrayMan;
        private HotkeyManager HotkeyMan;
        private FlyoutManager FlyoutMan;

        private SettingsWindow Settings;

        public App()
        {
            Settings = new SettingsWindow();
            Settings.Show();

            FlyoutMan = new FlyoutManager();
            TrayMan = new TrayManager(Settings);
            AudioMan = new AudioManager(FlyoutMan);
            HotkeyMan = new HotkeyManager(AudioMan);
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            TrayMan.Dispose();
            HotkeyMan.Dispose();
            FlyoutMan.Dispose();

            base.OnExit(e);
        }
    }

}
