using System.ComponentModel.DataAnnotations.Schema;
using OnlineClothes.Domain.Entities.Aggregate;

namespace OnlineClothes.Domain.Entities;

public class ProductInMaterial
{
	public ProductInMaterial()
	{
	}

	public ProductInMaterial(int clotheDetailId, int materialId)
	{
		ClotheDetailId = clotheDetailId;
		MaterialId = materialId;
	}

	public ProductInMaterial(ProductDetail detail, ClotheMaterialType materialType)
	{
		Detail = detail;
		MaterialType = materialType;
	}

	[ForeignKey("ClotheDetailId")] public ProductDetail Detail { get; set; } = null!;
	public int ClotheDetailId { get; set; }

	[ForeignKey("MaterialId")] public ClotheMaterialType MaterialType { get; set; } = null!;
	public int MaterialId { get; set; }
}
