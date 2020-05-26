using System.Threading;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace RobotTools
{
	public class NetworkConnector
	{
		private long connectionTimeout = 100;
		public long ConnectionTimeout
		{
			get => connectionTimeout;
			set
			{
				connectionTimeout = Math.Max(0, value);
			}
		}
		private long delayBeforeRetry = 10;
		public long DelayBeforeRetry
		{
			get => delayBeforeRetry;
			set
			{
				delayBeforeRetry = Math.Max(0, Math.Min(value, connectionTimeout));
			}
		}

		public NetworkConnector()
		{
		}

		public ConnectionError LastError { get; private set; }

		public Socket TryConnecting(IPAddress address, int port, Protocol protocol)
		{
			if (address == null)
			{
				LastError = ConnectionError.INVALID_IPADDRESS;
				return null;
			}

			Socket socket = null;

			if (protocol == Protocol.TCP)
			{
				socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			}
			else if (protocol == Protocol.UDP)
			{
				socket = new Socket(address.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
			}

			long timeout = 0;
			int SAFTEY_CHECK = 0;

			while (socket.Connected == false)
			{
				try
				{
					socket.Connect(address, port);
				}
				catch (SocketException ex)
				{
					if (ex.SocketErrorCode == SocketError.TimedOut)
					{
						LastError = ConnectionError.CONNECTION_TIMED_OUT;
					}
					else if (ex.SocketErrorCode == SocketError.ConnectionRefused)
					{
						LastError = ConnectionError.CONNECTION_REFUSED;
					}
				}

				Thread.Sleep((int)DelayBeforeRetry);
				timeout += DelayBeforeRetry;

				if (timeout >= ConnectionTimeout)
				{
					break;
				}
				else if (SAFTEY_CHECK > 50)
				{
					break;
				}
			}

			if (socket.Connected == false)
			{
				LastError = ConnectionError.CONNECTION_TIMED_OUT;
				socket.Dispose();
				return null;
			}

			return socket;
		}
	}
}