namespace OnlineClothes.Application.Mapping.Dto;

public class ProductTypeDto
{
	public ProductTypeDto(int id, string name)
	{
		Id = id;
		Name = name;
	}

	public ProductTypeDto(ProductType domain) : this(domain.Id, domain.Name)
	{
		CreatedAt = domain.CreatedAt;
	}

	public int Id { get; set; }

	public string Name { get; set; }

	public DateTime CreatedAt { get; set; }
}
