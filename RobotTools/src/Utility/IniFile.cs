using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotTools
{
	public class IniFile
	{
		private Dictionary<string, Dictionary<string, object>> variables;
		private string input;

		public IniFile(string input, bool parseOnCreate = true)
		{
			this.input = input;

			if (parseOnCreate == true)
			{
				Parse();
			}
		}

		public KeyValuePair<string, object>[] GetAllIn(string header)
		{
			if (variables.ContainsKey(header) == true)
			{
				return variables[header].ToArray();
			}

			return null;
		}

		public object GetValue(string header, string name)
		{
			if (variables.ContainsKey(header) == true && variables[header].ContainsKey(name) == true)
			{
				return variables[header][name];
			}

			return null;
		}

		public void Parse()
		{
			input += '\n';

			bool isHeader = false;
			bool isName = false;
			bool isValue = false;
			bool isString = false;
			bool isArray = false;
			bool isEscape = false;
			bool isComment = false;

			bool isNewline = true;

			bool isDecimal = false;
			bool isInteger = false;
			bool wasString = false;

			bool isHex = false;
			bool isBinary = false;

			string header = string.Empty;
			string name = string.Empty;
			string value = string.Empty;

			variables = new Dictionary<string, Dictionary<string, object>>();
			variables.Add(string.Empty, new Dictionary<string, object>());

			List<object> tempArray = new List<object>();

			void CommitObject(object val)
			{
				if (variables[header].ContainsKey(name) == false)
				{
					variables[header].Add(name, null);
				}
				variables[header][name] = val;
			}

			object ToObject(string sval)
			{
				if (wasString == true)
				{
					return sval;
				}

				if (isDecimal == true)
				{
					decimal number;
					if (decimal.TryParse(sval, out number))
					{
						return number;
					}
				}

				if (isInteger == true || isBinary == true || isHex == true)
				{
					if (isBinary == true)
					{
						try
						{
							return Convert.ToInt32(value, 2);
						}
						catch (FormatException) { }
						catch (OverflowException) { }
					}
					else if (isHex == true)
					{
						try
						{
							return Convert.ToInt32(value, 16);
						}
						catch (FormatException) { }
						catch (OverflowException) { }
					}

					int number;
					if (int.TryParse(sval, out number))
					{
						return number;
					}
				}

				if (sval.ToLower() == "true")
				{
					return true;
				}
				if (sval.ToLower() == "false")
				{
					return false;
				}

				return sval;
			}

			bool IsComment(char c)
			{
				return c == '#' || c == ';';
			}

			bool IsImportant(char c)
			{
				return char.IsLetterOrDigit(c) == true
					   || c == '[' == true
					   || c == ']' == true
					   || c == ',' == true
					   || c == '.' == true
					   || c == '_' == true
					   || c == '=' == true
					   || c == '"' == true
					   || c == '\'' == true
					   || c == '\\' == true
					   || c == '#' == true
					   || c == ';' == true
					   || c == '\n' == true;
			}

			for (int i = 0; i < input.Length; i++)
			{
				if (input[i] == '\\')
				{
					isEscape = true;
					continue;
				}
				else if (isEscape == true)
				{
					if (char.ToLower(input[i]) == 'x')
					{
						isHex = true;
					}
					else if (char.ToLower(input[i]) == 'b')
					{
						isBinary = true;
					}
				}

				// Execute all of the following only if we aren't in a string. This is to ensure strings can have symbols that would otherwise
				// be significant to the syntax.
				if (isString == false)
				{
					// Ignore symbols like whitespace or tabs.
					if (IsImportant(input[i]) == false)
					{
						continue;
					}
					if (isComment == false)
					{
						if (IsComment(input[i]) == true)
						{
							isComment = true;
							goto endloopGoto;
						}

						if (input[i] == '[')
						{
							if (isValue == false)
							{
								isHeader = true;
								header = string.Empty;
							}
							else
							{
								isArray = true;
							}
							goto endloopGoto;
						}
						if (input[i] == ']')
						{
							if (isArray == true)
							{
								isArray = false;

								tempArray.Add(ToObject(value));
								CommitObject(tempArray);
								tempArray = new List<object>();

								value = string.Empty;
								name = string.Empty;

								isDecimal = false;
								isInteger = false;
								isString = false;
								wasString = false;
							}
							else if (isHeader == true)
							{
								isHeader = false;
								if (variables.ContainsKey(header) == false)
								{
									variables.Add(header, new Dictionary<string, object>());
								}
							}
							goto endloopGoto;
						}

						if (input[i] == ',' && isArray == true)
						{
							tempArray.Add(ToObject(value));
							value = string.Empty;

							isDecimal = false;
							isInteger = false;
							isString = false;
							wasString = false;

							goto endloopGoto;
						}

						if (input[i] == '=')
						{
							isValue = true;
							isName = false;
							goto endloopGoto;
						}

						if (input[i] == '"' || input[i] == '\'')
						{
							isString = !isString;
							goto endloopGoto;
						}
					}
					if (input[i] == '\n' && isEscape == false)
					{
						if (isArray == false)
						{
							if (value != string.Empty)
							{
								CommitObject(ToObject(value));

								name = string.Empty;
								value = string.Empty;
								wasString = false;
							}

							isNewline = true;
							isHeader = false;
							isName = false;
							isValue = false;
							isComment = false;

							isDecimal = false;
							isInteger = false;
						}

						isComment = false;
						goto endloopGoto;
					}
					if (isNewline == true && isHeader == false)
					{
						isName = true;
						isNewline = false;
					}

					if (isEscape == false && isComment == false)
					{
						if (isHeader == true)
						{
							header += input[i];
						}
						else if (isName == true)
						{
							name += input[i];
						}
						else if (isValue == true)
						{
							value += input[i];

							if (isDecimal == false && char.IsNumber(input[i]))
							{
								isInteger = true;
							}
							else if (isInteger == true && input[i] == '.')
							{
								// Only reconize the value as a decimal if there is only 1 decimal point.
								if (isDecimal == false)
								{
									isDecimal = true;
								}
								else
								{
									isDecimal = false;
								}
							}
						}
					}
				}
				// if we are in a string.
				else
				{
					if (input[i] == '"' || input[i] == '\'' && isEscape == false)
					{
						wasString = isString;
						isString = !isString;
						goto endloopGoto;
					}

					if (isValue == true)
					{
						value += input[i];
					}
				}

			endloopGoto:
				isEscape = false;
			}
		}
	}
}