using System.ComponentModel;

namespace OnlineClothes.Domain.Paging;

public class PagingRequest
{
	[DefaultValue(1)] public uint PageIndex { get; set; }

	[DefaultValue(20)] public uint PageSize { get; set; }
}
