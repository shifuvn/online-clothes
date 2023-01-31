using System.Linq.Expressions;
using AutoMapper;
using OnlineClothes.Application.Commons;
using OnlineClothes.Application.Features.Categories.Queries.Single;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Domain.Paging;

namespace OnlineClothes.Application.Features.Categories.Queries.Paging;

public class GetPagingCategoryQueryHandler : IRequestHandler<GetPagingCategoryQuery,
	JsonApiResponse<PagingModel<GetSingleCategoryQueryViewModel>>>
{
	private readonly ICategoryRepository _categoryRepository;
	private readonly IMapper _mapper;

	public GetPagingCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
	{
		_categoryRepository = categoryRepository;
		_mapper = mapper;
	}

	public async Task<JsonApiResponse<PagingModel<GetSingleCategoryQueryViewModel>>> Handle(
		GetPagingCategoryQuery request, CancellationToken cancellationToken)
	{
		var paging = await _categoryRepository.PagingAsync(
			FilterBuilder<Category>.True(),
			new PagingRequest(request.PageIndex, request.PageSize),
			BuildProjectSelector(),
			BuildOrderSelector(request),
			null,
			cancellationToken);

		return JsonApiResponse<PagingModel<GetSingleCategoryQueryViewModel>>.Success(data: paging);
	}

	private static Func<IQueryable<Category>, IQueryable<GetSingleCategoryQueryViewModel>>
		BuildProjectSelector()
	{
		return q => q.Select(item => new GetSingleCategoryQueryViewModel(item));
	}

	private static
		Func<IQueryable<Category>, IOrderedQueryable<Category>>
		BuildOrderSelector(GetPagingCategoryQuery request)
	{
		return QuerySortOrder.IsDescending(request.OrderBy)
			? query => query.OrderByDescending(SortByDefinition(request.SortBy))
			: query => query.OrderBy(SortByDefinition(request.SortBy));
	}

	private static Expression<Func<Category, object>> SortByDefinition(string? sortBy)
	{
		return sortBy?.ToLower() switch
		{
			"id" => product => product.Id,
			"name" => product => product.Name,
			"created" => product => product.CreatedAt,
			_ => product => product.Name
		};
	}
}
