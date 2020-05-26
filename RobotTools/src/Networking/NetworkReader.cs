using System;
using System.IO;
using System.Net.Sockets;

namespace RobotTools
{
	public class NetworkReader : IDisposable
	{
		private Socket reader;
		private Stream stream;

		public Socket BaseSocket => reader;
		public Stream BaseStream => stream;

		public NetworkReader(Socket reader)
		{
			this.reader = reader;
			stream = new MemoryStream();
		}

		public byte[] Read(int count)
		{
			byte[] bytes = new byte[count];

			SocketError error;
			reader.Receive(bytes, 0, count, SocketFlags.None, out error);

			stream.Write(bytes, 0, bytes.Length);
			return bytes;
		}

		public byte[] Read()
		{
			return Read(reader.Available);
		}

		public void Flush()
		{
			Read();
		}

		public void Close()
		{
			if (reader.Connected == true)
			{
				reader.Disconnect(false);
			}
			else
			{
				reader.Close();
			}
		}

		public void Dispose()
		{
			Flush();
			Close();
		}
	}
}