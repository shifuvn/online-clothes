using OnlineClothes.Support.Utilities.Extensions;

namespace OnlineClothes.Application.Mapping.Dto;

public class ProductBasicDto
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public decimal Price { get; set; }
	public List<string> Skus { get; set; } = new();

	// todo: Image

	public static ProductBasicDto ToModel(Product entity)
	{
		return new ProductBasicDto
		{
			Id = entity.Id,
			Name = entity.Name,
			Price = entity.Price,
			Skus = entity.ProductSkus.SelectList(q => q.Sku)
		};
	}
}
