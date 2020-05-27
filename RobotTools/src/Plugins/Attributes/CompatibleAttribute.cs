using System;

namespace RobotTools
{

	[AttributeUsage(AttributeTargets.Class)]
	public class CompatibleAttribute : Attribute
	{
		public Version Version { get; private set; }

		public CompatibleAttribute(string version)
		{
			Version ver;
			if (Version.TryParse(version, out ver) == false)
			{
				throw new FormatException("The version string provided is invalid");
			}

			this.Version = ver;
		}

		public CompatibleAttribute(int major, int minor, int build)
		{
			this.Version = new Version(major, minor, build);
		}
	}
}