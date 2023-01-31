using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Products.Queries.Single;

public class GetSingleProductQueryHandler : IRequestHandler<GetSingleProductQuery, JsonApiResponse<ProductBasicDto>>
{
	private readonly IProductRepository _productRepository;

	public GetSingleProductQueryHandler(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	public async Task<JsonApiResponse<ProductBasicDto>> Handle(GetSingleProductQuery request,
		CancellationToken cancellationToken)
	{
		var product = await _productRepository.GetByIntKey(request.Id, cancellationToken);

		return JsonApiResponse<ProductBasicDto>.Success(data: new ProductBasicDto(product));
	}
}
