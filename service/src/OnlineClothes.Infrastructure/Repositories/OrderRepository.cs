using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Infrastructure.Repositories;

public class OrderRepository : EfCoreRepositoryBase<Order, int>, IOrderRepository
{
	public OrderRepository(AppDbContext dbContext) : base(dbContext)
	{
	}
}
