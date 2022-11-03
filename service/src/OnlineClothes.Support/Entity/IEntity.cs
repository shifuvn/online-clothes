namespace OnlineClothes.Support.Entity;

public interface IEntity<out TKey> : IDateTimeSupportEntity
{
	TKey Id { get; }
}

public interface IEntity : IDateTimeSupportEntity
{
	string Id { get; }
}