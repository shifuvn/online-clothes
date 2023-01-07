using Microsoft.EntityFrameworkCore;
using OnlineClothes.Application.Persistence.Abstracts;
using OnlineClothes.Domain.Paging;
using OnlineClothes.Persistence.Context;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.Entity;

namespace OnlineClothes.Persistence.Repository;

public class EfCorePagingRepository<TEntity, TKey> : EfCoreReadOnlyRepositoryBase<TEntity, TKey>,
	IEfCorePagingRepository<TEntity, TKey>
	where TEntity : class, IEntity<TKey>, new()
{
	public EfCorePagingRepository(AppDbContext dbContext) : base(dbContext)
	{
	}

	public virtual async Task<PagingModel<TProject>> PagingAsync<TProject>(
		FilterBuilder<TEntity> filterBuilder,
		PagingRequest pageRequest,
		Func<IQueryable<TEntity>, IQueryable<TProject>> selectorFunc,
		Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderFunc = null,
		CancellationToken cancellationToken = default)
	{
		var total = await CountAsync(filterBuilder, cancellationToken);
		var entriesQueryable = BuildPreEntryQueryable(filterBuilder, pageRequest, orderFunc, cancellationToken);

		var result = await selectorFunc(entriesQueryable).ToListAsync(cancellationToken);
		return PagingModel<TProject>.ToPages(total, result, pageRequest.PageSize);
	}

	public virtual async Task<PagingModel<TEntity>> PagingAsync(
		FilterBuilder<TEntity> filterBuilder,
		PagingRequest pageRequest,
		Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderFunc = null,
		CancellationToken cancellationToken = default)
	{
		var total = await CountAsync(filterBuilder, cancellationToken);
		var entriesQueryable = BuildPreEntryQueryable(filterBuilder, pageRequest, orderFunc, cancellationToken);

		var result = await entriesQueryable.ToListAsync(cancellationToken);
		return PagingModel<TEntity>.ToPages(total, result, pageRequest.PageSize);
	}

	private IQueryable<TEntity> BuildPreEntryQueryable(
		FilterBuilder<TEntity> filterBuilder,
		PagingRequest pageRequest,
		Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderFunc = null,
		CancellationToken cancellationToken = default)
	{
		var offset = (pageRequest.PageIndex - 1) * pageRequest.PageSize;
		var query = DbSet.Where(filterBuilder.Statement);


		if (orderFunc is not null)
		{
			query = orderFunc(query);
		}

		var entriesQueryable = query
			.Skip(offset)
			.Take(pageRequest.PageSize);

		return entriesQueryable;
	}
}
