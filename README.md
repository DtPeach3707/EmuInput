# EmuKeyInput

EmuKeyInput is an External Tool for the Bizhawk Emulator (Version 2.6.1) which detects high-level virtual keyboard presses (such as keyboard.press() in Python's pynput library) and then sends lower level inputs to the Bizhawk Emulator through the SendInput function (in C#, although the function comes from C++) so it will respond to the virtual keystroke, as in its current version, the emulator does not respond to virtual key inputs. 

The code that I include has a limited amount of keys, as I designed this to play on Genesis ROMs and for a specific DRL project I'm doing. If you clone this repo you can add more Inputs to the SendInput function (in MyTool.cs) with the scancodes for the desired keys. You can find a table of scancodes (for PC) here: http://www.philipstorr.id.au/pcbook/book3/scancode.htm. Make sure you stick with single byte scancodes (only 2D and not E0,2D , for example) unless you know how to deal with extended scancode keys (such as the arrow keys on a laptop).

For the external tool to work properly, you need to change the controls a little bit (Up -> I, Down -> K, Left -> J, and Right -> L). This was done so I didn't have to deal with extended scancode keys. 

Additionally, you may need to change around the naming of the files so everything can see each other. I'd follow Bizhawk's External Tool tutorial for help on what should be renamed (https://github.com/TASVideos/BizHawk-ExternalTools/wiki/Development-quickstart), which was also a big help for me when I was making my external tool (I followed it for a lot of the development, and I didn't change any of the naming because of my lack of confidence in C#). Namely, in the .csproj file, you may need to change the HintPath variables so the programs can find each other. 

You will need some C# IDE (I used Visual Studio) to build the tool and include the proper assemblies. If some errors crop up even if the .csproj works perfectly fine, see what assembly refrence you'll need to include (in Visual Studio) by going to Project->Add Assembly Refrence

This definitley has some cleaning up I could do (including how it also interprets, and therefore intereferes with, actual key presses, making it a sizable pain to stop), but with my fairly limited knowledge of C# and Visual Studio in general, I don't feel too confident in my abilities to successfully do that. 
