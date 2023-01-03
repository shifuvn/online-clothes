using OnlineClothes.Domain.Entities.Aggregate;
using OnlineClothes.Persistence.Repositories.Abstracts;

namespace OnlineClothes.Infrastructure.Repositories.Abstracts;

public interface IAccountTokenCodeRepository : IRootRepository<AccountTokenCode, int>
{
}
