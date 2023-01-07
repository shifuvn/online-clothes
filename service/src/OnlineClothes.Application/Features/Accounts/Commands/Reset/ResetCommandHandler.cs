using Microsoft.Extensions.Options;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Services.Mailing;
using OnlineClothes.Application.Services.Mailing.Models;
using OnlineClothes.Application.StandaloneConfigurations;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.Exceptions;

namespace OnlineClothes.Application.Features.Accounts.Commands.Reset;

internal sealed class ResetCommandHandler : IRequestHandler<ResetCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IAccountRepository _accountRepository;
	private readonly AppDomainConfiguration _domainConfiguration;
	private readonly ILogger<ResetCommand> _logger;
	private readonly IMailingService _mailingService;
	private readonly ITokenRepository _tokenRepository;
	private readonly IUnitOfWork _unitOfWork;

	public ResetCommandHandler(ILogger<ResetCommand> logger,
		IOptions<AppDomainConfiguration> appDomainOptions,
		IMailingService mailingService,
		IUnitOfWork unitOfWork,
		IAccountRepository accountRepository,
		ITokenRepository tokenRepository)
	{
		_logger = logger;
		_mailingService = mailingService;
		_unitOfWork = unitOfWork;
		_accountRepository = accountRepository;
		_tokenRepository = tokenRepository;
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

		await _tokenRepository.AddAsync(recoveryCode, cancellationToken: cancellationToken);

		var saves = await _unitOfWork.SaveChangesAsync(cancellationToken);

		if (saves)
		{
			await SendResetAccountEmail(cancellationToken, account, recoveryCode);
			return JsonApiResponse<EmptyUnitResponse>.Success();
		}

		return JsonApiResponse<EmptyUnitResponse>.Fail();
	}

	private async Task SendResetAccountEmail(CancellationToken cancellationToken, AccountUser account,
		AccountTokenCode recoveryCode)
	{
		var mail = new MailingTemplate(account.Email, "Recovery account", EmailTemplateNames.ResetPassword,
			new
			{
				RecoveryUrl = $"{_domainConfiguration}/api/v1/accounts/recovery?token={recoveryCode.TokenCode}"
			});

		await _mailingService.SendEmailAsync(mail, cancellationToken);

		_logger.LogInformation("Account {Email} request for resetting password", account.Email);
	}
}
