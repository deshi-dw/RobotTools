using System;
namespace RobotTools
{
	public abstract class ProviderPlugin<T1, T2> where T2 : Delegate
	{
		public void Request(T1 request)
		{
			Request(request, null);
		}
		public abstract void Request(T1 request, T2 callback);
	}
}