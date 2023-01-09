using System.Linq.Expressions;
using OnlineClothes.Application.Commons;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Domain.Paging;
using OnlineClothes.Support.Builders.Predicate;

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
		var viewModel = await _productRepository.PagingAsync(
			PreSearchQueryable(request),
			new PagingRequest(request.PageIndex, request.PageSize),
			ProjectToTypeSelector(),
			PreOrderQueryable(request),
			new[] { "ProductSkus" },
			cancellationToken);

		return JsonApiResponse<PagingModel<ProductBasicDto>>.Success(data: viewModel);
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
		PreOrderQueryable(GetPagingProductQuery request)
	{
		return Check.ShouldOrderDescending(request.OrderBy)
			? query => query.OrderByDescending(SortByDefinition(request.SortBy))
			: query => query.OrderBy(SortByDefinition(request.SortBy));
	}

	private static Expression<Func<Product, object>> SortByDefinition(string? sortBy)
	{
		return sortBy?.ToLower() switch
		{
			"price" => product => product.Price,
			"name" => product => product.Name,
			_ => product => product.Name
		};
	}

	// Include all check method handler
	private static class Check
	{
		// Default order behaviour (high => low)
		public static bool ShouldOrderDescending(string? orderBy)
		{
			return string.IsNullOrEmpty(orderBy) || orderBy.Equals(QuerySortOrder.Descending);
		}
	}
}
