using OnlineClothes.Application.Mapping.ViewModels;
using OnlineClothes.Domain.Paging;

namespace OnlineClothes.Application.Features.Brands.Queries.Paging;

public class GetPagingBrandQuery : PagingRequest, IRequest<JsonApiResponse<PagingModel<BrandViewModel>>>
{
	public GetPagingBrandQuery(PagingRequest page) : base(page)
	{
	}
}
