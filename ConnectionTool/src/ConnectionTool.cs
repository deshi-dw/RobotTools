using System.Threading;
using System.Net;
using System;

namespace RobotTools.ConnectionToolPlugin
{
	public class ConnectionTool : ToolPlugin
	{
		private RtcsReader reader = null;
		private RtcsWriter writer = null;

		public RtcsReader Reader { get => reader; set => reader = value; }
		public RtcsWriter Writer { get => writer; set => writer = value; }

		public Connect Connect { get; private set; }

		public override void Enable()
		{
			Connect = new Connect(this, new NetworkConnector());

			Console.WriteLine("**** Robot Connection Tool ****");
			Console.WriteLine("this is a console application for connecting to the robot. type 'help' to see all of the commands.");
			Console.WriteLine();

			string input;
			while ((input = Console.ReadLine()) != "quit")
			{
				Console.WriteLine();
				int index = 0;
				string command = GetNext(input, ref index);

				switch (command)
				{
					case "help":
						PrintHelp(GetNext(input, ref index));
						break;

					case "connect":
						Connnect(GetNext(input, ref index), GetNext(input, ref index), GetNext(input, ref index));
						break;

					case "disconnect":
						break;

					case "info":
						break;

					default:
						break;
				}

				Console.WriteLine();
			}
		}
		public override void Disable() { }

		private string GetNext(string input, ref int start)
		{
			if (start == input.Length)
			{
				return string.Empty;
			}

			int end = input.IndexOf(' ', start + 1);

			if (end == -1)
			{
				end = input.Length;
			}

			if (end == start)
			{
				return string.Empty;
			}

			string value = input.Substring(start, end - start).Trim();

			start = end;

			return value;
		}

		private void Error(string message)
		{
			Console.WriteLine($"error: {message}");
		}

		private void Connnect(string ip, string port, string protocol)
		{
			if (ip == string.Empty || port == string.Empty)
			{
				Error("connect needs to by supplied with at least an ip and port.");
				return;
			}

			if (protocol == string.Empty)
			{
				protocol = "tcp";
			}

			Connect.Command args = new Connect.Command();
			try
			{
				args.address = IPAddress.Parse(ip);
			}
			catch (FormatException)
			{
				Error("invalid ip address supplied.");
				return;
			}

			if (Int32.TryParse(port, out args.port) == false)
			{
				Error("invalid port number supplied.");
				return;
			}

			protocol = protocol.ToLower();
			if (protocol == "udp")
			{
				args.protocol = Protocol.UDP;
			}
			else if (protocol == "tcp")
			{
				args.protocol = Protocol.TCP;
			}
			else
			{
				Error("invalid protocol supplied.");
			}

			bool waiting = true;
			Connect.Request(args,
			(ConnectionError error, IPEndPoint remote) => {
				waiting = false;

				if (error == ConnectionError.CONNECTION_REFUSED)
				{
					Error("connection refused.");
					return;
				}
				else if (error == ConnectionError.CONNECTION_TIMED_OUT)
				{
					Error("connection timed out.");
					return;
				}
				else if (error == ConnectionError.INVALID_IPADDRESS)
				{
					Error("invalid ip address.");
					return;
				}

				Console.WriteLine($"successfully connected to server. ({remote})");
			});

			int __extra_timeout = 30;
			while (__extra_timeout >= 0 && waiting)
			{
				__extra_timeout--;
				Thread.Sleep(250);
			}
		}

		private void PrintHelp(string topic)
		{
			if (topic == string.Empty)
			{
				Console.WriteLine("connect {address} {port} [protocol] - connects to a server at the supplied address and port.");
				Console.WriteLine("disconnect - disconnects from a connect server.");
				Console.WriteLine("send {message} - sends a message to the server.");
				Console.WriteLine("ping {address} - check if an address is online without having to connect to it.");
				Console.WriteLine("info - prints general info about the current connection.");

			}
			else
			{
				switch (topic)
				{
					case "connect":
						Console.WriteLine("connect {address} {port} [protocol]");
						Console.WriteLine("  address - the address of the server to connect to. This can be Ipv4, Ipv6, or a hostname.");
						Console.WriteLine("  port - the server port to connect to.");
						Console.WriteLine("  protocol (OPTIONAL) - establish either a tcp or udp connection. default is tcp.");
						Console.WriteLine();
						Console.WriteLine("examples:");
						Console.WriteLine("  connect 192.168.1.100 12345");
						Console.WriteLine("  connect google.com 8080 udp");

						break;

					default:
						break;
				}
			}
		}

	}
}