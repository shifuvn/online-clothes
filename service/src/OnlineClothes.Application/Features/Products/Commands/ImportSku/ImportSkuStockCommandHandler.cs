using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Products.Commands.ImportSku;

public class
	ImportSkuStockCommandHandler : IRequestHandler<ImportSkuStockCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly ISkuRepository _skuRepository;
	private readonly IUnitOfWork _unitOfWork;

	public ImportSkuStockCommandHandler(
		IUnitOfWork unitOfWork,
		ISkuRepository skuRepository)
	{
		_unitOfWork = unitOfWork;
		_skuRepository = skuRepository;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(ImportSkuStockCommand request,
		CancellationToken cancellationToken)
	{
		var productSku = await _skuRepository.GetByStrKey(request.Sku, cancellationToken);

		productSku.ImportStock(request.Quantity);

		_skuRepository.Update(productSku);


		return await _unitOfWork.SaveChangesAsync(cancellationToken)
			? JsonApiResponse<EmptyUnitResponse>.Success(message: "Thêm hàng thành công")
			: JsonApiResponse<EmptyUnitResponse>.Fail();
	}
}
