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

        private SettingsPage Settings;

        public App()
        {
            FlyoutMan = new FlyoutManager();
            TrayMan = new TrayManager();
            AudioMan = new AudioManager(FlyoutMan);
            HotkeyMan = new HotkeyManager(AudioMan);
            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            Settings = new SettingsPage();
            Settings.Show();
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
