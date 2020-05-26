using System.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace RobotTools
{
	// @todo: Add enable/disable plugin feature
	// @body: plugins need to be able to be individually turned on and off.
	public class PluginLoader
	{
		private List<PluginData> plugins = new List<PluginData>();
		private List<Plugin> constructed = new List<Plugin>();
		private Dictionary<Type, List<Type>> usables = new Dictionary<Type, List<Type>>();

		public PluginLoader(Assembly[] assemblies)
		{
			Load(assemblies);
		}

		public void Load(Assembly[] assemblies)
		{
			foreach (Assembly assembly in assemblies)
			{
				Type[] types = assembly.GetExportedTypes();

				foreach (Type type in types)
				{
					if (type.BaseType != null && type.BaseType.BaseType == typeof(Plugin))
					{
						PluginData data = new PluginData();

						// get plugin type.
						data.pluginType = type;

						// get plugin attributes.
						object[] attributes = type.GetCustomAttributes(typeof(Plugin), true);
						foreach (object attribute in attributes)
						{

							if (attribute is PluginNameAttribute name)
							{
								data.name = name.Value;
							}
							if (attribute is PluginDescriptionAttribute description)
							{
								data.description = description.Value;
							}
						}

						plugins.Add(data);
					}
				}
			}

			foreach (PluginData plugin in plugins)
			{
				constructed.Add((Plugin)Activator.CreateInstance(plugin.pluginType));
			}
		}

		public void EnableAll()
		{
			foreach (Plugin plugin in constructed)
			{
				plugin.Enable();
			}
		}
	}
}