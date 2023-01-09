using OnlineClothes.Domain.Entities.Aggregate;

namespace OnlineClothes.Domain.Entities;

public class ProductCategory
{
	public int ProductId { get; set; }
	[ForeignKey("ProductId")] public Product Product { get; set; } = null!;
	public int CategoryId { get; set; }
	[ForeignKey("CategoryId")] public Category Category { get; set; } = null!;

	public static ProductCategory ToEntity(int productId, int categoryId)
	{
		return new ProductCategory { CategoryId = categoryId, ProductId = productId };
	}
}
