using Microsoft.EntityFrameworkCore;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.ProductTypes.Queries.All;

public class
	GetAllProductTypeQueryHandler : IRequestHandler<GetAllProductTypeQuery,
		JsonApiResponse<ICollection<ProductTypeDto>>>
{
	private readonly IProductTypeRepository _productTypeRepository;

	public GetAllProductTypeQueryHandler(IProductTypeRepository productTypeRepository)
	{
		_productTypeRepository = productTypeRepository;
	}

	public async Task<JsonApiResponse<ICollection<ProductTypeDto>>> Handle(GetAllProductTypeQuery request,
		CancellationToken cancellationToken)
	{
		var data = await _productTypeRepository.AsQueryable()
			.Select(type => new ProductTypeDto(type))
			.ToListAsync(cancellationToken);

		return JsonApiResponse<ICollection<ProductTypeDto>>.Success(data: data);
	}
}
