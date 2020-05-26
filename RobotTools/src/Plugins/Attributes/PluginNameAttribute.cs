using System;

namespace RobotTools
{

	[AttributeUsage(AttributeTargets.Class)]
	public class PluginNameAttribute : Attribute
	{
		public string Value { get; private set; }
		public PluginNameAttribute(string name)
		{
			Value = name;
		}
	}
}