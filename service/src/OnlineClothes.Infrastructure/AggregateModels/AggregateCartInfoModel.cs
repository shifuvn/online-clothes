using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OnlineClothes.Domain.Entities;

namespace OnlineClothes.Infrastructure.AggregateModels;

public class AggregateCartInfoModel
{
	public AggregateCartInfoModel(string id, List<Item> items)
	{
		Id = id;
		Items = items;
	}

	public string Id { get; set; }

	public List<Item> Items { get; set; }


	public class Item
	{
		public Item(string productId, int quantity, string name, double price, string? imageUrl = null)
		{
			ProductId = productId;
			Quantity = quantity;
			Name = name;
			Price = price;
			ImageUrl = imageUrl;
		}

		public string ProductId { get; set; }
		public int Quantity { get; set; }
		public string Name { get; set; }
		public double Price { get; set; }
		public string? ImageUrl { get; set; }

		public static Item Create(AggregateLookupCart input)
		{
			var detail = input.Items.Details.First();
			return new Item(input.Items.ProductId, input.Items.Quantity, detail.Name, detail.Price,
				detail.ImageUrls.FirstOrDefault());
		}
	}
}

public class AggregateLookupCart
{
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; }

	public Item Items { get; set; }

	public class Item
	{
		public Item(string productId, int quantity, List<ProductClothe> details)
		{
			ProductId = productId;
			Quantity = quantity;
			Details = details;
		}

		[BsonRepresentation(BsonType.ObjectId)]
		public string ProductId { get; set; }

		public int Quantity { get; set; }
		public List<ProductClothe> Details { get; set; }
	}
}
