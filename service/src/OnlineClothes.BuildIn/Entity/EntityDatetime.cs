namespace OnlineClothes.BuildIn.Entity;

/// <summary>
/// Support datetime tracker for entity.
/// </summary>
public interface IEntityDatetimeSupport : IEntitySupportCreatedAt, IEntitySupportModifiedAt
{
}

public interface IEntitySupportCreatedAt
{
	DateTime CreatedAt { get; set; }
}

public interface IEntitySupportModifiedAt
{
	DateTime ModifiedAt { get; set; }
}
