using System;
using System.Runtime.InteropServices;

namespace RobotTools.Win32
{
	public static class User32
	{
		public const string libraryName = "user32.dll";
		public const CharSet libraryCharset = CharSet.Unicode;

		/// <summary>
		/// Registers the devices that supply the raw input data.
		/// </summary>
		/// <param name="rawInputDevice">An array of RawInputDevice structures that represent the devices that supply the raw input.</param>
		/// <param name="numDevices">The number of RawInputDevice structures pointed to by rawInputDevices.</param>
		/// <param name="size">The size, in bytes, of a RawInputDevice structure.</param>
		/// <returns>true if the function succeeds; otherwise, false. If the function fails, call GetLastError for more information.</returns>
		/// <remarks>https://docs.microsoft.com/en-ca/windows/win32/api/winuser/nf-winuser-registerrawinputdevices</remarks>
		[DllImport(libraryName, SetLastError = true, CharSet = libraryCharset)]
		public static extern bool RegisterRawInputDevices([Out, In] RawInputDevice[] rawInputDevice, uint numDevices, uint size);

		/// <summary>
		/// Enumerates the raw input devices attached to the system.
		/// </summary>
		/// <param name="rawInputDevice">An array of RawInputDeviceList structures for the devices attached to the system. If NULL, the number of devices are returned in *puiNumDevices.</param>
		/// <param name="numDevices">If rawInputDeviceList is NULL, the function populates this variable with the number of devices attached to the system; otherwise, this variable specifies the number of RawInputDeviceList structures that can be contained in the buffer to which rawInputDeviceList points. If this value is less than the number of devices attached to the system, the function returns the actual number of devices in this variable and fails with ERROR_INSUFFICIENT_BUFFER.</param>
		/// <param name="size">The size of a RawInputDeviceList structure, in bytes.</param>
		/// <returns>If the function is successful, the return value is the number of devices stored in the buffer pointed to by pRawInputDeviceList. On any other error, the function returns (UINT) -1 and GetLastError returns the error indication.</returns>
		[DllImport(libraryName, SetLastError = true, CharSet = libraryCharset)]
		public static extern int GetRawInputDeviceList([Out] RawInputDeviceList[] rawInputDevice, ref uint numDevices, uint size);

		/// <summary>
		/// Retrieves information about the raw input device.
		/// </summary>
		/// <param name="hDevice">A handle to the raw input device. This comes from the hDevice member of RawInputHeader or from GetRawInputDeviceList.</param>
		/// <param name="command">Specifies what data will be returned in data. This parameter can be one of the following values.</param>
		/// <param name="data">A pointer to a buffer that contains the information specified by command. If command is DEVICE_INFO, set the size member of DeviceInfo to sizeof(DeviceInfo) before calling GetRawInputDeviceInfo.</param>
		/// <param name="size">The size, in bytes, of the data in data.</param>
		/// <returns>If successful, this function returns a non-negative number indicating the number of bytes copied to data. If data is not large enough for the data, the function returns -1. If data is NULL, the function returns a value of zero. In both of these cases, size is set to the minimum size required for the data buffer. Call GetLastError to identify any other errors.</returns>
		[DllImport(libraryName, SetLastError = true, CharSet = libraryCharset)]
		public static extern uint GetRawInputDeviceInfo(IntPtr hDevice, GetRawInputDeviceInfoFlag command, ref DeviceInfo data, ref uint size);

		[DllImport(libraryName, SetLastError = true, CharSet = libraryCharset)]
		public static extern uint GetRawInputDeviceInfo(IntPtr hDevice, GetRawInputDeviceInfoFlag command, [Out] byte[] data, ref uint size);

		/// <summary>
		/// Retrieves the raw input from the specified device.
		/// </summary>
		/// <param name="hRawInput">A handle to the RawInput structure. This comes from the lParam in WM_INPUT.</param>
		/// <param name="command">The command flag. This parameter can be one of the following values.</param>
		/// <param name="data">A pointer to the data that comes from the RawInput structure. This depends on the value of command. If data is NULL, the required size of the buffer is returned in ref size.</param>
		/// <param name="size">The size, in bytes, of the data in data.</param>
		/// <param name="headerSize">The size, in bytes, of the RawInputHeader structure.</param>
		/// <returns>If data is NULL and the function is successful, the return value is 0. If data is not NULL and the function is successful, the return value is the number of bytes copied into data. If there is an error, the return value is (UINT)-1.</returns>
		[DllImport(libraryName, SetLastError = true, CharSet = libraryCharset)]
		public static extern uint GetRawInputData(IntPtr hRawInput, GetRawInputFlag command, out RawInput data, ref uint size, uint headerSize);

		[DllImport(libraryName, SetLastError = true, CharSet = libraryCharset)]
		public static extern uint GetRawInputData(IntPtr hRawInput, GetRawInputFlag command, byte[] data, ref uint size, uint headerSize);

		/// <summary>
		/// Performs a buffered read of the raw input data.
		/// </summary>
		/// <param name="data">A pointer to a buffer of RawInput structures that contain the raw input data. If NULL, the minimum required buffer, in bytes, is returned in ref size.</param>
		/// <param name="size">The size, in bytes, of a RawInput structure.</param>
		/// <param name="headerSize">The size, in bytes, of the RawInputHeader structure.</param>
		/// <returns>If data is NULL and the function is successful, the return value is zero. If data is not NULL and the function is successful, the return value is the number of RAWINPUT structures written to data.</returns>

		[DllImport(libraryName, SetLastError = true, CharSet = libraryCharset)]
		public static extern uint GetRawInputBuffer([In, Out] RawInput[] data, ref uint size, uint headerSize);

		/// <summary>
		/// Calls the default raw input procedure to provide default processing for any raw input messages that an application does not process. This function ensures that every message is processed. DefRawInputProc is called with the same parameters received by the window procedure.
		/// </summary>
		/// <param name="data">An array of RawInput structures.</param>
		/// <param name="size">The number of RawInput structures pointed to by data.</param>
		/// <param name="cbSizeHeader">The size, in bytes, of the RawInput structure.</param>
		/// <returns>If successful, the function returns S_OK. Otherwise it returns an error value.</returns>

		[DllImport(libraryName, SetLastError = true, CharSet = libraryCharset)]
		public static extern IntPtr DefRawInputProc([In, Out] RawInput[] data, uint size, uint cbSizeHeader);
	}
}
