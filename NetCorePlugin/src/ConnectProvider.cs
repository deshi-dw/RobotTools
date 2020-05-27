using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace RobotTools.NetCorePlugin
{
	public class ConnectProvider : ProviderPlugin<ConnectCommand, ConnectProvider.Callback>
	{
		private NetworkConnector connector;
		private NetCore core;

		public ConnectProvider(NetCore core, NetworkConnector connector)
		{
			this.core = core;
			this.connector = connector;
		}

		public delegate void Callback(ConnectionError error, IPEndPoint remote);

		public override async void Request(ConnectCommand request, Callback callback)
		{
			Task<Socket> socketTask = Task.Run(() => connector.TryConnecting(request.address, request.port, request.protocol));
			Socket socket = await socketTask;

			IPEndPoint remote = null;

			if (socket != null)
			{
				core.Reader = new RcReader(new NetworkStream(socket));
				core.Writer = new RcWriter(new NetworkStream(socket));
				remote = (IPEndPoint)socket.RemoteEndPoint;
			}

			callback?.Invoke(connector.LastError, remote);
		}
	}
}