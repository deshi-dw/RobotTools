using System;

namespace RobotTools
{

	[AttributeUsage(AttributeTargets.Class)]
	public class CompatibleProductsAttribute : Attribute
	{
		public uint[] Ids { get; private set; }
		public string[] StringIds { get; private set; }

		public bool IsCompatible(uint product)
		{
			for (int i = Ids.Length - 1; i >= 0; i--)
			{
				if (Ids[i] == product)
				{
					return true;
				}
			}

			return false;
		}

		public CompatibleProductsAttribute(params uint[] productIds)
		{
			Ids = productIds;
			StringIds = DevicePathParser.UintToHexString(productIds);
		}
	}
}