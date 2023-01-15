using Newtonsoft.Json;
using OnlineClothes.BuildIn.Entity.Event;

namespace OnlineClothes.Application.EventHandler;

public class EntityCategoryCreatingEventHandler : INotificationHandler<DomainEvent<Category>>
{
	private readonly ILogger<EntityCategoryCreatingEventHandler> _logger;

	public EntityCategoryCreatingEventHandler(ILogger<EntityCategoryCreatingEventHandler> logger)
	{
		_logger = logger;
	}

	public Task Handle(DomainEvent<Category> notification, CancellationToken cancellationToken)
	{
		if (notification.EventActionType == DomainEventActionType.Created)
		{
			_logger.LogInformation("Create new category: {object}",
				JsonConvert.SerializeObject(notification.EventPayloadData));
		}

		return Task.CompletedTask;
	}
}
