using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Image.Commands.UploadProduct;

public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommand, JsonApiResponse<int>>
{
	private readonly IImageRepository _imageRepository;
	private readonly IUnitOfWork _unitOfWork;

	public UploadProductImageCommandHandler(IImageRepository imageRepository, IUnitOfWork unitOfWork)
	{
		_imageRepository = imageRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<JsonApiResponse<int>> Handle(UploadProductImageCommand request,
		CancellationToken cancellationToken)
	{
		var image = await _imageRepository.UploadProductFile(request.File, cancellationToken);

		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);

		if (!save)
		{
			return JsonApiResponse<int>.Fail();
		}

		return JsonApiResponse<int>.Created(data: image.Id);
	}
}
