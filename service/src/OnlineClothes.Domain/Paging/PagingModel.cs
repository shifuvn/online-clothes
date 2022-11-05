namespace OnlineClothes.Domain.Paging;

public class PagingModel<T> where T : class
{
	public PagingModel(ulong totalCount, IEnumerable<T> items)
	{
		Total = totalCount;

		var enumerable = items as T[] ?? items.ToArray();
		ItemCount = (uint)enumerable.Length;
		Items = enumerable;
	}

	public ulong Total { get; set; }
	public uint ItemCount { get; set; }
	public IEnumerable<T>? Items { get; set; }
}
