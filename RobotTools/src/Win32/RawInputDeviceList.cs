using System;
using System.Runtime.InteropServices;

namespace RobotTools.Win32
{
	[StructLayout(LayoutKind.Sequential)]
	public struct RawInputDeviceList
	{
		public IntPtr hDevice;
		public uint dwType;
	}
}
