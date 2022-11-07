namespace OnlineClothes.Support.Utilities;

public static partial class Util
{
	public static class List
	{
		public static List<T> Empty<T>()
		{
			return Array.Empty<T>().ToList();
		}

		public static bool IsNullOrEmpty<T>(IEnumerable<T>? target)
		{
			return target is null || !target.Any();
		}
	}
}
