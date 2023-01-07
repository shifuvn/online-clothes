using OnlineClothes.Support.Entity;

namespace OnlineClothes.Application.Persistence.Abstracts;

/// <summary>
/// Wrap all implement of repository
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IEfCoreRepository<TEntity, TKey> : IEfCoreWriteRepository<TEntity, TKey>,
	IEfCorePagingRepository<TEntity, TKey>
	where TEntity : class, IEntity<TKey>, new()
{
}
