using OnlineClothes.Domain.Entities.Aggregate;
using OnlineClothes.Infrastructure.AggregateModels;
using OnlineClothes.Persistence.Repositories.Abstracts;

namespace OnlineClothes.Infrastructure.Repositories.Abstracts;

public interface ICartRepository : IRootRepository<AccountCart, int>
{
	Task<AggregateCartInfoModel?> GetItems(CancellationToken cancellationToken = default);
}
