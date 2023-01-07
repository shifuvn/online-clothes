namespace OnlineClothes.Application.Mapping.Dto;

public class ProductSkuBasicDto
{
	public ProductSkuBasicDto(int productId, string sku, string name, decimal price, int inStock)
	{
		ProductId = productId;
		Sku = sku;
		Name = name;
		Price = price;
		InStock = inStock;
	}

	public int ProductId { get; set; }
	public string Sku { get; set; } = null!;
	public string Name { get; set; } = null!;
	public decimal Price { get; set; }
	public int InStock { get; set; }

	//public string? ImageUrl { get; set; }

	public static ProductSkuBasicDto ToModel(ProductSku entity)
	{
		return new ProductSkuBasicDto(entity.ProductId, entity.Sku, entity.Product.Name, entity.GetPrice(),
			entity.InStock);
	}
}
