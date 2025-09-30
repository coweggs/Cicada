using System.Windows;
using System.Windows.Media;
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
            SliderBar.Width = 105f/100f * value;
        }

        public void SetIcon(BitmapSource icon)
        {
            AppIconImage.Source = icon;
        }

        private readonly SolidColorBrush unmuted =
            (SolidColorBrush)new BrushConverter().ConvertFromString("#4cc2ff");
        private readonly SolidColorBrush muted =
            (SolidColorBrush)new BrushConverter().ConvertFromString("#e81123");
        public void SetMute(bool isMuted)
        {
            SliderBar.Background = isMuted ? muted : unmuted;
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
