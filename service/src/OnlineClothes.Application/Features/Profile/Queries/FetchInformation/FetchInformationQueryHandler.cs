using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Services.UserContext;

namespace OnlineClothes.Application.Features.Profile.Queries.FetchInformation;

internal sealed class
	FetchInformationQueryHandler : IRequestHandler<FetchInformationQuery, JsonApiResponse<FetchInformationQueryResult>>
{
	private readonly IAccountRepository _accountRepository;
	private readonly IUserContext _userContext;

	public FetchInformationQueryHandler(IUserContext userContext, IAccountRepository accountRepository)
	{
		_userContext = userContext;
		_accountRepository = accountRepository;
	}

	public async Task<JsonApiResponse<FetchInformationQueryResult>> Handle(FetchInformationQuery request,
		CancellationToken cancellationToken)
	{
		var account = await _accountRepository.GetByIntKey(_userContext.GetNameIdentifier(), cancellationToken);

		return JsonApiResponse<FetchInformationQueryResult>.Success(data: FetchInformationQueryResult.ToModel(account));
	}
}
