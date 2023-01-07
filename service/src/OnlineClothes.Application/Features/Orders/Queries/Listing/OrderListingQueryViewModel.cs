namespace OnlineClothes.Application.Features.Orders.Queries.Listing;

public class OrderListingQueryViewModel
{
	public OrderListingQueryViewModel(string orderId, string customerEmail, double totalPrice, DateTime createdAt,
		OrderState state)
	{
		OrderId = orderId;
		CustomerEmail = customerEmail;
		TotalPrice = totalPrice;
		CreatedAt = createdAt;
		State = state;
	}

	public string OrderId { get; set; }
	public string CustomerEmail { get; set; }
	public double TotalPrice { get; set; }
	public DateTime CreatedAt { get; set; }
	public OrderState State { get; set; }
}
