# EmuPress

This project is an External Tool for the Bizhawk Emulator which detects high-level virtual keyboard presses (such as keyboard.press() in Python's pynput library) and then sends lower level inputs to the Bizhawk Emulator through the SendInput function (in C#, although the function comes from C++)

The code that I include has a limited amount of keys, as I designed this to play on Genesis ROMs and for a specific DRL project I'm doing. If you clone this repo you can add more Inputs to the SendInput function (in MyTool.cs) with the scancodes for the desired keys. You can find a table of scancodes (for PC) here: http://www.philipstorr.id.au/pcbook/book3/scancode.htm. Make sure you stick with single byte scancodes (only 2D and not E0,2D , for example) unless you know how to deal with extended keys.

For the external tool to work properly, you need to change the controls (Up -> I, Down -> K, Left -> J, and Right -> L). This was done so I didn't have to deal with extended scancode keys. 

Additionally, you may need to change around the naming of the files so everything can see each other. I'd follow Bizhawk's External Tool tutorial for help on what should be renamed (https://github.com/TASVideos/BizHawk-ExternalTools/wiki/Development-quickstart). Namely, in the .csproj file, you may need to change the HintPath variables so the programs can find each other. 
