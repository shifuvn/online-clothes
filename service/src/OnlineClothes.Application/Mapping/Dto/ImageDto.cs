namespace OnlineClothes.Application.Mapping.Dto;

public class ImageDto
{
	public int Id { get; set; }
	public string Url { get; set; } = null!;
	public string? AltName { get; set; }

	public static ImageDto ToModel(ImageObject entity)
	{
		return new ImageDto
		{
			Id = entity.Id,
			Url = entity.Url,
			AltName = entity.AltName
		};
	}
}
