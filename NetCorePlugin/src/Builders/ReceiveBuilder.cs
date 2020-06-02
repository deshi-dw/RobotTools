using System.IO;
using System.Net;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace RobotTools
{
	public class ReceiveBuilder
	{
		private NetworkReader reader;

		public delegate byte[] Delegate();

		public Delegate Build(Socket socket)
		{
			this.reader = new NetworkReader(socket, new MemoryStream(4096));
			return Read;
		}

		private byte[] Read()
		{
			return reader.Receive();

		}
	}
}