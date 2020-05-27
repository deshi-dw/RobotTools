using System;
using System.IO;
using System.Net.Sockets;

namespace RobotTools.Application
{
	public static class RobotToolsApplication
	{
		private static void Main(string[] args)
		{
			string currentDir = AppDomain.CurrentDomain.BaseDirectory;
			PluginFinder finder = new PluginFinder($"{currentDir}\\plugins");
			PluginLoader loader = new PluginLoader(finder.FindAll());

			// loader.EnableAll();
		}
	}
}