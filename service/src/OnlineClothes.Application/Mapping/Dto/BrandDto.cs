namespace OnlineClothes.Application.Mapping.Dto;

public class BrandDto
{
	public BrandDto()
	{
	}

	public BrandDto(int id, string name, string? description, string? contactEmail)
	{
		Id = id;
		Name = name;
		Description = description;
		ContactEmail = contactEmail;
	}

	public int Id { get; init; }
	public string Name { get; set; } = null!;
	public string? Description { get; set; }
	public string? ContactEmail { get; set; }

	public static BrandDto? ToModel(Brand? entity)
	{
		return entity is null || entity.Id == 0
			? null
			: new BrandDto(entity.Id, entity.Name, entity.Description, entity.ContactEmail);
	}
}
