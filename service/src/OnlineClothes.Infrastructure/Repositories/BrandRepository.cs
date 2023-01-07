using Microsoft.EntityFrameworkCore;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Infrastructure.Repositories;

internal class BrandRepository : EfCoreRepositoryBase<Brand, int>, IBrandRepository
{
	public BrandRepository(AppDbContext dbContext) : base(dbContext)
	{
	}

	public async Task<Brand> GetByNameAsync(string name, CancellationToken cancellationToken = default)
	{
		var entry = await AsQueryable()
			.FirstAsync(brand => brand.Name.Equals(name), cancellationToken: cancellationToken);

		return entry;
	}

	public async Task<bool> IsNameExistedAsync(string name, CancellationToken cancellationToken = default)
	{
		return await DbSet.AnyAsync(brand => brand.Name.Equals(name), cancellationToken: cancellationToken);
	}
}
