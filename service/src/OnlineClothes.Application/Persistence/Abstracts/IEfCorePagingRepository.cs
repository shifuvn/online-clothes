using OnlineClothes.Domain.Paging;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.Entity;

namespace OnlineClothes.Application.Persistence.Abstracts;

public interface IEfCorePagingRepository<TEntity, TKey> : IEfCoreReadOnlyRepository<TEntity, TKey>
	where TEntity : class, IEntity<TKey>, new()
{
	Task<PagingModel<TProject>> PagingAsync<TProject>(
		FilterBuilder<TEntity> filterBuilder,
		PagingRequest pageRequest,
		Func<IQueryable<TEntity>, IQueryable<TProject>> selectorFunc,
		Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderFunc = null,
		IEnumerable<string>? includeProps = null,
		CancellationToken cancellationToken = default);

	Task<PagingModel<TEntity>> PagingAsync(
		FilterBuilder<TEntity> filterBuilder,
		PagingRequest pageRequest,
		Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderFunc = null,
		IEnumerable<string>? includeProps = null,
		CancellationToken cancellationToken = default);
}
