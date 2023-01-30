using System.ComponentModel;
using OnlineClothes.Application.Commons;
using OnlineClothes.Domain.Paging;

namespace OnlineClothes.Application.Features.Skus.Queries.Paging;

public class GetPagingSkuQuery : PagingRequest, IRequest<JsonApiResponse<PagingModel<ProductSkuBasicDto>>>
{
	public string? SkuKeyword { get; set; } // key search

	[DefaultValue(QuerySortOrder.Descending)]
	public string? OrderBy { get; set; }

	public string? SortBy { get; set; }
}
