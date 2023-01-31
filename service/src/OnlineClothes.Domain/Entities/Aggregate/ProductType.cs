using Newtonsoft.Json;

namespace OnlineClothes.Domain.Entities.Aggregate;

public class ProductType : EntityBase
{
	public ProductType()
	{
	}

	public ProductType(string name)
	{
		Name = name;
	}

	public string Name { get; set; } = null!;

	[JsonIgnore] public virtual ICollection<Product> Products { get; set; } = new List<Product>();

	public void Update(string newName)
	{
		Name = newName;
	}
}
