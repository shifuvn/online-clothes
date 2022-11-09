using OnlineClothes.Domain.Entities;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Persistence.Context;
using OnlineClothes.Persistence.Repositories;

namespace OnlineClothes.Infrastructure.Repositories;

public class CartRepository : RootRepositoryBase<AccountCart, string>, ICartRepository
{
	public CartRepository(IMongoDbContext dbContext) : base(dbContext)
	{
	}
}
