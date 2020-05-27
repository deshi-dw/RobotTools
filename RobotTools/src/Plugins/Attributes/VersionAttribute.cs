using System;

namespace RobotTools
{

	[AttributeUsage(AttributeTargets.Class)]
	public class VersionAttribute : Attribute
	{
		public string Text { get => Version.ToString(3); }
		public Version Version { get; private set; }
		public VersionAttribute(string version)
		{
			Version ver;
			if (Version.TryParse(version, out ver) == false)
			{
				throw new FormatException("The version string provided is invalid");
			}

			this.Version = ver;
		}

		public VersionAttribute(int major, int minor, int build)
		{
			this.Version = new Version(major, minor, build);
		}
	}
}