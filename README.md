<div align="center">
  <img width="250" height="250" alt="Cicada" src="https://github.com/user-attachments/assets/2a3d1427-ec16-4803-8194-f8ff8f8c9e07" />
</div>

## What is Cicada?
Cicada is a simple program that brings keybinds to volume mixer, specifically on the currently active window. <br/>

## Main Features
- Volume Up Hotkey: `Alt+F7`
- Volume Down Hotkey: `Alt+F6`
- Mute Self Hotkey: `Alt+F5`
- Isolate Hotkey: `Alt+M` <br>
  When isolate is pressed, every other active audio session is muted. If all are already muted, then everything is unmuted. Active session always becomes unmuted.
- Run on Startup <br>
  Right clicking the tray icon brings up a context menu, with an option to toggle run on startup.

To change hotkeys, or change the increment, edit the `config.ini` file within the same directory as the exe.
Proper hotkey names can be found here: https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.keys

<div align="center">
  <img width="393" height="150" alt="Cicada Flyout" src="https://github.com/user-attachments/assets/d38ef1ba-b131-4cb3-bd0c-befadae29f5a" />
  <img width="268" height="150" alt="image" src="https://github.com/user-attachments/assets/fea5ab16-155c-4f84-9edf-836b4666db17" />
</div>

## Notes
- Cicada can only modify the volume of apps with active audio sessions (the ones that show up in volume mixer).
- Some apps work on multiple processes, and thus Cicada may accidentally miss them with features such as isolate.
- Don't press F4 instead of F5 🦄

## Credits:
- Session Audio Manipulation: https://github.com/naudio/NAudio
- Global Keyhooks: https://github.com/gmamaladze/globalmousekeyhook
- IniFile Class: https://stackoverflow.com/questions/217902/reading-writing-an-ini-file
