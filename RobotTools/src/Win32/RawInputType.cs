namespace RobotTools.Win32
{
	public enum RawInputType : uint
	{
		/// <summary>Data comes from an HID that is not a keyboard or a mouse.</summary>
		HID = 2,
		/// <summary>Data comes from a keyboard. </summary>
		KEYBOARD = 1,
		/// <summary>Data comes from a mouse. </summary>
		MOUSE = 0
	}
}
