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

			this.directory = new DirectoryInfo(directory).FullName;

			AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;
		}

		public Assembly[] FindAll()
		{
			string[] paths = Directory.GetFiles(directory);
			List<Assembly> plugins = new List<Assembly>(paths.Length);

			foreach (string path in paths)
			{
				Assembly plugin = GetAssembly(path);
				if (plugin != null)
				{
					plugins.Add(plugin);
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
				if (Path.GetFileName(path) == pluginName)
				{
					plugin = GetAssembly(path);
					break;
				}
			}

			return plugin;
		}

		private Assembly GetAssembly(string path)
		{
			if (Path.GetExtension(path) == ".dll")
			{
				try
				{
					return Assembly.LoadFile(path);
				}
				// @todo Handle exception
				catch (BadImageFormatException ex)
				{
					throw ex;
				}
			}

			return null;
		}

		private Assembly ResolveAssembly(object sender, ResolveEventArgs args)
		{
			AssemblyName[] assemblyNames = args.RequestingAssembly.GetReferencedAssemblies();

			Console.WriteLine($"Failed to find all the dependencies for '{args.RequestingAssembly.GetName().Name}'");
			Console.Write($"Looking for '{args.Name.Substring(0, args.Name.IndexOf(','))}'... ");

			foreach (AssemblyName name in assemblyNames)
			{
				string shortName = args.Name.Substring(0, args.Name.IndexOf(','));
				if (shortName == name.Name)
				{
					string path = $"{directory}\\{shortName}.dll";

					Console.WriteLine($"Found '{name.Name}'. Adding reference to '{path}'");
					return GetAssembly(path);
				}
			}

			return null;
		}
	}
}