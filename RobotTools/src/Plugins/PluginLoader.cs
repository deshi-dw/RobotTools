using System.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace RobotTools
{
	public class PluginLoader
	{
		public PluginLoader(Assembly[] assemblies)
		{
			Load(assemblies);
		}

		public PluginIdentifier[] Load(Assembly[] assemblies)
		{
			List<PluginIdentifier> pluginIdentifiers = new List<PluginIdentifier>();

			foreach (Assembly assembly in assemblies)
			{
				Type[] types = assembly.GetExportedTypes();

				foreach (Type type in types)
				{
					if (type.BaseType == typeof(Plugin))
					{
						PluginData data = new PluginData();

						// get plugin type.
						data.pluginType = type;

						// get plugin attributes.
						object[] attributes = type.GetCustomAttributes(typeof(Plugin), true);
						foreach (object attribute in attributes)
						{

							if (attribute is NameAttribute name)
							{
								data.name = name.Text;
							}
							else if (attribute is DescriptionAttribute description)
							{
								data.description = description.Text;
							}
							else if (attribute is IdAttribute id)
							{
								data.id = id.Text;
							}
							else if (attribute is VersionAttribute version)
							{
								data.version = version.Version;
							}
							else if (attribute is AuthorAttribute author)
							{
								data.author = author.Name;
							}
							else if (attribute is CompatibleAttribute compatible)
							{
								data.compatiable = compatible.Version;
							}
							else if (attribute is DependenciesAttribute dependencies)
							{
								data.dependencies = dependencies.Dependencies;
							}
						}

						Plugin plugin = (Plugin)Activator.CreateInstance(data.pluginType);
						pluginIdentifiers.Add(new PluginIdentifier() { plugin = plugin, data = data });
					}
				}
			}

			return pluginIdentifiers.ToArray();
		}
	}
}