using MediatR;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Infrastructure.Services.Auth.Abstracts;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Accounts.Commands.SignIn;

internal sealed class
	SignInAccountCommandHandler : IRequestHandler<SignInAccountCommand, JsonApiResponse<SignInAccountCommandResult>>
{
	private const string ErrorLoginFailMessage = "Email hoặc mật khẩu không chính xác";
	private const string ErrorAccountNotActivateMessage = "Tài khoảng chưa được kích hoạt";

	private readonly IAuthService _authService;
	private readonly IUserAccountRepository _userAccountRepository;

	public SignInAccountCommandHandler(IUserAccountRepository userAccountRepository, IAuthService authService)
	{
		_userAccountRepository = userAccountRepository;
		_authService = authService;
	}

	public async Task<JsonApiResponse<SignInAccountCommandResult>> Handle(SignInAccountCommand request,
		CancellationToken cancellationToken)
	{
		var account =
			await _userAccountRepository.FindOneAsync(
				FilterBuilder<UserAccount>.Where(acc => acc.Email.Equals(request.Email)), cancellationToken);

		if (account is null || !account.VerifyPassword(request.Password))
		{
			return JsonApiResponse<SignInAccountCommandResult>.Fail(ErrorLoginFailMessage);
		}

		if (!account.IsValid())
		{
			return JsonApiResponse<SignInAccountCommandResult>.Fail(ErrorAccountNotActivateMessage);
		}

		var responseModel = new SignInAccountCommandResult(_authService.CreateJwtAccessToken(account));

		return JsonApiResponse<SignInAccountCommandResult>.Success(data: responseModel);
	}
}
