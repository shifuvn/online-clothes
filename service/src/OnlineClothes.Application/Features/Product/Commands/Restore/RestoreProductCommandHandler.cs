using MediatR;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Persistence.Extensions;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Product.Commands.Restore;

public class RestoreProductCommandHandler : IRequestHandler<RestoreProductCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IProductRepository _productRepository;

	public RestoreProductCommandHandler(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(RestoreProductCommand request,
		CancellationToken cancellationToken)
	{
		var updatedResult = await _productRepository.UpdateOneAsync(
			request.ProductId,
			update => update.Set(q => q.IsDeleted, false), cancellationToken: cancellationToken);

		return updatedResult.Any()
			? JsonApiResponse<EmptyUnitResponse>.Success()
			: JsonApiResponse<EmptyUnitResponse>.Fail();
	}
}
