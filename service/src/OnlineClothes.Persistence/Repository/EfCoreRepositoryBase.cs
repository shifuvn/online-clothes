using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineClothes.Application.Persistence.Abstracts;
using OnlineClothes.BuildIn.Builders.Predicate;
using OnlineClothes.BuildIn.Entity;
using OnlineClothes.Persistence.Context;

namespace OnlineClothes.Persistence.Repository;

public abstract class EfCoreRepositoryBase<TEntity, TKey> : EfCorePagingRepository<TEntity, TKey>,
	IEfCoreRepository<TEntity, TKey>
	where TEntity : class, IEntity<TKey>, new()
{
	protected EfCoreRepositoryBase(AppDbContext dbContext) : base(dbContext)
	{
	}

	public virtual async Task<TEntity?> AddAsync(TEntity entity,
		bool notify = true,
		CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(entity);

		await DbSet.AddAsync(entity, cancellationToken);

		AddDomainEventPayload(entity, notify);

		return entity;
	}

	public virtual async Task AddBatchAsync(IList<TEntity> entities,
		bool notify = true,
		CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(entities);

		await DbSet.AddRangeAsync(entities, cancellationToken);

		if (!notify)
		{
			return;
		}

		foreach (var entity in entities)
		{
			AddDomainEventPayload(entity, notify);
		}
	}

	public virtual void Update(TEntity entity,
		bool notify = true)
	{
		ArgumentNullException.ThrowIfNull(entity);
		DbSet.Attach(entity).State = EntityState.Modified;

		AddDomainEventPayload(entity, notify);
	}

	public virtual void UpdateOneField(TEntity entity,
		Expression<Func<TEntity, object>> updateDef,
		bool notify = true)
	{
		ArgumentNullException.ThrowIfNull(entity);
		ArgumentNullException.ThrowIfNull(updateDef);

		DbSet.Attach(entity).Property(updateDef).IsModified = true;
		AddDomainEventPayload(entity, notify);
	}

	public virtual void Delete(TEntity entity,
		bool notify = true)
	{
		ArgumentNullException.ThrowIfNull(entity);

		DbSet.Attach(entity).State = EntityState.Deleted;
		AddDomainEventPayload(entity, notify);
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

	public virtual void DeleteBatch(FilterBuilder<TEntity> filterBuilder,
		bool notify = true)
	{
		var entries = DbSet.Where(filterBuilder.Statement).ToList();
		DbSet.RemoveRange(entries);

		foreach (var entity in entries)
		{
			AddDomainEventPayload(entity, notify);
		}
	}

	private static void AddDomainEventPayload(TEntity entity, bool notify)
	{
		if (notify)
		{
			entity.AddEventPayload();
		}
	}
}
