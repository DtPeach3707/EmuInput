using System;
using System.Runtime.InteropServices;
using System.Windows.Input;

using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;


namespace Net.MyStuff.MyTool

{

	[ExternalTool("EmuKeyInput", Description = MyToolForm.ToolDescription)]

	public sealed class MyToolForm : ToolFormBase, IExternalToolForm
	{
		//Description of Tool
		const string ToolDescription = "Takes high-level virtual keystrokes and turns them into lower-level inputs that Bizhawk responds to.";
		//Structs for SendInput function 
		[StructLayout(LayoutKind.Sequential)]
		public struct MouseInput
		{
			public int dx;
			public int dy;
			public uint mouseData;
			public uint dwFlags;
			public uint time;
			public IntPtr dwExtraInfo;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct KeyboardInput
		{
			public ushort wVk;
			public ushort wScan;
			public uint dwFlags;
			public uint time;
			public IntPtr dwExtraInfo;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct HardwareInput
		{
			public uint uMsg;
			public ushort wParamL;
			public ushort wParamH;
		}
		[StructLayout(LayoutKind.Explicit)]
		public struct InputUnion
		{
			[FieldOffset(0)] public MouseInput mi;
			[FieldOffset(0)] public KeyboardInput ki;
			[FieldOffset(0)] public HardwareInput hi;
		}
		public struct Input
		{
			public int type;
			public InputUnion u;
		}
		//Flags for SendInput function
		[Flags]
		public enum InputType
		{
			Mouse = 0,
			Keyboard = 1,
			Hardware = 2
		}
		[Flags]
		public enum KeyEventF
		{
			KeyDown = 0x0000,
			ExtendedKey = 0x0001,
			KeyUp = 0x0002,
			Unicode = 0x0004,
			Scancode = 0x0008
		}
		//DllImports for SendInput
		[DllImport("user32.dll", SetLastError = true)]
		private static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);
		[DllImport("user32.dll")]
		private static extern IntPtr GetMessageExtraInfo();
		protected override string WindowTitleStatic => "MyTool"; // required when superclass is ToolFormBase

		public MyToolForm(){} //Case for when tool is first loaded, nothing needed here

		public override void Restart(){} //Case for when ROM restarts, nothing needed here

		protected override void UpdateAfter() // Processing keyboard inputs, this code loops
		{
			Input[] inputs = new Input[]
			{
				new Input //If you wish to add other keys copy here to line 101 and paste it somewhere in the new Input[] space, making sure to change wScan value to the desired single-byte scan code
				{
					type = (int)InputType.Keyboard,
					u = new InputUnion
					{
						ki = new KeyboardInput
						{
							wVk = 0,
							wScan = 0x2D, // X
							dwFlags = Keyboard.IsKeyDown(Key.X) ? (uint)(KeyEventF.KeyDown | KeyEventF.Scancode) : (uint)(KeyEventF.KeyUp | KeyEventF.Scancode),
							dwExtraInfo = GetMessageExtraInfo()
						}
					}
				},
				new Input
				{
					type = (int)InputType.Keyboard,
					u = new InputUnion
					{
						ki = new KeyboardInput
						{
							wVk = 0,
							wScan = 0x25, // K (down button in my control scheme)
							dwFlags = Keyboard.IsKeyDown(Key.Down) ? (uint)(KeyEventF.KeyDown | KeyEventF.Scancode) : (uint)(KeyEventF.KeyUp | KeyEventF.Scancode),
							dwExtraInfo = GetMessageExtraInfo()
						}
					}
				},
				new Input
				{
					type = (int)InputType.Keyboard,
					u = new InputUnion
					{
						ki = new KeyboardInput
						{
							wVk = 0,
							wScan = 0x17, // I (up button in my control scheme)
							dwFlags = Keyboard.IsKeyDown(Key.Up) ? (uint)(KeyEventF.KeyDown | KeyEventF.Scancode) : (uint)(KeyEventF.KeyUp | KeyEventF.Scancode),
							dwExtraInfo = GetMessageExtraInfo()
						}
					}
				},
				new Input
				{
					type = (int)InputType.Keyboard,
					u = new InputUnion
					{
						ki = new KeyboardInput
						{
							wVk = 0,
							wScan = 0x26, // L (right button in my control scheme)
							dwFlags = Keyboard.IsKeyDown(Key.Right) ? (uint)(KeyEventF.KeyDown | KeyEventF.Scancode) : (uint)(KeyEventF.KeyUp | KeyEventF.Scancode),
							dwExtraInfo = GetMessageExtraInfo()
						}
					}
				},
				new Input
				{
					type = (int)InputType.Keyboard,
					u = new InputUnion
					{
						ki = new KeyboardInput
						{
							wVk = 0,
							wScan = 0x24, // J (left button in my control scheme)
							dwFlags = Keyboard.IsKeyDown(Key.Left) ? (uint)(KeyEventF.KeyDown | KeyEventF.Scancode) : (uint)(KeyEventF.KeyUp | KeyEventF.Scancode),
							dwExtraInfo = GetMessageExtraInfo()
						}
					}
				},
				new Input
				{
					type = (int)InputType.Keyboard,
					u = new InputUnion
					{
						ki = new KeyboardInput
						{
							wVk = 0,
							wScan = 0x1C, // Enter scancode (pause button, at least for Genesis)
							dwFlags = Keyboard.IsKeyDown(Key.Return) ? (uint)(KeyEventF.KeyDown | KeyEventF.Scancode) : (uint)(KeyEventF.KeyUp | KeyEventF.Scancode),
							dwExtraInfo = GetMessageExtraInfo()
						}
					}
				}
			};

			SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input))); //Line that sends the input that Bizhawk responds to
		}
	}
}
