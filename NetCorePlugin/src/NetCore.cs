using System.Net.Sockets;

namespace RobotTools.NetCorePlugin
{
	public class NetCore : Plugin
	{
		public ConnectBuilder.Delegate Connect { get; private set; }
		public DisconnectBuilder.Delegate Disconnect { get; private set; }
		public SendBuilder.Delegate SendTcp { get; private set; }
		public SendBuilder.Delegate SendUdp { get; private set; }
		public ReceiveBuilder.Delegate ReceiveTcp { get; private set; }
		public ReceiveBuilder.Delegate ReceiveUdp { get; private set; }

		private NetCoreData data;

		public override void Disable()
		{
			Connect = null;
			Disconnect = null;
			SendTcp = null;
			SendUdp = null;
			ReceiveTcp = null;
			ReceiveUdp = null;
		}

		public override void Enable(PluginManager manager)
		{
			data = new NetCoreData();

			Connect += new ConnectBuilder().Build(data.tcp);
			Connect += new ConnectBuilder().Build(data.udp);

			Disconnect += new DisconnectBuilder().Build(data.tcp);
			Disconnect += new DisconnectBuilder().Build(data.udp);

			SendTcp += new SendBuilder().Build(data.tcp);
			SendUdp += new SendBuilder().Build(data.udp);

			ReceiveTcp += new ReceiveBuilder().Build(data.tcp);
			ReceiveUdp += new ReceiveBuilder().Build(data.udp);
		}
	}
}