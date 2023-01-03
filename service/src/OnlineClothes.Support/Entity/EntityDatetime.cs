namespace OnlineClothes.Support.Entity;

/// <summary>
/// Support datetime tracker for entity.
/// </summary>
public abstract class EntityDatetimeBase : IEntityDatetimeSupport
{
	public DateTime CreatedAt { get; set; }
	public DateTime ModifiedAt { get; set; }
}

/// <summary>
/// Used this entity to mark where entity is created as well as updated
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
