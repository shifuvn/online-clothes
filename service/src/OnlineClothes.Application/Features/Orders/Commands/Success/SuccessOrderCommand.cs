namespace OnlineClothes.Application.Features.Orders.Commands.Success;

public class SuccessOrderCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public SuccessOrderCommand(string orderId)
	{
		OrderId = orderId;
	}

	public string OrderId { get; set; }
}
