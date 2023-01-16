using OnlineClothes.Application.Helpers;

namespace OnlineClothes.Application.Features.Accounts.Events;

public class AccountCreatingEventHandler : INotificationHandler<DomainEvent<AccountUser>>
{
	private readonly AccountActivationHelper _accountActivationHelper;
	private readonly ILogger<AccountCreatingEventHandler> _logger;

	public AccountCreatingEventHandler(AccountActivationHelper accountActivationHelper,
		ILogger<AccountCreatingEventHandler> logger)
	{
		_accountActivationHelper = accountActivationHelper;
		_logger = logger;
	}

	public async Task Handle(DomainEvent<AccountUser> notification, CancellationToken cancellationToken)
	{
		if (notification.EventActionType == DomainEventActionType.Created)
		{
			await HandleEventCreateAccount(notification, cancellationToken);
		}
	}

	private async Task HandleEventCreateAccount(
		DomainEvent<AccountUser> notification,
		CancellationToken cancellationToken)
	{
		if (notification.EventPayloadData is not AccountUser account)
		{
			return;
		}

		if (!account.IsActivated)
		{
			await _accountActivationHelper.ProcessActivateAccountAsync(account, cancellationToken);
		}
	}
}
