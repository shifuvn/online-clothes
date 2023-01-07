namespace OnlineClothes.Application.Features.Orders.Commands.Cancel;

public class CancelOrderCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public CancelOrderCommand(string orderId)
	{
		OrderId = orderId;
	}

	public string OrderId { get; set; }
}
