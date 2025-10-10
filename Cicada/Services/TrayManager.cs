using Cicada.Flyouts;
using System.Diagnostics;
using System.IO;

namespace Cicada.Services
{
    public class TrayManager
    {
        public readonly NotifyIcon TrayIcon;
        private readonly SettingsWindow settingsWindow;

        public TrayManager(SettingsWindow _settingsWindow)
        {
            settingsWindow = _settingsWindow;

            // tray icon
            TrayIcon = new NotifyIcon();
            TrayIcon.Visible = true;

            var exeDir = AppDomain.CurrentDomain.BaseDirectory;
            TrayIcon.Icon = new Icon(Path.Combine(exeDir, "cicada.ico"));

            TrayIcon.Text = "Cicada";

            TrayIcon.ContextMenuStrip = new ContextMenuStrip();
            TrayIcon.ContextMenuStrip.Items.Add("Settings", null, Settings_Clicked);
            TrayIcon.ContextMenuStrip.Items.Add("Exit", null, Exit_Clicked);
        }

        private void Settings_Clicked(object? sender, EventArgs e)
        {
            settingsWindow.Show();
            settingsWindow.Activate();
        }

        private void Exit_Clicked(object? sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        public void Dispose()
        {
            TrayIcon.Dispose();
        }
    }
}
