namespace OnlineClothes.Application.Features.Orders.Commands.Checkout;

public class CheckoutOrderCommandViewModel
{
	public CheckoutOrderCommandViewModel(string errorId)
	{
		ErrorId = errorId;
	}

	public string ErrorId { get; set; }
}
