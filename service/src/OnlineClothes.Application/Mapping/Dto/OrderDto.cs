using OnlineClothes.Support.Utilities.Extensions;

namespace OnlineClothes.Application.Mapping.Dto;

public class OrderDto
{
	public int Id { get; set; }
	public string Email { get; set; } = null!;
	public OrderState State { get; set; }
	public decimal TotalPaid { get; set; }
	public bool IsPaid { get; set; }
	public string Address { get; set; } = null!;
	public string? Note { get; set; }
	public List<OrderItemDto> Items { get; set; } = new();

	public static OrderDto ToModel(Order entity)
	{
		var items = new List<OrderItemDto>();
		if (entity.Items.Any())
		{
			items = entity.Items.SelectList(OrderItemDto.ToModel);
		}

		return new OrderDto
		{
			Id = entity.Id,
			Email = entity.Account.Email,
			State = entity.State,
			TotalPaid = entity.TotalPaid,
			IsPaid = entity.IsPaid,
			Address = entity.Address,
			Note = entity.Note,
			Items = items
		};
	}
}
