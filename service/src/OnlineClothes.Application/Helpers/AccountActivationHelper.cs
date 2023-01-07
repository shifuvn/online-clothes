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
	private readonly IMailingService _mailingService;
	private readonly ITokenRepository _tokenRepository;
	private readonly IUnitOfWork _unitOfWork;

	public AccountActivationHelper(
		IOptions<AppDomainConfiguration> domainConfigurationOption,
		IOptions<AccountActivationConfiguration> accountActivationConfigurationOption,
		IMailingService mailingService,
		IUnitOfWork unitOfWork,
		ITokenRepository tokenRepository)
	{
		_accountActivationConfiguration = accountActivationConfigurationOption.Value;
		_domainConfiguration = domainConfigurationOption.Value;
		_mailingService = mailingService;
		_unitOfWork = unitOfWork;
		_tokenRepository = tokenRepository;
	}

	public async Task<AccountActivationResult> StartNewAccount(AccountUser account,
		CancellationToken cancellationToken = default)
	{
		if (!_accountActivationConfiguration.ByEmail)
		{
			account.Activate();
			return AccountActivationResult.Activated;
		}

		await SendActivationMail(
			account,
			await CreateVerificationTokenCode(account, cancellationToken),
			cancellationToken);

		return AccountActivationResult.WaitConfirm;
	}

	private async Task<AccountTokenCode> CreateVerificationTokenCode(AccountUser account,
		CancellationToken cancellationToken)
	{
		var newTokenCode = new AccountTokenCode(account.Email, AccountTokenType.Verification, TimeSpan.FromMinutes(15));
		await _tokenRepository.AddAsync(newTokenCode, cancellationToken: cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return newTokenCode;
	}

	private async Task SendActivationMail(AccountUser account,
		AccountTokenCode newTokenCode,
		CancellationToken cancellationToken = default)
	{
		var mail = new MailingTemplate(account.Email, "Verify Account", EmailTemplateNames.VerifyAccount,
			new
			{
				ConfirmedUrl =
					$"{_domainConfiguration}/api/v1/accounts/activate?token={newTokenCode.TokenCode}"
			});

		await _mailingService.SendEmailAsync(mail, cancellationToken);
	}
}

public enum AccountActivationResult
{
	WaitConfirm,
	Activated
}
