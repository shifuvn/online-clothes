using OnlineClothes.Infrastructure.AggregateModels;

namespace OnlineClothes.Application.Features.Cart.Queries.GetInfo;

public class GetCartInfoQueryViewModel
{
	public GetCartInfoQueryViewModel(List<Item>? items)
	{
		Items = items;
	}

	public List<Item>? Items { get; set; }

	public class Item
	{
		public Item(string productId, string productName, int quantity, double price, string? imageUrl = null)
		{
			ProductId = productId;
			ProductName = productName;
			Quantity = quantity;
			Price = price;
			ImageUrl = imageUrl;
		}

		public string ProductId { get; set; }
		public string ProductName { get; set; }
		public int Quantity { get; set; }
		public double Price { get; set; }
		public string? ImageUrl { get; set; }

		public static Item Create(AggregateCartInfoModel.Item input)
		{
			return new Item(input.ProductId, input.Name, input.Quantity, input.Price, input.ImageUrl);
		}
	}
}
