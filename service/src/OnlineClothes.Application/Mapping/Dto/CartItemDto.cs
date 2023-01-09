using OnlineClothes.Domain.Entities;

namespace OnlineClothes.Application.Mapping.Dto;

public class CartItemDto
{
	public CartItemDto()
	{
	}

	public CartItemDto(ProductSkuBasicDto productSku, int quantity)
	{
		ProductSku = productSku;
		Quantity = quantity;
	}

	public ProductSkuBasicDto ProductSku { get; set; } = null!;
	public int Quantity { get; set; }

	public static CartItemDto ToModel(CartItem entity)
	{
		return new CartItemDto
		{
			Quantity = entity.Quantity,
			ProductSku = ProductSkuBasicDto.ToModel(entity.ProductSku)
		};
	}
}
