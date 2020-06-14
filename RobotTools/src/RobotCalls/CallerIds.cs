using System;
using System.Collections.Generic;

namespace RobotTools
{
	public class CallerIds
	{
		private Dictionary<string, int> ids = new Dictionary<string, int>();

		public event Action<int, int> OnChangeId;

		public CallerIds() { }

		public CallerIds(Dictionary<string, int> ids)
		{
			this.ids = ids;
		}

		public CallerIds(List<KeyValuePair<string, int>> idsValues)
		{
			for (int i = idsValues.Count - 1; i >= 0; i--)
			{
				this.ids.Add(idsValues[i].Key, idsValues[i].Value);
			}
		}

		public int Get(string idName)
		{
			if (ids.ContainsKey(idName))
			{
				return ids[idName];
			}

			return -1;
		}

		public void Change(string idName, int id)
		{
			if (ids.ContainsKey(idName) == false)
			{
				ids.Add(idName, id);
			}
			else
			{
				ids[idName] = id;
			}
		}

		public void Add(string idName, int id)
		{
			if (ids.ContainsKey(idName) == false)
			{
				ids.Add(idName, id);
			}
		}
	}
}