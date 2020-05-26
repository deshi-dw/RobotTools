using System;

namespace RobotTools
{
	public class PluginData
	{
		public string name;
		public string id;
		public string description;
		public Version version;
		public Version compatiable;
		public string[] dependencies;

		public Type pluginType;
	}
}