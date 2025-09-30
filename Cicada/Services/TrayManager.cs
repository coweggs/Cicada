using Microsoft.Win32;

namespace Cicada.Services
{
    internal class TrayManager
    {
        private const string RegKeyName = "Cicada";

        private readonly NotifyIcon TrayIcon;

        public TrayManager()
        {
            TrayIcon = new System.Windows.Forms.NotifyIcon();

            // tray icon
            TrayIcon.Visible = true;

            TrayIcon.Icon = new System.Drawing.Icon("cicada.ico");
            TrayIcon.Text = "Cicada";

            TrayIcon.ContextMenuStrip = new ContextMenuStrip();
            bool startupEnabled = IsRunOnStartupEnabled();
            ToolStripMenuItem RunOnStartupItem = new ToolStripMenuItem()
            {
                Text = "Run on Startup",
                Checked = startupEnabled,
                CheckOnClick = false
            };
            RunOnStartupItem.Click += (s, e) =>
            {
                RunOnStartupItem.Checked = !RunOnStartupItem.Checked;
                SetRunOnStartup(RunOnStartupItem.Checked);
            };
            TrayIcon.ContextMenuStrip.Items.Add(RunOnStartupItem);
            TrayIcon.ContextMenuStrip.Items.Add("Exit", null, Exit_Clicked);
        }

        private bool IsRunOnStartupEnabled()
        {
            RegistryKey? key = Registry.CurrentUser.OpenSubKey
                (@"Software\Microsoft\Windows\CurrentVersion\Run", false);
            return key?.GetValue(RegKeyName) != null;
        }

        private void SetRunOnStartup(bool enabled)
        {
            RegistryKey? key = Registry.CurrentUser.OpenSubKey
                (@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            if (enabled)
            {
                key?.SetValue(RegKeyName, Application.ExecutablePath);
            }
            else
            {
                key?.DeleteValue(RegKeyName, false);
            }
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
