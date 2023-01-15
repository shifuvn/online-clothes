namespace OnlineClothes.BuildIn.Entity.Event;

public abstract class AuditDomainEventBase : IAuditDomainEvent
{
	public Guid Id { get; init; }
	public DateTime CreatedAt { get; init; }
}

public interface IAuditDomainEvent
{
	Guid Id { get; init; }
	DateTime CreatedAt { get; init; }
}
