namespace OnlineClothes.Domain.Entities.Aggregate;

[Table("Image")]
public class ImageObject : EntityNonDateTimeBase<int>, IEntitySupportCreatedAt
{
	public ImageObject()
	{
	}

	public ImageObject(string url, string? altName = null)
	{
		Url = url;
		AltName = altName;
	}

	public string Url { get; set; } = null!;
	public string? AltName { get; set; }
	public DateTime CreatedAt { get; set; }

	public void ChangeAltName(string newAltName)
	{
		AltName = newAltName;
	}
}
