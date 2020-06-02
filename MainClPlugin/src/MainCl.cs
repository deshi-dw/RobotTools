using System;
using System.Threading.Tasks;

namespace RobotTools.MainClPlugin
{
	public class MainCl : Plugin
	{
		private bool hasQuit = false;

		public CommandExecutor Executor { get; private set; }

		public override void Enable(PluginManager manager)
		{
			Executor = new CommandExecutor(new CommandParser());
			Executor.Register("quit", 0, OnQuit);

			Task.Run(() => {
				while (hasQuit == false)
				{
					Executor.Execute(Console.ReadLine());
				}
			});
		}
		public override void Disable() { }

		private void OnQuit(string[] args)
		{
			hasQuit = true;
		}
	}
}