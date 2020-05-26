using System.Reflection;
using System.IO;
using System;
using System.Collections.Generic;

namespace RobotTools
{
	public class PluginFinder
	{
		string directory = string.Empty;

		// @todo Make a invalid plugin list
		// @body: make a function that returns a list of all the invalid plugins.

		public PluginFinder(string directory)
		{
			if (Directory.Exists(directory) == false)
			{
				throw new DirectoryNotFoundException();
			}

			this.directory = directory;
		}

		public Assembly[] FindAll()
		{
			string[] paths = Directory.GetFiles(directory);
			List<Assembly> plugins = new List<Assembly>(paths.Length);

			foreach (string path in paths)
			{
				if (Path.GetExtension(path) == ".dll")
				{
					try
					{
						plugins.Add(Assembly.LoadFile(path));
					}
					// @todo Handle exception
					catch (BadImageFormatException ex)
					{
						throw ex;
					}
				}
			}

			return plugins.ToArray();
		}

		public Assembly Find(string pluginName)
		{
			string[] paths = Directory.GetFiles(directory);
			Assembly plugin = null;

			foreach (string path in paths)
			{
				if (Path.GetExtension(path) == "dll" && Path.GetFileName(path) == pluginName)
				{
					try
					{
						plugin = Assembly.LoadFile(path);
					}
					// @todo Handle exception
					catch (BadImageFormatException ex)
					{
						throw ex;
					}

				}
			}

			return plugin;
		}
	}
}