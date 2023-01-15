using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using OnlineClothes.Application.Persistence.Abstracts;
using OnlineClothes.Persistence.Context;

namespace OnlineClothes.Persistence.Uow;

public class UnitOfWork : IUnitOfWork
{
	private readonly AppDbContext _dbContext;
	private readonly ILogger<UnitOfWork> _logger;
	private readonly IMediator _mediator;

	private bool _disposed;
	private IDbContextTransaction? _transaction;

	public UnitOfWork(ILogger<UnitOfWork> logger, AppDbContext dbContext, IMediator mediator)
	{
		_logger = logger;
		_dbContext = dbContext;
		_mediator = mediator;
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
			var saveChanges = _dbContext.SaveChanges() > 0;

			PublishNotifyEventOnSave();

			return saveChanges;
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
			var saveChanges = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

			await PublishNotifyEventOnSaveAsync();

			return saveChanges;
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
		_transaction?.Commit();
		PublishNotifyEvent();
	}

	public async Task CommitAsync(CancellationToken cancellationToken = default)
	{
		await _transaction?.CommitAsync(cancellationToken)!;
		await PublishNotifyEventAsync();
	}

	public void Rollback()
	{
		_transaction?.Rollback();
		_transaction?.Dispose();
	}

	public async Task RollbackAsync(CancellationToken cancellationToken = default)
	{
		await _transaction?.RollbackAsync(cancellationToken)!;
		await _transaction.DisposeAsync();
	}

	private void PublishNotifyEventOnSave()
	{
		if (_transaction is not null)
		{
			return;
		}

		PublishNotifyEvent();
	}

	private async Task PublishNotifyEventOnSaveAsync()
	{
		if (_transaction is not null)
		{
			return;
		}

		await PublishNotifyEventAsync();
	}

	private void PublishNotifyEvent()
	{
		foreach (var domainEvent in _dbContext.DomainEvents)
		{
			Task.Run(async () => await _mediator.Publish(domainEvent));
		}

		// Remove all notify events
		_dbContext.DomainEvents.Clear();
	}

	private async Task PublishNotifyEventAsync()
	{
		foreach (var domainEvent in _dbContext.DomainEvents)
		{
			await _mediator.Publish(domainEvent);
		}

		// Remove all notify events
		_dbContext.DomainEvents.Clear();
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
