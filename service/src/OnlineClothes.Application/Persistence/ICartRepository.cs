namespace OnlineClothes.Application.Persistence;

public interface ICartRepository : IEfCoreRepository<AccountCart, int>
{
	Task<AccountCart> GetCurrentCart();
}
