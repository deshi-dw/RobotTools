using System;
using System.Collections.Generic;

namespace RobotTools
{
	public class CallerIdsFile
	{
		public static CallerIds Read(IniFile file)
		{
			KeyValuePair<string, object>[] values = file.GetAllIn("CallerIds");
			CallerIds callerIds = new CallerIds();

			for (int i = values.Length - 1; i >= 0; i--)
			{
				if (values[i].Value is int id)
				{
					callerIds.Add(values[i].Key, id);
				}
			}

			return callerIds;
		}
		// public static void Write(CallerIds ids, WiniFile file) { }
	}
}