using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Services.Mailing;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.Exceptions;

namespace OnlineClothes.Application.Features.Accounts.Queries.Recovery;

internal sealed class RecoveryQueryHandler : IRequestHandler<RecoveryQuery, JsonApiResponse<RecoveryQueryResult>>
{
	private readonly IAccountRepository _accountRepository;
	private readonly ILogger<RecoveryQueryHandler> _logger;
	private readonly IMailingService _mailingService;
	private readonly ITokenRepository _tokenRepository;
	private readonly IUnitOfWork _unitOfWork;

	public RecoveryQueryHandler(ILogger<RecoveryQueryHandler> logger,
		IMailingService mailingService,
		IUnitOfWork unitOfWork,
		ITokenRepository tokenRepository,
		IAccountRepository accountRepository)
	{
		_logger = logger;
		_mailingService = mailingService;
		_unitOfWork = unitOfWork;
		_tokenRepository = tokenRepository;
		_accountRepository = accountRepository;
	}

	public async Task<JsonApiResponse<RecoveryQueryResult>> Handle(RecoveryQuery request,
		CancellationToken cancellationToken)
	{
		var tokenCode = await _tokenRepository.FindOneAsync(
			FilterBuilder<AccountTokenCode>.Where(code =>
				code.TokenCode == request.Token && code.TokenType == AccountTokenType.ResetPassword),
			cancellationToken);

		NullValueReferenceException.ThrowIfNull(tokenCode, nameof(tokenCode));

		if (!tokenCode.IsValid())
		{
			return JsonApiResponse<RecoveryQueryResult>.Fail();
		}

		var account = await _accountRepository.GetByEmail(tokenCode.Email, cancellationToken);
		var newPassword = PasswordHasher.RandomPassword(6);
		account!.SetPassword(newPassword);

		_accountRepository.Update(account);
		var updatedResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
		if (updatedResult)
		{
			return JsonApiResponse<RecoveryQueryResult>.Success(data: new RecoveryQueryResult
				{ NewPassword = newPassword });
		}

		return JsonApiResponse<RecoveryQueryResult>.Fail();
	}
}
