using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using OnlineClothes.Persistence.Context;
using OnlineClothes.Persistence.Repositories.Abstracts;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.Entity;

namespace OnlineClothes.Persistence.Repositories;

public abstract class RootRepositoryBase<T, TKey> : RootReadOnlyRepositoryBase<T, TKey>, IRootRepository<T, TKey>
	where T : IEntity<TKey>
{
	protected RootRepositoryBase(IMongoDbContext dbContext) : base(dbContext)
	{
	}

	public virtual async Task<TProject> FindOneOrInsertAsync<TProject>(FilterBuilder<T> filterBuilder,
		T entity,
		Expression<Func<T, TProject>> selector,
		ReturnDocument returnDocument = ReturnDocument.After, CancellationToken cancellationToken = default)
	{
		return await IMongoCollectionExtensions.FindOneAndUpdateAsync(Collection, filterBuilder.Statement,
			new BsonDocumentUpdateDefinition<T>(new BsonDocument("$setOnInsert",
				entity.ToBsonDocument(Collection.DocumentSerializer))),
			new FindOneAndUpdateOptions<T, TProject>
			{
				IsUpsert = true,
				ReturnDocument = returnDocument,
				Projection = Builders<T>.Projection.Expression(selector)
			},
			cancellationToken);
	}

	public virtual async Task InsertAsync(T entity, CancellationToken cancellationToken = default)
	{
		await Collection.InsertOneAsync(entity, cancellationToken: cancellationToken)
			.ConfigureAwait(false);
	}

	public virtual async Task InsertManyAsync(ICollection<T> entities, CancellationToken cancellationToken = default)
	{
		var models = new List<WriteModel<T>>(entities.Count);
		foreach (var item in entities)
		{
			var upsert = new InsertOneModel<T>(item);
			models.Add(upsert);
		}

		await Collection.BulkWriteAsync(models, cancellationToken: cancellationToken);
	}

	public virtual async Task InsertManyAsync<TDerived>(ICollection<TDerived> entities,
		CancellationToken cancellationToken = default)
		where TDerived : T
	{
		if (entities.Count > 0)
		{
			await Collection
				.OfType<TDerived>()
				.InsertManyAsync(entities, cancellationToken: cancellationToken)
				.ConfigureAwait(false);
		}
	}

	public virtual Task<UpdateResult> UpdateOneAsync(string id,
		Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update,
		bool isUpsert = false, CancellationToken cancellationToken = default)
	{
		return UpdateOneAsync(id, update, new UpdateOptions { IsUpsert = isUpsert }, cancellationToken);
	}

	public virtual async Task<UpdateResult> UpdateOneAsync(string id,
		Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update,
		UpdateOptions options, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(id, nameof(id));

		var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));

		return await Collection
			.UpdateOneAsync(filter, update(Builders<T>.Update), options, cancellationToken)
			.ConfigureAwait(false);
	}

	public virtual Task<UpdateResult> UpdateOneAsync(FilterBuilder<T> filterBuilder,
		Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update, bool isUpsert = false,
		CancellationToken cancellationToken = default)
	{
		return UpdateOneAsync(filterBuilder.Statement, update, new UpdateOptions { IsUpsert = isUpsert },
			cancellationToken);
	}

	public virtual Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> filter,
		Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update, bool isUpsert = false,
		CancellationToken cancellationToken = default)
	{
		return UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = isUpsert }, cancellationToken);
	}

	public virtual Task<UpdateResult> UpdateOneAsync(FilterBuilder<T> filterBuilder,
		Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update, UpdateOptions options,
		CancellationToken cancellationToken = default)
	{
		return UpdateOneAsync((FilterDefinition<T>)filterBuilder.Statement, update, options, cancellationToken);
	}

	public virtual async Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> filter,
		Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update, UpdateOptions options,
		CancellationToken cancellationToken = default)
	{
		return await Collection
			.UpdateOneAsync(filter, update(Builders<T>.Update), options, cancellationToken)
			.ConfigureAwait(false);
	}

	public virtual Task<UpdateResult> UpdateOneAsync<TDerived>(string id,
		Func<UpdateDefinitionBuilder<TDerived>, UpdateDefinition<TDerived>> update, bool isUpsert = false,
		CancellationToken cancellationToken = default) where TDerived : T
	{
		return UpdateOneAsync(id, update, new UpdateOptions { IsUpsert = isUpsert }, cancellationToken);
	}

	public virtual async Task<UpdateResult> UpdateOneAsync<TDerived>(string id,
		Func<UpdateDefinitionBuilder<TDerived>, UpdateDefinition<TDerived>> update, UpdateOptions options,
		CancellationToken cancellationToken = default) where TDerived : T
	{
		ArgumentNullException.ThrowIfNull(id, nameof(id));


		var filter = Builders<TDerived>.Filter.Eq("_id", ObjectId.Parse(id));
		return await Collection.OfType<TDerived>()
			.UpdateOneAsync(filter, update(Builders<TDerived>.Update), options, cancellationToken)
			.ConfigureAwait(false);
	}

	public virtual Task<UpdateResult> UpdateOneAsync<TDerived>(FilterBuilder<TDerived> filterBuilder,
		Func<UpdateDefinitionBuilder<TDerived>,
			UpdateDefinition<TDerived>> update,
		bool isUpsert = false,
		CancellationToken cancellationToken = default) where TDerived : T
	{
		return UpdateOneAsync(filterBuilder.Statement, update, new UpdateOptions { IsUpsert = isUpsert },
			cancellationToken);
	}

	public virtual Task<UpdateResult> UpdateOneAsync<TDerived>(FilterDefinition<TDerived> filter,
		Func<UpdateDefinitionBuilder<TDerived>,
			UpdateDefinition<TDerived>> update,
		bool isUpsert = false,
		CancellationToken cancellationToken = default) where TDerived : T
	{
		return UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = isUpsert }, cancellationToken);
	}

	public virtual Task<UpdateResult> UpdateOneAsync<TDerived>(FilterBuilder<TDerived> filterBuilder,
		Func<UpdateDefinitionBuilder<TDerived>,
			UpdateDefinition<TDerived>> update,
		UpdateOptions options,
		CancellationToken cancellationToken = default) where TDerived : T
	{
		return UpdateOneAsync((FilterDefinition<TDerived>)filterBuilder.Statement, update, options, cancellationToken);
	}

	public virtual async Task<UpdateResult> UpdateOneAsync<TDerived>(FilterDefinition<TDerived> filter,
		Func<UpdateDefinitionBuilder<TDerived>, UpdateDefinition<TDerived>> update, UpdateOptions options,
		CancellationToken cancellationToken = default) where TDerived : T
	{
		return await Collection.OfType<TDerived>()
			.UpdateOneAsync(filter, update(Builders<TDerived>.Update), options, cancellationToken)
			.ConfigureAwait(false);
	}

	public virtual async Task UpdateManyAsync(FilterBuilder<T> filterBuilder,
		Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update, UpdateOptions? options = null,
		CancellationToken cancellationToken = default)
	{
		await Collection
			.UpdateManyAsync(filterBuilder.Statement, update(Builders<T>.Update), options, cancellationToken)
			.ConfigureAwait(false);
	}

	public virtual Task<TProjection> FindOneAndUpdateAsync<TProjection>(FilterBuilder<T> filterBuilder,
		Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update,
		Expression<Func<T, TProjection>> returnProjection,
		ReturnDocument returnDocument = ReturnDocument.After,
		bool isUpsert = false,
		CancellationToken cancellationToken = default)
	{
		return FindOneAndUpdateAsync(
			filterBuilder,
			update,
			Builders<T>.Projection.Expression(returnProjection),
			returnDocument, isUpsert, cancellationToken);
	}

	public virtual async Task<TProjection> FindOneAndUpdateAsync<TProjection>(FilterBuilder<T> filterBuilder,
		Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> update,
		ProjectionDefinition<T, TProjection> selector,
		ReturnDocument returnDocument = ReturnDocument.After, bool isUpsert = false,
		CancellationToken cancellationToken = default)
	{
		return await Collection.FindOneAndUpdateAsync(
				filterBuilder.Statement,
				update(Builders<T>.Update),
				new FindOneAndUpdateOptions<T, TProjection>
				{
					Projection = selector,
					ReturnDocument = returnDocument,
					IsUpsert = isUpsert
				},
				cancellationToken)
			.ConfigureAwait(false);
	}


	public virtual async Task<ReplaceOneResult> ReplaceOneAsync(string id, T entity, bool isUpsert = false,
		CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrEmpty(id))
			throw new ArgumentNullException(nameof(id));

		var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
		return await Collection.ReplaceOneAsync(filter, entity, new ReplaceOptions { IsUpsert = isUpsert },
				cancellationToken)
			.ConfigureAwait(false);
	}

	public virtual async Task<ReplaceOneResult> ReplaceOneAsync(FilterBuilder<T> filterBuilder, T entity,
		bool isUpsert = false,
		CancellationToken cancellationToken = default)
	{
		return await Collection.ReplaceOneAsync(filterBuilder.Statement, entity,
				new ReplaceOptions { IsUpsert = isUpsert },
				cancellationToken)
			.ConfigureAwait(false);
	}

	public virtual Task<TProject> FindOneAndReplaceAsync<TProject>(FilterBuilder<T> filterBuilder,
		T replacement,
		Expression<Func<T, TProject>> selector,
		ReturnDocument returnDocument = ReturnDocument.After,
		bool isUpsert = false,
		CancellationToken cancellationToken = default)
	{
		return FindOneAndReplaceAsync(
			filterBuilder,
			replacement,
			Builders<T>.Projection.Expression(selector),
			returnDocument, isUpsert, cancellationToken);
	}

	public virtual async Task<TProject> FindOneAndReplaceAsync<TProject>(FilterBuilder<T> filterBuilder, T replacement,
		ProjectionDefinition<T, TProject> selector,
		ReturnDocument returnDocument = ReturnDocument.After,
		bool isUpsert = false,
		CancellationToken cancellationToken = default)
	{
		return await Collection.FindOneAndReplaceAsync(
				filterBuilder.Statement,
				replacement,
				new FindOneAndReplaceOptions<T, TProject>
				{
					Projection = selector,
					ReturnDocument = returnDocument,
					IsUpsert = isUpsert
				},
				cancellationToken)
			.ConfigureAwait(false);
	}

	public virtual async Task<DeleteResult> DeleteOneAsync(string id, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(id, nameof(id));

		var filter = new BsonDocumentFilterDefinition<T>(new BsonDocument("_id", ObjectId.Parse(id)));

		return await DeleteOneAsync(filter, cancellationToken);
	}

	public virtual Task<DeleteResult> DeleteOneAsync(FilterBuilder<T> filterBuilder,
		CancellationToken cancellationToken = default)
	{
		return DeleteOneAsync(Builders<T>.Filter.Where(filterBuilder.Statement), cancellationToken);
	}

	public virtual async Task<DeleteResult> DeleteOneAsync(FilterDefinition<T> filter,
		CancellationToken cancellationToken = default)
	{
		return await Collection.DeleteOneAsync(filter, cancellationToken).ConfigureAwait(false);
	}

	public virtual async Task<DeleteResult> DeleteManyAsync(FilterBuilder<T> filterBuilder,
		CancellationToken cancellationToken = default)
	{
		return await Collection.DeleteManyAsync(filterBuilder.Statement, cancellationToken);
	}

	public virtual Task<T> FindOneAndDeleteAsync(string id, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(id, nameof(id));


		var filter = new BsonDocumentFilterDefinition<T>(new BsonDocument("_id", ObjectId.Parse(id)));
		return FindOneAndDeleteAsync(filter, cancellationToken);
	}

	public virtual async Task<T> FindOneAndDeleteAsync(FilterBuilder<T> filterBuilder,
		CancellationToken cancellationToken = default)
	{
		return await Collection.FindOneAndDeleteAsync<T>(filterBuilder.Statement, cancellationToken: cancellationToken)
			.ConfigureAwait(false);
	}

	public virtual async Task<T> FindOneAndDeleteAsync(FilterDefinition<T> filter,
		CancellationToken cancellationToken = default)
	{
		return await Collection.FindOneAndDeleteAsync(filter, cancellationToken: cancellationToken)
			.ConfigureAwait(false);
	}
}
