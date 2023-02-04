using Microsoft.EntityFrameworkCore;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Skus.Queries.OtherSku;

public class GetAllSkuInTheSameProductQueryHandler : IRequestHandler<GetAllSkuInTheSameProductQuery,
	JsonApiResponse<List<GetAllSkuInTheSameProductQueryViewModel>>>
{
	private readonly IProductRepository _productRepository;

	public GetAllSkuInTheSameProductQueryHandler(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	public async Task<JsonApiResponse<List<GetAllSkuInTheSameProductQueryViewModel>>> Handle(
		GetAllSkuInTheSameProductQuery request, CancellationToken cancellationToken)
	{
		var data = await _productRepository
			.AsQueryable()
			.Include(q => q.ProductSkus)
			.FirstAsync(q => q.Id == request.ProductId, cancellationToken: cancellationToken);

		var skuIds =
			data.ProductSkus.SelectList(q => new GetAllSkuInTheSameProductQueryViewModel(q.Sku, q.DisplaySkuName));

		return JsonApiResponse<List<GetAllSkuInTheSameProductQueryViewModel>>.Success(data: skuIds);
	}
}
