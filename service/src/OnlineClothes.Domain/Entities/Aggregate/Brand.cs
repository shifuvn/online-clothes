namespace OnlineClothes.Domain.Entities.Aggregate;

public class Brand : EntityBase
{
	public Brand()
	{
	}

	public Brand(string name, string? description, string? contactEmail)
	{
		Name = name;
		Description = description;
		ContactEmail = contactEmail;
	}

	public string Name { get; set; } = null!;
	public string? Description { get; set; }
	public string? ContactEmail { get; set; }

	public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
