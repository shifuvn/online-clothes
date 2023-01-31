namespace OnlineClothes.Application.Mapping.Dto;

public class ProductBasicDto
{
	public ProductBasicDto(Product domain)
	{
		Id = domain.Id;
		Name = domain.Name;
		Price = domain.Price;
		Skus = domain.ProductSkus.SelectList(q => new SkuInfo(q.Sku, q.IsDeleted));
		Brand = domain.Brand is null ? null : new BrandDto(domain.Brand);
		Type = domain.ProductType is null ? null : new ProductTypeDto(domain.ProductType);
		Category = domain.ProductCategories.SelectList(q => new CategoryDto(q.Category));
		ThumbnailUrl = domain.ThumbnailImage?.Url;
		IsPublish = domain.IsPublish;
		CreatedAt = domain.CreatedAt;
	}

	public int Id { get; set; }
	public string Name { get; set; }
	public decimal Price { get; set; }
	public List<SkuInfo> Skus { get; set; }
	public BrandDto? Brand { get; set; }
	public ProductTypeDto? Type { get; set; }
	public List<CategoryDto> Category { get; set; }
	public string? ThumbnailUrl { get; set; }
	public bool IsPublish { get; set; }
	public DateTime CreatedAt { get; set; }

	public class SkuInfo
	{
		public SkuInfo(string id, bool isDeleted)
		{
			Id = id;
			IsDeleted = isDeleted;
		}

		public string Id { get; set; }
		public bool IsDeleted { get; set; }
	}
}
