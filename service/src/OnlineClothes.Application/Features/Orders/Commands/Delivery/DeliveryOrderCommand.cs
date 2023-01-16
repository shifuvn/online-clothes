namespace OnlineClothes.Application.Features.Orders.Commands.Delivery;

public class DeliveryOrderCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public DeliveryOrderCommand(int orderId)
	{
		OrderId = orderId;
	}

	public int OrderId { get; set; }
}
