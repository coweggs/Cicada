using Cicada.Helpers;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;

namespace Cicada.Services
{
    internal class FlyoutManager
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        private const int FlyoutDelayMs = 2000;
        private System.Threading.Timer timer;

        private Flyout flyout;
        private IsolateFlyout isolateFlyout;

        public FlyoutManager()
        {
            flyout = new Flyout();
            flyout.ShowInTaskbar = false;
            flyout.Topmost = true;
            flyout.Focusable = false;
            isolateFlyout = new IsolateFlyout();
            isolateFlyout.ShowInTaskbar = false;
            isolateFlyout.Topmost = true;
            isolateFlyout.Focusable = false;

            timer = new System.Threading.Timer(_ =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    flyout.Hide();
                    isolateFlyout.Hide();
                });
            }, null, Timeout.Infinite, Timeout.Infinite); // start stopped
        }

        public void ShowFlyout(float level, bool muted, uint pId)
        {
            isolateFlyout.Hide();
            flyout.SetVolumeText(((int)(level * 100)).ToString());
            flyout.SetSliderValue(level * 100);
            flyout.SetMute(muted);
            flyout.SetFlyoutPosition();
            BitmapSource icon = ForegroundIconHelper.GetForegroundWindowIcon();
            flyout.SetIcon(icon);
            flyout.Show();
            // start/reset timer
            timer.Change(FlyoutDelayMs, Timeout.Infinite);
        }

        public void ShowIsolateFlyout(bool allWereMuted, uint pId)
        {
            flyout.Hide();
            isolateFlyout.SetText(allWereMuted);
            isolateFlyout.SetFlyoutPosition();
            BitmapSource icon = ForegroundIconHelper.GetForegroundWindowIcon();
            isolateFlyout.SetIcon(icon);
            isolateFlyout.Show();
            // switch user focus back to foreground pId
            SetForegroundWindow(System.Diagnostics.Process.GetProcessById((int)pId).Handle);
            // start/reset timer
            timer.Change(FlyoutDelayMs, Timeout.Infinite);
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
