using OnlineClothes.Domain.Attributes;
using OnlineClothes.Domain.Common;
using OnlineClothes.Support.Utilities;

namespace OnlineClothes.Domain.Entities;

[BsonCollection("products")]
public class ProductClothe : RootDocumentBase
{
	public ProductClothe()
	{
	}

	public ProductClothe(string name, string description, double price, uint stock)
	{
		Name = name;
		Description = description;
		Price = price;
		Stock = stock;
	}

	public string Name { get; set; } = null!;
	public string Description { get; set; } = null!;
	public HashSet<string> Tags { get; set; } = new();
	public double Price { get; set; }
	public uint Stock { get; set; }

	public HashSet<string> ImageUrls { get; set; } = new();

	public ClotheDetail Detail { get; set; } = new();

	public bool IsValid()
	{
		return Stock > 0 && Price > 0;
	}

	public void ChangePrice(double newPrice)
	{
		Price = newPrice;
	}

	public void ImportStock(uint count = 1)
	{
		Stock += count;
	}

	public void ExportStock(uint count = 1)
	{
		if (Stock >= count)
		{
			Stock -= count;
		}
	}

	public static ProductClothe Create(string name,
		string description,
		double price = 0,
		uint stock = 0,
		ICollection<string>? tags = null,
		ClotheDetail? detail = null)
	{
		var product = new ProductClothe
		{
			Name = name,
			Description = description,
			Price = price,
			Stock = stock
		};

		if (!Util.Array.IsNullOrEmpty(tags))
		{
			product.Tags = tags!.ToHashSet();
		}

		if (detail is not null)
		{
			product.Detail = detail;
		}

		return product;
	}


	public class ClotheDetail
	{
		public ClotheDetail() : this(new HashSet<ClotheSize>(), new List<ClotheMaterial>())
		{
		}

		public ClotheDetail(IEnumerable<ClotheSize> sizes, IEnumerable<ClotheMaterial> materials)
		{
			Sizes = sizes.ToHashSet();
			Materials = materials.ToHashSet();
		}

		public HashSet<ClotheSize> Sizes { get; set; }
		public HashSet<ClotheMaterial> Materials { get; set; }
		public ClotheType Type { get; set; }
	}
}
