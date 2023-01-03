using OnlineClothes.Domain.Entities.Aggregate;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Persistence.Context;
using OnlineClothes.Persistence.Repositories;

namespace OnlineClothes.Infrastructure.Repositories;

public sealed class AccountRepository : RootRepositoryBase<AccountUser, int>,
	IAccountRepository
{
	public AccountRepository(IMongoDbContext dbContext) : base(dbContext)
	{
	}
}
