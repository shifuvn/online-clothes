using Microsoft.EntityFrameworkCore.Storage;

namespace OnlineClothes.Application.Persistence.Abstracts;

public interface IUnitOfWork : IDisposable
{
	void BeginTransaction();
	Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
	bool SaveChanges();
	Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
	void Commit();
	Task CommitAsync(CancellationToken cancellationToken = default);
	void Rollback();
	Task RollbackAsync(CancellationToken cancellationToken = default);
}
