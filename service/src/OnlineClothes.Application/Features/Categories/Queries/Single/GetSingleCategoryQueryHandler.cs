using AutoMapper;
using OnlineClothes.Application.Mapping.ViewModels;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Categories.Queries.Single;

public class
	GetSingleCategoryQueryHandler : IRequestHandler<GetSingleCategoryQuery, JsonApiResponse<CategoryViewModel>>
{
	private readonly ICategoryRepository _categoryRepository;
	private readonly IMapper _mapper;

	public GetSingleCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
	{
		_categoryRepository = categoryRepository;
		_mapper = mapper;
	}

	public async Task<JsonApiResponse<CategoryViewModel>> Handle(GetSingleCategoryQuery request,
		CancellationToken cancellationToken)
	{
		var entry = await _categoryRepository.GetByIntKey(request.Id, cancellationToken);
		return JsonApiResponse<CategoryViewModel>.Success(data: _mapper.Map<CategoryViewModel>(entry));
	}
}
