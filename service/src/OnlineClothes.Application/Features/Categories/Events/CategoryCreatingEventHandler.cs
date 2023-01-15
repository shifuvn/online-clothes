namespace OnlineClothes.Application.Features.Categories.Events;

public class CategoryCreatingEventHandler : INotificationHandler<DomainEvent<Category>>
{
	private readonly ILogger<CategoryCreatingEventHandler> _logger;

	public CategoryCreatingEventHandler(ILogger<CategoryCreatingEventHandler> logger)
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
