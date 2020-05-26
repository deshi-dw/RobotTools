using System;

namespace RobotTools.Win32
{
	public enum GetRawInputFlag : uint
	{
		/// <summary>
		/// Get the header information from the RawInput structure.
		/// </summary>
		HEADER = 0x10000005,

		/// <summary>
		/// Get the raw data from the RawInput structure.
		/// </summary>
		INPUT = 0x10000003
	}
}
