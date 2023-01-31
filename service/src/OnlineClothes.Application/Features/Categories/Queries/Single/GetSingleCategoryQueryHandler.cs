using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Categories.Queries.Single;

public class
	GetSingleCategoryQueryHandler : IRequestHandler<GetSingleCategoryQuery,
		JsonApiResponse<GetSingleCategoryQueryViewModel>>
{
	private readonly ICategoryRepository _categoryRepository;

	public GetSingleCategoryQueryHandler(ICategoryRepository categoryRepository)
	{
		_categoryRepository = categoryRepository;
	}

	public async Task<JsonApiResponse<GetSingleCategoryQueryViewModel>> Handle(GetSingleCategoryQuery request,
		CancellationToken cancellationToken)
	{
		var entry = await _categoryRepository.GetByIntKey(request.Id, cancellationToken);
		return JsonApiResponse<GetSingleCategoryQueryViewModel>.Success(
			data: new GetSingleCategoryQueryViewModel(entry));
	}
}
