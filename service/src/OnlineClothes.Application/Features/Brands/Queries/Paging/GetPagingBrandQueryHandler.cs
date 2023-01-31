using OnlineClothes.Application.Features.Brands.Queries.Single;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Domain.Paging;

namespace OnlineClothes.Application.Features.Brands.Queries.Paging;

public class GetPagingBrandQueryHandler : IRequestHandler<GetPagingBrandQuery,
	JsonApiResponse<PagingModel<GetSingleBrandQueryViewModel>>>
{
	private readonly IBrandRepository _brandRepository;

	public GetPagingBrandQueryHandler(IBrandRepository brandRepository)
	{
		_brandRepository = brandRepository;
	}

	public async Task<JsonApiResponse<PagingModel<GetSingleBrandQueryViewModel>>> Handle(GetPagingBrandQuery request,
		CancellationToken cancellationToken)
	{
		var pagingModel = await _brandRepository.PagingAsync(
			FilterBuilder<Brand>.True(),
			new PagingRequest(request),
			BuildProjectSelector(),
			BuildOrderSelector(),
			null,
			cancellationToken);

		return JsonApiResponse<PagingModel<GetSingleBrandQueryViewModel>>.Success(data: pagingModel);
	}

	private static Func<IQueryable<Brand>, IQueryable<GetSingleBrandQueryViewModel>>
		BuildProjectSelector()
	{
		return q => q.Select(item => new GetSingleBrandQueryViewModel(item));
	}

	private static Func<IQueryable<Brand>, IOrderedQueryable<Brand>>
		BuildOrderSelector()
	{
		return brands => brands.OrderBy(q => q.Id);
	}
}
