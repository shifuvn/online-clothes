using Newtonsoft.Json;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Brands.Commands.Delete;

public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IBrandRepository _brandRepository;
	private readonly ILogger<DeleteBrandCommandHandler> _logger;
	private readonly IUnitOfWork _unitOfWork;

	public DeleteBrandCommandHandler(IUnitOfWork unitOfWork, IBrandRepository brandRepository,
		ILogger<DeleteBrandCommandHandler> logger)
	{
		_unitOfWork = unitOfWork;
		_brandRepository = brandRepository;
		_logger = logger;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(DeleteBrandCommand request,
		CancellationToken cancellationToken)
	{
		var brand = await _brandRepository.GetByIntKey(request.Id, cancellationToken);
		_brandRepository.Delete(brand);

		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);

		if (!save)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail();
		}

		_logger.LogInformation("Delete brand: {object}", JsonConvert.SerializeObject(brand));
		return JsonApiResponse<EmptyUnitResponse>.Success();
	}
}
