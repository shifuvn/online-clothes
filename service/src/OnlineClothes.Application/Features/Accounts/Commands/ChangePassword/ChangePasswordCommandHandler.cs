using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Services.UserContext;

namespace OnlineClothes.Application.Features.Accounts.Commands.ChangePassword;

internal sealed class
	ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IAccountRepository _accountRepository;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IUserContext _userContext;

	public ChangePasswordCommandHandler(
		IUserContext userContext,
		IUnitOfWork unitOfWork,
		IAccountRepository accountRepository)
	{
		_userContext = userContext;
		_unitOfWork = unitOfWork;
		_accountRepository = accountRepository;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(ChangePasswordCommand request,
		CancellationToken cancellationToken)
	{
		var account =
			await _accountRepository.GetByIntKey(_userContext.GetNameIdentifier(), cancellationToken);

		if (!account.VerifyPassword(request.CurrentPassword))
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail("Mật khẩu hiện tại không chính xác");
		}

		account.SetPassword(request.NewPassword);
		_accountRepository.UpdateOneField(
			account,
			p => p.HashedPassword);

		var updatedResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

		return updatedResult
			? JsonApiResponse<EmptyUnitResponse>.Success()
			: JsonApiResponse<EmptyUnitResponse>.Fail();
	}
}
