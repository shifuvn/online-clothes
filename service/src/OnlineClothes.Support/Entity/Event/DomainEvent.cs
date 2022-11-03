using MediatR;

namespace OnlineClothes.Support.Entity.Event;

public class DomainEvent<TEntity> : INotification where TEntity : class, ISupportDomainEvent
{
	public DomainEvent()
	{
		Id = Guid.NewGuid();
		CreatedAt = DateTime.UtcNow;
	}

	public DomainEvent(string name, DomainEventAction action, TEntity? eventData = null) : this()
	{
		EventName = name;
		EventAction = action;
		EventData = eventData;
	}

	public DomainEvent(DomainEventAction action, TEntity? data = null) :
		this(nameof(TEntity), action, data)
	{
	}

	public object? DynamicData { get; init; }

	public Guid Id { get; init; }
	public string EventName { get; init; } = null!;
	public DomainEventAction EventAction { get; init; }
	public TEntity? EventData { get; init; }

	public DateTime CreatedAt { get; init; }

	public static DomainEvent<TEntity> Create(DomainEventAction action, TEntity? data = null)
	{
		return new DomainEvent<TEntity>(action, data);
	}

	public static DomainEvent<TEntity> Create(string eventName, DomainEventAction action, TEntity? data = null)
	{
		return new DomainEvent<TEntity>(eventName, action, data);
	}

	public static DomainEvent<TEntity> Create(string eventName, DomainEventAction action,
		object? dynamicData)
	{
		return new DomainEvent<TEntity>(eventName, action)
		{
			DynamicData = dynamicData
		};
	}
}

public enum DomainEventAction
{
	Queried,
	Created,
	Updated,
	Deleted
}