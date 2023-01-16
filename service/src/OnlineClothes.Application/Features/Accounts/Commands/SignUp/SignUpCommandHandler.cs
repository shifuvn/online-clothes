using Microsoft.AspNetCore.Http;
using OnlineClothes.Application.Helpers;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Accounts.Commands.SignUp;

internal sealed class
	SignUpCommandHandler : IRequestHandler<SignUpCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly AccountActivationHelper _accountActivationHelper;
	private readonly IAccountRepository _accountRepository;
	private readonly ICartRepository _cartRepository;
	private readonly ILogger<SignUpCommandHandler> _logger;
	private readonly IUnitOfWork _unitOfWork;

	public SignUpCommandHandler(
		AccountActivationHelper accountActivationHelper,
		IUnitOfWork unitOfWork,
		IAccountRepository accountRepository,
		ICartRepository cartRepository,
		ILogger<SignUpCommandHandler> logger)
	{
		_accountActivationHelper = accountActivationHelper;
		_unitOfWork = unitOfWork;
		_accountRepository = accountRepository;
		_cartRepository = cartRepository;
		_logger = logger;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(SignUpCommand request,
		CancellationToken cancellationToken)
	{
		var existedAccount = await _accountRepository.GetByEmail(request.Email, cancellationToken);
		if (existedAccount is not null)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail("Tài khoản đã tồn tại");
		}


		// begin tx
		await _unitOfWork.BeginTransactionAsync(cancellationToken);

		var activateResult = await InitiateAccount(request, cancellationToken);

		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);
		if (!save)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail();
		}

		// commit tx
		await _unitOfWork.CommitAsync(cancellationToken);

		return activateResult == AccountActivationResultType.Activated
			? JsonApiResponse<EmptyUnitResponse>.Success(StatusCodes.Status201Created, "Đăng ký tài khoản thành công")
			: JsonApiResponse<EmptyUnitResponse>.Success(StatusCodes.Status201Created, "Kiểm tra email của bạn");
	}

	private async Task<AccountActivationResultType> InitiateAccount(
		SignUpCommand request,
		CancellationToken cancellationToken = default)
	{
		var account = AccountUser.Create(
			request.Email,
			request.Password,
			Fullname.Create(request.FirstName, request.LastName));

		await _accountRepository.AddAsync(account, cancellationToken: cancellationToken);

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		var cart = new AccountCart { AccountId = account.Id };

		await _cartRepository.AddAsync(cart, cancellationToken: cancellationToken);

		return _accountActivationHelper.ActivateNewAccount(account);
	}
}
