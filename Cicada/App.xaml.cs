using Cicada.Services;
using System.Windows;

namespace Cicada
{
    public partial class App : System.Windows.Application
    {
        private AudioManager AudioMan;
        private TrayManager TrayMan;
        private SettingsManager SettingsMan;
        private HotkeyManager HotkeyMan;
        private FlyoutManager FlyoutMan;

        public App()
        {
            FlyoutMan = new FlyoutManager();
            TrayMan = new TrayManager();
            SettingsMan = new SettingsManager();
            AudioMan = new AudioManager(FlyoutMan);
            HotkeyMan = new HotkeyManager(AudioMan, SettingsMan);
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
