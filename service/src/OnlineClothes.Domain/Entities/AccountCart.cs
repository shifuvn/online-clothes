using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OnlineClothes.Domain.Attributes;

namespace OnlineClothes.Domain.Entities;

[BsonCollection("carts")]
public class AccountCart : RootDocumentBase
{
	public AccountCart()
	{
	}

	public AccountCart(string accountId, List<CartItem> items)
	{
		AccountId = accountId;
		Items = items;
	}

	[BsonRepresentation(BsonType.ObjectId)]
	public string AccountId { get; set; }

	public List<CartItem> Items { get; set; }

	public void AddItem(string productId, int quantity)
	{
		foreach (var cartItem in Items)
		{
			if (cartItem.ProductId != productId) continue;

			cartItem.Quantity += quantity;
			PostCheck(cartItem);

			return;
		}

		Items.Add(new CartItem(productId, quantity));
	}

	public void RemoveItem(string productId, int quantity)
	{
		foreach (var cartItem in Items)
		{
			if (cartItem.ProductId != productId) continue;

			cartItem.Quantity -= quantity;
			PostCheck(cartItem);

			return;
		}
	}

	private void PostCheck(CartItem item)
	{
		if (item.Quantity <= 0)
		{
			Items.Remove(item);
		}
	}

	public class CartItem
	{
		public CartItem(string productId, int quantity)
		{
			ProductId = productId;
			Quantity = quantity;
		}

		public CartItem(string productId, int quantity, ProductClothe detail) : this(productId, quantity)
		{
			Detail = detail;
		}

		[BsonRepresentation(BsonType.ObjectId)]
		public string ProductId { get; set; }

		public int Quantity { get; set; }

		// used for $lookup
		public ProductClothe Detail { get; set; }
	}
}
