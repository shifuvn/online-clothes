using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineClothes.Application.Persistence.Abstracts;
using OnlineClothes.Persistence.Context;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.Entity;
using OnlineClothes.Support.Exceptions;
using OnlineClothes.Support.Utilities.Extensions;

namespace OnlineClothes.Persistence.Repository;

public abstract class EfCoreReadOnlyRepositoryBase<TEntity, TKey> : IEfCoreReadOnlyRepository<TEntity, TKey>
	where TEntity : class, IEntity<TKey>, new()
{
	protected readonly AppDbContext DbContext;
	protected readonly DbSet<TEntity> DbSet;
	private bool _disposed;

	protected EfCoreReadOnlyRepositoryBase(AppDbContext dbContext)
	{
		DbContext = dbContext;
		DbSet = dbContext.Set<TEntity>();
	}

	public DbSet<TEntity> Table => DbSet;

	public IQueryable<TEntity> AsQueryable(bool noTracking = true)
	{
		return noTracking ? DbSet.AsNoTracking() : DbSet.AsQueryable();
	}

	public virtual async Task<List<TEntity>> FindAsync(FilterBuilder<TEntity> filterBuilder,
		int offset = 0,
		int limit = 0,
		Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderFunc = null,
		CancellationToken cancellationToken = default)
	{
		var queryable = AsQueryable()
			.Where(filterBuilder.Statement);

		if (orderFunc is not null)
		{
			queryable = orderFunc(queryable);
		}

		queryable = queryable.Skip(offset);

		if (limit != 0)
		{
			queryable = queryable.Take(limit);
		}

		return await queryable.ToListAsync(cancellationToken);
	}

	public virtual async Task<bool> AnyAsync(FilterBuilder<TEntity> filterBuilder,
		CancellationToken cancellationToken = default)
	{
		return await AsQueryable()
			.AnyAsync(filterBuilder.Statement, cancellationToken: cancellationToken);
	}

	public virtual async Task<TEntity?> FindOneAsync(object?[]? keyValues,
		CancellationToken cancellationToken = default)
	{
		return await DbSet.FindAsync(keyValues, cancellationToken)
			.ConfigureAwait(false);
	}

	public virtual Task<TEntity?> FindOneAsync(FilterBuilder<TEntity> filterBuilder,
		CancellationToken cancellationToken = default)
	{
		return FindOneAsync(filterBuilder.Statement, cancellationToken);
	}

	public virtual async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate,
		CancellationToken cancellationToken = default)
	{
		return await DbSet.FirstOrDefaultAsync(predicate, cancellationToken);
	}

	public virtual Task<TEntity?> FindOneAsync(FilterBuilder<TEntity> filterBuilder,
		List<string>? includes,
		CancellationToken cancellationToken = default)
	{
		return FindOneAsync(filterBuilder, includes, true, cancellationToken);
	}

	public virtual Task<TEntity?> FindOneAsync(FilterBuilder<TEntity> filterBuilder,
		List<string>? includes,
		bool noTracking = true,
		CancellationToken cancellationToken = default)
	{
		return FindOneAsync(filterBuilder, includes, noTracking, default, cancellationToken);
	}

	public virtual Task<TEntity?> FindOneAsync(FilterBuilder<TEntity> filterBuilder,
		List<string>? includes,
		bool noTracking = true,
		bool ignoreQueryFilters = true,
		CancellationToken cancellationToken = default)
	{
		return FindOneAsync(filterBuilder, p => p, includes, noTracking, ignoreQueryFilters, cancellationToken);
	}

	public virtual async Task<TProject?> FindOneAsync<TProject>(FilterBuilder<TEntity> filterBuilder,
		Expression<Func<TEntity, TProject>> selector,
		List<string>? includes,
		bool noTracking = true,
		bool ignoreQueryFilters = true,
		CancellationToken cancellationToken = default)
	{
		return await BuildIQueryable(DbSet, includes, noTracking, ignoreQueryFilters)
			.Where(filterBuilder.Statement)
			.Select(selector)
			.FirstOrDefaultAsync(cancellationToken);
	}

	public virtual async Task<TEntity> GetOneAsync(object?[]? keyValues, CancellationToken cancellationToken = default)
	{
		var entry = await FindOneAsync(keyValues, cancellationToken);
		DataNotFoundException.ThrowIfNull(entry);

		return entry;
	}

	public virtual async Task<TEntity> GetOneAsync(FilterBuilder<TEntity> filterBuilder,
		CancellationToken cancellationToken = default)
	{
		var entry = await FindOneAsync(filterBuilder, cancellationToken);
		DataNotFoundException.ThrowIfNull(entry);

		return entry;
	}

	public virtual async Task<TEntity> GetOneAsync(FilterBuilder<TEntity> filterBuilder,
		List<string>? includes,
		CancellationToken cancellationToken = default)
	{
		var entry = await FindOneAsync(filterBuilder, includes, cancellationToken);
		DataNotFoundException.ThrowIfNull(entry);

		return entry;
	}

	public virtual async Task<TProject> GetOneAsync<TProject>(FilterBuilder<TEntity> filterBuilder,
		Expression<Func<TEntity, TProject>> selector,
		List<string>? includes,
		CancellationToken cancellationToken = default)
	{
		var entry = await FindOneAsync(filterBuilder, selector, includes, default, default, cancellationToken);
		DataNotFoundException.ThrowIfNull(entry);

		return entry;
	}

	public virtual async Task<long> CountAsync(CancellationToken cancellationToken = default)
	{
		return await DbSet.LongCountAsync(cancellationToken);
	}

	public virtual async Task<long> CountAsync(FilterBuilder<TEntity> filterBuilder,
		CancellationToken cancellationToken = default)
	{
		return await DbSet.LongCountAsync(filterBuilder.Statement, cancellationToken);
	}

	public virtual async Task<TEntity> GetByIntKey(int key, CancellationToken cancellationToken = default)
	{
		var entry = await FindOneAsync(new object[] { key }, cancellationToken);
		DataNotFoundException.ThrowIfNull(entry);

		return entry;
	}

	public virtual async Task<TEntity> GetByStrKey(string key, CancellationToken cancellationToken = default)
	{
		var entry = await FindOneAsync(new object[] { key }, cancellationToken);
		DataNotFoundException.ThrowIfNull(entry);

		return entry;
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing)
			{
				DbContext.Dispose();
			}
		}

		_disposed = true;
	}

	private static IQueryable<TEntity> BuildIQueryable(
		IQueryable<TEntity> queryable,
		List<string>? includes,
		bool noTracking = true,
		bool ignoreFilter = true
	)
	{
		// tracking
		if (noTracking)
		{
			queryable = queryable.AsNoTracking();
		}

		// include
		if (includes is not null && !includes.IsEmpty())
		{
			foreach (var include in includes)
			{
				queryable = queryable.Include(include);
			}
		}

		// ignoreFilter
		if (ignoreFilter)
		{
			queryable = queryable.IgnoreQueryFilters();
		}

		return queryable;
	}
}
