namespace OnlineClothes.Application.Features.Orders.Commands.Checkout;

public class CheckoutOrderCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public CheckoutOrderCommand(string address, string? note)
	{
		Address = address;
		Note = note;
	}

	public string Address { get; set; }
	public string? Note { get; set; }
}
