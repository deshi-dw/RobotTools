using System;
using System.Net;

using RobotTools.MainClPlugin;

namespace RobotTools.NetCorePlugin
{
	[Name("Net Core Cl")]
	[Description("a command-line plugin made for interacting with netcore through the command line.")]
	[Id("netcorecl")]
	[Version("1.0.0")]
	[Dependencies("netcore", "maincl")]
	[Compatible("1.0.0")]
	public class NetCoreCl : Plugin
	{

		private NetCore net = null;
		private CommandExecutor executor = null;

		public override void Disable()
		{
		}

		public override void Enable(PluginManager manager)
		{
			net = manager.Get<NetCore>();
			executor = manager.Get<MainCl>().Executor;

			executor.Register("connect", 3, OnConnect);
		}

		public NetCoreCl(CommandExecutor executor)
		{
			this.executor = executor;
		}

		public void OnConnect(string[] args)
		{
			// Initialize arguments.
			if (args[0] == string.Empty || args[1] == string.Empty)
			{
				Error("address and port are not specified.");
				return;
			}

			IPAddress address;

			try
			{
				address = IPAddress.Parse(args[0]);
			}
			catch (Exception)
			{
				Error("failed to parse address.");
				return;
			}

			int port;
			if (Int32.TryParse(args[1], out port) == false)
			{
				Error("Failed to parse port.");
				return;
			}

			Protocol protocol = Protocol.TCP;

			if (args[2] == "tcp")
			{
				protocol = Protocol.TCP;
			}
			else if (args[2] == "udp")
			{
				protocol = Protocol.UDP;
			}
			else
			{
				Error("invalid protocol.");
			}

			// execute command.
			net.Connect.Request(new ConnectCommand(address, port, protocol), OnConnectCallback);
			executor.Wait(1000);
		}

		public void OnConnectCallback(ConnectionError error, IPEndPoint remote)
		{
			if (error == ConnectionError.CONNECTION_TIMED_OUT)
			{
				Error("connection timed out.");
				return;
			}
			else if (error == ConnectionError.CONNECTION_REFUSED)
			{
				Error("connection was refused.");
				return;
			}
			else if (error == ConnectionError.INVALID_IPADDRESS)
			{
				Error("invalid address.");
				return;
			}

			Console.WriteLine($"successfully connected to server. ({remote})");
			executor.Resume();
		}

		private void Error(string error)
		{
			Console.WriteLine($"error: {error}");
		}
	}
}