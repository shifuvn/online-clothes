using OnlineClothes.Domain.Entities;
using OnlineClothes.Domain.Entities.Aggregate;

namespace OnlineClothes.UnitTest.Domain;

[Collection("Domain AccountCart")]
public class AccountCartTest
{
	[Fact]
	[Trait("Category", "Domain_AccountCart")]
	public void AddItem_ToEmptyCart()
	{
		// arrange
		var cart = new AccountCart();

		// act
		cart.IncreaseItem(1, 2);

		// assert
		Assert.NotNull(cart);
		Assert.Single(cart.Items);
	}

	[Fact]
	[Trait("Category", "Domain_AccountCart")]
	public void AddItem_ToExistingCart()
	{
		// arrange
		var cart = new AccountCart
		{
			AccountId = 1,
			Items = new List<CartItem> { new(1, 2) }
		};

		// act
		cart.IncreaseItem(2, 2);
		cart.IncreaseItem(1, 4);

		var item1 = cart.Items.First();

		// assert
		Assert.NotNull(cart);
		Assert.Equal(2, cart.Items.Count);
		Assert.Equal(6, item1.Quantity);
	}

	[Fact]
	[Trait("Category", "Domain_AccountCart")]
	public void RemoveItem_EmptyCart()
	{
		// arrange
		var cart = new AccountCart
		{
			AccountId = 1,
			Items = new List<CartItem>
			{
				new(1, 2),
				new(2, 2)
			}
		};

		// act
		cart.DecreaseItem(1, 6);
		cart.DecreaseItem(2, 2);

		// assert
		Assert.NotNull(cart);
		Assert.Empty(cart.Items);
	}
}
