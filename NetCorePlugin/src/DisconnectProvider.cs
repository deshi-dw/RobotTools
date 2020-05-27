using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace RobotTools.NetCorePlugin
{
	public class DisconnectProvider : ProviderPlugin<DisconnectCommand, DisconnectProvider.Callback>
	{
		private NetCore core;

		public DisconnectProvider(NetCore core)
		{
			this.core = core;
		}

		public delegate void Callback(ConnectionError error, IPEndPoint remote);

		public override async void Request(DisconnectCommand request, Callback callback)
		{
		}
	}
}