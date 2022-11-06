using OnlineClothes.Domain.Entities;
using OnlineClothes.Persistence.Repositories.Abstracts;

namespace OnlineClothes.Infrastructure.Repositories.Abstracts;

public interface IAccountTokenCodeRepository : IRootRepository<AccountTokenCode, string>
{
}
