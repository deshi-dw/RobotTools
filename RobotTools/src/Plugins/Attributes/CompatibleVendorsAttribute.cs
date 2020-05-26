using System;

namespace RobotTools
{

	[AttributeUsage(AttributeTargets.Class)]
	public class CompatibleVendorsAttribute : Attribute
	{
		public uint[] Ids { get; private set; }
		public string[] StringIds { get; private set; }

		public bool IsCompatible(uint vendor)
		{
			for (int i = Ids.Length - 1; i >= 0; i--)
			{
				if (Ids[i] == vendor)
				{
					return true;
				}
			}

			return false;
		}

		public CompatibleVendorsAttribute(params uint[] vendorIds)
		{
			Ids = vendorIds;
			StringIds = DevicePathParser.UintToHexString(vendorIds);
		}
	}
}