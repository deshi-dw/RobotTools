using System;
using System.Runtime.InteropServices;

namespace RobotTools.Win32
{
	/// <summary>
	/// Defines information for the raw input devices.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct RawInputDevice
	{
		/// <summary>
		/// 	Top level collection Usage page for the raw input device.
		/// </summary>
		public HidUsagePage usagePage;

		/// <summary>
		/// Top level collection Usage for the raw input device.
		/// </summary>
		public HidUsage usage;

		/// <summary>
		/// Mode flag that specifies how to interpret the information provided by UsagePage and Usage.
		/// </summary>
		public RawInputDeviceFlags flags;

		/// <summary>
		/// A handle to the target window. This will be the window that inputs are received from.
		/// If this is NULL, the inputs will follow the keyboard fucus.
		/// </summary>
		public IntPtr hwnd;
	}

}
