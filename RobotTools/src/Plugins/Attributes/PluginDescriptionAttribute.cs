using System;

namespace RobotTools
{

	[AttributeUsage(AttributeTargets.Class)]
	public class PluginDescriptionAttribute : Attribute
	{
		public string Value { get; private set; }
		public PluginDescriptionAttribute(string desc)
		{
			Value = desc;
		}
	}
}