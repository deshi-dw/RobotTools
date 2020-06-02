using System.Text;
using System;
using System.Net;
using System.Threading.Tasks;
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

			executor.Register("connect", 2, OnConnect);
			executor.Register("disconnect", 0, OnDisconnect);
			executor.Register("send-tcp", 1, OnSendTcp);
			executor.Register("send-udp", 1, OnSendUdp);
			executor.Register("receive-tcp", 1, OnReceiveTcp);
			executor.Register("receive-udp", 1, OnReceiveUdp);
		}

		public NetCoreCl()
		{
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

			if (Int32.TryParse(args[1], out int port) == false)
			{
				Error("Failed to parse port.");
				return;
			}

			Task.Run(() => OnConnectCallback(net.Connect(address, port)));
			executor.Wait(1000);
		}

		public void OnConnectCallback(ConnectStatus error)
		{
			if (error == ConnectStatus.TIMED_OUT)
			{
				Error("connection timed out.");
				return;
			}
			else if (error == ConnectStatus.REFUSED)
			{
				Error("connection was refused.");
				return;
			}
			else if (error == ConnectStatus.UNKNOWN_ERROR)
			{
				Error("unkown error.");
				return;
			}

			Console.WriteLine($"successfully connected to the server.");
			executor.Resume();
		}

		public void OnDisconnect(string[] args)
		{
			Task.Run(() => net.Disconnect()).ContinueWith((Task task) => OnDisconnectCallback());
			executor.Wait(100);
		}
		public void OnDisconnectCallback()
		{
			Console.WriteLine($"successfully disconnected from the server.");
			executor.Resume();
		}

		public void OnSendTcp(string[] args)
		{
			Task.Run(() => net.SendTcp(Encoding.ASCII.GetBytes("Hello world."))).ContinueWith((Task task) => OnSendCallback());
		}

		public void OnSendUdp(string[] args)
		{
			Task.Run(() => net.SendUdp(Encoding.ASCII.GetBytes("Hello world."))).ContinueWith((Task task) => OnSendCallback());
		}

		public void OnReceiveTcp(string[] args)
		{
			Task.Run(() => net.ReceiveTcp()).ContinueWith((Task<byte[]> data) => OnReceiveCallback(data.Result));
		}

		public void OnReceiveUdp(string[] args)
		{
			Task.Run(() => net.ReceiveUdp()).ContinueWith((Task<byte[]> data) => OnReceiveCallback(data.Result));
		}

		public void OnSendCallback() { }
		public void OnReceiveCallback(byte[] data)
		{
			Console.WriteLine($"received: {Encoding.ASCII.GetString(data)}");
		}

		private void Error(string error)
		{
			Console.WriteLine($"error: {error}");
		}
	}
}