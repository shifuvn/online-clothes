using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Infrastructure.Repositories;

internal class CategoryRepository : EfCoreRepositoryBase<Category, int>, ICategoryRepository
{
	public CategoryRepository(AppDbContext dbContext) : base(dbContext)
	{
	}
}
