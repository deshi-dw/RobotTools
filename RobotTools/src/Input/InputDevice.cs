namespace RobotTools
{
	public class InputDevice
	{
		public string Path { get; private set; }
		public ushort VendorId { get; private set; }
		public ushort ProductId { get; private set; }

		public string Name { get; private set; }
		public string Manufacturer { get; private set; }

		public event System.Action<byte[]> OnUpdate;

		public InputDevice(string path, string name, string manufacturer)
		{
			this.Path = path;

			ProductId = DevicePathParser.GetProductIdFromString(Path);
			VendorId = DevicePathParser.GetVendorIdFromString(Path);

			this.Name = name;
			this.Manufacturer = manufacturer;
		}
	}
}