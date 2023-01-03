using MongoDB.Driver;
using OnlineClothes.Domain.Entities.Aggregate;

namespace OnlineClothes.Persistence.Context.Indexing;

internal class MongoDbIndexing
{
	internal static async Task CreateAccountUserIndexesAsync(IMongoCollection<AccountUser> collection)
	{
		await collection.Indexes.DropAllAsync();

		await collection.Indexes.CreateManyAsync(
			new List<CreateIndexModel<AccountUser>>
			{
				new(Builders<AccountUser>.IndexKeys.Text(q => q.Email)),
				new(Builders<AccountUser>.IndexKeys.Text(q => q.FirstName)),
				new(Builders<AccountUser>.IndexKeys.Text(q => q.LastName))
			});
	}

	internal static async Task CreateAccountTokenCodeIndexesAsync(IMongoCollection<AccountTokenCode> collection)
	{
		await collection.Indexes.DropAllAsync();

		await collection.Indexes.CreateManyAsync(
			new List<CreateIndexModel<AccountTokenCode>>
			{
				new(Builders<AccountTokenCode>.IndexKeys.Text(q => q.TokenCode)),
				new(Builders<AccountTokenCode>.IndexKeys.Text(q => q.Email))
			});
	}
}
