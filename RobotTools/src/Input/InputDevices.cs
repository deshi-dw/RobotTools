using System.Collections.Generic;
using System.Management;

namespace RobotTools
{
	public static class InputDevices
	{
		public static InputDevice[] GetDevices()
		{
			ManagementObjectSearcher searcher = new ManagementObjectSearcher();
			List<InputDevice> inputDevices = new List<InputDevice>();

			string[] deviceIds = GetDeviceIds();

			for (int i = deviceIds.Length - 1; i >= 0; i--)
			{
				searcher.Query.QueryString = $"SELECT * FROM Win32_PnPEntity WHERE PNPDeviceID = {deviceIds[i]}";
				ManagementObjectCollection usbDevices = searcher.Get();

				foreach (ManagementObject usbDevice in usbDevices)
				{

					inputDevices.Add(new InputDevice((string)usbDevice.GetPropertyValue("DeviceID"),
													 (string)usbDevice.GetPropertyValue("Name"),
													 (string)usbDevice.GetPropertyValue("Manufacturer")));
				}
			}

			return inputDevices.ToArray();
		}

		public static string[] GetDeviceIds()
		{
			ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * from Win32_USBControllerDevice");
			ManagementObjectCollection usbAddresses = searcher.Get();

			string[] deviceIds = new string[usbAddresses.Count];
			int i = 0;

			foreach (ManagementObject usbAddress in usbAddresses)
			{
				string usbAddressValue = (string)usbAddress.GetPropertyValue("Dependent");
				string key = "DeviceID=\"";

				int start = usbAddressValue.IndexOf(key) + key.Length;
				deviceIds[i] = usbAddressValue.Substring(start, usbAddressValue.Length - start - 1);
				i++;
			}

			usbAddresses.Dispose();
			searcher.Dispose();

			return deviceIds;
		}
	}
}