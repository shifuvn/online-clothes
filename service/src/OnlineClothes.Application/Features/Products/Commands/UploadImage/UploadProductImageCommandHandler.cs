namespace OnlineClothes.Application.Features.Products.Commands.UploadImage;

public sealed class
	UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommand, JsonApiResponse<EmptyUnitResponse>>
{
	//private readonly ILogger<UploadProductImageCommandHandler> _logger;
	//private readonly IObjectFileStorage _objectFileStorage;
	//private readonly IProductRepository _productRepository;

	//public UploadProductImageCommandHandler(IObjectFileStorage objectFileStorage, IProductRepository productRepository,
	//	ILogger<UploadProductImageCommandHandler> logger)
	//{
	//	_objectFileStorage = objectFileStorage;
	//	_productRepository = productRepository;
	//	_logger = logger;
	//}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(UploadProductImageCommand request,
		CancellationToken cancellationToken)
	{
		throw new NotImplementedException();

		//var product = await _productRepository.GetOneAsync(request.ProductId, cancellationToken);

		//var prefixDirectory = ObjectFileStorage.CombinePrefixDirectory("products");
		//var fileName = $"1_{product.Id}.png";
		//var storageObject = new ObjectFileStorage(request.File, prefixDirectory, fileName, request.File.ContentType);

		//var uploadUrl = await _objectFileStorage.UploadAsync(storageObject, cancellationToken);

		//if (!product.ImageUrls.Any())
		//{
		//	product.ImageUrls = new List<string> { uploadUrl };
		//}
		//else
		//{
		//	product.ImageUrls[0] = uploadUrl; // hardcode cause now only 1 image
		//}

		//var updatedResult = await _productRepository.UpdateOneAsync(
		//	product.Id,
		//	update => update.Set(q => q.ImageUrls, product.ImageUrls),
		//	cancellationToken: cancellationToken);

		//if (!updatedResult.Any())
		//{
		//	return JsonApiResponse<EmptyUnitResponse>.Fail();
		//}

		//_logger.LogInformation("Upload product {Id} image at url {Url}", product.Id, uploadUrl);
		//return JsonApiResponse<EmptyUnitResponse>.Success(StatusCodes.Status201Created,
		//	"Thêm ảnh cho sản phẩm thành công");
	}
}
