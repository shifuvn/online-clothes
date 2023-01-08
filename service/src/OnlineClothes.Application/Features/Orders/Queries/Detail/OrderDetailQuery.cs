namespace OnlineClothes.Application.Features.Orders.Queries.Detail;

public class OrderDetailQuery : IRequest<JsonApiResponse<OrderDto>>
{
	public OrderDetailQuery(int orderId)
	{
		OrderId = orderId;
	}

	public int OrderId { get; set; }
}
