namespace OnlineClothes.Domain.Entities.Aggregate;

public class AccountCart : EntityBase
{
	public AccountCart()
	{
	}

	public AccountCart(AccountUser account) : this()
	{
		Account = account;
	}

	public AccountCart(int accountId) : this()
	{
		AccountId = accountId;
	}

	public int AccountId { get; set; }

	[ForeignKey("AccountId")] public AccountUser Account { get; set; } = null!;

	public virtual ICollection<CartItem> Items { get; set; } = new List<CartItem>();

	/// <summary>
	/// Put new item in cart.
	/// <para>If item is already in cart, it will be increase number</para>
	/// </summary>
	public void PutItem(string skuId, int number)
	{
		var item = new CartItem(this, skuId, number);
		var entry = FindItemInCart(item.ProductSkuId);

		if (entry is null)
		{
			Items.Add(item);
		}
		else
		{
			entry.Increase(item.Quantity);
			PostCheck(entry);
		}
	}

	/// <summary>
	/// If item is already in cart, it will be decreased till ZERO.
	/// <para>If number is ZERO, item will be remove from cart</para>
	/// </summary>
	public void RemoveItem(string skuId, int number)
	{
		var item = new CartItem(this, skuId, number);
		var entry = FindItemInCart(item.ProductSkuId);

		if (entry is null)
		{
			return;
		}

		entry.Decrease(item.Quantity);
		PostCheck(entry);
	}

	//public void IncreaseItem(string productSku, int number)
	//{
	//	var item = FindItemInCart(productSku);

	//	if (item is null)
	//	{
	//		Items.Add(new CartItem(Id, productSku, number));
	//		return;
	//	}

	//	item.Increase(number);
	//	PostCheck(item);
	//}

	//public void DecreaseItem(string productSku, int number)
	//{
	//	var item = FindItemInCart(productSku);

	//	if (item is null)
	//	{
	//		return;
	//	}

	//	item.Decrease(number);
	//	PostCheck(item);
	//}

	private CartItem? FindItemInCart(string skuId)
	{
		return Items.FirstOrDefault(q => q.ProductSkuId == skuId);
	}

	private void PostCheck(CartItem item)
	{
		if (item.Quantity <= 0)
		{
			Items.Remove(item);
		}
	}
}
