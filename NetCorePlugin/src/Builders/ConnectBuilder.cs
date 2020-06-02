using System.Net;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace RobotTools
{
	public class ConnectBuilder
	{
		private Socket socket;

		public delegate ConnectStatus Delegate(IPAddress address, int port);

		public Delegate Build(Socket socket)
		{
			this.socket = socket;
			return Connect;
		}

		private ConnectStatus Connect(IPAddress address, int port)
		{
			ConnectStatus status = ConnectStatus.SUCCESS;

			try
			{
				socket.Connect(address, port);
			}
			catch (SocketException ex)
			{
				if (ex.SocketErrorCode == SocketError.TimedOut)
				{
					status = ConnectStatus.TIMED_OUT;
				}
				else if (ex.SocketErrorCode == SocketError.ConnectionRefused)
				{
					status = ConnectStatus.REFUSED;
				}
				else
				{
					status = ConnectStatus.UNKNOWN_ERROR;
				}
			}

			return status;
		}
	}
}