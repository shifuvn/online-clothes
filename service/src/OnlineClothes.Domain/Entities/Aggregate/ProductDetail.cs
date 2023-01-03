using System.ComponentModel.DataAnnotations.Schema;
using OnlineClothes.Domain.Common;
using OnlineClothes.Support.Entity;

namespace OnlineClothes.Domain.Entities.Aggregate;

public class ProductDetail : EntityBase
{
	[ForeignKey("ProductId")] public Product Product { get; set; } = null!;
	public string ProductSku { get; set; } = null!;

	public double Price { get; set; }

	public int InStock { get; set; }

	public ClotheSizeType? SizeType { get; set; }

	[ForeignKey("MaterialTypeId")] public ClotheMaterialType? MaterialType { get; set; }
	public int? MaterialTypeId { get; set; }
}
