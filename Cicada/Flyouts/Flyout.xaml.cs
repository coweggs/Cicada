using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Cicada
{
    /// <summary>
    /// Interaction logic for Flyout.xaml
    /// </summary>
    public partial class Flyout : Window
    {
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_NOACTIVATE = 0x08000000;

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        public Flyout()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // set the window style to noactivate.
            var helper = new WindowInteropHelper(this);
            SetWindowLong(helper.Handle, GWL_EXSTYLE,
                GetWindowLong(helper.Handle, GWL_EXSTYLE) | WS_EX_NOACTIVATE);
        }

        public void SetVolumeText(string text)
        {
            VolumeText.Text = text;
        }

        public void SetSliderValue(float value)
        {
            SliderBar.Width = 108f / 100f * value;
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
