using Microsoft.EntityFrameworkCore;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Infrastructure.Repositories;

internal class AccountRepository : EfCoreRepositoryBase<AccountUser, int>, IAccountRepository
{
	public AccountRepository(AppDbContext dbContext) : base(dbContext)
	{
	}

	public override async Task<AccountUser> GetByIntKey(int key, CancellationToken cancellationToken = default)
	{
		var entry = await AsQueryable()
			.Include(q => q.AvatarImage)
			.FirstAsync(q => q.Id == key, cancellationToken: cancellationToken);
		return entry;
	}

	public async Task<AccountUser?> GetByEmail(string email, CancellationToken cancellationToken = default)
	{
		var entry = await DbSet.FirstOrDefaultAsync(q => q.Email.Equals(email), cancellationToken: cancellationToken);
		return entry;
	}
}
