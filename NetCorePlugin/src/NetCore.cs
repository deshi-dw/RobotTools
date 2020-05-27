namespace RobotTools.NetCorePlugin
{
	public class NetCore : Plugin
	{
		private ConnectProvider connect;
		private DisconnectProvider disconnect;
		// private RcReadProvider rcRead;
		// private RcWriteProvider rcWrite;
		// private NetPingProvider ping;
		// private NetInfoProvider info;

		public ConnectProvider Connect
		{
			get => this.connect;
		}

		public RcReader Reader { set; get; }
		public RcWriter Writer { set; get; }

		public override void Disable()
		{
		}

		public override void Enable(PluginManager manager)
		{
		}
	}
}