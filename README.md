<div align="center">
  <img width="250" height="250" alt="Cicada" src="https://github.com/user-attachments/assets/2a3d1427-ec16-4803-8194-f8ff8f8c9e07" />
</div>

## What is Cicada?
Cicada is a simple program that brings keybinds to volume mixer, specifically on the currently active window. <br/>

## Main Features
- **Foreground Application Volume Control** <br>
  Using the specified hotkeys, you can change the volume of ONLY whatever app you're currently on! <br>
  Volume Up: `Alt+F7` <br>
  Volume Down: `Alt+F6` <br>
  Mute Self: `Alt+F5` <br>
- **Isolate Applcation Volume** <br>
  When isolate is pressed, every OTHER active audio session is muted. If all are already muted, then everything is unmuted. Active session always becomes unmuted. <br>
  Isolate: `Alt+M` <br>
- **Editable Volume Step** <br>
  Change the amount volume up and down change the session volume by! This can be changed via the settings window/config.ini.

<div align="center">
  <img width="305" height="144" alt="image" src="https://github.com/user-attachments/assets/a0e03b08-cccc-4bb6-bbe5-8adfdb4773d8" />
  <img width="234" height="144" alt="image" src="https://github.com/user-attachments/assets/fc67a2ca-5f34-4297-9ad5-5abd6445083d" />
</div>

## How to Change Hotkeys (and other settings)
<div align="center">
  <img width="811" height="625" alt="image" src="https://github.com/user-attachments/assets/168bb73d-0b58-4e9d-8b88-4767ef5103fc" />
</div>
You can access this settings menu via the tray icon. Alternatively, you can manually edit the `config.ini` file within the same directory as the exe.


## Notes
- Cicada can only modify the volume of apps with active audio sessions (the ones that show up in volume mixer).
- Some apps work on multiple processes, and thus Cicada may accidentally miss them with features such as isolate.
- Don't press F4 instead of F5 🦄

## Credits:
- Session Audio Manipulation: https://github.com/naudio/NAudio
- Global Keyhooks: https://github.com/gmamaladze/globalmousekeyhook
- Fluent UI: https://github.com/lepoco/wpfui
- IniFile Class: https://stackoverflow.com/questions/217902/reading-writing-an-ini-file
