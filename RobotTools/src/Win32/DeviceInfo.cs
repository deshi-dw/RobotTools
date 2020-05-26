using System.Runtime.InteropServices;

namespace RobotTools.Win32
{
	/// <summary>
	/// Defines the raw input data coming from any device.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
	public struct DeviceInfo
	{
		/// <summary>
		/// The size, in bytes, of the DeviceInfo structure.
		/// </summary>
		[FieldOffset(0)]
		public uint size;

		/// <summary>
		/// The type of raw input data. This member can be one of the following values.
		/// </summary>
		[FieldOffset(4)]
		public RawInputType type;

		/// <summary>
		/// If type is MOUSE, this is the DeviceInfoMouse structure that defines the mouse.
		/// </summary>
		[FieldOffset(8)]
		public DeviceInfoMouse mouseInfo;

		/// <summary>
		/// If type is Keyboard, this is the DeviceInfoKeyboard structure that defines the keyboard.
		/// </summary>
		[FieldOffset(8)]
		public DeviceInfoKeyboard keyboardInfo;

		/// <summary>
		/// If type is HID, this is the DeviceInfoHid structure that defines the HID device.
		/// </summary>
		[FieldOffset(8)]
		public DeviceInfoHid hidInfo;
	}
}
