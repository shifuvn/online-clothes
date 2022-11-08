using OnlineClothes.Domain.Entities;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Persistence.Context;
using OnlineClothes.Persistence.Repositories;

namespace OnlineClothes.Infrastructure.Repositories;

public class ProductRepository : RootRepositoryBase<ProductClothe, string>, IProductRepository
{
	public ProductRepository(IMongoDbContext dbContext) : base(dbContext)
	{
	}
}
