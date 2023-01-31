using System.Linq.Expressions;
using OnlineClothes.Application.Commons;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Domain.Paging;

namespace OnlineClothes.Application.Features.Skus.Queries.Paging;

public class
	GetPagingSkuQueryHandler : IRequestHandler<GetPagingSkuQuery, JsonApiResponse<PagingModel<ProductSkuBasicDto>>>
{
	private readonly ISkuRepository _skuRepository;

	public GetPagingSkuQueryHandler(ISkuRepository skuRepository)
	{
		_skuRepository = skuRepository;
	}

	public async Task<JsonApiResponse<PagingModel<ProductSkuBasicDto>>> Handle(GetPagingSkuQuery request,
		CancellationToken cancellationToken)
	{
		var data = await _skuRepository.PagingAsync(
			BuildSearchQuery(request),
			new PagingRequest(request.PageIndex, request.PageSize),
			BuildProjectSelector(),
			BuildOrderSelector(request),
			new[] { "Product", "Image" },
			cancellationToken);

		return JsonApiResponse<PagingModel<ProductSkuBasicDto>>.Success(data: data);
	}

	private static FilterBuilder<ProductSku> BuildSearchQuery(GetPagingSkuQuery request)
	{
		var filterBuilder = new FilterBuilder<ProductSku>().Empty();
		if (!string.IsNullOrEmpty(request.SkuKeyword?.Trim()))
		{
			filterBuilder.And(sku => sku.Sku.Contains(request.SkuKeyword, StringComparison.OrdinalIgnoreCase));
		}

		return filterBuilder;
	}

	private static Func<IQueryable<ProductSku>, IQueryable<ProductSkuBasicDto>> BuildProjectSelector()
	{
		return sku => sku.Select(q => ProductSkuBasicDto.ToModel(q));
	}

	private static
		Func<IQueryable<ProductSku>, IOrderedQueryable<ProductSku>>
		BuildOrderSelector(GetPagingSkuQuery request)
	{
		return Check.ShouldOrderDescending(request.OrderBy)
			? query => query.OrderByDescending(SortByDefinition(request.SortBy))
			: query => query.OrderBy(SortByDefinition(request.SortBy));
	}

	private static Expression<Func<ProductSku, object>> SortByDefinition(string? sortBy)
	{
		return sortBy?.ToLower() switch
		{
			"price" => product => product.AddOnPrice + product.Product.Price,
			"sku" => product => product.Sku,
			"created" => product => product.CreatedAt,
			_ => product => product.CreatedAt
		};
	}

	// Include all check method handler
	private static class Check
	{
		// Default order behaviour (high => low)
		public static bool ShouldOrderDescending(string? orderBy)
		{
			return string.IsNullOrEmpty(orderBy) ||
			       orderBy.Equals(QuerySortOrder.Descending, StringComparison.OrdinalIgnoreCase);
		}
	}
}
