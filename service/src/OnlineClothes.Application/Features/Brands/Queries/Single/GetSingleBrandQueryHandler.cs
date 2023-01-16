using AutoMapper;
using OnlineClothes.Application.Mapping.ViewModels;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Brands.Queries.Single;

public class
	GetSingleBrandQueryHandler : IRequestHandler<GetSingleBrandQuery, JsonApiResponse<BrandViewModel>>
{
	private readonly IBrandRepository _brandRepository;
	private readonly IMapper _mapper;

	public GetSingleBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
	{
		_brandRepository = brandRepository;
		_mapper = mapper;
	}

	public async Task<JsonApiResponse<BrandViewModel>> Handle(GetSingleBrandQuery request,
		CancellationToken cancellationToken)
	{
		var entry = await _brandRepository.GetByIntKey(request.Id, cancellationToken);

		return JsonApiResponse<BrandViewModel>.Success(data: _mapper.Map<BrandViewModel>(entry));
	}
}
