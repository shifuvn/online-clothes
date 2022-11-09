using OnlineClothes.Domain.Attributes;

namespace OnlineClothes.Domain.Entities;

[BsonCollection("carts")]
public class AccountCart : RootDocumentBase
{
	public string AccountId { get; set; }

	public HashSet<CartItem> Items { get; set; } = new();

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

		public string ProductId { get; set; }
		public int Quantity { get; set; }
	}
}
