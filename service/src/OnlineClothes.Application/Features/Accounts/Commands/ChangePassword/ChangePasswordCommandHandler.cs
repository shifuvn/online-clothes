using MediatR;
using Microsoft.Extensions.Logging;
using OnlineClothes.Domain.Common;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Infrastructure.Services.UserContext.Abstracts;
using OnlineClothes.Persistence.Extensions;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Accounts.Commands.ChangePassword;

internal sealed class
	ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly ILogger<ChangePasswordCommandHandler> _logger;
	private readonly IUserAccountRepository _userAccountRepository;
	private readonly IUserContext _userContext;

	public ChangePasswordCommandHandler(ILogger<ChangePasswordCommandHandler> logger,
		IUserAccountRepository userAccountRepository,
		IUserContext userContext)
	{
		_logger = logger;
		_userAccountRepository = userAccountRepository;
		_userContext = userContext;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(ChangePasswordCommand request,
		CancellationToken cancellationToken)
	{
		var account = await _userAccountRepository.GetOneAsync(_userContext.GetNameIdentifier(), cancellationToken);

		if (!account.VerifyPassword(request.CurrentPassword))
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail("Mật khẩu hiện tại không chính xác");
		}

		var newHashPassword = PasswordHasher.Hash(request.NewPassword);

		var updatedResult = await _userAccountRepository.UpdateOneAsync(account.Id,
			p => p.Set(acc => acc.HashedPassword, newHashPassword),
			cancellationToken: cancellationToken);

		return updatedResult.Any()
			? JsonApiResponse<EmptyUnitResponse>.Success()
			: JsonApiResponse<EmptyUnitResponse>.Fail();
	}
}
