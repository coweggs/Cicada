using System.Windows;
using System.Windows.Media.Imaging;

namespace Cicada
{
    /// <summary>
    /// Interaction logic for IsolateFlyout.xaml
    /// </summary>
    public partial class IsolateFlyout : Window
    {
        public IsolateFlyout()
        {
            InitializeComponent();
        }

        public void SetText(bool allWereMuted)
        {
            VolumeText.Text = allWereMuted ? "Unmuted Others" : "Muted Others";
        }

        public void SetIcon(BitmapSource icon)
        {
            AppIconImage.Source = icon;
        }

        public void SetFlyoutPosition()
        {
            double marginBottom = 12;
            // excludes taskbar
            Rect workingArea = System.Windows.SystemParameters.WorkArea;
            Left = workingArea.Left + (workingArea.Width - Width) / 2;
            Top = workingArea.Bottom - Height - marginBottom;
        }
    }
}
