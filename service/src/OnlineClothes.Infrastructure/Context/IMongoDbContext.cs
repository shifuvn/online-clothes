using MongoDB.Driver;

namespace OnlineClothes.Infrastructure.Context;

/// <summary>
/// Only support single dbContext
/// </summary>
public interface IMongoDbContext
{
	IMongoDatabase GetDatabase();

	IMongoCollection<TCollection> GetCollection<TCollection>(string? name = null) where TCollection : class;
}
