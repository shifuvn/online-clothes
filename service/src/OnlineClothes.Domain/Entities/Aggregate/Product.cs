using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;

namespace OnlineClothes.Domain.Entities.Aggregate;

public class Product : EntityBase
{
	public Product()
	{
	}

	public Product(string name, decimal price, bool isPublish = false)
	{
		Name = name;
		Price = price;
		IsPublish = isPublish;
	}

	public Product(
		string name,
		string? description,
		decimal price,
		int? brandId,
		ClotheType? type,
		bool isDeleted = false) : this(name, price, isDeleted)
	{
		Description = description;
		BrandId = brandId;
		Type = type;
	}

	public string Name { get; set; } = null!;
	public string? Description { get; set; }
	public decimal Price { get; set; }
	[DefaultValue(null)] public int? BrandId { get; set; }
	public ClotheType? Type { get; set; }
	[DefaultValue(true)] public bool IsPublish { get; set; }

	[ForeignKey("BrandId")] public Brand? Brand { get; set; }

	[JsonIgnore] public virtual ICollection<ProductSku> ProductSkus { get; set; } = new Collection<ProductSku>();

	[JsonIgnore]
	public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new Collection<ProductCategory>();

	public bool IsAvailable()
	{
		return IsPublish;
	}

	public void Delete()
	{
		IsPublish = false;
	}

	public void Restore()
	{
		IsPublish = true;
	}
}
