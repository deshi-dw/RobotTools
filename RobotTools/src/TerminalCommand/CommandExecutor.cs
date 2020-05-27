using System.Threading;
using System;
using System.Collections.Generic;

namespace RobotTools
{
	public class CommandExecutor
	{
		private CommandParser parser;
		private Dictionary<string, ExecutorCommandArgs> commands = new Dictionary<string, ExecutorCommandArgs>();

		private bool isWaiting = false;

		private struct ExecutorCommandArgs
		{
			public int argumentCount;
			public Action<string[]> execute;

			public ExecutorCommandArgs(int argumentCount, Action<string[]> execute)
			{
				this.argumentCount = argumentCount;
				this.execute = execute;
			}
		}

		public CommandExecutor(CommandParser parser)
		{
			this.parser = parser;
		}

		public void Register(string command, int argumentCount, Action<string[]> execute)
		{
			commands.Add(command, new ExecutorCommandArgs(argumentCount, execute));
		}
		public void Unregister(string command)
		{
			commands.Remove(command);
		}

		public void Wait(int timeout)
		{
			isWaiting = true;

			int time = 0;
			while (time < timeout && isWaiting == true)
			{
				Thread.Sleep(50);
				time += 50;
			}
		}
		public void Resume()
		{
			isWaiting = false;
		}

		public void Execute(string input)
		{
			parser.NewInput(input);
			string command = parser.GetNext();

			if (commands.ContainsKey(command) == false)
			{
				// TODO: Errorcheck
			}

			string[] args = new string[commands[command].argumentCount];
			for (int i = 0; i < args.Length; i++)
			{
				args[i] = parser.GetNext();
			}

			commands[command].execute(args);
		}
	}
}