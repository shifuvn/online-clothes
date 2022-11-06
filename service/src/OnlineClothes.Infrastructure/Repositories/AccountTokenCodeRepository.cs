using OnlineClothes.Domain.Entities;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Persistence.Context;
using OnlineClothes.Persistence.Repositories;

namespace OnlineClothes.Infrastructure.Repositories;

public class AccountTokenCodeRepository : RootRepositoryBase<AccountTokenCode, string>, IAccountTokenCodeRepository
{
	public AccountTokenCodeRepository(IMongoDbContext dbContext) : base(dbContext)
	{
	}
}
