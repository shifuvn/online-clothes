using System.Linq.Expressions;
using OnlineClothes.Application.Commons;
using OnlineClothes.Application.Features.ProductTypes.Queries.Single;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Domain.Paging;

namespace OnlineClothes.Application.Features.ProductTypes.Queries.Paging;

public class GetPagingProductTypeQueryHandler : IRequestHandler<GetPagingProductTypeQuery,
	JsonApiResponse<PagingModel<GetSingleProductTypeQueryViewModel>>>
{
	private readonly IProductTypeRepository _productTypeRepository;

	public GetPagingProductTypeQueryHandler(IProductTypeRepository productTypeRepository)
	{
		_productTypeRepository = productTypeRepository;
	}

	public async Task<JsonApiResponse<PagingModel<GetSingleProductTypeQueryViewModel>>> Handle(
		GetPagingProductTypeQuery request, CancellationToken cancellationToken)
	{
		var data = await _productTypeRepository.PagingAsync(
			new FilterBuilder<ProductType>().Empty(),
			new PagingRequest(request.PageIndex, request.PageSize),
			BuildProjectSelector(),
			BuildOrderSelector(request),
			null,
			cancellationToken);

		return JsonApiResponse<PagingModel<GetSingleProductTypeQueryViewModel>>.Success(data: data);
	}

	private static Func<IQueryable<ProductType>, IQueryable<GetSingleProductTypeQueryViewModel>>
		BuildProjectSelector()
	{
		return q => q.Select(item => new GetSingleProductTypeQueryViewModel(item));
	}

	private static
		Func<IQueryable<ProductType>, IOrderedQueryable<ProductType>>
		BuildOrderSelector(GetPagingProductTypeQuery request)
	{
		return QuerySortOrder.IsDescending(request.OrderBy)
			? query => query.OrderByDescending(BuildSortBySelector(request.SortBy))
			: query => query.OrderBy(BuildSortBySelector(request.SortBy));
	}

	private static Expression<Func<ProductType, object>> BuildSortBySelector(string? sortBy)
	{
		return sortBy?.ToLower() switch
		{
			"id" => product => product.Id,
			"name" => product => product.Name,
			"created" => product => product.CreatedAt,
			_ => product => product.CreatedAt
		};
	}
}
