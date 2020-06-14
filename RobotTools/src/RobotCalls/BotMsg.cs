using System.Runtime.InteropServices;

namespace RobotTools
{
	public class BotMsg
	{
		byte id;
		ushort size;
		byte[] data;
		ushort footer;
	}
}