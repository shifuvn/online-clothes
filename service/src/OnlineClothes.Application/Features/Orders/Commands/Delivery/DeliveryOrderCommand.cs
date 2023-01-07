namespace OnlineClothes.Application.Features.Orders.Commands.Delivery;

public class DeliveryOrderCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public DeliveryOrderCommand(string orderId)
	{
		OrderId = orderId;
	}

	public string OrderId { get; set; }
}
