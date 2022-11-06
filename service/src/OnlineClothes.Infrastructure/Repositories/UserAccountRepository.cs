using OnlineClothes.Domain.Entities;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Persistence.Context;
using OnlineClothes.Persistence.Repositories;

namespace OnlineClothes.Infrastructure.Repositories;

public sealed class UserAccountRepository : RootRepositoryBase<AccountUser, string>,
	IUserAccountRepository
{
	public UserAccountRepository(IMongoDbContext dbContext) : base(dbContext)
	{
	}
}
