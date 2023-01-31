using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.ProductTypes.Queries.Single;

public class GetSingleProductTypeQueryHandler : IRequestHandler<GetSingleProductTypeQuery,
	JsonApiResponse<GetSingleProductTypeQueryViewModel>>
{
	private readonly IProductTypeRepository _productTypeRepository;

	public GetSingleProductTypeQueryHandler(IProductTypeRepository productTypeRepository)
	{
		_productTypeRepository = productTypeRepository;
	}

	public async Task<JsonApiResponse<GetSingleProductTypeQueryViewModel>> Handle(GetSingleProductTypeQuery request,
		CancellationToken cancellationToken)
	{
		var productType = await _productTypeRepository.GetByIntKey(request.Id, cancellationToken);

		return JsonApiResponse<GetSingleProductTypeQueryViewModel>.Success(
			data: new GetSingleProductTypeQueryViewModel(productType));
	}
}
