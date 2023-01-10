using Microsoft.AspNetCore.Http;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Services.ObjectStorage;
using OnlineClothes.Application.Services.ObjectStorage.Models;
using OnlineClothes.Support.Builders.Predicate;

namespace OnlineClothes.Application.Helpers;

public class SkuImageFileHelper
{
	private readonly IImageRepository _imageRepository;
	private readonly IObjectStorage _objectStorage;
	private readonly IUnitOfWork _unitOfWork;

	public SkuImageFileHelper(IObjectStorage objectStorage, IImageRepository imageRepository, IUnitOfWork unitOfWork)
	{
		_objectStorage = objectStorage;
		_imageRepository = imageRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task ReplaceImageFile(ProductSku sku, IFormFile? file)
	{
		if (file is null)
		{
			await DeleteImageFile(sku);
			return;
		}

		var productImageName = sku.Sku;
		var objectStorage = new ObjectStorage(
			file,
			ObjectStorage.CombinePrefixDirectory("product"),
			productImageName);

		await _objectStorage.UploadAsync(objectStorage);
	}

	public async Task DeleteImageFile(ProductSku sku)
	{
		if (sku.Image is null)
		{
			return;
		}

		var image = await _imageRepository.FindAndDelete(FilterBuilder<ImageObject>.Where(q => q.Id == sku.ImageId));

		if (image is null)
		{
			return;
		}

		await _objectStorage.DeleteAsync(image.Url);
		sku.Image = null;

		await _unitOfWork.SaveChangesAsync();
	}
}
