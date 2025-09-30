using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

// vibe coded <3
namespace Cicada.Helpers
{
    public static class ForegroundIconHelper
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr", SetLastError = true)]
        private static extern IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex);

        private const uint WM_GETICON = 0x007F;
        private const int ICON_BIG = 1; // 32x32 icon
        private const int GCL_HICONSM = -34;

        public static BitmapSource GetForegroundWindowIcon()
        {
            IntPtr hwnd = GetForegroundWindow();
            if (hwnd == IntPtr.Zero)
                return null;

            Icon icon = GetWindowIcon(hwnd);
            if (icon == null)
                return null;

            BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions()
            );

            return bitmapSource;
        }

        private static Icon GetWindowIcon(IntPtr hwnd)
        {
            try
            {
                // Try to get the small icon via WM_GETICON
                IntPtr hIcon = SendMessage(hwnd, WM_GETICON, (IntPtr)ICON_BIG, IntPtr.Zero);
                if (hIcon != IntPtr.Zero)
                    return Icon.FromHandle(hIcon);

                // Fallback: get class small icon
                hIcon = GetClassLongPtr(hwnd, GCL_HICONSM);
                if (hIcon != IntPtr.Zero)
                    return Icon.FromHandle(hIcon);

                // Optional: fallback to process executable icon
                uint pid;
                GetWindowThreadProcessId(hwnd, out pid);
                Process proc = Process.GetProcessById((int)pid);
                try
                {
                    return Icon.ExtractAssociatedIcon(proc.MainModule.FileName);
                }
                catch
                {
                    // Access denied or system process
                    return null;
                }
            }
            catch
            {
                // Any unexpected error
                return null;
            }
        }

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
    }
}
