using System;

namespace RobotTools
{

	[AttributeUsage(AttributeTargets.Class)]
	public class VersionAttribute : Attribute
	{
		public string Text { get => Version.ToString(3); }
		public Version Version { get; private set; }
		public VersionAttribute(Version version)
		{
			Version = version;
		}
	}
}