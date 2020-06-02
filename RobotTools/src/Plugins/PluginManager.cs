using System;
using System.Collections.Generic;

namespace RobotTools
{
	public class PluginManager
	{
		private readonly List<PluginIdentifier> pluginIdentifiers = new List<PluginIdentifier>();

		public event Action OnEnableEnd;
		public event Action OnDisableEnd;

		public PluginManager(PluginLoader loader, PluginFinder finder)
		{
			pluginIdentifiers.AddRange(loader.Load(finder.FindAll()));
		}

		public PluginManager(PluginIdentifier[] identifiers)
		{
			pluginIdentifiers.AddRange(identifiers);
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

			OnEnableEnd?.Invoke();
		}
		public void DisableAll()
		{
			foreach (PluginIdentifier pluginId in pluginIdentifiers)
			{
				pluginId.plugin.Disable();
			}

			OnDisableEnd?.Invoke();
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