using System.ComponentModel;

namespace OnlineClothes.Domain.Paging;

public class PagingRequest
{
	[DefaultValue(1)] private int _pageIndex { get; set; }

	[DefaultValue(20)] private int _pageSize { get; set; }

	public int PageIndex
	{
		get => _pageIndex;
		set => _pageIndex = value <= 0 ? 1 : value;
	}

	public int PageSize
	{
		get => _pageSize;
		set => _pageSize = value <= 0 ? 1 : value;
	}
}
