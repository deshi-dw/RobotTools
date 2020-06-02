using System.Net.Sockets;

namespace RobotTools
{
	public class DisconnectBuilder
	{
		private Socket socket;

		public delegate void Delegate();

		public Delegate Build(Socket socket)
		{
			this.socket = socket;
			return Disconnect;
		}

		private void Disconnect()
		{
			if (socket.Connected == true)
			{
				socket.Disconnect(true);
			}
		}
	}
}