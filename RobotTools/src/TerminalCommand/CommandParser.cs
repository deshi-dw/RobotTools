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
			if (index == input.Length)
			{
				return string.Empty;
			}

			int end = input.IndexOf(' ', index + 1);

			if (end == -1)
			{
				end = input.Length;
			}

			if (end == index)
			{
				return string.Empty;
			}

			string value = input.Substring(index, end - index).Trim();

			index = end;

			return value;
		}
	}
}