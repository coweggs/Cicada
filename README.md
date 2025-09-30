# Cicada
Keybinds for Volume Mixer on Foreground Active Window.
By default:
- Volume Up: `Alt+F7`
- Volume Down: `Alt+F6`
- Mute: `Alt+F5`
- Isolate: `Alt+M`

To change keybinds, or change the incremenet, edit the `config.ini` file within the same directory as the exe.

# Notes
- This program only detects apps with active audio sessions (the ones that show up in volume mixer).
- If you have two of the same program open, it will effect both, which is also how it works in Microsoft's Volume Mixer.

# Credits:
- Session Audio Manipulation: https://github.com/naudio/NAudio
- Global Keyhooks: https://github.com/gmamaladze/globalmousekeyhook
- IniFile Class: https://stackoverflow.com/questions/217902/reading-writing-an-ini-file
