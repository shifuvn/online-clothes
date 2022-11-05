using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OnlineClothes.Domain.Common;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Accounts.Commands.SignUp;

internal sealed class
	SignUpAccountCommandHandler : IRequestHandler<SignUpAccountCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly ILogger<SignUpAccountCommandHandler> _logger;
	private readonly IUserAccountRepository _userAccountRepository;

	public SignUpAccountCommandHandler(ILogger<SignUpAccountCommandHandler> logger,
		IUserAccountRepository userAccountRepository)
	{
		_logger = logger;
		_userAccountRepository = userAccountRepository;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(SignUpAccountCommand request,
		CancellationToken cancellationToken)
	{
		var existingAccount =
			await _userAccountRepository.FindOneAsync(FilterBuilder<UserAccount>.Where(p => p.Email == request.Email),
				cancellationToken);

		if (existingAccount is not null)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail("Tài khoảng đã tồn tại");
		}

		var newAccount = UserAccount.Create(request.Email, request.Password, UserAccountRole.Client);
		_logger.LogInformation("Create new account {Email}", newAccount.Email);

		await _userAccountRepository.InsertAsync(newAccount, cancellationToken);

		return JsonApiResponse<EmptyUnitResponse>.Success(StatusCodes.Status201Created);
	}
}
