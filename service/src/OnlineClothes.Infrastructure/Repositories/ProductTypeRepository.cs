using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Infrastructure.Repositories;

public class ProductTypeRepository : EfCoreRepositoryBase<ProductType, int>, IProductTypeRepository
{
	public ProductTypeRepository(AppDbContext dbContext) : base(dbContext)
	{
	}
}
