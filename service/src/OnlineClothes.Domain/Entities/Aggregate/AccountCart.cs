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

	public decimal TotalPaid()
	{
		return Items.Sum(item => item.IntoMoney());
	}

	public bool IsEmpty()
	{
		return Items.Count == 0;
	}

	public void UpdateItemQuantity(string sku, int number)
	{
		var item = new CartItem(this, sku, number);
		var entry = FindItemInCart(item.ProductSkuId);

		if (entry is null)
		{
			Items.Add(item);
			PostCheck(item);
		}
		else
		{
			entry.UpdateNumber(item.Quantity);
			PostCheck(entry);
		}
	}

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

	public void Clear()
	{
		Items.Clear();
	}
}
