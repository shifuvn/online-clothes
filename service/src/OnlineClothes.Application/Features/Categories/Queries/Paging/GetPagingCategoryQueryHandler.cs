using AutoMapper;
using OnlineClothes.Application.Mapping.ViewModels;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Domain.Paging;
using OnlineClothes.Support.Builders.Predicate;

namespace OnlineClothes.Application.Features.Categories.Queries.Paging;

public class GetPagingCategoryQueryHandler : IRequestHandler<GetPagingCategoryQuery,
	JsonApiResponse<PagingModel<CategoryViewModel>>>
{
	private readonly ICategoryRepository _categoryRepository;
	private readonly IMapper _mapper;

	public GetPagingCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
	{
		_categoryRepository = categoryRepository;
		_mapper = mapper;
	}

	public async Task<JsonApiResponse<PagingModel<CategoryViewModel>>> Handle(
		GetPagingCategoryQuery request, CancellationToken cancellationToken)
	{
		var paging = await _categoryRepository.PagingAsync(
			FilterBuilder<Category>.True(),
			new PagingRequest(request.PageIndex, request.PageSize),
			SelectorFunc(),
			DefaultOrderByFunc(),
			null,
			cancellationToken);

		return JsonApiResponse<PagingModel<CategoryViewModel>>.Success(data: paging);
	}

	private Func<IQueryable<Category>, IQueryable<CategoryViewModel>>
		SelectorFunc()
	{
		return q => q.Select(item => _mapper.Map<CategoryViewModel>(item));
	}

	private static
		Func<IQueryable<Category>, IOrderedQueryable<Category>>
		DefaultOrderByFunc()
	{
		return category => category.OrderBy(q => q.Id);
	}
}
