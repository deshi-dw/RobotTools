using System.Runtime.InteropServices;

namespace RobotTools.Win32
{
	[StructLayout(LayoutKind.Sequential)]
	public struct RawHid
	{
		public uint dwSizHid;
		public uint dwCount;
		public byte bRawData;
	}
}
