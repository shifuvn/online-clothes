using OnlineClothes.Domain.Entities.Aggregate;
using OnlineClothes.Support.Entity;

namespace OnlineClothes.Domain.Entities;

public class ClotheBrand : EntityBase<string>
{
	public ClotheBrand()
	{
	}

	public ClotheBrand(string id, string name, string? description, string? contactEmail)
	{
		Id = id;
		Name = name;
		Description = description;
		ContactEmail = contactEmail;
	}

	public string Name { get; set; } = null!;

	public string? Description { get; set; }

	public string? ContactEmail { get; set; }

	public ICollection<Product> Products { get; set; } = new List<Product>();
}
