using OnlineClothes.Application.Helpers;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Accounts.Queries.ResendActivation;

public sealed class
	ResendActivationQueryHandler : IRequestHandler<ResendActivationQuery, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly AccountActivationHelper _accountActivationHelper;
	private readonly IAccountRepository _accountRepository;
	private readonly ILogger<ResendActivationQueryHandler> _logger;

	public ResendActivationQueryHandler(
		ILogger<ResendActivationQueryHandler> logger,
		AccountActivationHelper accountActivationHelper,
		IAccountRepository accountRepository)
	{
		_logger = logger;
		_accountActivationHelper = accountActivationHelper;
		_accountRepository = accountRepository;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(ResendActivationQuery request,
		CancellationToken cancellationToken)
	{
		var account = await _accountRepository.GetByEmail(request.Email, cancellationToken);

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
