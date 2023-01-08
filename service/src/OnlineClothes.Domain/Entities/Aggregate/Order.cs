using Newtonsoft.Json;

namespace OnlineClothes.Domain.Entities.Aggregate;

public class Order : EntityBase
{
	public Order()
	{
	}

	public Order(int accountId, decimal totalPaid, string address, string? note, OrderState state, bool isPaid = false)
	{
		AccountId = accountId;
		TotalPaid = totalPaid;
		Address = address;
		Note = note;
		State = state;
		IsPaid = isPaid;
	}

	public Order(int accountId,
		decimal totalPaid,
		string address,
		string? note,
		OrderState state,
		bool isPaid,
		ICollection<OrderItem> items) : this(accountId, totalPaid, address, note, state, isPaid)
	{
		// ReSharper disable once VirtualMemberCallInConstructor
		Items = items;
	}


	public int AccountId { get; set; }
	public OrderState State { get; set; }
	public decimal TotalPaid { get; set; }
	public bool IsPaid { get; set; }
	public string Address { get; set; } = null!;
	public string? Note { get; set; }

	[ForeignKey("AccountId")] public AccountUser Account { get; set; } = null!;

	[JsonIgnore] public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
