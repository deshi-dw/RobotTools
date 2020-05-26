using System;
using System.IO;
using System.Net.Sockets;

namespace RobotTools
{
	public class NetworkWriter : IDisposable
	{
		private Socket sender;
		private Stream stream;
		private bool closeStreamOnDispose = false;

		public Socket BaseSocket => sender;
		public Stream BaseStream => stream;

		public NetworkWriter(Socket sender, Stream stream, bool closeStreamOnDispose = true)
		{
			this.closeStreamOnDispose = closeStreamOnDispose;

			this.sender = sender;
			this.stream = stream;
		}

		public void Send(int count)
		{
			byte[] bytes = new byte[count];
			stream.Read(bytes, 0, count);

			SocketError error;
			sender.Send(bytes, 0, bytes.Length, SocketFlags.None, out error);
		}

		public void Send()
		{
			Send((int)(stream.Length - stream.Position));
		}

		public void Flush()
		{
			stream.Flush();
		}

		public void Close()
		{
			if (sender.Connected == true)
			{
				sender.Disconnect(false);
			}
			else
			{
				sender.Close();
			}
		}

		public void Dispose()
		{
			Flush();
			Close();

			if (closeStreamOnDispose == true)
			{
				stream.Close();
			}
		}
	}
}