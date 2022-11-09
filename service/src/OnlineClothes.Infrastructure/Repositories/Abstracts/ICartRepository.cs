using OnlineClothes.Domain.Entities;
using OnlineClothes.Infrastructure.AggregateModels;
using OnlineClothes.Persistence.Repositories.Abstracts;

namespace OnlineClothes.Infrastructure.Repositories.Abstracts;

public interface ICartRepository : IRootRepository<AccountCart, string>
{
	Task<AggregateCartInfoModel> GetItems(CancellationToken cancellationToken = default);
}
