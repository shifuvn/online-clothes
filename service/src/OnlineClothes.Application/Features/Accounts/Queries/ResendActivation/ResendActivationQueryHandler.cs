using MediatR;
using Microsoft.Extensions.Logging;
using OnlineClothes.Application.Helpers;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Accounts.Queries.ResendActivation;

public sealed class
	ResendActivationQueryHandler : IRequestHandler<ResendActivationQuery, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly AccountActivationHelper _accountActivationHelper;
	private readonly IAccountRepository _accountRepository;
	private readonly ILogger<ResendActivationQueryHandler> _logger;

	public ResendActivationQueryHandler(ILogger<ResendActivationQueryHandler> logger,
		AccountActivationHelper accountActivationHelper, IAccountRepository accountRepository)
	{
		_logger = logger;
		_accountActivationHelper = accountActivationHelper;
		_accountRepository = accountRepository;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(ResendActivationQuery request,
		CancellationToken cancellationToken)
	{
		var account = await _accountRepository.FindOneAsync(
			FilterBuilder<AccountUser>.Where(q => q.Email.Equals(request.Email)), cancellationToken);

		if (account is null)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail();
		}

		var activateResult = await _accountActivationHelper.StartNewAccount(account, cancellationToken);

		_logger.LogInformation("Resend activate account {Email}", account.Email);

		return activateResult == AccountActivationResult.Activated
			? JsonApiResponse<EmptyUnitResponse>.Success()
			: JsonApiResponse<EmptyUnitResponse>.Success(message: "Kiểm tra email của bạn");
	}
}
