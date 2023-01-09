using Microsoft.EntityFrameworkCore;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Products.Queries.Detail;

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
			.Include(q => q.Product)
			.ThenInclude(q => q.Brand)
			.Include(q => q.Product)
			.ThenInclude(q => q.ProductCategories)
			.ThenInclude(q => q.Category)
			.FirstOrDefaultAsync(q => q.Sku.Equals(request.Sku), cancellationToken);

		if (product is null)
		{
			return JsonApiResponse<ProductSkuDto>.Fail();
		}

		var viewModel = ProductSkuDto.ToModel(product);
		return JsonApiResponse<ProductSkuDto>.Success(data: viewModel);
	}
}
