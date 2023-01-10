using Microsoft.AspNetCore.Http;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Services.ObjectStorage;
using OnlineClothes.Application.Services.ObjectStorage.Models;
using OnlineClothes.Application.Services.UserContext;

namespace OnlineClothes.Infrastructure.Repositories;

public class ImageRepository : EfCoreRepositoryBase<ImageObject, int>, IImageRepository
{
	private readonly IAccountRepository _accountRepository;
	private readonly IObjectStorage _objectStorage;
	private readonly IUserContext _userContext;

	public ImageRepository(
		AppDbContext dbContext,
		IUserContext userContext,
		IAccountRepository accountRepository,
		IObjectStorage objectStorage) :
		base(dbContext)
	{
		_userContext = userContext;
		_accountRepository = accountRepository;
		_objectStorage = objectStorage;
	}

	public async Task AddAccountAvatarFileAsync(IFormFile file)
	{
		var account = await _accountRepository.GetByIntKey(_userContext.GetNameIdentifier());

		var uploadUrl = await UploadProfileImageToStorage(file);

		var imageObject = CreateProfileAvatar(account, uploadUrl);
		await AddAsync(imageObject);

		_accountRepository.Update(account);
		account.AvatarImage = imageObject;
	}

	public async Task<ImageObject?> AddProductImageFileAsync(
		IFormFile? file,
		string sku,
		CancellationToken cancellationToken = default)
	{
		if (file is null)
		{
			return null;
		}

		var uploadedUrl = await UploadProductImageToStorage(file, sku);

		// create new record
		var image = new ImageObject(uploadedUrl);
		await AddAsync(image, cancellationToken: cancellationToken);

		return image;
	}

	private async Task<string> UploadProductImageToStorage(IFormFile? file, string sku)
	{
		ArgumentNullException.ThrowIfNull(sku);

		var productImageName = sku;
		var objectStorage = new ObjectStorage(
			file,
			ObjectStorage.CombinePrefixDirectory("product"),
			productImageName);

		return await _objectStorage.UploadAsync(objectStorage);
	}


	private async Task<string> UploadProfileImageToStorage(IFormFile file)
	{
		var accountImageName = $"profile-{_userContext.GetNameIdentifier()}";
		var objectStorage = new ObjectStorage(
			file,
			ObjectStorage.CombinePrefixDirectory("profile", _userContext.GetNameIdentifier().ToString()),
			accountImageName);

		var uploadUrl = await _objectStorage.UploadAsync(objectStorage);
		return uploadUrl;
	}

	private ImageObject CreateProfileAvatar(AccountUser account, string uploadedUrl)
	{
		return new ImageObject(uploadedUrl, $"{account.Email} avatar");
	}
}
