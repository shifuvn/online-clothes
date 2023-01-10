using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Products.Commands.PromoteThumbnail;

public class
	PromoteProductThumbnailCommandHandler : IRequestHandler<PromoteProductThumbnailCommand,
		JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IImageRepository _imageRepository;
	private readonly IProductRepository _productRepository;
	private readonly IUnitOfWork _unitOfWork;

	public PromoteProductThumbnailCommandHandler(
		IProductRepository productRepository,
		IImageRepository imageRepository,
		IUnitOfWork unitOfWork)
	{
		_productRepository = productRepository;
		_imageRepository = imageRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(PromoteProductThumbnailCommand request,
		CancellationToken cancellationToken)
	{
		var product = await _productRepository.GetByIntKey(request.ProductId, cancellationToken);

		_productRepository.Update(product);

		product.ThumbnailImageId = request.ImageId;

		return await _unitOfWork.SaveChangesAsync(cancellationToken)
			? JsonApiResponse<EmptyUnitResponse>.Success()
			: JsonApiResponse<EmptyUnitResponse>.Fail();
	}
}
