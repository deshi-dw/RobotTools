using System;

namespace RobotTools
{

	[AttributeUsage(AttributeTargets.Class)]
	public class AuthorAttribute : Attribute
	{
		public string Name { get; private set; }
		public AuthorAttribute(string name)
		{
			Name = name;
		}
	}
}