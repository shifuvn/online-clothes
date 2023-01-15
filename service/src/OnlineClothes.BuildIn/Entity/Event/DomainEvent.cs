using System.Runtime.Serialization;
using MediatR;

namespace OnlineClothes.BuildIn.Entity.Event;

[DataContract(IsReference = true)]
public class DomainEvent<TEntity> : IDomainEvent where TEntity : class, ISupportDomainEvent
{
	private DomainEvent()
	{
		Id = Guid.NewGuid();
		CreatedAt = DateTime.UtcNow;
	}

	public DomainEvent(string name, DomainEventActionType actionType, TEntity? eventData = null) : this()
	{
		EventName = name;
		EventActionType = actionType;
		EventPayloadData = eventData;
	}

	// Audit
	[DataMember] public Guid Id { get; init; }
	[DataMember] public DateTime CreatedAt { get; init; }
	[DataMember] public string EventName { get; set; } = null!;
	[DataMember] public DomainEventActionType EventActionType { get; set; }
	public object? EventPayloadData { get; set; }
}

public interface IDomainEvent : INotification, IAuditDomainEvent
{
	string EventName { get; set; }
	DomainEventActionType EventActionType { get; set; }
	object? EventPayloadData { get; set; }
}
