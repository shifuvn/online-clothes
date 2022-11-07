using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OnlineClothes.Application.Helpers;
using OnlineClothes.Domain.Common;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Domain.Entities.Common;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Accounts.Commands.SignUp;

internal sealed class
	SignUpCommandHandler : IRequestHandler<SignUpCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly AccountActivationHelper _accountActivationHelper;
	private readonly IAccountRepository _accountRepository;
	private readonly ILogger<SignUpCommandHandler> _logger;

	public SignUpCommandHandler(ILogger<SignUpCommandHandler> logger,
		IAccountRepository accountRepository,
		AccountActivationHelper accountActivationHelper)
	{
		_logger = logger;
		_accountRepository = accountRepository;
		_accountActivationHelper = accountActivationHelper;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(SignUpCommand request,
		CancellationToken cancellationToken)
	{
		var existingAccount =
			await _accountRepository.FindOneAsync(FilterBuilder<AccountUser>.Where(p => p.Email == request.Email),
				cancellationToken);

		if (existingAccount is not null)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail("Tài khoản đã tồn tại");
		}

		var newAccount = AccountUser.Create(request.Email, request.Password,
			FullNameHelper.Create(request.FirstName, request.LastName), UserAccountRole.Client);

		var activateResult = await _accountActivationHelper.StartNewAccount(newAccount, cancellationToken);

		await _accountRepository.InsertAsync(newAccount, cancellationToken);
		_logger.LogInformation("Create new account {Email}", newAccount.Email);

		return activateResult == AccountActivationResult.Activated
			? JsonApiResponse<EmptyUnitResponse>.Success(StatusCodes.Status201Created)
			: JsonApiResponse<EmptyUnitResponse>.Success(StatusCodes.Status201Created, "Kiểm tra email của bạn");
	}
}
