namespace OnlineClothes.Support.Utilities;

public static partial class Util
{
	public static class Array
	{
		public static T[] Empty<T>()
		{
			return System.Array.Empty<T>();
		}

		public static bool IsNullOrEmpty<T>(IEnumerable<T>? target)
		{
			return target is null || !target.Any();
		}
	}
}
