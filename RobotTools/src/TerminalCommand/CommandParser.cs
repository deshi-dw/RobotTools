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
			index = GetNextInternal(out string result);
			return result;
		}

		public string Peek()
		{
			GetNextInternal(out string result);
			return result;
		}

		private int GetNextInternal(out string result)
		{
			if (index == input.Length)
			{
				result = string.Empty;
				return 0;
			}

			int end = input.IndexOf(' ', index + 1);

			if (end == -1)
			{
				end = input.Length;
			}

			if (end == index)
			{
				result = string.Empty;
				return 0;
			}

			result = input.Substring(index, end - index).Trim();
			return end;
		}
	}
}