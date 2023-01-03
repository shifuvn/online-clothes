using OnlineClothes.Domain.Entities.Aggregate;
using OnlineClothes.Persistence.Repositories.Abstracts;

namespace OnlineClothes.Infrastructure.Repositories.Abstracts;

public interface IAccountRepository : IRootRepository<AccountUser, int>
{
}
