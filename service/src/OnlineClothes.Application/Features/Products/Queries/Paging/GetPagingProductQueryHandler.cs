using System.Linq.Expressions;
using OnlineClothes.Application.Commons;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Domain.Paging;

namespace OnlineClothes.Application.Features.Products.Queries.Paging;

public class
	GetPagingProductQueryHandler : IRequestHandler<GetPagingProductQuery,
		JsonApiResponse<PagingModel<ProductBasicDto>>>
{
	private readonly IProductRepository _productRepository;

	public GetPagingProductQueryHandler(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	public async Task<JsonApiResponse<PagingModel<ProductBasicDto>>> Handle(GetPagingProductQuery request,
		CancellationToken cancellationToken)
	{
		var data = await _productRepository.PagingAsync(
			PreSearchQueryable(request),
			new PagingRequest(request.PageIndex, request.PageSize),
			ProjectToTypeSelector(),
			BuildOrderSelector(request),
			new[] { "ProductSkus", "ThumbnailImage" },
			cancellationToken);

		return JsonApiResponse<PagingModel<ProductBasicDto>>.Success(data: data);
	}

	private static FilterBuilder<Product> PreSearchQueryable(GetPagingProductQuery request)
	{
		// default will query publish product
		var filterBuilder = new FilterBuilder<Product>(q => q.IsPublish);

		AppendFilterKeyword(request, filterBuilder);
		AppendFilterCategory(request, filterBuilder);
		AppendFilterBrand(request, filterBuilder);

		return filterBuilder;
	}

	private static void AppendFilterCategory(GetPagingProductQuery request,
		FilterBuilder<Product> filterBuilder)
	{
		if (request.CategoryId is not null && request.CategoryId != 0)
		{
			filterBuilder.And(product => product.ProductCategories.Any(pc => pc.CategoryId == request.CategoryId));
		}
	}

	private static void AppendFilterKeyword(GetPagingProductQuery request,
		FilterBuilder<Product> filterBuilder)
	{
		if (!string.IsNullOrEmpty(request.Keyword))
		{
			filterBuilder.And(product => product.Name.ToLower().Contains(request.Keyword.ToLower()));
		}
	}

	private static void AppendFilterBrand(GetPagingProductQuery request,
		FilterBuilder<Product> filterBuilder)
	{
		if (request.BrandId is not null && request.BrandId != 0)
		{
			filterBuilder.And(product => product.BrandId == request.BrandId);
		}
	}

	private static Func<IQueryable<Product>, IQueryable<ProductBasicDto>>
		ProjectToTypeSelector()
	{
		return product => product.Select(q => ProductBasicDto.ToModel(q));
	}

	private static
		Func<IQueryable<Product>, IOrderedQueryable<Product>>
		BuildOrderSelector(GetPagingProductQuery request)
	{
		return QuerySortOrder.IsDescending(request.OrderBy)
			? query => query.OrderByDescending(SortByDefinition(request.SortBy))
			: query => query.OrderBy(SortByDefinition(request.SortBy));
	}

	private static Expression<Func<Product, object>> SortByDefinition(string? sortBy)
	{
		return sortBy?.ToLower() switch
		{
			"price" => product => product.Price,
			"name" => product => product.Name,
			"id" => product => product.Id,
			"created" => product => product.CreatedAt,
			_ => product => product.Name
		};
	}
}
