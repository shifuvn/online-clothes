using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Infrastructure.Repositories;

public class ProductSkuRepository : EfCoreRepositoryBase<ProductSku, string>, ISkuRepository
{
	private readonly IMapper _mapper;

	public ProductSkuRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext)
	{
		_mapper = mapper;
	}

	public async Task<ProductSku> GetSkuDetailBySkuAsync(string sku, CancellationToken cancellationToken = default)
	{
		var entry = await AsQueryable()
			.Include(item => item.Product)
			.FirstAsync(item => item.Sku == sku, cancellationToken);

		return entry;
	}
}
