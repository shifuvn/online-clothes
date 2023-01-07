using OnlineClothes.Application.Persistence;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.Exceptions;

namespace OnlineClothes.Application.Features.Accounts.Queries.Activate;

internal sealed class ActivateQueryHandler : IRequestHandler<ActivateQuery, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IAccountRepository _accountRepository;
	private readonly ILogger<ActivateQueryHandler> _logger;
	private readonly ITokenRepository _tokenRepository;
	private readonly IUnitOfWork _unitOfWork;

	public ActivateQueryHandler(ILogger<ActivateQueryHandler> logger,
		IUnitOfWork unitOfWork,
		ITokenRepository tokenRepository,
		IAccountRepository accountRepository)
	{
		_logger = logger;
		_unitOfWork = unitOfWork;
		_tokenRepository = tokenRepository;
		_accountRepository = accountRepository;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(ActivateQuery request,
		CancellationToken cancellationToken)
	{
		var tokenCode = await _tokenRepository.FindOneAsync(FilterBuilder<AccountTokenCode>.Where(x =>
			x.TokenCode == request.Token && x.TokenType == AccountTokenType.Verification), cancellationToken);

		NullValueReferenceException.ThrowIfNull(tokenCode, nameof(tokenCode));

		if (!tokenCode.IsValid())
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail();
		}

		var account =
			await _accountRepository.GetByEmail(tokenCode.Email, cancellationToken);

		NullValueReferenceException.ThrowIfNull(account);

		// Begin tx
		await _unitOfWork.BeginTransactionAsync(cancellationToken);

		account.Activate();
		_accountRepository.Update(account);

		var updateResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

		if (updateResult)
		{
			_tokenRepository.Delete(tokenCode);
			await _unitOfWork.SaveChangesAsync(cancellationToken);

			_logger.LogInformation("Account {Email} is activated", tokenCode.Email);
		}

		try
		{
			// Commit
			await _unitOfWork.CommitAsync(cancellationToken);
			return JsonApiResponse<EmptyUnitResponse>.Success();
		}
		catch (Exception e)
		{
			_logger.LogError(e, "{message}", e.Message);
			throw new Exception(e.Message);
		}
	}
}
