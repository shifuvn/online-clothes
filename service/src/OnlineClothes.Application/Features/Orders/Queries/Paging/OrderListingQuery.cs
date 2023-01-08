using OnlineClothes.Domain.Paging;

namespace OnlineClothes.Application.Features.Orders.Queries.Paging;

public class OrderListingQuery : PagingRequest, IRequest<JsonApiResponse<PagingModel<OrderDto>>>
{
	public OrderListingQuery()
	{
	}

	public OrderListingQuery(PagingRequest page) : base(page)
	{
	}
}
