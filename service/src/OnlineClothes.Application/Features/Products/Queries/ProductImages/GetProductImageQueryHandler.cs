using Microsoft.EntityFrameworkCore;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Products.Queries.ProductImages;

public class GetProductImageQueryHandler : IRequestHandler<GetProductImageQuery, JsonApiResponse<List<ImageDto>>>
{
	private readonly IProductRepository _productRepository;
	private readonly ISkuRepository _skuRepository;

	public GetProductImageQueryHandler(IProductRepository productRepository, ISkuRepository skuRepository)
	{
		_productRepository = productRepository;
		_skuRepository = skuRepository;
	}

	public async Task<JsonApiResponse<List<ImageDto>>> Handle(
		GetProductImageQuery request,
		CancellationToken cancellationToken)
	{
		var data = await _skuRepository.AsQueryable()
			.Include(q => q.Image)
			.Where(q => q.ProductId == request.Id && q.ImageId != null)
			.Select(q => ImageDto.ToModel(q.Image!))
			.ToListAsync(cancellationToken);

		return JsonApiResponse<List<ImageDto>>.Success(data: data);
	}
}
