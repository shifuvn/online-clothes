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
		int? typeId,
		bool isDeleted = false) : this(name, price, isDeleted)
	{
		Description = description;
		BrandId = brandId;
		TypeId = typeId;
	}

	public string Name { get; set; } = null!;
	public string? Description { get; set; }
	public decimal Price { get; set; }
	[DefaultValue(null)] public int? BrandId { get; set; }
	public int? TypeId { get; set; }
	[DefaultValue(true)] public bool IsPublish { get; set; }
	public int? ThumbnailImageId { get; set; }

	[ForeignKey("ThumbnailImageId")] public ImageObject? ThumbnailImage { get; set; }
	[ForeignKey("BrandId")] public Brand? Brand { get; set; }
	[ForeignKey("TypeId")] public ProductType? ProductType { get; set; }

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
