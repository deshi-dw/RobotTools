using System.Collections.Generic;
using System;

namespace RobotTools
{
	public class RobotCaller
	{
		public struct Call
		{
			public Func<byte[]> sender;
			public Action<byte[]> receiver;

			public Call(Func<byte[]> sender, Action<byte[]> receiver)
			{
				this.sender = sender;
				this.receiver = receiver;
			}
		}

		Dictionary<int, Call> callers = new Dictionary<int, Call>();

		public void Assign(int id, Call caller)
		{
			callers.Add(id, caller);
		}

		public byte[] Send(int id)
		{
			if (callers.ContainsKey(id))
			{
				return callers[id].sender?.Invoke();
			}

			return null;
		}
		public void Receive(int id, byte[] data)
		{
			if (callers.ContainsKey(id))
			{
				callers[id].receiver?.Invoke(data);
			}
		}
	}
}