namespace OnlineClothes.Application.Features.Order.Queries.Listing;

public class OrderListingQueryViewModel
{
	public OrderListingQueryViewModel(string orderId, string customerEmail, double totalPrice, DateTime createdAt)
	{
		OrderId = orderId;
		CustomerEmail = customerEmail;
		TotalPrice = totalPrice;
		CreatedAt = createdAt;
	}

	public string OrderId { get; set; }
	public string CustomerEmail { get; set; }
	public double TotalPrice { get; set; }
	public DateTime CreatedAt { get; set; }
}
