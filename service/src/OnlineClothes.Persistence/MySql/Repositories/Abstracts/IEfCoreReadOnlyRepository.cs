using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.Entity;

namespace OnlineClothes.Persistence.MySql.Repositories.Abstracts;

public interface IEfCoreReadOnlyRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>, new()
{
	#region FindOne

	Task<TEntity?> FindOneAsync(object?[]? keyValues, CancellationToken cancellationToken = default);

	Task<TEntity?> FindOneAsync(FilterBuilder<TEntity> filterBuilder,
		CancellationToken cancellationToken = default);

	Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate,
		CancellationToken cancellationToken = default);

	Task<TEntity?> FindOneAsync(
		FilterBuilder<TEntity> filterBuilder,
		Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include,
		CancellationToken cancellationToken = default);

	Task<TEntity?> FindOneAsync(
		FilterBuilder<TEntity> filterBuilder,
		Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include,
		bool asTracking,
		CancellationToken cancellationToken = default);

	Task<TEntity?> FindOneAsync(
		FilterBuilder<TEntity> filterBuilder,
		Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include,
		bool asTracking,
		bool ignoreQueryFilters = true,
		CancellationToken cancellationToken = default);

	Task<TProject?> FindOneAsync<TProject>(
		FilterBuilder<TEntity> filterBuilder,
		Expression<Func<TEntity, TProject>> selector,
		Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include,
		bool asTracking,
		bool ignoreQueryFilters = true,
		CancellationToken cancellationToken = default);

	#endregion

	#region Count

	Task<long> CountAsync(CancellationToken cancellationToken = default);

	Task<long> CountAsync(FilterBuilder<TEntity> filterBuilder, CancellationToken cancellationToken = default);

	#endregion
}
