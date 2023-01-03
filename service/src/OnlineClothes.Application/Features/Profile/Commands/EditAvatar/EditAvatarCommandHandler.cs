using MediatR;
using Microsoft.Extensions.Logging;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Infrastructure.Services.Storage.Abstracts;
using OnlineClothes.Infrastructure.Services.Storage.Models;
using OnlineClothes.Infrastructure.Services.UserContext.Abstracts;
using OnlineClothes.Persistence.Extensions;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Profile.Commands.EditAvatar;

internal sealed class EditAvatarCommandHandler : IRequestHandler<EditAvatarCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IAccountRepository _accountRepository;
	private readonly IObjectFileStorage _objectFileStorage;
	private readonly IUserContext _userContext;
	private ILogger<EditAvatarCommandHandler> _logger;


	public EditAvatarCommandHandler(ILogger<EditAvatarCommandHandler> logger, IAccountRepository accountRepository,
		IObjectFileStorage objectFileStorage, IUserContext userContext)
	{
		_logger = logger;
		_accountRepository = accountRepository;
		_objectFileStorage = objectFileStorage;
		_userContext = userContext;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(EditAvatarCommand request,
		CancellationToken cancellationToken)
	{
		var account = await _accountRepository.GetOneAsync(_userContext.GetNameIdentifier(), cancellationToken);


		var prefixDirectory = ObjectFileStorage.CombinePrefixDirectory("avatars");
		var fileName = $"{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}_{request.Avatar.FileName}";
		var storageObject = new ObjectFileStorage(request.Avatar, prefixDirectory, fileName);

		var cloudObjectUrl = await _objectFileStorage.UploadAsync(storageObject, cancellationToken);
		account.ImageUrl = cloudObjectUrl;

		var updatedResult = await _accountRepository.UpdateOneAsync(
			account.Id.ToString(),
			update => update.Set(acc => acc.ImageUrl, account.ImageUrl),
			cancellationToken: cancellationToken);

		return updatedResult.Any()
			? JsonApiResponse<EmptyUnitResponse>.Success()
			: JsonApiResponse<EmptyUnitResponse>.Fail();
	}
}
