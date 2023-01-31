using OnlineClothes.Application.Features.Categories.Queries.Single;
using OnlineClothes.Domain.Paging;

namespace OnlineClothes.Application.Features.Categories.Queries.Paging;

public class GetPagingCategoryQuery : PagingRequest,
	IRequest<JsonApiResponse<PagingModel<GetSingleCategoryQueryViewModel>>>
{
	public GetPagingCategoryQuery(int pageIndex, int pageSize) : base(pageIndex, pageSize)
	{
	}

	public GetPagingCategoryQuery(PagingRequest pagingRequest) : this(pagingRequest.PageIndex, pagingRequest.PageSize)
	{
	}
}
