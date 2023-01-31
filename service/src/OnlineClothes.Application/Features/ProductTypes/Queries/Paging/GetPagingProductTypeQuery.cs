using OnlineClothes.Application.Features.ProductTypes.Queries.Single;
using OnlineClothes.Domain.Paging;

namespace OnlineClothes.Application.Features.ProductTypes.Queries.Paging;

public class GetPagingProductTypeQuery : PagingRequest,
	IRequest<JsonApiResponse<PagingModel<GetSingleProductTypeQueryViewModel>>>
{
	public string? OrderBy { get; set; }
	public string? SortBy { get; set; }
}
