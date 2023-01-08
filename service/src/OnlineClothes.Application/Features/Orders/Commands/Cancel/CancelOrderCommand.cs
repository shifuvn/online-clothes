namespace OnlineClothes.Application.Features.Orders.Commands.Cancel;

public class CancelOrderCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public CancelOrderCommand(int orderId)
	{
		OrderId = orderId;
	}

	public int OrderId { get; set; }
}
