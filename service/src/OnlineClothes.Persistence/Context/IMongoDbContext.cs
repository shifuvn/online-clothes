using MongoDB.Driver;
using OnlineClothes.Support.Entity;

namespace OnlineClothes.Persistence.Context;

/// <summary>
/// Only support single dbContext
/// </summary>
public interface IMongoDbContext
{
	IMongoDatabase GetDatabase();

	IMongoCollection<TEntity> GetCollection<TEntity, TKey>(string? name = null) where TEntity : IEntity<TKey>;
}
