using System;

namespace RobotTools
{
	public static class DevicePathParser
	{
		public static ushort GetVendorIdFromString(string str)
		{
			return UInt16.Parse(GetValueAfterPrefixFromString(str, "VID_", new char[] { '&', '#' }),
								System.Globalization.NumberStyles.HexNumber);
		}
		public static ushort GetProductIdFromString(string str)
		{
			return UInt16.Parse(GetValueAfterPrefixFromString(str, "PID_", new char[] { '&', '#' }),
								System.Globalization.NumberStyles.HexNumber);
		}

		public static string UshortToHexString(ushort value)
		{
			return value.ToString("X2");
		}
		public static string UintToHexString(uint value)
		{
			return value.ToString("X4");
		}

		public static string[] UshortToHexString(ushort[] values)
		{
			string[] stingValues = new string[values.Length];

			for (int i = values.Length - 1; i >= 0; i--)
			{
				stingValues[i] = values[i].ToString("X2");
			}
			return stingValues;
		}
		public static string[] UintToHexString(uint[] values)
		{
			string[] stingValues = new string[values.Length];

			for (int i = values.Length - 1; i >= 0; i--)
			{
				stingValues[i] = values[i].ToString("X4");
			}
			return stingValues;
		}
		public static string GetValueAfterPrefixFromString(string str, string prefix, char endSymbol)
		{
			int start = str.IndexOf(prefix);
			if (start == -1)
			{
				return null;
			}
			start += prefix.Length;

			int end = str.IndexOf(endSymbol, start);
			if (end <= start)
			{
				return null;
			}

			return str.Substring(start, end - start);
		}
		public static string GetValueAfterPrefixFromString(string str, string prefix, params char[] endSymbol)
		{
			int start = str.IndexOf(prefix);
			if (start == -1)
			{
				return null;
			}
			start += prefix.Length;

			int end = str.IndexOfAny(endSymbol, start);
			if (end <= start)
			{
				return null;
			}

			return str.Substring(start, end - start);
		}
		public static string GetValueAfterPrefixFromString(string str, string prefix, string endSymbol)
		{
			int start = str.IndexOf(prefix);
			if (start == -1)
			{
				return null;
			}
			start += prefix.Length;

			int end = str.IndexOf(endSymbol, start);
			if (end <= start)
			{
				return null;
			}

			return str.Substring(start, end - start);
		}
		public static string GetValueAfterPrefixFromString(string str, string prefix)
		{
			int start = str.IndexOf(prefix);
			if (start == -1)
			{
				return null;
			}

			return str.Substring(start, str.Length - start);
		}
	}
}