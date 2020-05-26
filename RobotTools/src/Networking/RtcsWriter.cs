using System.IO;
using System.Net.Sockets;

namespace RobotTools
{
	public class RtcsWriter
	{
		private Stream stream;
		private BinaryWriter binary;

		public RtcsWriter(Stream stream)
		{
			this.stream = stream;
			this.binary = new BinaryWriter(this.stream);
		}

		public int WriteHeader(RtcsHeader header)
		{
			int pos1 = (int)binary.BaseStream.Position;

			binary.Write(header.headerId.ToCharArray());
			binary.Write(header.headerSize);

			binary.Write(header.addressSize);
			binary.Write(header.address.ToCharArray());
			binary.Write(header.port);

			return (int)binary.BaseStream.Position - pos1;
		}

		public int WriteFooter(RtcsFooter footer)
		{
			int pos1 = (int)binary.BaseStream.Position;

			binary.Write(footer.footerId.ToCharArray());
			binary.Write(footer.footerSize);

			binary.Write(footer.addressSize);
			binary.Write(footer.address.ToCharArray());
			binary.Write(footer.port);

			return (int)binary.BaseStream.Position - pos1;
		}

		public int WriteEvent(RtcsEvent rtcsEvent)
		{
			int pos1 = (int)binary.BaseStream.Position;

			binary.Write(rtcsEvent.id);
			binary.Write(rtcsEvent.size);
			binary.Write(rtcsEvent.data);

			return (int)binary.BaseStream.Position - pos1;
		}
	}
}