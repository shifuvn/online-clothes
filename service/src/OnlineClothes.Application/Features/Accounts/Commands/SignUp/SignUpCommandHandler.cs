using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OnlineClothes.Domain.Common;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Domain.Entities.Common;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Infrastructure.Services.Mailing;
using OnlineClothes.Infrastructure.Services.Mailing.Abstracts;
using OnlineClothes.Infrastructure.Services.Mailing.Models;
using OnlineClothes.Infrastructure.StandaloneConfigurations;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Accounts.Commands.SignUp;

internal sealed class
	SignUpCommandHandler : IRequestHandler<SignUpCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IAccountRepository _accountRepository;
	private readonly IAccountTokenCodeRepository _accountTokenCodeRepository;
	private readonly AppDomainConfiguration _domainConfiguration;
	private readonly ILogger<SignUpCommandHandler> _logger;
	private readonly IMailingService _mailingService;

	public SignUpCommandHandler(ILogger<SignUpCommandHandler> logger,
		IAccountRepository accountRepository,
		IMailingService mailingService,
		IAccountTokenCodeRepository accountTokenCodeRepository,
		IOptions<AppDomainConfiguration> appDomainOption)
	{
		_logger = logger;
		_accountRepository = accountRepository;
		_mailingService = mailingService;
		_accountTokenCodeRepository = accountTokenCodeRepository;
		_domainConfiguration = appDomainOption.Value;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(SignUpCommand request,
		CancellationToken cancellationToken)
	{
		var existingAccount =
			await _accountRepository.FindOneAsync(FilterBuilder<AccountUser>.Where(p => p.Email == request.Email),
				cancellationToken);

		if (existingAccount is not null)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail("Tài khoảng đã tồn tại");
		}

		var newAccount = AccountUser.Create(request.Email, request.Password,
			FullNameHelper.Create(request.FirstName, request.LastName), UserAccountRole.Client);
		var verifyAccountTokenCode =
			new AccountTokenCode(newAccount.Email, AccountTokenType.Verification, TimeSpan.FromHours(24));

		await _accountTokenCodeRepository.InsertAsync(verifyAccountTokenCode, cancellationToken);
		var mail = new MailingTemplate(newAccount.Email, "Verify Account", EmailTemplateNames.VerifyAccount,
			new
			{
				ConfirmedUrl =
					$"{_domainConfiguration}/api/v1/accounts/activate?token={verifyAccountTokenCode.TokenCode}"
			});

		await _mailingService.SendEmailAsync(mail, cancellationToken);

		await _accountRepository.InsertAsync(newAccount, cancellationToken);
		_logger.LogInformation("Create new account {Email}", newAccount.Email);

		return JsonApiResponse<EmptyUnitResponse>.Success(StatusCodes.Status201Created);
	}
}
