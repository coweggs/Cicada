# Cicada

<img width="50" height="50" alt="Cicada" src="https://github.com/user-attachments/assets/2a3d1427-ec16-4803-8194-f8ff8f8c9e07" />


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
