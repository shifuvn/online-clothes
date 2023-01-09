using Microsoft.EntityFrameworkCore;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Support.Builders.Predicate;

namespace OnlineClothes.Infrastructure.Repositories;

public class ProductSkuRepository : EfCoreRepositoryBase<ProductSku, string>, ISkuRepository
{
	public ProductSkuRepository(AppDbContext dbContext) : base(dbContext)
	{
	}

	public async Task<ProductSku> GetSkuDetailBySkuAsync(string sku, CancellationToken cancellationToken = default)
	{
		var entry = await AsQueryable()
			.Include(item => item.Product)
			.FirstAsync(item => item.Sku == sku, cancellationToken);

		return entry;
	}

	public override async Task<List<ProductSku>> FindAsync(FilterBuilder<ProductSku> filterBuilder,
		int offset = 0,
		int limit = 0,
		Func<IQueryable<ProductSku>, IOrderedQueryable<ProductSku>>? orderFunc = null,
		CancellationToken cancellationToken = default)
	{
		var queryable = AsQueryable()
			.Include(sku => sku.Product)
			.Where(filterBuilder.Statement);

		if (orderFunc is not null)
		{
			queryable = orderFunc(queryable);
		}

		queryable = queryable.Skip(offset);

		if (limit != 0)
		{
			queryable = queryable.Take(limit);
		}

		return await queryable.ToListAsync(cancellationToken);
	}
}
