using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace Cicada
{
    /// <summary>
    /// Interaction logic for NoSessionFlyout.xaml
    /// </summary>
    public partial class NoSessionFlyout : Window
    {
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_NOACTIVATE = 0x08000000;

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        public NoSessionFlyout()
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
