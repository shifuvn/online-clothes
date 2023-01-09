using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using OnlineClothes.Application.Persistence.Abstracts;
using OnlineClothes.Persistence.Context;

namespace OnlineClothes.Persistence.Uow;

public class UnitOfWork : IUnitOfWork
{
	private readonly AppDbContext _dbContext;
	private readonly ILogger<UnitOfWork> _logger;
	private bool _disposed;
	private IDbContextTransaction _transaction = null!;

	public UnitOfWork(ILogger<UnitOfWork> logger, AppDbContext dbContext)
	{
		_logger = logger;
		_dbContext = dbContext;
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	public void BeginTransaction()
	{
		_transaction = _dbContext.Database.BeginTransaction();
	}

	public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
	{
		_transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
		return _transaction;
	}

	public bool SaveChanges()
	{
		try
		{
			_dbContext.ChangeTracker.AutoDetectChangesEnabled = true;
			return _dbContext.SaveChanges() > 0;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "{message}", ex.Message);
			Rollback();
			return false;
		}
	}

	public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			_dbContext.ChangeTracker.AutoDetectChangesEnabled = true;
			return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "{message}", ex.Message);
			await RollbackAsync(cancellationToken);
			return false;
		}
	}

	public void Commit()
	{
		_transaction.Commit();
	}

	public async Task CommitAsync(CancellationToken cancellationToken = default)
	{
		await _transaction.CommitAsync(cancellationToken);
	}

	public void Rollback()
	{
		_transaction.Rollback();
		_transaction.Dispose();
	}

	public async Task RollbackAsync(CancellationToken cancellationToken = default)
	{
		await _transaction.RollbackAsync(cancellationToken);
		await _transaction.DisposeAsync();
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing)
			{
				_dbContext.Dispose();
			}
		}

		_disposed = true;
	}
}
