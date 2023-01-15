using OnlineClothes.Application.Helpers;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Accounts.Queries.ResendActivation;

public sealed class
	ResendActivationQueryHandler : IRequestHandler<ResendActivationQuery, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly AccountActivationHelper _accountActivationHelper;
	private readonly IAccountRepository _accountRepository;

	public ResendActivationQueryHandler(
		AccountActivationHelper accountActivationHelper,
		IAccountRepository accountRepository)
	{
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

		await _accountActivationHelper.ProcessActivateAccountAsync(account, cancellationToken);

		return JsonApiResponse<EmptyUnitResponse>.Success(message: "Kiểm tra email của bạn");
	}
}
