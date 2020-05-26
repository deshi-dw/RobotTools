using System.Runtime.InteropServices;

namespace RobotTools.Win32
{
	/// <summary>
	/// Defines the raw input data coming from the specified mouse.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct DeviceInfoMouse
	{
		/// <summary>
		/// The identifier of the mouse device.
		/// </summary>
		public uint id;

		/// <summary>
		/// The number of buttons for the mouse.
		/// </summary>
		public uint numberOfButtons;

		/// <summary>
		/// The number of data points per second. This information may not be applicable for every mouse device.
		/// </summary>
		public uint sampleRate;

		/// <summary>
		/// TRUE if the mouse has a wheel for horizontal scrolling; otherwise, FALSE.
		/// </summary>
		public bool hasHorizontalWheel;
	}
}
