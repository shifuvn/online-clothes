namespace OnlineClothes.Domain.Paging;

public class PagingModel<T> where T : class
{
	public PagingModel(long totalCount, IEnumerable<T> items)
	{
		Total = totalCount;

		var enumerable = items as T[] ?? items.ToArray();
		ItemCount = enumerable.Length;
		Items = enumerable;
	}

	private long Total { get; }
	private int ItemCount { get; }
	public IEnumerable<T>? Items { get; set; }

	public int Pages => (int)(Total / ItemCount);
}
