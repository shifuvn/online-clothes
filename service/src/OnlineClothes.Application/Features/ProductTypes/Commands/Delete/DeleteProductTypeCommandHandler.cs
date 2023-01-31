using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.ProductTypes.Commands.Delete;

public class
	DeleteProductTypeCommandHandler : IRequestHandler<DeleteProductTypeCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IProductTypeRepository _productTypeRepository;
	private readonly IUnitOfWork _unitOfWork;

	public DeleteProductTypeCommandHandler(IProductTypeRepository productTypeRepository, IUnitOfWork unitOfWork)
	{
		_productTypeRepository = productTypeRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(DeleteProductTypeCommand request,
		CancellationToken cancellationToken)
	{
		var productType = await _productTypeRepository.GetByIntKey(request.Id, cancellationToken);

		_productTypeRepository.Delete(productType);
		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);

		return save
			? JsonApiResponse<EmptyUnitResponse>.Success()
			: JsonApiResponse<EmptyUnitResponse>.Fail();
	}
}
