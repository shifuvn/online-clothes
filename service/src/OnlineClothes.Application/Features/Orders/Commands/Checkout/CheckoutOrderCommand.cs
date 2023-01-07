namespace OnlineClothes.Application.Features.Orders.Commands.Checkout;

public class CheckoutOrderCommand : IRequest<JsonApiResponse<CheckoutOrderCommandViewModel>>
{
	public CheckoutOrderCommand(string address)
	{
		Address = address;
	}

	public string Address { get; set; }
}
