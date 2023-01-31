using OnlineClothes.Application.Features.Categories.Queries.Single;
using OnlineClothes.Domain.Paging;

namespace OnlineClothes.Application.Features.Categories.Queries.Paging;

public class GetPagingCategoryQuery : PagingRequest,
	IRequest<JsonApiResponse<PagingModel<GetSingleCategoryQueryViewModel>>>
{
	public string? OrderBy { get; set; }
	public string? SortBy { get; set; }
}
