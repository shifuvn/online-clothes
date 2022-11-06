using OnlineClothes.Domain.Entities;
using OnlineClothes.Persistence.Repositories.Abstracts;

namespace OnlineClothes.Infrastructure.Repositories.Abstracts;

public interface IUserAccountRepository : IRootRepository<AccountUser, string>
{
}
