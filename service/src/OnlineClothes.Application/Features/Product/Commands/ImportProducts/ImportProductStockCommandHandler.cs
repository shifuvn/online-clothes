using MediatR;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Persistence.Extensions;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Product.Commands.ImportProducts;

public class
	ImportProductStockCommandHandler : IRequestHandler<ImportProductStockCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IProductRepository _productRepository;

	public ImportProductStockCommandHandler(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(ImportProductStockCommand request,
		CancellationToken cancellationToken)
	{
		var updatedResult = await _productRepository.UpdateOneAsync(
			request.ProductId,
			update => update.Set(q => q.Stock, request.Quantity),
			cancellationToken: cancellationToken);

		return updatedResult.Any()
			? JsonApiResponse<EmptyUnitResponse>.Success(message: "Thêm hàng thành công")
			: JsonApiResponse<EmptyUnitResponse>.Fail();
	}
}
