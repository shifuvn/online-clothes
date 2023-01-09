using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IProductRepository _productRepository;
	private readonly IUnitOfWork _unitOfWork;

	public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
	{
		_productRepository = productRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(DeleteProductCommand request,
		CancellationToken cancellationToken)
	{
		var product = await _productRepository.GetByIntKey(request.Id, cancellationToken);

		_productRepository.Update(product);
		product.Delete();

		return await _unitOfWork.SaveChangesAsync(cancellationToken)
			? JsonApiResponse<EmptyUnitResponse>.Success()
			: JsonApiResponse<EmptyUnitResponse>.Fail();
	}
}
