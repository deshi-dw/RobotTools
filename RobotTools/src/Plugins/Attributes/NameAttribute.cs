using System;

namespace RobotTools
{

	[AttributeUsage(AttributeTargets.Class)]
	public class NameAttribute : Attribute
	{
		public string Text { get; private set; }
		public NameAttribute(string name)
		{
			Text = name;
		}
	}
}