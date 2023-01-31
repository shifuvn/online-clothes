using System.Collections.Concurrent;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OnlineClothes.Application.Persistence.Abstracts;
using OnlineClothes.BuildIn.Entity.Event;
using OnlineClothes.Persistence.Context;
using OnlineClothes.Persistence.Internal.Extensions;

namespace OnlineClothes.Persistence.Uow;

public class UnitOfWork : IUnitOfWork
{
	private readonly AppDbContext _dbContext;
	private readonly ConcurrentQueue<object> _delayQueue = new();
	private readonly ILogger<UnitOfWork> _logger;
	private readonly IServiceScopeFactory _serviceScopeFactory;

	private bool _disposed;

	private IDbContextTransaction? _transaction;

	public UnitOfWork(
		ILogger<UnitOfWork> logger,
		AppDbContext dbContext,
		IServiceScopeFactory serviceScopeFactory)
	{
		_logger = logger;
		_dbContext = dbContext;
		_serviceScopeFactory = serviceScopeFactory;
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
			PushDomainEventsToQueueOnSave();

			var saveChanges = _dbContext.SaveChanges() > 0;
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
			PushDomainEventsToQueueOnSave();

			var saveChanges = await _dbContext.SaveChangesAsync(cancellationToken) > 0;
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
		PushDomainEventsBackgroundQueue();
	}

	public async Task CommitAsync(CancellationToken cancellationToken = default)
	{
		await _transaction?.CommitAsync(cancellationToken)!;
		PushDomainEventsBackgroundQueue();
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

	private void PushDomainEventsToQueueOnSave()
	{
		if (_transaction is not null)
		{
			// push to delay queue, wait for commit success
			PushToDelayDomainEventQueue();
			return;
		}

		PushDomainEventsBackgroundQueue();
	}

	private void PushToDelayDomainEventQueue()
	{
		var entityEntries = _dbContext.ChangeTracker.Entries<ISupportDomainEvent>();

		foreach (var entityEntry in entityEntries)
		{
			var eventPayloads = entityEntry.Entity.EventPayloads;
			CreateDomainEventsFromPayload(eventPayloads, entityEntry);
		}
	}

	/// <summary>
	/// Add entity payload to domain event
	/// </summary>
	/// <param name="eventPayloads"></param>
	/// <param name="entityEntry"></param>
	private void CreateDomainEventsFromPayload(
		IEnumerable<DomainEventPayload> eventPayloads,
		EntityEntry<ISupportDomainEvent> entityEntry)
	{
		if (entityEntry.State == EntityState.Unchanged)
		{
			return; // skip
		}

		foreach (var domainEventPayload in eventPayloads)
		{
			var openType = typeof(DomainEvent<>);
			Type[] tArgs = { entityEntry.Entity.GetType() };
			var target = openType.MakeGenericType(tArgs);

			var domainEvent = Activator.CreateInstance(target,
				domainEventPayload.Key,
				entityEntry.State.GetDomainEventAction(),
				domainEventPayload.Value);
			_delayQueue.Enqueue(domainEvent!);
		}
	}

	private void PushDomainEventsBackgroundQueue()
	{
		while (!_delayQueue.IsEmpty && _delayQueue.TryDequeue(out var @event))
		{
			Task.Run(async () => await PublishNotifyEvent((IDomainEvent)@event))
				.Wait();
		}
	}

	private async Task PublishNotifyEvent(IDomainEvent @event)
	{
		using var scope = _serviceScopeFactory.CreateScope();
		var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

		await mediator.Publish(@event);

		var detailEvent =
			$"{{eventId: {@event.Id}, eventName: {@event.EventName}, eventAction: {@event.EventActionType}, createdAt: {@event.CreatedAt}}}";
		_logger.LogInformation("[EVENTS] Process event -- at {now}, detail: {detail}",
			DateTime.UtcNow.ToString("R"),
			detailEvent);
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
