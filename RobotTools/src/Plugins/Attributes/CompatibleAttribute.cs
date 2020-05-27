using System;

namespace RobotTools
{

	[AttributeUsage(AttributeTargets.Class)]
	public class CompatibleAttribute : Attribute
	{
		public Version Version { get; private set; }
		public CompatibleAttribute(Version version)
		{
			Version = version;
		}
	}
}