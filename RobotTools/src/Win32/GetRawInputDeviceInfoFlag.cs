using System;

namespace RobotTools.Win32
{
	public enum GetRawInputDeviceInfoFlag : uint
	{
		/// <summary>
		/// data points to a string that contains the device name. For this command only, the value in size is the character count (not the byte count).
		/// </summary>
		DEVICE_NAME = 0x20000007,

		/// <summary>
		/// data points to an DeviceInfo structure.
		/// </summary>
		DEVICE_INFO = 0x2000000b,

		/// <summary>
		/// data points to the previously parsed data.
		/// </summary>
		PRE_PARSED_DATA = 0x20000005
	}
}
