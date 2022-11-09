namespace OnlineClothes.Application.Features.Cart.Queries.GetInfo;

public class GetCartInfoQueryViewModel
{
	public GetCartInfoQueryViewModel(Item items)
	{
		Items = items;
	}

	public Item Items { get; set; }

	public class Item
	{
		public Item(string productId, string productName, string quantity, double price, string imageUrl)
		{
			ProductId = productId;
			ProductName = productName;
			Quantity = quantity;
			Price = price;
			ImageUrl = imageUrl;
		}

		public string ProductId { get; set; }
		public string ProductName { get; set; }
		public string Quantity { get; set; }
		public double Price { get; set; }
		public string ImageUrl { get; set; }
	}
}
