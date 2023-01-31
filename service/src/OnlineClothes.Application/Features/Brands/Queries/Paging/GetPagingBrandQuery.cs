using OnlineClothes.Application.Features.Brands.Queries.Single;
using OnlineClothes.Domain.Paging;

namespace OnlineClothes.Application.Features.Brands.Queries.Paging;

public class GetPagingBrandQuery : PagingRequest, IRequest<JsonApiResponse<PagingModel<GetSingleBrandQueryViewModel>>>
{
	public GetPagingBrandQuery(PagingRequest page) : base(page)
	{
	}
}
