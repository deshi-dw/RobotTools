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

		public override async void Request(Command request, Callback callback)
		{
			Task<Socket> socketTask = Task.Run(() => connector.TryConnecting(request.address, request.port, request.protocol));
			Socket socket = await socketTask;

			IPEndPoint remote = null;

			if (socket != null)
			{
				tool.Reader = new RtcsReader(new NetworkStream(socket));
				tool.Writer = new RtcsWriter(new NetworkStream(socket));
				remote = (IPEndPoint)socket.RemoteEndPoint;
			}

			callback?.Invoke(connector.LastError, remote);
		}
	}
}