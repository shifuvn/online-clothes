using MediatR;
using Microsoft.Extensions.Logging;
using OnlineClothes.Domain.Entities.Aggregate;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.Exceptions;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Accounts.Queries.Activate;

internal sealed class ActivateQueryHandler : IRequestHandler<ActivateQuery, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IAccountRepository _accountRepository;
	private readonly IAccountTokenCodeRepository _accountTokenCodeRepository;
	private readonly ILogger<ActivateQueryHandler> _logger;

	public ActivateQueryHandler(ILogger<ActivateQueryHandler> logger, IAccountRepository accountRepository,
		IAccountTokenCodeRepository accountTokenCodeRepository)
	{
		_logger = logger;
		_accountRepository = accountRepository;
		_accountTokenCodeRepository = accountTokenCodeRepository;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(ActivateQuery request,
		CancellationToken cancellationToken)
	{
		var tokenCode = await _accountTokenCodeRepository.FindOneAsync(FilterBuilder<AccountTokenCode>.Where(x =>
			x.TokenCode == request.Token && x.TokenType == AccountTokenType.Verification), cancellationToken);

		NullValueReferenceException.ThrowIfNull(tokenCode, nameof(tokenCode));

		if (!tokenCode.IsValid())
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail();
		}

		var updateResult = await _accountRepository.UpdateOneAsync(
			FilterBuilder<AccountUser>.Where(acc => acc.Email.Equals(tokenCode.Email)),
			p => p.Set(acc => acc.IsActivated, true),
			cancellationToken: cancellationToken);

		if (updateResult.IsAcknowledged && updateResult.IsModifiedCountAvailable)
		{
			await _accountTokenCodeRepository.DeleteOneAsync(tokenCode.Id.ToString(), cancellationToken);
			_logger.LogInformation("Account {Email} is activated", tokenCode.Email);
		}

		return JsonApiResponse<EmptyUnitResponse>.Success();
	}
}
