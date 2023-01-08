namespace OnlineClothes.Application.Features.Orders.Commands.Success;

public class SuccessOrderCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public SuccessOrderCommand(int orderId)
	{
		OrderId = orderId;
	}

	public int OrderId { get; set; }
}
