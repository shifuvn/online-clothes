using OnlineClothes.Support.Entity;

namespace OnlineClothes.Persistence.MySql.Repositories.Abstracts;

/// <summary>
/// Wrap all implement of repository
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IEfCoreRepository<TEntity, TKey> : IEfCoreWriteRepository<TEntity, TKey>,
	IEfCoreReadOnlyRepository<TEntity, TKey>
	where TEntity : class, IEntity<TKey>, new()
{
}
