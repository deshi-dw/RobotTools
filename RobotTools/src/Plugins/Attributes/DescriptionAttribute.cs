using System;

namespace RobotTools
{

	[AttributeUsage(AttributeTargets.Class)]
	public class DescriptionAttribute : Attribute
	{
		public string Text { get; private set; }
		public DescriptionAttribute(string desc)
		{
			Text = desc;
		}
	}
}