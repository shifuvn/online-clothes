using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Services.ObjectStorage;
using OnlineClothes.Application.Services.ObjectStorage.Models;
using OnlineClothes.Application.Services.UserContext;

namespace OnlineClothes.Application.Features.Profile.Commands.EditAvatar;

internal sealed class EditAvatarCommandHandler : IRequestHandler<EditAvatarCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IAccountRepository _accountRepository;
	private readonly IObjectFileStorage _objectFileStorage;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IUserContext _userContext;


	public EditAvatarCommandHandler(
		IAccountRepository accountRepository,
		IObjectFileStorage objectFileStorage,
		IUserContext userContext, IUnitOfWork unitOfWork)
	{
		_accountRepository = accountRepository;
		_objectFileStorage = objectFileStorage;
		_userContext = userContext;
		_unitOfWork = unitOfWork;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(EditAvatarCommand request,
		CancellationToken cancellationToken)
	{
		var account = await _accountRepository.GetByIntKey(_userContext.GetNameIdentifier(), cancellationToken);


		var prefixDirectory = ObjectFileStorage.CombinePrefixDirectory("avatars");
		var fileName = $"{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}_{request.Avatar.FileName}";
		var storageObject = new ObjectFileStorage(request.Avatar, prefixDirectory, fileName);

		account.ImageUrl = await _objectFileStorage.UploadAsync(storageObject, cancellationToken);

		_accountRepository.Update(account);
		var saves = await _unitOfWork.SaveChangesAsync(cancellationToken);

		return saves
			? JsonApiResponse<EmptyUnitResponse>.Success()
			: JsonApiResponse<EmptyUnitResponse>.Fail();
	}
}
