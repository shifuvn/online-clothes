namespace OnlineClothes.Application.Commons;

public static class QuerySortOrder
{
	public const string Ascending = "asc";
	public const string Descending = "desc";

	public static bool IsDescending(string? orderBy)
	{
		return string.IsNullOrEmpty(orderBy) || orderBy.Equals(Descending, StringComparison.OrdinalIgnoreCase);
	}
}
