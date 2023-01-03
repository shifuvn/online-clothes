using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OnlineClothes.Domain.Entities.Aggregate;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Infrastructure.Services.Mailing;
using OnlineClothes.Infrastructure.Services.Mailing.Abstracts;
using OnlineClothes.Infrastructure.Services.Mailing.Models;
using OnlineClothes.Infrastructure.StandaloneConfigurations;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.Exceptions;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Accounts.Commands.Reset;

internal sealed class ResetCommandHandler : IRequestHandler<ResetCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IAccountRepository _accountRepository;
	private readonly IAccountTokenCodeRepository _accountTokenCodeRepository;
	private readonly AppDomainConfiguration _domainConfiguration;
	private readonly ILogger<ResetCommand> _logger;
	private readonly IMailingService _mailingService;

	public ResetCommandHandler(ILogger<ResetCommand> logger,
		IAccountRepository accountRepository,
		IAccountTokenCodeRepository accountTokenCodeRepository,
		IOptions<AppDomainConfiguration> appDomainOptions, IMailingService mailingService)
	{
		_logger = logger;
		_accountRepository = accountRepository;
		_accountTokenCodeRepository = accountTokenCodeRepository;
		_mailingService = mailingService;
		_domainConfiguration = appDomainOptions.Value;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(ResetCommand request,
		CancellationToken cancellationToken)
	{
		var account = await _accountRepository.FindOneAsync(
			FilterBuilder<AccountUser>.Where(acc => acc.Email.Equals(request.Email)), cancellationToken);

		NullValueReferenceException.ThrowIfNull(account, nameof(account));

		var recoveryCode =
			new AccountTokenCode(account.Email, AccountTokenType.ResetPassword, TimeSpan.FromMinutes(15));

		await _accountTokenCodeRepository.InsertAsync(recoveryCode, cancellationToken);

		var mail = new MailingTemplate(account.Email, "Recovery account", EmailTemplateNames.ResetPassword,
			new
			{
				RecoveryUrl = $"{_domainConfiguration}/api/v1/accounts/recovery?token={recoveryCode.TokenCode}"
			});

		await _mailingService.SendEmailAsync(mail, cancellationToken);

		_logger.LogInformation("Account {Email} request for resetting password", account.Email);

		return JsonApiResponse<EmptyUnitResponse>.Success();
	}
}
