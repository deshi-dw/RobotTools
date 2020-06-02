using System.IO;
using System.Net;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace RobotTools
{
	public class SendBuilder
	{
		private NetworkWriter writer;

		public delegate void Delegate(byte[] message);

		public Delegate Build(Socket socket)
		{
			this.writer = new NetworkWriter(socket, new MemoryStream(4096));
			return Send;
		}

		private void Send(byte[] message)
		{
			writer.BaseStream.Write(message, 0, message.Length);
			writer.Send();

		}
	}
}