using OnlineClothes.Domain.Entities.Aggregate;

namespace OnlineClothes.Domain.Entities;

public class CartItem
{
	public CartItem()
	{
	}

	public CartItem(string productSku, int quantity = 1) : this()
	{
		ProductSkuId = productSku;
		Quantity = quantity;
	}

	public CartItem(int cartId, string productSku, int quantity = 1) : this(productSku, quantity)
	{
		CartId = cartId;
	}

	public CartItem(AccountCart cart, string productSku, int quantity = 1) : this(productSku, quantity)
	{
		Cart = cart;
	}

	public int CartId { get; set; }
	public string ProductSkuId { get; set; } = null!;
	public int Quantity { get; set; }

	[ForeignKey("CartId")] public AccountCart Cart { get; set; } = null!;
	[ForeignKey("ProductSkuId")] public ProductSku ProductSku { get; set; } = null!;

	public void UpdateNumber(int number)
	{
		Quantity = number;
	}

	public decimal IntoMoney()
	{
		return ProductSku.GetPrice() * Quantity;
	}
}
