using Cicada.Services;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Gma.System.MouseKeyHook;

namespace Cicada.Flyouts
{
    public partial class SettingsPage : Window, IDisposable
    {
        private readonly IKeyboardMouseEvents GlobalHook;

        public SettingsPage()
        {
            InitializeComponent();

            GlobalHook = Hook.GlobalEvents();
            GlobalHook.KeyDown += OnKeyDown;
            GlobalHook.KeyUp += OnKeyUp;

            PreviewMouseDown += (s, e) =>
            {
                if (WaitingForHotkey && CurrentButton != null)
                {
                    CurrentButton.Content = LastHotkeyInput;
                    WaitingForHotkey = false;
                }
            };

            VolumeUpHotkey.Click += HandleHotkeyClick;
            VolumeDownHotkey.Click += HandleHotkeyClick;
            MuteAudioHotkey.Click += HandleHotkeyClick;
            IsolateAudioHotkey.Click += HandleHotkeyClick;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border? border = sender as Border;
            border?.Focus();
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

        #region Hotkey Input Buttons: Helper Functions
        private bool WaitingForHotkey = false;
        private string LastHotkeyInput = "";
        private string CurrentHotkeyInput = "";
        private System.Windows.Controls.Button? CurrentButton;

        private bool IsModifierKey(Keys key)
        {
            return key == Keys.LShiftKey ||
                   key == Keys.RShiftKey ||
                   key == Keys.ShiftKey ||
                   key == Keys.LControlKey ||
                   key == Keys.RControlKey ||
                   key == Keys.ControlKey ||
                   key == Keys.LMenu || // alt key
                   key == Keys.RMenu ||
                   key == Keys.Menu || // super key
                   key == Keys.LWin || // i dont think gma detects these but whatever
                   key == Keys.RWin;
        }

        private void HandleHotkeyClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button? button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                CurrentButton = button;
                LastHotkeyInput = button.Content.ToString() ?? "";
                CurrentHotkeyInput = "Press a key";
                button.Content = CurrentHotkeyInput;
                WaitingForHotkey = true;
            }
        }

        private void OnKeyDown(object? sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (!WaitingForHotkey || CurrentButton == null)
                return;
            if (!IsModifierKey(e.KeyCode))
            {
                Keys Mods = e.Modifiers;
                string ModsText = Mods == Keys.None ? "" : (Mods.ToString() + " + ").Replace(", ", " + ");
                CurrentHotkeyInput = ModsText + e.KeyCode.ToString();
                CurrentButton.Content = CurrentHotkeyInput;
            }
        }

        private void OnKeyUp(object? sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (!WaitingForHotkey || CurrentButton == null)
                return;
            CurrentButton.Content = CurrentHotkeyInput;
            WaitingForHotkey = false;
        }
        #endregion

        private string LastVolumeStep = "2";
        private void VolumeStep_LostFocus(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.TextBox? box = sender as System.Windows.Controls.TextBox;
            if (box == null)
                return;
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

        private void VolumeUpHotkey_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void VolumeDownHotkey_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void MuteAudioHotkey_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void IsolateAudioHotkey_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        public void Dispose()
        {
            GlobalHook.Dispose();
        }
    }
}
