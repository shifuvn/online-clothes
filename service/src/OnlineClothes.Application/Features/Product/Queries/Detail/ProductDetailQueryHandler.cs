using MediatR;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Product.Queries.Detail;

public class
	ProductDetailQueryHandler : IRequestHandler<ProductDetailQuery, JsonApiResponse<ProductDetailQueryViewModel>>
{
	private readonly IProductRepository _productRepository;

	public ProductDetailQueryHandler(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	public async Task<JsonApiResponse<ProductDetailQueryViewModel>> Handle(ProductDetailQuery request,
		CancellationToken cancellationToken)
	{
		var product = await _productRepository.GetOneAsync(request.ProductId, cancellationToken);
		var viewModel = ProductDetailQueryViewModel.Create(product);
		return JsonApiResponse<ProductDetailQueryViewModel>.Success(data: viewModel);
	}
}
