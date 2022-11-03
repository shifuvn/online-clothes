namespace OnlineClothes.Support.Entity;

/// <summary>
/// Used this entity to mark where entity is created as well as updated
/// </summary>
public interface IDateTimeSupportEntity : IEntitySupportCreatedAt, IEntitySupportModifiedAt
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