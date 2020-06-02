using System;
using System.Threading.Tasks;

namespace RobotTools.MainClPlugin
{
	[Name("MainCl")]
	[Description("the main entry-point for command-line based plugins.")]
	[Id("maincl")]
	[Version("1.0.0")]
	[Dependencies()]
	[Compatible("1.0.0")]
	public class MainCl : Plugin
	{
		private bool hasQuit = false;

		public CommandExecutor Executor { get; private set; }

		public override void Enable(PluginManager manager)
		{
			Executor = new CommandExecutor(new CommandParser());
			Executor.Register("quit", 0, OnQuit);

			manager.OnEnableEnd += () => {
				while (hasQuit == false)
				{
					Executor.Execute(Console.ReadLine());
				}
			};
		}
		public override void Disable() { }

		private void OnQuit(string[] args)
		{
			hasQuit = true;
		}
	}
}