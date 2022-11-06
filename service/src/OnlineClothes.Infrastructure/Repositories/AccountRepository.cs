using OnlineClothes.Domain.Entities;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Persistence.Context;
using OnlineClothes.Persistence.Repositories;

namespace OnlineClothes.Infrastructure.Repositories;

public sealed class AccountRepository : RootRepositoryBase<AccountUser, string>,
	IAccountRepository
{
	public AccountRepository(IMongoDbContext dbContext) : base(dbContext)
	{
	}
}
