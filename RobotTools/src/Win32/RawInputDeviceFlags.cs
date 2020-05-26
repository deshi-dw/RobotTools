using System;

namespace RobotTools.Win32
{
	/// <summary>Enumeration containing flags for a raw input device.</summary>
	[Flags]
	public enum RawInputDeviceFlags : uint
	{
		NONE = 0,

		/// <summery>
		/// If set, this removes the top level collection from the inclusion list. This tells the operating system to stop reading from a device which matches the top level collection.
		/// </summary>
		REMOVE = 0x00000001,

		/// <summery>
		/// If set, this specifies the top level collections to exclude when reading a complete usage page. This flag only affects a TLC whose usage page is already specified with PAGE_ONLY.
		/// </summary>
		EXCLUDE = 0x00000010,

		/// <summery>
		/// If set, this specifies all devices whose top level collection is from the specified usagePage. Note that usage must be zero. To exclude a particular top level collection, use EXCLUDE.
		/// </summary>
		PAGE_ONLY = 0x00000020,

		/// <summery>
		/// If set, this prevents any devices specified by usagePage or usage from generating legacy messages. This is only for the mouse and keyboard.
		/// </summary>
		NO_LEGACY = 0x00000030,

		/// <summery>
		/// If set, this enables the caller to receive input in the background only if the foreground application does not process it. In other words, if the foreground application is not registered for raw input, then the background application that is registered will receive the input.
		/// </summary>
		INPUT_EXSINK = 0x00001000,

		/// <summery>
		/// If set, this enables the caller to receive the input even when the caller is not in the foreground. Note that hwndTarget must be specified.
		/// </summary>
		INPUT_SINK = 0x00000100,

		/// <summery>
		/// If set, the mouse button click does not activate the other window.
		/// </summary>
		CAPTURE_MOUSE = 0x00000200,

		/// <summery>
		/// If set, this enables the caller to receive WM_INPUT_DEVICE_CHANGE notifications for device arrival and device removal.
		/// </summary>
		DEVNOTIFY = 0x00002000,

		/// <summery>
		/// If set, the application-defined keyboard device hotkeys are not handled. However, the system hotkeys; for example, ALT+TAB and CTRL+ALT+DEL, are still handled. By default, all keyboard hotkeys are handled. NO_HOT_KEYS can be specified even if NO_LEGACY is not specified and hwnd is NULL.
		/// </summary>
		NO_HOT_KEYS = 0x00000200,

		/// <summery>
		/// If set, the application command keys are handled. APP_KEYS can be specified only if NO_LEGACY is specified for a keyboard device.
		/// </summary>
		APP_KEYS = 0x00000400
	}

}
