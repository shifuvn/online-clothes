namespace OnlineClothes.Application.Mapping.Dto;

public class CategoryDto
{
	public CategoryDto(int id, string name, string? description)
	{
		Id = id;
		Name = name;
		Description = description;
	}

	public CategoryDto()
	{
	}

	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string? Description { get; set; }

	public static CategoryDto ToModel(Category entity)
	{
		return new CategoryDto(entity.Id, entity.Name, entity.Description);
	}
}
