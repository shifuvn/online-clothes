namespace OnlineClothes.BuildIn.Entity.Event;

public interface IAuditDomainEvent
{
	Guid Id { get; init; }
	DateTime CreatedAt { get; init; }
}
