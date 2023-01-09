using System.ComponentModel;

namespace OnlineClothes.Application.Persistence.Schemas.Products;

public class PutProductInRepoObject
{
	public string Name { get; set; } = null!;
	public string? Description { get; set; }
	public double Price { get; set; }
	public ClotheType? Type { get; set; }
	public int? BrandId { get; set; }
	public HashSet<int> CategoryIds { get; set; } = new();
	[DefaultValue(true)] public bool IsPublish { get; set; }
}
