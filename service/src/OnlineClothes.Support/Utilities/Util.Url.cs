using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace OnlineClothes.Support.Utilities;

public static partial class Util
{
	public static class Url
	{
		private const string RegCharPattern = "(?:[^a-z0-9-_./]|(?<=['\"])s)";

		[Pure]
		public static string RemoveSpecialCharacters(string input)
		{
			var regex = new Regex(RegCharPattern,
				RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
			return regex.Replace(input, string.Empty);
		}
	}
}
