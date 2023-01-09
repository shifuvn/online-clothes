using OnlineClothes.Domain.Entities;

namespace OnlineClothes.Application.Mapping.Dto;

public class OrderItemDto
{
	public string ProductName { get; set; } = null!;
	public string ProductSkuId { get; set; } = null!;
	public int Quantity { get; set; }
	public decimal Price { get; set; }

	public static OrderItemDto ToModel(OrderItem entity)
	{
		return new OrderItemDto
		{
			ProductName = entity.ProductSku.Product.Name,
			ProductSkuId = entity.ProductSkuId,
			Quantity = entity.Quantity,
			Price = entity.Price
		};
	}
}
