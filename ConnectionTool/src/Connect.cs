using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace RobotTools.ConnectionToolPlugin
{
	public class Connect : ProviderPlugin<Connect.Command, Connect.Callback>
	{
		private NetworkConnector connector;
		private ConnectionTool tool;

		public Connect(ConnectionTool tool, NetworkConnector connector)
		{
			this.tool = tool;
			this.connector = connector;
		}

		public delegate void Callback(ConnectionError error, IPEndPoint remote);

		public struct Command
		{
			public int port;
			public IPAddress address;
			public Protocol protocol;
		}

		public Connect(ConnectionTool tool)
		{
			this.tool = tool;
		}

		public override async void Request(Command request, Callback callback)
		{
			Socket socket = await connector.TryConnecting(request.address, request.port, request.protocol);

			if (socket != null)
			{
				tool.Reader = new RtcsReader(new NetworkStream(socket));
				tool.Writer = new RtcsWriter(new NetworkStream(socket));
			}

			callback?.Invoke(connector.LastError, (IPEndPoint)socket.RemoteEndPoint);
		}
	}
}