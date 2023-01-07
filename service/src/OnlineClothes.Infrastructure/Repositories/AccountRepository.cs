using Microsoft.EntityFrameworkCore;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Infrastructure.Repositories;

internal class AccountRepository : EfCoreRepositoryBase<AccountUser, int>, IAccountRepository
{
	public AccountRepository(AppDbContext dbContext) : base(dbContext)
	{
	}

	public async Task<AccountUser?> GetByEmail(string email, CancellationToken cancellationToken = default)
	{
		var entry = await DbSet.FirstOrDefaultAsync(q => q.Email.Equals(email), cancellationToken: cancellationToken);
		return entry;
	}
}
