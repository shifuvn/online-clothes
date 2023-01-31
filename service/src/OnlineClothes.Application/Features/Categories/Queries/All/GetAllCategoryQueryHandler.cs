using Microsoft.EntityFrameworkCore;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Categories.Queries.All;

public class
	GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, JsonApiResponse<ICollection<CategoryDto>>>
{
	private readonly ICategoryRepository _categoryRepository;

	public GetAllCategoryQueryHandler(ICategoryRepository categoryRepository)
	{
		_categoryRepository = categoryRepository;
	}

	public async Task<JsonApiResponse<ICollection<CategoryDto>>> Handle(GetAllCategoryQuery request,
		CancellationToken cancellationToken)
	{
		var data = await _categoryRepository.AsQueryable()
			.Select(category => new CategoryDto(category))
			.ToListAsync(cancellationToken);

		return JsonApiResponse<ICollection<CategoryDto>>.Success(data: data);
	}
}
