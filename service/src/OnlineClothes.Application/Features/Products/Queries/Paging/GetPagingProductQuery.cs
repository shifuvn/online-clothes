using System.ComponentModel;
using OnlineClothes.Application.Commons;
using OnlineClothes.Domain.Paging;

namespace OnlineClothes.Application.Features.Products.Queries.Paging;

public class GetPagingProductQuery : PagingRequest, IRequest<JsonApiResponse<PagingModel<ProductBasicDto>>>
{
	public string? Keyword { get; set; } // key search

	[DefaultValue(QuerySortOrder.Descending)]
	public string? OrderBy { get; set; }

	public string? SortBy { get; set; }
	public int? CategoryId { get; set; } // filter by category
	public int? BrandId { get; set; } // filter by brand
}
