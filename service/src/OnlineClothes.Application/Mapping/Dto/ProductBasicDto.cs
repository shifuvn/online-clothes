namespace OnlineClothes.Application.Mapping.Dto;

public class ProductBasicDto
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public decimal Price { get; set; }
	public List<string> Skus { get; set; } = new();
	public string? ThumbnailUrl { get; set; }
	public bool IsPublish { get; set; }
	public DateTime CreatedAt { get; set; }

	public static ProductBasicDto ToModel(Product entity)
	{
		return new ProductBasicDto
		{
			Id = entity.Id,
			Name = entity.Name,
			Price = entity.Price,
			Skus = entity.ProductSkus.SelectList(q => q.Sku),
			ThumbnailUrl = entity.ThumbnailImage?.Url,
			IsPublish = entity.IsPublish,
			CreatedAt = entity.CreatedAt
		};
	}
}
