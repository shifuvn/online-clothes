using OnlineClothes.Domain.Paging;

namespace OnlineClothes.Application.Features.Orders.Queries.Listing;

public class OrderListingQuery : PagingRequest, IRequest<JsonApiResponse<PagingModel<OrderListingQueryViewModel>>>
{
	public OrderListingQuery(int pageIndex, int pageSize) : base(pageIndex, pageSize)
	{
	}
}
