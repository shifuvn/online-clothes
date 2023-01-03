using System.ComponentModel.DataAnnotations.Schema;
using OnlineClothes.Domain.Entities.Aggregate;

namespace OnlineClothes.Domain.Entities;

public class CartItem
{
	public CartItem()
	{
	}

	public CartItem(int productId, int quantity = 1) : this()
	{
		ProductId = productId;
		Quantity = quantity;
	}

	public CartItem(int cartId, int productId, int quantity = 1) : this(productId, quantity)
	{
		CartId = cartId;
	}

	public CartItem(AccountCart cart, int productId, int quantity = 1) : this(productId, quantity)
	{
		Cart = cart;
	}

	[ForeignKey("CartId")] public AccountCart Cart { get; set; } = null!;
	public int CartId { get; set; }

	// foreign key
	public int ProductId { get; set; }
	public int Quantity { get; set; }

	public void Increase(int number)
	{
		Quantity += number;
	}

	public void Decrease(int number)
	{
		Quantity -= number;
	}

	public static CartItem Create(AccountCart cart, int productId, int quantity)
	{
		return new CartItem(cart, productId, quantity);
	}

	public static CartItem Create(int cartId, int productId, int quantity)
	{
		return new CartItem(cartId, productId, quantity);
	}
}
