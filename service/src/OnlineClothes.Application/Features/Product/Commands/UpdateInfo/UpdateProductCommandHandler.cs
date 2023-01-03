using MediatR;
using Microsoft.Extensions.Logging;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Product.Commands.UpdateInfo;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly ILogger<UpdateProductCommandHandler> _logger;
	private readonly IProductRepository _productRepository;

	public UpdateProductCommandHandler(ILogger<UpdateProductCommandHandler> logger,
		IProductRepository productRepository)
	{
		_logger = logger;
		_productRepository = productRepository;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(UpdateProductCommand request,
		CancellationToken cancellationToken)
	{
		//var product = await _productRepository.GetOneAsync(request.ProductId, cancellationToken);

		//var requestBody = request.Body;

		//Func<UpdateDefinitionBuilder<ProductClothe>, UpdateDefinition<ProductClothe>> updateDef = update => update
		//	.Set(q => q.Name, requestBody.Name)
		//	.Set(q => q.Description, requestBody.Description)
		//	.Set(q => q.Price, requestBody.Price)
		//	.Set(q => q.Tags, requestBody.Tags)
		//	.Set(q => q.Categories, requestBody.Categories)
		//	.Set(q => q.Detail.Materials, requestBody.Materials)
		//	.Set(q => q.Detail.Sizes, requestBody.Sizes);

		//var updatedResult =
		//	await _productRepository.UpdateOneAsync(request.ProductId, updateDef, cancellationToken: cancellationToken);

		//return updatedResult.Any()
		//	? JsonApiResponse<EmptyUnitResponse>.Success(message: "Chỉnh sửa thành công")
		//	: JsonApiResponse<EmptyUnitResponse>.Fail();
		throw new NotImplementedException();
	}
}
