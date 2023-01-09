using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineClothes.Application.Persistence.Abstracts;
using OnlineClothes.Persistence.Context;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.Entity;

namespace OnlineClothes.Persistence.Repository;

public abstract class EfCoreRepositoryBase<TEntity, TKey> : EfCorePagingRepository<TEntity, TKey>,
	IEfCoreRepository<TEntity, TKey>
	where TEntity : class, IEntity<TKey>, new()
{
	protected EfCoreRepositoryBase(AppDbContext dbContext) : base(dbContext)
	{
	}

	public virtual async Task<TEntity?> FindAndDelete(FilterBuilder<TEntity> filterBuilder,
		bool notify = true,
		CancellationToken cancellationToken = default)
	{
		var entry = await FindOneAsync(filterBuilder, cancellationToken);
		if (entry is null)
		{
			return null;
		}

		Delete(entry, notify);
		return entry;
	}

	public virtual async Task<TEntity?> AddAsync(TEntity entity,
		bool notify = true,
		CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(entity);

		await DbSet.AddAsync(entity, cancellationToken);

		if (notify)
		{
			//await _mediator.Publish(DomainEvent<TEntity>.Create(DomainEventAction.Created, entity), cancellationToken);
		}

		return entity;
	}

	public virtual async Task<bool> AddBatchAsync(IList<TEntity> entities,
		bool notify = true,
		CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(entities);

		await DbSet.AddRangeAsync(entities, cancellationToken);

		if (notify)
		{
			//await _mediator.Publish(DomainEvent<TEntity>.Create($"Batch{typeof(TEntity)}", DomainEventAction.Created,
			//	entities), cancellationToken);
		}

		return true;
	}

	public virtual void Update(TEntity entity,
		bool notify = true)
	{
		ArgumentNullException.ThrowIfNull(entity);
		DbSet.Attach(entity).State = EntityState.Modified;

		if (notify)
		{
			//await _mediator.Publish(DomainEvent<TEntity>.Create(DomainEventAction.Updated, entity), cancellationToken);
		}
	}

	public virtual void UpdateOneField(TEntity entity,
		Expression<Func<TEntity, object>> updateDef,
		bool notify = true)
	{
		ArgumentNullException.ThrowIfNull(entity);
		ArgumentNullException.ThrowIfNull(updateDef);

		DbSet.Attach(entity).Property(updateDef).IsModified = true;

		if (notify)
		{
			//await _mediator.Publish(DomainEvent<TEntity>.Create(DomainEventAction.Updated, entity), cancellationToken);
		}
	}

	public virtual void Delete(TEntity entity,
		bool notify = true)
	{
		ArgumentNullException.ThrowIfNull(entity);

		DbSet.Attach(entity).State = EntityState.Deleted;

		if (notify)
		{
			//await _mediator.Publish(DomainEvent<TEntity>.Create(DomainEventAction.Deleted, entity), cancellationToken);
		}
	}

	public virtual void DeleteBatch(FilterBuilder<TEntity> filterBuilder,
		bool notify = true)
	{
		DbSet.RemoveRange(DbSet.Where(filterBuilder.Statement).ToList());

		if (notify)
		{
			//await _mediator.Publish(
			//	DomainEvent<TEntity>.Create($"Batch{nameof(TEntity)}", DomainEventAction.Deleted),
			//	cancellationToken);
		}
	}
}
