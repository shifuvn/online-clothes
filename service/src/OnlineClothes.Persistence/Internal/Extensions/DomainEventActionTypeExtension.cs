using Microsoft.EntityFrameworkCore;
using OnlineClothes.BuildIn.Entity.Event;

namespace OnlineClothes.Persistence.Internal.Extensions;

public static class DomainEventActionTypeExtension
{
	public static DomainEventActionType GetDomainEventAction(this EntityState entityState)
	{
		return entityState switch
		{
			EntityState.Added => DomainEventActionType.Created,
			EntityState.Deleted => DomainEventActionType.Deleted,
			EntityState.Modified => DomainEventActionType.Modified,
			_ => DomainEventActionType.Unknown
		};
	}
}
