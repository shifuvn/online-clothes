using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Brands.Queries.Single;

public class
	GetSingleBrandQueryHandler : IRequestHandler<GetSingleBrandQuery, JsonApiResponse<GetSingleBrandQueryViewModel>>
{
	private readonly IBrandRepository _brandRepository;

	public GetSingleBrandQueryHandler(IBrandRepository brandRepository)
	{
		_brandRepository = brandRepository;
	}

	public async Task<JsonApiResponse<GetSingleBrandQueryViewModel>> Handle(GetSingleBrandQuery request,
		CancellationToken cancellationToken)
	{
		var entry = await _brandRepository.GetByIntKey(request.Id, cancellationToken);

		return JsonApiResponse<GetSingleBrandQueryViewModel>.Success(data: new GetSingleBrandQueryViewModel(entry));
	}
}
