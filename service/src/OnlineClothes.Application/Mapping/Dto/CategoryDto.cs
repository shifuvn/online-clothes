namespace OnlineClothes.Application.Mapping.Dto;

public class CategoryDto
{
	public CategoryDto(int id, string name, string? description)
	{
		Id = id;
		Name = name;
		Description = description;
	}

	public CategoryDto(Category domain) : this(domain.Id, domain.Name, domain.Description)
	{
		CreatedAt = domain.CreatedAt;
	}

	public int Id { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }
	public DateTime CreatedAt { get; set; }
}
