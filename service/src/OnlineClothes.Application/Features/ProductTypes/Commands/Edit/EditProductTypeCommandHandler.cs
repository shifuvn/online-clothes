using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.ProductTypes.Commands.Edit;

public class EditProductTypeCommandHandler : IRequestHandler<EditProductTypeCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IProductTypeRepository _productTypeRepository;
	private readonly IUnitOfWork _unitOfWork;

	public EditProductTypeCommandHandler(IUnitOfWork unitOfWork, IProductTypeRepository productTypeRepository)
	{
		_unitOfWork = unitOfWork;
		_productTypeRepository = productTypeRepository;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(EditProductTypeCommand request,
		CancellationToken cancellationToken)
	{
		var existedName = await _productTypeRepository.AnyAsync(
			FilterBuilder<ProductType>.Where(q => q.Name.Equals(request.Name)), cancellationToken);

		if (existedName)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail("Tên này đã được dùng");
		}

		var productType = await _productTypeRepository.GetByIntKey(request.Id, cancellationToken);

		productType.Update(request.Name);
		_productTypeRepository.Update(productType);

		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);

		return save
			? JsonApiResponse<EmptyUnitResponse>.Success()
			: JsonApiResponse<EmptyUnitResponse>.Fail();
	}
}
