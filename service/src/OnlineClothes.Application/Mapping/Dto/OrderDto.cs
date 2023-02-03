namespace OnlineClothes.Application.Mapping.Dto;

public class OrderDto
{
	public OrderDto(Order domain)
	{
		var items = new List<OrderItemDto>();
		if (domain.Items.Any())
		{
			items = domain.Items.SelectList(OrderItemDto.ToModel);
		}

		Id = domain.Id;
		Email = domain.Account.Email;
		State = domain.State.ToString();
		TotalPaid = domain.TotalPaid;
		IsPaid = domain.IsPaid;
		Address = domain.Address;
		Note = domain.Note;
		Items = items;
		CreatedAt = domain.CreatedAt;
	}

	public int Id { get; set; }
	public string Email { get; set; }
	public string State { get; set; }
	public decimal TotalPaid { get; set; }
	public bool IsPaid { get; set; }
	public string Address { get; set; }
	public string? Note { get; set; }
	public List<OrderItemDto> Items { get; set; }
	public DateTime CreatedAt { get; set; }
}
