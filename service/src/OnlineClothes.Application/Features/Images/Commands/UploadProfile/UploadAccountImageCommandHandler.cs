using OnlineClothes.Application.Helpers;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Services.UserContext;

namespace OnlineClothes.Application.Features.Images.Commands.UploadProfile;

public class
	UploadAccountImageCommandHandler : IRequestHandler<UploadAccountImageCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IAccountRepository _accountRepository;
	private readonly StorageImageFileHelper _storageImageFileHelper;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IUserContext _userContext;


	public UploadAccountImageCommandHandler(
		IUnitOfWork unitOfWork,
		IAccountRepository accountRepository,
		IUserContext userContext,
		StorageImageFileHelper storageImageFileHelper)
	{
		_unitOfWork = unitOfWork;
		_accountRepository = accountRepository;
		_userContext = userContext;
		_storageImageFileHelper = storageImageFileHelper;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(UploadAccountImageCommand request,
		CancellationToken cancellationToken)
	{
		// begin tx
		await _unitOfWork.BeginTransactionAsync(cancellationToken);

		var account = await _accountRepository.GetByIntKey(_userContext.GetNameIdentifier(), cancellationToken);

		_accountRepository.Update(account);

		await _storageImageFileHelper.AddOrUpdateAccountProfileAvatarAsync(account, request.File, cancellationToken);

		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);

		if (!save)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail();
		}

		// commit tx
		await _unitOfWork.CommitAsync(cancellationToken);

		return JsonApiResponse<EmptyUnitResponse>.Created();
	}
}
