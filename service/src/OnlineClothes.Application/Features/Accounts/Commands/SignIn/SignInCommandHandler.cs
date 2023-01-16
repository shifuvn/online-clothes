using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Services.Auth;

namespace OnlineClothes.Application.Features.Accounts.Commands.SignIn;

internal sealed class
	SignInCommandHandler : IRequestHandler<SignInCommand, JsonApiResponse<SignInCommandResult>>
{
	private const string ErrorLoginFailMessage = "Email hoặc mật khẩu không chính xác";
	private const string ErrorAccountNotActivateMessage = "Tài khoảng chưa được kích hoạt";
	private readonly IAccountRepository _accountRepository;

	private readonly IAuthorizeService _authorizeService;

	public SignInCommandHandler(
		IAuthorizeService authorizeService,
		IAccountRepository accountRepository)
	{
		_accountRepository = accountRepository;
		_authorizeService = authorizeService;
	}

	public async Task<JsonApiResponse<SignInCommandResult>> Handle(SignInCommand request,
		CancellationToken cancellationToken)
	{
		var account = await _accountRepository.GetByEmail(request.Email, cancellationToken);

		if (account is null || !account.VerifyPassword(request.Password))
		{
			return JsonApiResponse<SignInCommandResult>.Fail(ErrorLoginFailMessage);
		}

		if (!account.IsValid())
		{
			return JsonApiResponse<SignInCommandResult>.Fail(ErrorAccountNotActivateMessage);
		}

		var responseModel = new SignInCommandResult(_authorizeService.CreateJwtAccessToken(account));

		return JsonApiResponse<SignInCommandResult>.Success(data: responseModel);
	}
}
