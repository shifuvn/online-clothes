namespace OnlineClothes.Application.Mapping.Dto;

public class BrandDto
{
	public BrandDto(int id, string name, string? description, string? contactEmail)
	{
		Id = id;
		Name = name;
		Description = description;
		ContactEmail = contactEmail;
	}

	public BrandDto(Brand domain) : this(domain.Id, domain.Name, domain.Description, domain.ContactEmail)
	{
		CreatedAt = domain.CreatedAt;
	}

	public int Id { get; init; }
	public string Name { get; set; }
	public string? Description { get; set; }
	public string? ContactEmail { get; set; }
	public DateTime CreatedAt { get; set; }
}
