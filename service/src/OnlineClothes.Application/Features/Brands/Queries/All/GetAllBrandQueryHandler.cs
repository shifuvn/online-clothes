using Microsoft.EntityFrameworkCore;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Brands.Queries.All;

public class GetAllBrandQueryHandler : IRequestHandler<GetAllBrandQuery, JsonApiResponse<ICollection<BrandDto>>>
{
	private readonly IBrandRepository _brandRepository;

	public GetAllBrandQueryHandler(IBrandRepository brandRepository)
	{
		_brandRepository = brandRepository;
	}

	public async Task<JsonApiResponse<ICollection<BrandDto>>> Handle(GetAllBrandQuery request,
		CancellationToken cancellationToken)
	{
		var data = await _brandRepository.AsQueryable()
			.Select(brand => new BrandDto(brand))
			.ToListAsync(cancellationToken);

		return JsonApiResponse<ICollection<BrandDto>>.Success(data: data);
	}
}
