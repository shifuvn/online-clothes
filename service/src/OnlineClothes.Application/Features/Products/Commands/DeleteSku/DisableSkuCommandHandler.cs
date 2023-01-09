using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Products.Commands.DeleteSku;

public class DisableSkuCommandHandler : IRequestHandler<DisableSkuCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly ILogger<DisableSkuCommandHandler> _logger;
	private readonly ISkuRepository _skuRepository;
	private readonly IUnitOfWork _unitOfWork;


	public DisableSkuCommandHandler(
		ILogger<DisableSkuCommandHandler> logger,
		IUnitOfWork unitOfWork,
		ISkuRepository skuRepository)
	{
		_logger = logger;
		_unitOfWork = unitOfWork;
		_skuRepository = skuRepository;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(DisableSkuCommand request,
		CancellationToken cancellationToken)
	{
		var productSku = await _skuRepository.GetByStrKey(request.Sku, cancellationToken);

		_skuRepository.Update(productSku);
		productSku.Disable();

		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);
		if (!save)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail();
		}

		_logger.LogInformation("Disable product {Id} successfully", request.Sku);
		return JsonApiResponse<EmptyUnitResponse>.Success();
	}
}
