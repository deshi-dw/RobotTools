using System.Runtime.InteropServices;

namespace RobotTools.Win32
{
	/// <summary>
	/// Defines the raw input data coming from the specified Human Interface Device (HID).
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct DeviceInfoHid
	{
		/// <summary>
		/// The vendor identifier for the HID.
		/// </summary>
		public uint vendorId;

		/// <summary>
		/// The product identifier for the HID.
		/// </summary>
		public uint productId;

		/// <summary>
		/// The version number for the HID.
		/// </summary>
		public uint versionNumber;

		/// <summary>
		/// The top-level collection Usage Page for the device.
		/// </summary>
		public HidUsagePage usagePage;

		/// <summary>
		/// The top-level collection Usage for the device.
		/// </summary>
		public HidUsage usage;
	}
}
