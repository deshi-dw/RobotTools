namespace RobotTools
{
	public class CommandParser
	{
		private string input = string.Empty;
		private int index = 0;

		public CommandParser(string input)
		{
			this.input = input;
		}

		public CommandParser() { }

		public void NewInput(string input)
		{
			this.input = input;
			this.index = 0;
		}

		public string GetNext()
		{
			string result = string.Empty;
			index = GetNextInternal(result);

			return result;
		}

		public string Peek()
		{
			string result = string.Empty;
			GetNextInternal(result);

			return result;
		}

		private int GetNextInternal(string result)
		{
			if (index == input.Length)
			{
				return 0;
			}

			int end = input.IndexOf(' ', index + 1);

			if (end == -1)
			{
				end = input.Length;
			}

			if (end == index)
			{
				return 0;
			}

			string value = input.Substring(index, end - index).Trim();

			result = value;
			return end;
		}
	}
}