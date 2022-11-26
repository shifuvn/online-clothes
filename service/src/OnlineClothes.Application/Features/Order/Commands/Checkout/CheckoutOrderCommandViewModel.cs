namespace OnlineClothes.Application.Features.Order.Commands.Checkout;

public class CheckoutOrderCommandViewModel
{
	public CheckoutOrderCommandViewModel(string errorId)
	{
		ErrorId = errorId;
	}

	public string ErrorId { get; set; }
}
