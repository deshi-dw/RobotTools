using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace RobotTools
{
	public class NetworkAcceptor
	{
		public NetworkAcceptor()
		{
		}

		// @todo: TryAccepting uses a listener socket that is created and deleted every call
		// @body: This could be quite expensive.
		public async Task<Socket> TryAccepting(int localport, Protocol protocol)
		{
			Socket listener = null;
			IPAddress localaddress = Dns.GetHostEntry("localhost").AddressList[0];

			if (protocol == Protocol.TCP)
			{
				listener = new Socket(localaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			}
			else if (protocol == Protocol.UDP)
			{
				listener = new Socket(localaddress.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
			}

			if (listener == null)
			{
				return null;
			}

			listener.Bind(new IPEndPoint(localaddress, localport));

			if (protocol == Protocol.TCP)
			{
				listener.Listen(6);
			}

			Socket client = await listener.AcceptAsync();

			return client;
		}
	}
}