using MediatR;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Infrastructure.Services.Abstracts;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Accounts.Commands.SignIn;

public class
	SignInAccountCommandHandler : IRequestHandler<SignInAccountCommand, JsonApiResponse<SignInAccountCommandResult>>
{
	private const string LoginFailMessage = "Email hoặc mật khẩu không chính xác";
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
			return JsonApiResponse<SignInAccountCommandResult>.Fail(LoginFailMessage);
		}

		var responseModel = new SignInAccountCommandResult(_authService.CreateJwtAccessToken(account));

		return JsonApiResponse<SignInAccountCommandResult>.Success(data: responseModel);
	}
}
