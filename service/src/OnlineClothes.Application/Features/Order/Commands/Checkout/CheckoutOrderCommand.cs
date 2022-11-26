using MediatR;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Order.Commands.Checkout;

public class CheckoutOrderCommand : IRequest<JsonApiResponse<CheckoutOrderCommandViewModel>>
{
	public CheckoutOrderCommand(string address)
	{
		Address = address;
	}

	public string Address { get; set; }
}
