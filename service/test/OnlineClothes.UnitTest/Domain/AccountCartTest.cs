using OnlineClothes.Domain.Entities;

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
		cart.AddItem("0x123", 2);

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
			AccountId = "0xabc",
			Items = new List<AccountCart.CartItem> { new("0x123", 2) }
		};

		// act
		cart.AddItem("0x456", 2);
		cart.AddItem("0x123", 4);

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
			AccountId = "0xabc",
			Items = new List<AccountCart.CartItem>
			{
				new("0x123", 2),
				new("0x456", 2)
			}
		};

		// act
		cart.RemoveItem("0x123", 6);
		cart.RemoveItem("0x456", 2);

		// assert
		Assert.NotNull(cart);
		Assert.Empty(cart.Items);
	}
}
