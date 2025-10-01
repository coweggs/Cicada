using System.Windows;
using System.Windows.Media.Imaging;

namespace Cicada
{
    /// <summary>
    /// Interaction logic for Flyout.xaml
    /// </summary>
    public partial class Flyout : Window
    {
        public Flyout()
        {
            InitializeComponent();
        }

        public void SetVolumeText(string text)
        {
            VolumeText.Text = text;
        }

        public void SetSliderValue(float value)
        {
            SliderBar.Width = 105f / 100f * value;
        }

        public void SetIcon(BitmapSource icon)
        {
            AppIconImage.Source = icon;
        }

        public void SetMute(bool isMuted)
        {
            AppIconImage.Opacity = isMuted ? 0.2f : 1.0f;
            MuteIcon.Opacity = isMuted ? 1.0f : 0.0f;
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
