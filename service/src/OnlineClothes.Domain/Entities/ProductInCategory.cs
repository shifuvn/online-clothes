using System.ComponentModel.DataAnnotations.Schema;
using OnlineClothes.Domain.Entities.Aggregate;

namespace OnlineClothes.Domain.Entities;

public class ProductInCategory
{
	[ForeignKey("ProductId")] public Product Product { get; set; } = null!;
	public int ProductId { get; set; }

	[ForeignKey("ClotheCategoryId")] public ClotheCategory ClotheCategory { get; set; } = null!;
	public int ClotheCategoryId { get; set; }
}
