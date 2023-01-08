namespace OnlineClothes.Application.Persistence;

public interface IOrderRepository : IEfCoreRepository<Order, int>
{
	Task<Order> GetOneByUserContext(int orderId, CancellationToken cancellationToken = default);
}
