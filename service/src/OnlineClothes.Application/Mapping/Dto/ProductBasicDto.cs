namespace OnlineClothes.Application.Mapping.Dto;

public class ProductBasicDto
{
	public ProductBasicDto(Product domain)
	{
		Id = domain.Id;
		Name = domain.Name;
		Description = domain.Description;
		Price = domain.Price;
		Skus = domain.ProductSkus.SelectList(q => new SkuInfo(q.Sku, q.IsDeleted, q.DisplaySkuName));
		Brand = domain.Brand is null ? null : new BrandDto(domain.Brand);
		Category = domain.ProductCategories.SelectList(q => new CategoryDto(q.Category));
		ThumbnailUrl = domain.ThumbnailImage?.Url;
		IsPublish = domain.IsPublish;
		CreatedAt = domain.CreatedAt;
	}

	public int Id { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }
	public decimal Price { get; set; }
	public List<SkuInfo> Skus { get; set; }
	public BrandDto? Brand { get; set; }
	public List<CategoryDto> Category { get; set; }
	public string? ThumbnailUrl { get; set; }
	public bool IsPublish { get; set; }
	public DateTime CreatedAt { get; set; }

	public class SkuInfo
	{
		public SkuInfo(string id, bool isDeleted, string? displaySkuName)
		{
			Id = id;
			IsDeleted = isDeleted;
			DisplaySkuName = displaySkuName;
		}

		public string Id { get; set; }
		public string? DisplaySkuName { get; set; }
		public bool IsDeleted { get; set; }
	}
}
