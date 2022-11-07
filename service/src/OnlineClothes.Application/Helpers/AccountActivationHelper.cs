using Microsoft.Extensions.Options;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Infrastructure.Services.Mailing;
using OnlineClothes.Infrastructure.Services.Mailing.Abstracts;
using OnlineClothes.Infrastructure.Services.Mailing.Models;
using OnlineClothes.Infrastructure.StandaloneConfigurations;

namespace OnlineClothes.Application.Helpers;

public class AccountActivationHelper
{
	private readonly AccountActivationConfiguration _accountActivationConfiguration;
	private readonly IAccountRepository _accountRepository;
	private readonly IAccountTokenCodeRepository _accountTokenCodeRepository;
	private readonly AppDomainConfiguration _domainConfiguration;
	private readonly IMailingService _mailingService;

	public AccountActivationHelper(IOptions<AppDomainConfiguration> domainConfigurationOption,
		IOptions<AccountActivationConfiguration> accountActivationConfigurationOption,
		IAccountTokenCodeRepository accountTokenCodeRepository,
		IMailingService mailingService,
		IAccountRepository accountRepository)
	{
		_accountActivationConfiguration = accountActivationConfigurationOption.Value;
		_domainConfiguration = domainConfigurationOption.Value;
		_accountTokenCodeRepository = accountTokenCodeRepository;
		_mailingService = mailingService;
		_accountRepository = accountRepository;
	}

	public async Task<AccountActivationResult> StartNewAccount(AccountUser account,
		CancellationToken cancellationToken = default)
	{
		if (!_accountActivationConfiguration.ByEmail)
		{
			account.Activate();

			// update activated status
			await _accountRepository.UpdateOneAsync(
				account.Id,
				update => update.Set(acc => acc.IsActivated, account.IsActivated),
				cancellationToken: cancellationToken);

			return AccountActivationResult.Activated;
		}

		var newTokenCode = await CreateVerificationTokenCode(account, cancellationToken);
		await SendActivationMail(account, cancellationToken, newTokenCode);

		return AccountActivationResult.WaitConfirm;
	}

	private async Task<AccountTokenCode> CreateVerificationTokenCode(AccountUser account,
		CancellationToken cancellationToken)
	{
		var newTokenCode = new AccountTokenCode(account.Email, AccountTokenType.Verification, TimeSpan.FromMinutes(15));
		await _accountTokenCodeRepository.InsertAsync(newTokenCode, cancellationToken);
		return newTokenCode;
	}

	private async Task SendActivationMail(AccountUser account, CancellationToken cancellationToken,
		AccountTokenCode newTokenCode)
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
