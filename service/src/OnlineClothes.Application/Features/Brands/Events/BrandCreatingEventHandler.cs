namespace OnlineClothes.Application.Features.Brands.Events;

public class BrandCreatingEventHandler : INotificationHandler<DomainEvent<Brand>>
{
	private readonly ILogger<BrandCreatingEventHandler> _logger;

	public BrandCreatingEventHandler(ILogger<BrandCreatingEventHandler> logger)
	{
		_logger = logger;
	}

	public Task Handle(DomainEvent<Brand> notification, CancellationToken cancellationToken)
	{
		if (notification.EventActionType == DomainEventActionType.Created)
		{
			_logger.LogInformation("Create brand: {object}",
				JsonConvert.SerializeObject(notification.EventPayloadData));
		}

		return Task.CompletedTask;
	}
}
