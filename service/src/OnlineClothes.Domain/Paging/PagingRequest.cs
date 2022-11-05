using System.ComponentModel;

namespace OnlineClothes.Domain.Paging;

public class PagingRequest
{
	public PagingRequest(uint pageIndex, uint pageSize)
	{
		PageIndex = pageIndex;
		PageSize = pageSize;
	}

	public PagingRequest(int pageIndex, int pageSize)
	{
		if (pageIndex < 0)
		{
			pageIndex = 0;
		}

		if (pageSize < 0)
		{
			pageSize = 0;
		}

		PageIndex = (uint)pageIndex;
		PageSize = (uint)pageSize;
	}

	[DefaultValue(1)] public uint PageIndex { get; set; }

	[DefaultValue(20)] public uint PageSize { get; set; }
}
