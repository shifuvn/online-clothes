using MediatR;
using Microsoft.Extensions.Logging;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.Exceptions;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Accounts.Commands.Activate;

internal sealed class ActivateCommandHandler : IRequestHandler<ActivateCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IAccountTokenCodeRepository _accountTokenCodeRepository;
	private readonly ILogger<ActivateCommandHandler> _logger;
	private readonly IUserAccountRepository _userAccountRepository;

	public ActivateCommandHandler(ILogger<ActivateCommandHandler> logger, IUserAccountRepository userAccountRepository,
		IAccountTokenCodeRepository accountTokenCodeRepository)
	{
		_logger = logger;
		_userAccountRepository = userAccountRepository;
		_accountTokenCodeRepository = accountTokenCodeRepository;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(ActivateCommand request,
		CancellationToken cancellationToken)
	{
		var tokenCode = await _accountTokenCodeRepository.FindOneAsync(FilterBuilder<AccountTokenCode>.Where(x =>
			x.TokenCode == request.Token && x.TokenType == AccountTokenType.Verification), cancellationToken);

		NullValueReferenceException.ThrowIfNull(tokenCode, nameof(tokenCode));

		if (!tokenCode.IsValid())
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail();
		}

		var updateResult = await _userAccountRepository.UpdateOneAsync(
			FilterBuilder<UserAccount>.Where(acc => acc.Email.Equals(tokenCode.Email)),
			p => p.Set(acc => acc.IsActivated, true),
			cancellationToken: cancellationToken);

		if (updateResult.IsAcknowledged && updateResult.IsModifiedCountAvailable)
		{
			await _accountTokenCodeRepository.DeleteOneAsync(tokenCode.Id, cancellationToken);
			_logger.LogInformation("Account {Email} is activated", tokenCode.Email);
		}

		return JsonApiResponse<EmptyUnitResponse>.Success();
	}
}
