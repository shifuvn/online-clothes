using Microsoft.Extensions.Options;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Services.Mailing;
using OnlineClothes.Application.Services.Mailing.Models;
using OnlineClothes.Application.StandaloneConfigurations;

namespace OnlineClothes.Application.Helpers;

public class AccountActivationHelper
{
	private readonly AccountActivationConfiguration _accountActivationConfiguration;
	private readonly AppDomainConfiguration _domainConfiguration;
	private readonly ILogger<AccountActivationHelper> _logger;
	private readonly IMailingService _mailingService;
	private readonly ITokenRepository _tokenRepository;
	private readonly IUnitOfWork _unitOfWork;

	public AccountActivationHelper(
		IOptions<AppDomainConfiguration> domainConfigurationOption,
		IOptions<AccountActivationConfiguration> accountActivationConfigurationOption,
		IMailingService mailingService,
		IUnitOfWork unitOfWork,
		ITokenRepository tokenRepository, ILogger<AccountActivationHelper> logger)
	{
		_accountActivationConfiguration = accountActivationConfigurationOption.Value;
		_domainConfiguration = domainConfigurationOption.Value;
		_mailingService = mailingService;
		_unitOfWork = unitOfWork;
		_tokenRepository = tokenRepository;
		_logger = logger;
	}

	public AccountActivationResultType ActivateNewAccount(AccountUser account)
	{
		if (_accountActivationConfiguration.ByEmail)
		{
			return AccountActivationResultType.WaitConfirm;
		}

		account.Activate();
		return AccountActivationResultType.Activated;
	}

	public async Task ProcessActivateAccountAsync(AccountUser account, CancellationToken cancellationToken = default)
	{
		await SendActivationMail(
			account,
			await CreateVerificationTokenCode(account, cancellationToken),
			cancellationToken);
	}

	private async Task<AccountTokenCode> CreateVerificationTokenCode(AccountUser account,
		CancellationToken cancellationToken)
	{
		var tokenCode = new AccountTokenCode(account.Email, AccountTokenType.Verification, TimeSpan.FromMinutes(10));

		await _tokenRepository.AddAsync(tokenCode, cancellationToken: cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return tokenCode;
	}

	private async Task SendActivationMail(AccountUser account,
		AccountTokenCode newTokenCode,
		CancellationToken cancellationToken = default)
	{
		var mailTemplate = new MailingTemplate(
			account.Email,
			"Verify Account",
			EmailTemplateNames.VerifyAccount,
			new
			{
				ConfirmedUrl =
					$"{_domainConfiguration}/verified-email?token={newTokenCode.TokenCode}"
			});

		await _mailingService.SendEmailAsync(mailTemplate, cancellationToken);
	}
}
