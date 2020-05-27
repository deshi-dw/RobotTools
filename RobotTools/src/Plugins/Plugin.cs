namespace RobotTools
{
	public abstract class Plugin
	{
		public abstract void Enable(PluginManager manager);
		public abstract void Disable();
	}
}