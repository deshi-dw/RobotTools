using System.Runtime.InteropServices;

namespace RobotTools.Win32
{
	[StructLayout(LayoutKind.Sequential)]
	public struct RawInput
	{
		public RawInputHeader header;
		public Union body;

		[StructLayout(LayoutKind.Explicit)]
		public struct Union
		{
			[FieldOffset(0)]
			public RawMouse mouse;

			[FieldOffset(0)]
			public RawKeyboard keyboard;

			[FieldOffset(0)]
			public RawHid hid;
		}
	}
}
