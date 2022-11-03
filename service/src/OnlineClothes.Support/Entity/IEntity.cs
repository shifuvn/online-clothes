namespace OnlineClothes.Support.Entity;

public interface IEntity<TKey> : IDateTimeSupportEntity
{
    TKey Id { get; set; }
}

public interface IEntity
{
}

