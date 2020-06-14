using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace RobotTools
{
	public class BotMsgStream : Stream
	{
		private List<BotMsg> messages = new List<BotMsg>();

		public override bool CanRead => true;

		public override bool CanSeek => true;

		public override bool CanWrite => true;

		public override long Length => throw new System.NotImplementedException();

		public override long Position { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

		public override int Read(byte[] buffer, int offset, int count)
		{
			List<byte> bytes = new List<byte>();

			// Read messages as bytes.
			for (int i = 0; i < count; i++)
			{
				if (i + offset > messages.Count - 1)
				{
					break;
				}
				bytes.AddRange(ToBytes(messages[i + offset]));
			}

			// Put read bytes into the buffer.
			for (int i = bytes.Count - 1; i >= 0; i--)
			{
				buffer[i] = bytes[i];
			}

			return bytes.Count;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new System.NotImplementedException();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new System.NotImplementedException();
		}

		public override void SetLength(long value)
		{
			throw new System.NotImplementedException();
		}

		public override void Flush()
		{
			throw new System.NotImplementedException();
		}

		private static BotMsg ToMsg(byte[] message)
		{
			BotMsg msg = new BotMsg();

			int msgSize = Marshal.SizeOf(msg);
			IntPtr msgPtr = Marshal.AllocHGlobal(msgSize);
			Marshal.Copy(message, 0, msgPtr, msgSize);

			msg = (BotMsg)Marshal.PtrToStructure(msgPtr, msg.GetType());
			Marshal.FreeHGlobal(msgPtr);

			return msg;
		}
		private static byte[] ToBytes(BotMsg msg)
		{
			int msgSize = Marshal.SizeOf(msg);
			byte[] msgBytes = new byte[msgSize];

			IntPtr msgPtr = Marshal.AllocHGlobal(msgSize);
			Marshal.StructureToPtr(msg, msgPtr, true);
			Marshal.Copy(msgPtr, msgBytes, 0, msgSize);

			Marshal.FreeHGlobal(msgPtr);

			return msgBytes;
		}
	}
}