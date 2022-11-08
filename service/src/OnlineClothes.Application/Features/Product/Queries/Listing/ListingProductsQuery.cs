using MediatR;
using OnlineClothes.Application.Commons;
using OnlineClothes.Domain.Paging;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Product.Queries.Listing;

public class ListingProductsQuery : PagingRequest,
	IRequest<JsonApiResponse<PagingModel<ListingProductQueryResultModel>>>
{
	// search query
	public string? Q { get; set; }

	// sort order
	public string Sort { get; set; } = QuerySortOrder.Ascending;

	// sort by
	public string SortBy { get; set; } = "name";

	public string? Category { get; set; }
}
