﻿using Microsoft.Extensions.Options;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Services.Mailing;
using OnlineClothes.Application.Services.Mailing.Models;
using OnlineClothes.Application.StandaloneConfigurations;

namespace OnlineClothes.Application.Features.Accounts.Commands.Recovery;

internal sealed class
	RecoveryAccountCommandHandler : IRequestHandler<RecoveryAccountCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IAccountRepository _accountRepository;
	private readonly AppDomainConfiguration _domainConfiguration;
	private readonly ILogger<RecoveryAccountCommand> _logger;
	private readonly IMailingService _mailingService;
	private readonly ITokenRepository _tokenRepository;
	private readonly IUnitOfWork _unitOfWork;

	public RecoveryAccountCommandHandler(ILogger<RecoveryAccountCommand> logger,
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

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(RecoveryAccountCommand request,
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
			await SendRecoveryAccountMail(account, recoveryCode, cancellationToken);
			return JsonApiResponse<EmptyUnitResponse>.Success();
		}

		return JsonApiResponse<EmptyUnitResponse>.Fail();
	}

	private async Task SendRecoveryAccountMail(
		AccountUser account,
		AccountTokenCode recoveryCode,
		CancellationToken cancellationToken = default)
	{
		var mail = new MailingTemplate(account.Email, "Recovery account", EmailTemplateNames.RecoveryPassword,
			new
			{
				RecoveryUrl = $"{_domainConfiguration}/auth/reset-password?token={recoveryCode.TokenCode}"
			});

		await _mailingService.SendEmailAsync(mail, cancellationToken);

		_logger.LogInformation("Account {Email} request for resetting password", account.Email);
	}
}
