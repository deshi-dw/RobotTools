using System;
using System.Runtime.InteropServices;

namespace RobotTools.Win32
{
	[StructLayout(LayoutKind.Sequential)]
	public struct RawInputHeader
	{
		public RawInputType type; // Type of raw input (RIM_TYPEHID 2, RIM_TYPEKEYBOARD 1, RIM_TYPEMOUSE 0)
		public uint size; // Size in bytes of the entire input packet of data. This includes RAWINPUT plus possible extra input reports in the RAWHID variable length array.
		public IntPtr hDevice; // A handle to the device generating the raw input data.
		public IntPtr wParam; // RIM_INPUT 0 if input occurred while application was in the foreground else RIM_INPUTSINK 1 if it was not.
	}
}
