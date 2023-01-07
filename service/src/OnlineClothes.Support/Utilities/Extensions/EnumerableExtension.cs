namespace OnlineClothes.Support.Utilities.Extensions;

public static class EnumerableExtension
{
	public static IEnumerable<T> Except<T, TKey>(this IEnumerable<T> items, IEnumerable<T> other,
		Func<T, TKey> getKeyFunc)
	{
		return items
			.GroupJoin(other, getKeyFunc, getKeyFunc, (item, tempItems) => new { item, tempItems })
			.SelectMany(t => t.tempItems.DefaultIfEmpty(), (t, temp) => new { t, temp })
			.Where(t => ReferenceEquals(null, t.temp) || t.temp.Equals(default(T)))
			.Select(t => t.t.item);
	}

	public static List<TSelect> SelectList<T, TSelect>(this IEnumerable<T> source, Func<T, TSelect> selectorFunc)
	{
		return source.Select(selectorFunc).ToList();
	}
}
