using Microsoft.AspNetCore.Http;
using OnlineClothes.Application.Commons;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Services.ObjectStorage;
using OnlineClothes.Application.Services.ObjectStorage.Models;

namespace OnlineClothes.Application.Helpers;

public class StorageImageFileHelper
{
	private readonly IImageRepository _imageRepository;
	private readonly IObjectStorage _objectStorage;


	public StorageImageFileHelper(
		IObjectStorage objectStorage,
		IImageRepository imageRepository)
	{
		_objectStorage = objectStorage;
		_imageRepository = imageRepository;
	}

	public async Task AddOrUpdateAccountProfileAvatarAsync(
		AccountUser account,
		IFormFile file,
		CancellationToken cancellationToken = default)
	{
		if (account.AvatarImage is not null)
		{
			await UpdateAccountProfileAvatar(account, file, cancellationToken);
			return;
		}

		await AddAccountProfileAvatar(account, file, cancellationToken);
	}

	public async Task AddOrUpdateSkuImageAsync(
		ProductSku sku,
		IFormFile file,
		CancellationToken cancellationToken = default)
	{
		if (sku.Image is not null)
		{
			await UpdateSkuImage(sku, file);
			return;
		}

		await AddSkuImage(sku, file);
	}

	private async Task AddAccountProfileAvatar(AccountUser account, IFormFile file, CancellationToken cancellationToken)
	{
		var objectToUpload = new ObjectStorage(file, "profile", account.Id.ToString(), ContentType.ImageType);
		var uploadedUrl = await _objectStorage.UploadAsync(objectToUpload, cancellationToken);

		var imageObj = new ImageObject(uploadedUrl);

		await _imageRepository.AddAsync(imageObj, cancellationToken: cancellationToken);

		account.AvatarImage = imageObj;
	}

	private async Task UpdateAccountProfileAvatar(AccountUser account, IFormFile file,
		CancellationToken cancellationToken)
	{
		var identifierKey = ObjectStorage.GetIdentifierKey(account.AvatarImage!.Url);
		await _objectStorage.ReplaceAsync(file.OpenReadStream(), identifierKey, ContentType.ImageType,
			cancellationToken);
	}

	private async Task AddSkuImage(ProductSku sku, IFormFile file)
	{
		var objectToUpload = new ObjectStorage(file, "product", sku.Sku, ContentType.ImageType);
		var uploadedUrl = await _objectStorage.UploadAsync(objectToUpload);

		var imageObj = new ImageObject(uploadedUrl);

		await _imageRepository.AddAsync(imageObj);

		sku.Image = imageObj;
	}

	private async Task UpdateSkuImage(ProductSku sku, IFormFile file)
	{
		var key = ObjectStorage.GetIdentifierKey(sku.Image!.Url);
		await _objectStorage.ReplaceAsync(file.OpenReadStream(), key, ContentType.ImageType);
	}
}
