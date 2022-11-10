using OnlineClothes.Domain.Entities;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Persistence.Context;
using OnlineClothes.Persistence.Repositories;

namespace OnlineClothes.Infrastructure.Repositories;

public class OrderRepository : RootRepositoryBase<OrderProduct, string>, IOrderRepository
{
	public OrderRepository(IMongoDbContext dbContext) : base(dbContext)
	{
	}
}
