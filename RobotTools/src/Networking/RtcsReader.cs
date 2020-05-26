using System.IO;

namespace RobotTools
{
	public class RtcsReader
	{
		private Stream stream;
		private BinaryReader binary;


		public RtcsReader(Stream stream)
		{
			this.stream = stream;
			this.binary = new BinaryReader(this.stream);
		}

		public RtcsHeader ReadHeader()
		{
			RtcsHeader header = new RtcsHeader();
			int pos1 = (int)binary.BaseStream.Position;

			header.headerId = new string(binary.ReadChars(4));
			header.headerSize = binary.ReadUInt32();
			header.addressSize = binary.ReadUInt32();
			header.address = new string(binary.ReadChars((int)header.addressSize));
			header.port = binary.ReadUInt16();

			int pos2 = (int)binary.BaseStream.Position;

			if (pos2 - pos1 < header.headerSize)
			{
				throw new EndOfStreamException("The header size is bigger than the amount of data read.");
			}

			return header;
		}
		public RtcsFooter ReadFooter()
		{
			RtcsFooter footer = new RtcsFooter();

			footer.footerId = new string(binary.ReadChars(4));
			footer.footerSize = binary.ReadUInt32();
			footer.addressSize = binary.ReadUInt32();
			footer.address = new string(binary.ReadChars((int)footer.addressSize));
			footer.port = binary.ReadUInt16();

			return footer;
		}
		public RtcsEvent ReadEvent()
		{
			RtcsEvent rtcsEvent = new RtcsEvent();

			rtcsEvent.id = binary.ReadUInt16();
			rtcsEvent.size = binary.ReadUInt32();
			rtcsEvent.data = binary.ReadBytes((int)rtcsEvent.size);

			return rtcsEvent;
		}

		public bool IsEvent()
		{
			if (stream.Length - stream.Position < 4)
			{
				return false;
			}

			string id = new string(binary.ReadChars(4));
			binary.BaseStream.Position -= 4;

			return (id != "rtcH" && id != "rtcF");
		}

		public bool IsHeader()
		{
			if (stream.Length - stream.Position < 4)
			{
				return false;
			}

			string id = new string(binary.ReadChars(4));
			binary.BaseStream.Position -= 4;

			return id == "rtcH";
		}

		public bool IsFooter()
		{
			if (stream.Length - stream.Position < 4)
			{
				return false;
			}

			string id = new string(binary.ReadChars(4));
			binary.BaseStream.Position -= 4;

			return id == "rtcF";
		}
	}
}