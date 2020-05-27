using System.Collections.Generic;

namespace RobotTools
{
	public class PluginManager
	{
		private List<PluginIdentifier> pluginIdentifiers = new List<PluginIdentifier>();

		public PluginLoader Loader { get; private set; }
		public PluginFinder Finder { get; private set; }

		public PluginManager(PluginLoader loader, PluginFinder finder)
		{
			this.Loader = loader;
			this.Finder = finder;
		}

		public void AddPlugin(PluginIdentifier plugin)
		{
			pluginIdentifiers.Add(plugin);
		}
		public void AddPlugins(PluginIdentifier[] plugins)
		{
			pluginIdentifiers.AddRange(plugins);
		}

		public T Get<T>() where T : Plugin
		{
			foreach (PluginIdentifier pluginId in pluginIdentifiers)
			{
				if (typeof(T) == pluginId.data.pluginType)
				{
					return (T)pluginId.plugin;
				}
			}

			return null;
		}

		public void EnableAll()
		{
			foreach (PluginIdentifier pluginId in pluginIdentifiers)
			{
				pluginId.plugin.Enable(this);
			}
		}
		public void DisableAll()
		{
			foreach (PluginIdentifier pluginId in pluginIdentifiers)
			{
				pluginId.plugin.Disable();
			}
		}

		public void Enable(string id)
		{
			foreach (PluginIdentifier pluginId in pluginIdentifiers)
			{
				if (pluginId.data.id == id)
				{
					pluginId.plugin.Enable(this);
				}
			}
		}
		public void Disable(string id)
		{
			foreach (PluginIdentifier pluginId in pluginIdentifiers)
			{
				if (pluginId.data.id == id)
				{
					pluginId.plugin.Disable();
				}
			}
		}
	}
}