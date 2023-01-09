using System.Collections.ObjectModel;

namespace OnlineClothes.Domain.Entities.Aggregate;

public class Category : EntityBase
{
	public Category()
	{
	}

	public Category(string name, string? description)
	{
		Name = name;
		Description = description;
	}

	public string Name { get; set; } = null!;
	public string? Description { get; set; }

	public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new Collection<ProductCategory>();

	public void Update(string newName, string? newDesc)
	{
		Name = newName;
		Description = newDesc;
	}

	public static Category ToProductNavigationModel(int cateId)
	{
		return new Category { Id = cateId, Name = string.Empty };
	}
}
