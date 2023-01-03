using MediatR;
using MongoDB.Driver;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Infrastructure.Services.UserContext.Abstracts;
using OnlineClothes.Persistence.Extensions;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Profile.Commands.EditInformation;

public class EditInformationCommandHandler : IRequestHandler<EditInformationCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IAccountRepository _accountRepository;
	private readonly IUserContext _userContext;

	public EditInformationCommandHandler(IUserContext userContext, IAccountRepository accountRepository)
	{
		_userContext = userContext;
		_accountRepository = accountRepository;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(EditInformationCommand request,
		CancellationToken cancellationToken)
	{
		var account = await _accountRepository.GetOneAsync(_userContext.GetNameIdentifier(), cancellationToken);

		var updatedResult = await _accountRepository.UpdateOneAsync(
			account.Id.ToString(),
			update => update.Set(p => p.FirstName, request.FirstName.Trim())
				.Set(p => p.LastName, request.LastName.Trim()),
			cancellationToken: cancellationToken);

		return updatedResult.Any()
			? JsonApiResponse<EmptyUnitResponse>.Success()
			: JsonApiResponse<EmptyUnitResponse>.Fail();
	}
}
