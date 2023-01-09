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

	public SignUpCommandHandler(ILogger<SignUpCommandHandler> logger,
		AccountActivationHelper accountActivationHelper,
		IUnitOfWork unitOfWork,
		IAccountRepository accountRepository,
		ICartRepository cartRepository)
	{
		_logger = logger;
		_accountActivationHelper = accountActivationHelper;
		_unitOfWork = unitOfWork;
		_accountRepository = accountRepository;
		_cartRepository = cartRepository;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(SignUpCommand request,
		CancellationToken cancellationToken)
	{
		var existedAccount = await _accountRepository.GetByEmail(request.Email, cancellationToken);
		if (existedAccount is not null)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail("Tài khoản đã tồn tại");
		}

		var signUpAccount = AccountUser.Create(
			request.Email,
			request.Password,
			Fullname.Create(request.FirstName, request.LastName));

		// begin tx
		await _unitOfWork.BeginTransactionAsync(cancellationToken);

		var activateResult = await CreateAccount(signUpAccount, cancellationToken);

		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);
		if (!save)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail();
		}

		// commit tx
		await _unitOfWork.CommitAsync(cancellationToken);

		_logger.LogInformation("Create new account {Email}", signUpAccount.Email);

		return activateResult == AccountActivationResult.Activated
			? JsonApiResponse<EmptyUnitResponse>.Success(StatusCodes.Status201Created)
			: JsonApiResponse<EmptyUnitResponse>.Success(StatusCodes.Status201Created, "Kiểm tra email của bạn");
	}

	private async Task<AccountActivationResult> CreateAccount(
		AccountUser account,
		CancellationToken cancellationToken = default)
	{
		var activateResult = await _accountActivationHelper.StartNewAccount(account, cancellationToken);

		var cart = new AccountCart { Account = account };
		await _cartRepository.AddAsync(cart, cancellationToken: cancellationToken);

		return activateResult;
	}
}
