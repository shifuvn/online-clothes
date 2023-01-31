using Microsoft.EntityFrameworkCore;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Skus.Queries.Detail;

public class GetSkuDetailQueryHandler : IRequestHandler<GetSkuDetailQuery, JsonApiResponse<ProductSkuDto>>
{
	private readonly ISkuRepository _skuRepository;

	public GetSkuDetailQueryHandler(ISkuRepository skuRepository)
	{
		_skuRepository = skuRepository;
	}

	public async Task<JsonApiResponse<ProductSkuDto>> Handle(GetSkuDetailQuery request,
		CancellationToken cancellationToken)
	{
		var product = await _skuRepository.AsQueryable()
			.Include(sku => sku.Product)
			.ThenInclude(sku => sku.Brand)
			.Include(sku => sku.Product)
			.ThenInclude(sku => sku.ProductCategories)
			.ThenInclude(sku => sku.Category)
			.Include(sku => sku.Image)
			.Include(sku => sku.Product)
			.ThenInclude(sku => sku.ProductType)
			.FirstOrDefaultAsync(sku => sku.Sku.Equals(request.Sku), cancellationToken);

		if (product is null)
		{
			return JsonApiResponse<ProductSkuDto>.Fail();
		}

		var viewModel = new ProductSkuDto(product);
		return JsonApiResponse<ProductSkuDto>.Success(data: viewModel);
	}
}
