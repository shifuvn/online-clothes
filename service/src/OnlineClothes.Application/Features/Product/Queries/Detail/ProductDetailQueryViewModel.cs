using OnlineClothes.Domain.Common;
using OnlineClothes.Domain.Entities;

namespace OnlineClothes.Application.Features.Product.Queries.Detail;

public class ProductDetailQueryViewModel
{
	public string Name { get; set; } = null!;
	public string Description { get; set; } = null!;
	public double Price { get; set; }
	public int Stock { get; set; }
	public HashSet<string> Tags { get; set; } = new();
	public HashSet<string> Sizes { get; set; } = new();
	public HashSet<string> Materials { get; set; } = new();
	public string Type { get; set; } = ClotheType.Unknown.ToString();
	public List<string> ImageUrls { get; set; } = new();

	public static ProductDetailQueryViewModel Create(ProductClothe input)
	{
		var sizes = input.Detail.Sizes.Select(q => q.ToString()).ToHashSet();
		var materials = input.Detail.Materials.Select(q => q.ToString()).ToHashSet();

		return new ProductDetailQueryViewModel
		{
			Name = input.Name,
			Description = input.Description,
			Price = input.Price,
			Stock = input.Stock,
			ImageUrls = input.ImageUrls,
			Tags = input.Tags,
			Sizes = sizes,
			Materials = materials,
			Type = input.Detail.Type.ToString()
		};
	}
}
