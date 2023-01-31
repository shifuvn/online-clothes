namespace OnlineClothes.Application.Mapping.Dto;

public class ProductSkuDto : ProductSkuBasicDto
{
	public ProductSkuDto(
		int productId,
		string sku,
		string name,
		decimal price,
		int inStock,
		DateTime createdAt,
		bool isDeleted) : base(productId, sku, name, price, inStock, createdAt, isDeleted)
	{
	}

	public string? Description { get; set; }
	public ClotheSizeType? Size { get; set; }
	public ClotheType? Type { get; set; }
	public BrandDto? Brand { get; set; }
	public List<CategoryDto> Categories { get; set; } = new();
	public DateTime ModifiedAt { get; set; }

	public new static ProductSkuDto ToModel(ProductSku entity)
	{
		var result = new ProductSkuDto(
			entity.ProductId,
			entity.Sku,
			entity.Product.Name,
			entity.GetPrice(), entity.InStock,
			entity.CreatedAt,
			entity.IsDeleted)
		{
			Description = entity.Product.Description,
			Size = entity.Size,
			Type = entity.Product.Type,
			Categories =
				entity.Product.ProductCategories.SelectList(category => new CategoryDto(category.Category)),
			CreatedAt = entity.CreatedAt,
			ImageUrl = entity.Image?.Url,
			ModifiedAt = entity.ModifiedAt
		};

		if (entity.Product.Brand is not null)
		{
			result.Brand = new BrandDto(entity.Product.Brand);
		}

		return result;
	}
}
