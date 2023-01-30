namespace OnlineClothes.Application.Mapping.Dto;

public class ProductSkuDto : ProductSkuBasicDto
{
	public ProductSkuDto(
		int productId,
		string sku,
		string name,
		decimal price,
		int inStock,
		DateTime createdAt) : base(productId, sku, name, price, inStock, createdAt)
	{
	}

	public string? Description { get; set; }
	public ClotheSizeType? Size { get; set; }
	public ClotheType? Type { get; set; }
	public BrandDto? Brand { get; set; }
	public List<CategoryDto> Categories { get; set; } = new();
	public bool IsDeleted { get; set; }
	public DateTime ModifiedAt { get; set; }

	public new static ProductSkuDto ToModel(ProductSku entity)
	{
		return new ProductSkuDto(
			entity.ProductId,
			entity.Sku,
			entity.Product.Name,
			entity.GetPrice(), entity.InStock,
			entity.CreatedAt)
		{
			Description = entity.Product.Description,
			Brand = BrandDto.ToModel(entity.Product.Brand),
			Size = entity.Size,
			Type = entity.Product.Type,
			Categories =
				entity.Product.ProductCategories.SelectList(category => CategoryDto.ToModel(category.Category)),
			CreatedAt = entity.CreatedAt,
			ImageUrl = entity.Image?.Url,
			IsDeleted = entity.IsDeleted,
			ModifiedAt = entity.ModifiedAt
		};
	}
}
