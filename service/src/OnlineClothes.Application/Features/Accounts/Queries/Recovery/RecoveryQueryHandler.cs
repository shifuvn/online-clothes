using MediatR;
using Microsoft.Extensions.Logging;
using OnlineClothes.Domain.Common;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Infrastructure.Services.Mailing.Abstracts;
using OnlineClothes.Persistence.Extensions;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.Exceptions;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Accounts.Queries.Recovery;

internal sealed class RecoveryQueryHandler : IRequestHandler<RecoveryQuery, JsonApiResponse<RecoveryQueryResult>>
{
	private readonly IAccountTokenCodeRepository _accountTokenCodeRepository;
	private readonly ILogger<RecoveryQueryHandler> _logger;
	private readonly IMailingService _mailingService;
	private readonly IUserAccountRepository _userAccountRepository;

	public RecoveryQueryHandler(ILogger<RecoveryQueryHandler> logger,
		IAccountTokenCodeRepository accountTokenCodeRepository,
		IUserAccountRepository userAccountRepository,
		IMailingService mailingService)
	{
		_logger = logger;
		_accountTokenCodeRepository = accountTokenCodeRepository;
		_userAccountRepository = userAccountRepository;
		_mailingService = mailingService;
	}

	public async Task<JsonApiResponse<RecoveryQueryResult>> Handle(RecoveryQuery request,
		CancellationToken cancellationToken)
	{
		var tokenCode = await _accountTokenCodeRepository.FindOneAsync(
			FilterBuilder<AccountTokenCode>.Where(code =>
				code.TokenCode == request.Token && code.TokenType == AccountTokenType.ResetPassword),
			cancellationToken);

		NullValueReferenceException.ThrowIfNull(tokenCode, nameof(tokenCode));

		if (!tokenCode.IsValid())
		{
			return JsonApiResponse<RecoveryQueryResult>.Fail();
		}

		var newPassword = PasswordHasher.RandomPassword(6);

		var updatedResult = await _userAccountRepository.UpdateOneAsync(
			FilterBuilder<UserAccount>.Where(acc => acc.Email.Equals(tokenCode.Email)),
			p => p.Set(acc => acc.HashedPassword, PasswordHasher.Hash(newPassword)),
			cancellationToken: cancellationToken);

		if (updatedResult.Any())
		{
			return JsonApiResponse<RecoveryQueryResult>.Success(data: new RecoveryQueryResult
				{ NewPassword = newPassword });
		}

		return JsonApiResponse<RecoveryQueryResult>.Fail();
	}
}
