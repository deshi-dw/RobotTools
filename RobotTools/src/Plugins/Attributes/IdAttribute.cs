using System;

namespace RobotTools
{

	[AttributeUsage(AttributeTargets.Class)]
	public class IdAttribute : Attribute
	{
		public string Text { get; private set; }
		public IdAttribute(string idName)
		{
			Text = idName.ToLower();
		}
	}
}