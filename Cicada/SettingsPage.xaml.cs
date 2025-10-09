using Cicada.Services;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cicada.Flyouts
{
    public partial class SettingsPage : Window
    {
        private SettingsManager SettingsMan;

        public SettingsPage(SettingsManager settingsManager)
        {
            SettingsMan = settingsManager;
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;
            border.Focus();
        }

        private void Titlebar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private string LastVolumeStep = "2";
        private void VolumeStep_LostFocus(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.TextBox box = sender as System.Windows.Controls.TextBox;
            if (string.IsNullOrEmpty(box.Text))
            {
                box.Text = LastVolumeStep;
            }
            LastVolumeStep = box.Text;
        }

        private void OpenIniFile_Click(object sender, RoutedEventArgs e)
        {

            Process.Start(new ProcessStartInfo
            {
                FileName = "config.ini",
                UseShellExecute = true,
            });
        }

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            //
        }
    }
}
