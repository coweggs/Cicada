<div align="center">
  <img width="250" height="250" alt="Cicada" src="https://github.com/user-attachments/assets/2a3d1427-ec16-4803-8194-f8ff8f8c9e07" />
</div>

## What is Cicada?
Cicada is a simple program that brings keybinds to volume mixer, specifically on the currently active window.

By default, the keybinds are:
- Volume Up: `Alt+F7`
- Volume Down: `Alt+F6`
- Mute: `Alt+F5`
- Isolate: `Alt+M`
To change keybinds, or change the increment, edit the `config.ini` file within the same directory as the exe.
<div align="center">
  <img width="312" height="119" alt="Cicada Flyout" src="https://github.com/user-attachments/assets/d38ef1ba-b131-4cb3-bd0c-befadae29f5a" />
  <img width="213" height="119" alt="image" src="https://github.com/user-attachments/assets/fea5ab16-155c-4f84-9edf-836b4666db17" />
</div>



## Notes
- Make sure the program is actually selected before using the keybinds. Sometimes the Cicada flyout will be targetted as the new active window, pressing on the desired window again fixes this.
- This program only detects apps with active audio sessions (the ones that show up in volume mixer).
- If you have two of the same program open, it will effect both, which is also how it works in Microsoft's Volume Mixer.
- Don't press F4 instead of F5 🦄

## Credits:
- Session Audio Manipulation: https://github.com/naudio/NAudio
- Global Keyhooks: https://github.com/gmamaladze/globalmousekeyhook
- IniFile Class: https://stackoverflow.com/questions/217902/reading-writing-an-ini-file
