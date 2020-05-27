using System.Net;
namespace RobotTools.NetCorePlugin
{
	public struct ConnectCommand
	{
		public IPAddress address;
		public int port;
		public Protocol protocol;

		public ConnectCommand(IPAddress address, int port, Protocol protocol = Protocol.TCP)
		{
			this.address = address;
			this.port = port;
			this.protocol = protocol;
		}
	}
}