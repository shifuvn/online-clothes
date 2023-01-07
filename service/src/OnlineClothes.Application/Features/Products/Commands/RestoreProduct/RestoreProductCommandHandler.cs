using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Products.Commands.RestoreProduct;

public class RestoreProductCommandHandler : IRequestHandler<RestoreProductCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly ILogger<RestoreProductCommandHandler> _logger;
	private readonly IProductRepository _productRepository;
	private readonly IUnitOfWork _unitOfWork;

	public RestoreProductCommandHandler(IProductRepository productRepository,
		ILogger<RestoreProductCommandHandler> logger,
		IUnitOfWork unitOfWork)
	{
		_productRepository = productRepository;
		_logger = logger;
		_unitOfWork = unitOfWork;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(RestoreProductCommand request,
		CancellationToken cancellationToken)
	{
		var product = await _productRepository.GetByIntKey(request.ProductId, cancellationToken);

		_productRepository.Update(product);
		product.Restore();

		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);
		if (!save)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail();
		}

		_logger.LogInformation("Restore product {Id} successfully", request.ProductId);
		return JsonApiResponse<EmptyUnitResponse>.Success();
	}
}
