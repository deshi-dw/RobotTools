using System;

namespace RobotTools
{

	[AttributeUsage(AttributeTargets.Class)]
	public class DependenciesAttribute : Attribute
	{
		public string[] Dependencies { get; private set; }
		public DependenciesAttribute(params string[] dependencies)
		{
			Dependencies = dependencies;
		}
	}
}