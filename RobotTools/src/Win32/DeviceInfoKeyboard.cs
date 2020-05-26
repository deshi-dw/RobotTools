using System.Runtime.InteropServices;

namespace RobotTools.Win32
{
	/// <summary>
	/// Defines the raw input data coming from the specified keyboard.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct DeviceInfoKeyboard
	{
		/// <summary>
		/// The type of the keyboard.
		/// </summary>
		public uint type;

		/// <summary>
		/// The subtype of the keyboard.
		/// </summary>
		public uint subType;

		/// <summary>
		/// The scan code mode.
		/// </summary>
		public uint keyboardMode;

		/// <summary>
		/// The number of function keys on the keyboard.
		/// </summary>
		public uint numberOfFunctionKeys;

		/// <summary>
		/// The number of LED indicators on the keyboard.
		/// </summary>
		public uint numberOfIndicators;

		/// <summary>
		/// The total number of keys on the keyboard.
		/// </summary>
		public uint numberOfKeysTotal;
	}
}
