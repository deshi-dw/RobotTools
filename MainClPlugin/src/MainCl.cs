namespace RobotTools.MainClPlugin
{
	public class MainCl : Plugin
	{
		public CommandExecutor Executor { get; private set; }

		public override void Enable(PluginManager manager)
		{
			Executor = new CommandExecutor(new CommandParser());
		}
		public override void Disable() { }
	}
}