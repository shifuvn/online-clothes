using OnlineClothes.Domain.Entities.Aggregate;
using OnlineClothes.Support.Entity;

namespace OnlineClothes.Domain.Entities;

public class ClotheMaterialType : EntityBase
{
	public ClotheMaterialType()
	{
	}

	public ClotheMaterialType(string materialName)
	{
		MaterialName = materialName;
	}

	public string MaterialName { get; set; } = null!;

	public ICollection<ProductDetail> ProductDetails { get; set; } = new List<ProductDetail>();

	public void Rename(string newName)
	{
		MaterialName = newName;
	}
}
