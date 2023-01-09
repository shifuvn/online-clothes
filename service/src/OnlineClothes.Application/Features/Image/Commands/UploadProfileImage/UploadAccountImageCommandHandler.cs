using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Image.Commands.UploadProfileImage;

public class
	UploadAccountImageCommandHandler : IRequestHandler<UploadAccountImageCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IImageRepository _imageRepository;
	private readonly IUnitOfWork _unitOfWork;

	public UploadAccountImageCommandHandler(IImageRepository imageRepository, IUnitOfWork unitOfWork)
	{
		_imageRepository = imageRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(UploadAccountImageCommand request,
		CancellationToken cancellationToken)
	{
		// begin tx
		await _unitOfWork.BeginTransactionAsync(cancellationToken);

		await _imageRepository.UploadAccountAvatar(request.File);

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
