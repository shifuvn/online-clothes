using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Products.Commands.RestoreSku;

public class RestoreSkuCommandHandler : IRequestHandler<RestoreSkuCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly ILogger<RestoreSkuCommandHandler> _logger;
	private readonly ISkuRepository _skuRepository;
	private readonly IUnitOfWork _unitOfWork;

	public RestoreSkuCommandHandler(ISkuRepository skuRepository, IUnitOfWork unitOfWork,
		ILogger<RestoreSkuCommandHandler> logger)
	{
		_skuRepository = skuRepository;
		_unitOfWork = unitOfWork;
		_logger = logger;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(RestoreSkuCommand request,
		CancellationToken cancellationToken)
	{
		var sku = await _skuRepository.GetByStrKey(request.Sku, cancellationToken);

		_skuRepository.Update(sku);
		sku.Enable();

		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);
		if (!save)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail();
		}

		_logger.LogInformation("Enable product {Id} successfully", request.Sku);
		return JsonApiResponse<EmptyUnitResponse>.Success();
	}
}
