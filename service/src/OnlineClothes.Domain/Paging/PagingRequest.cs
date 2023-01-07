using System.ComponentModel;

namespace OnlineClothes.Domain.Paging;

public class PagingRequest
{
	public PagingRequest()
	{
	}

	public PagingRequest(int pageIndex, int pageSize)
	{
		this.pageIndex = pageIndex;
		this.pageSize = pageSize;
	}

	public PagingRequest(PagingRequest page) : this(page.PageIndex, page.PageSize)
	{
	}

	private int pageIndex { get; set; } = 1;

	private int pageSize { get; set; } = 20;

	[DefaultValue(1)]
	public int PageIndex
	{
		get => pageIndex;
		set => pageIndex = value <= 0 ? 1 : value;
	}

	[DefaultValue(20)]
	public int PageSize
	{
		get => pageSize;
		set => pageSize = value <= 0 ? 1 : value;
	}
}
