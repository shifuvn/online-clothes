using OnlineClothes.Application.Mapping.ViewModels;
using OnlineClothes.Domain.Paging;

namespace OnlineClothes.Application.Features.Categories.Queries.Paging;

public class GetPagingCategoryQuery : PagingRequest, IRequest<JsonApiResponse<PagingModel<CategoryViewModel>>>
{
	public GetPagingCategoryQuery(int pageIndex, int pageSize) : base(pageIndex, pageSize)
	{
	}

	public GetPagingCategoryQuery(PagingRequest pagingRequest) : this(pagingRequest.PageIndex, pagingRequest.PageSize)
	{
	}
}
