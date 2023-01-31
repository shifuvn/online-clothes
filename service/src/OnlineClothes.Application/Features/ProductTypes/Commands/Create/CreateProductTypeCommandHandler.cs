using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.ProductTypes.Commands.Create;

public class
	CreateProductTypeCommandHandler : IRequestHandler<CreateProductTypeCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IProductTypeRepository _productTypeRepository;
	private readonly IUnitOfWork _unitOfWork;

	public CreateProductTypeCommandHandler(IUnitOfWork unitOfWork, IProductTypeRepository productTypeRepository)
	{
		_unitOfWork = unitOfWork;
		_productTypeRepository = productTypeRepository;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(CreateProductTypeCommand request,
		CancellationToken cancellationToken)
	{
		var existedType = await _productTypeRepository.FindOneAsync(
			FilterBuilder<ProductType>.Where(q => q.Name == request.Name), cancellationToken: cancellationToken);

		if (existedType is not null)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail("Loại sản phẩm đã tồn tại");
		}

		var productType = new ProductType(request.Name);
		await _productTypeRepository.AddAsync(productType, cancellationToken: cancellationToken);
		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);

		return save ? JsonApiResponse<EmptyUnitResponse>.Created() : JsonApiResponse<EmptyUnitResponse>.Fail();
	}
}
