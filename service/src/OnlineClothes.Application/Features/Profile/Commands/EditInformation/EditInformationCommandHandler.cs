using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Services.UserContext;

namespace OnlineClothes.Application.Features.Profile.Commands.EditInformation;

public class EditInformationCommandHandler : IRequestHandler<EditInformationCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IAccountRepository _accountRepository;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IUserContext _userContext;

	public EditInformationCommandHandler(IUserContext userContext, IAccountRepository accountRepository,
		IUnitOfWork unitOfWork)
	{
		_userContext = userContext;
		_accountRepository = accountRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(EditInformationCommand request,
		CancellationToken cancellationToken)
	{
		var account = await _accountRepository.GetByIntKey(_userContext.GetNameIdentifier(), cancellationToken);
		request.Map(account);

		_accountRepository.Update(account);

		return await _unitOfWork.SaveChangesAsync(cancellationToken)
			? JsonApiResponse<EmptyUnitResponse>.Success()
			: JsonApiResponse<EmptyUnitResponse>.Fail();
	}
}
