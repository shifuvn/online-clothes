using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Images.Commands.DeleteSkuImage;

public class DeleteSkuImageCommandHandler : IRequestHandler<DeleteSkuImageCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IImageRepository _imageRepository;
	private readonly ISkuRepository _skuRepository;
	private readonly IUnitOfWork _unitOfWork;

	public DeleteSkuImageCommandHandler(ISkuRepository skuRepository, IImageRepository imageRepository,
		IUnitOfWork unitOfWork)
	{
		_skuRepository = skuRepository;
		_imageRepository = imageRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(DeleteSkuImageCommand request,
		CancellationToken cancellationToken)
	{
		var sku = await _skuRepository.GetSkuDetailBySkuAsync(request.Sku, cancellationToken);

		if (sku.Image is null)
		{
			return JsonApiResponse<EmptyUnitResponse>.Success();
		}

		_imageRepository.Delete(sku.Image);

		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);

		return JsonApiResponse<EmptyUnitResponse>.Success();
	}
}
