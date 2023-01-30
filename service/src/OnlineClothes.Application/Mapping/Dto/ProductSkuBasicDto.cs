namespace OnlineClothes.Application.Mapping.Dto;

public class ProductSkuBasicDto
{
	public ProductSkuBasicDto(int productId, string sku, string name, decimal price, int inStock, DateTime createdAt,
		string? imageUrl = null)
	{
		ProductId = productId;
		Sku = sku;
		Name = name;
		Price = price;
		InStock = inStock;
		CreatedAt = createdAt;
		ImageUrl = imageUrl;
	}

	public int ProductId { get; set; }
	public string Sku { get; set; } = null!;
	public string Name { get; set; } = null!;
	public decimal Price { get; set; }
	public int InStock { get; set; }
	public string? ImageUrl { get; set; }
	public DateTime CreatedAt { get; set; }

	public static ProductSkuBasicDto ToModel(ProductSku entity)
	{
		return new ProductSkuBasicDto(
			entity.ProductId,
			entity.Sku,
			entity.Product.Name,
			entity.GetPrice(),
			entity.InStock,
			entity.CreatedAt,
			entity.Image?.Url);
	}
}
