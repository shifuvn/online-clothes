using System.Linq.Expressions;
using MongoDB.Driver;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.Entity;

namespace OnlineClothes.Persistence.Abstracts.Repositories;

public interface IRootRepository<TEntity, TKey> : IRootReadOnlyRepository<TEntity, TKey>
	where TEntity : IEntity<TKey>
{
	#region Find & insert

	Task<TProject> FindOneOrInsertAsync<TProject>(
		FilterBuilder<TEntity> filterBuilder,
		TEntity entity,
		Expression<Func<TEntity, TProject>> selector,
		ReturnDocument returnDocument = ReturnDocument.After,
		CancellationToken cancellationToken = default);

	#endregion

	#region Insert

	Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
	Task InsertManyAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);

	Task InsertManyAsync<TDerived>(ICollection<TDerived> entities, CancellationToken cancellationToken = default)
		where TDerived : class, TEntity;

	#endregion

	#region Update

	Task<UpdateResult> UpdateOneAsync(
		string id,
		Func<UpdateDefinitionBuilder<TEntity>, UpdateDefinition<TEntity>> update,
		bool isUpsert = false,
		CancellationToken cancellationToken = default);

	Task<UpdateResult> UpdateOneAsync(
		string id,
		Func<UpdateDefinitionBuilder<TEntity>, UpdateDefinition<TEntity>> update,
		UpdateOptions options,
		CancellationToken cancellationToken = default);

	Task<UpdateResult> UpdateOneAsync(
		FilterBuilder<TEntity> filterBuilder,
		Func<UpdateDefinitionBuilder<TEntity>, UpdateDefinition<TEntity>> update,
		bool isUpsert = false,
		CancellationToken cancellationToken = default);

	Task<UpdateResult> UpdateOneAsync(
		FilterDefinition<TEntity> filter,
		Func<UpdateDefinitionBuilder<TEntity>, UpdateDefinition<TEntity>> update,
		bool isUpsert = false,
		CancellationToken cancellationToken = default);

	// base operations
	Task<UpdateResult> UpdateOneAsync(
		FilterBuilder<TEntity> filterBuilder,
		Func<UpdateDefinitionBuilder<TEntity>, UpdateDefinition<TEntity>> update,
		UpdateOptions options,
		CancellationToken cancellationToken = default);

	Task<UpdateResult> UpdateOneAsync(
		FilterDefinition<TEntity> filter,
		Func<UpdateDefinitionBuilder<TEntity>, UpdateDefinition<TEntity>> update,
		UpdateOptions options,
		CancellationToken cancellationToken = default);

	// derived
	Task<UpdateResult> UpdateOneAsync<TDerived>(
		string id,
		Func<UpdateDefinitionBuilder<TDerived>, UpdateDefinition<TDerived>> update,
		bool isUpsert = false,
		CancellationToken cancellationToken = default) where TDerived : TEntity;

	Task<UpdateResult> UpdateOneAsync<TDerived>(
		string id,
		Func<UpdateDefinitionBuilder<TDerived>, UpdateDefinition<TDerived>> update,
		UpdateOptions options,
		CancellationToken cancellationToken = default) where TDerived : TEntity;

	Task<UpdateResult> UpdateOneAsync<TDerived>(
		FilterBuilder<TDerived> filterBuilder,
		Func<UpdateDefinitionBuilder<TDerived>, UpdateDefinition<TDerived>> update,
		bool isUpsert = false,
		CancellationToken cancellationToken = default) where TDerived : TEntity;

	Task<UpdateResult> UpdateOneAsync<TDerived>(
		FilterDefinition<TDerived> filter,
		Func<UpdateDefinitionBuilder<TDerived>, UpdateDefinition<TDerived>> update,
		bool isUpsert = false,
		CancellationToken cancellationToken = default) where TDerived : TEntity;

	Task<UpdateResult> UpdateOneAsync<TDerived>(
		FilterBuilder<TDerived> filterBuilder,
		Func<UpdateDefinitionBuilder<TDerived>, UpdateDefinition<TDerived>> update,
		UpdateOptions options,
		CancellationToken cancellationToken = default) where TDerived : TEntity;

	Task<UpdateResult> UpdateOneAsync<TDerived>(
		FilterDefinition<TDerived> filter,
		Func<UpdateDefinitionBuilder<TDerived>, UpdateDefinition<TDerived>> update,
		UpdateOptions options,
		CancellationToken cancellationToken = default) where TDerived : TEntity;

	Task UpdateManyAsync(
		FilterBuilder<TEntity> filterBuilder,
		Func<UpdateDefinitionBuilder<TEntity>, UpdateDefinition<TEntity>> update,
		UpdateOptions? options = null,
		CancellationToken cancellationToken = default);

	// find and update
	Task<TProjection> FindOneAndUpdateAsync<TProjection>(
		FilterBuilder<TEntity> filterBuilder,
		Func<UpdateDefinitionBuilder<TEntity>, UpdateDefinition<TEntity>> update,
		Expression<Func<TEntity, TProjection>> returnProjection,
		ReturnDocument returnDocument = ReturnDocument.After,
		bool isUpsert = false,
		CancellationToken cancellationToken = default);

	Task<TProjection> FindOneAndUpdateAsync<TProjection>(
		FilterBuilder<TEntity> filterBuilder,
		Func<UpdateDefinitionBuilder<TEntity>, UpdateDefinition<TEntity>> update,
		ProjectionDefinition<TEntity, TProjection> returnProjection,
		ReturnDocument returnDocument = ReturnDocument.After,
		bool isUpsert = false,
		CancellationToken cancellationToken = default);

	#endregion

	#region Replace

	Task<ReplaceOneResult> ReplaceOneAsync(
		string id,
		TEntity entity,
		bool isUpsert = false,
		CancellationToken cancellationToken = default);

	Task<ReplaceOneResult> ReplaceOneAsync(
		FilterBuilder<TEntity> filterBuilder,
		TEntity entity,
		bool isUpsert = false,
		CancellationToken cancellationToken = default);

	Task<TProject> FindOneAndReplaceAsync<TProject>(
		FilterBuilder<TEntity> filterBuilder,
		TEntity replacement,
		Expression<Func<TEntity, TProject>> selector,
		ReturnDocument returnDocument = ReturnDocument.After,
		bool isUpsert = false,
		CancellationToken cancellationToken = default);

	Task<TProject> FindOneAndReplaceAsync<TProject>(
		FilterBuilder<TEntity> filterBuilder,
		TEntity replacement,
		ProjectionDefinition<TEntity, TProject> returnProjection,
		ReturnDocument returnDocument = ReturnDocument.After,
		bool isUpsert = false,
		CancellationToken cancellationToken = default);

	#endregion

	#region Delete

	Task<DeleteResult> DeleteOneAsync(string id, CancellationToken cancellationToken = default);

	Task<DeleteResult> DeleteOneAsync(FilterBuilder<TEntity> filterBuilder,
		CancellationToken cancellationToken = default);

	Task<DeleteResult> DeleteOneAsync(FilterDefinition<TEntity> filter,
		CancellationToken cancellationToken = default);

	Task<DeleteResult> DeleteManyAsync(
		FilterBuilder<TEntity> filterBuilder,
		CancellationToken cancellationToken = default);

	Task<TEntity> FindOneAndDeleteAsync(string id, CancellationToken cancellationToken = default);

	Task<TEntity> FindOneAndDeleteAsync(
		FilterBuilder<TEntity> filterBuilder,
		CancellationToken cancellationToken = default);

	Task<TEntity> FindOneAndDeleteAsync(
		FilterDefinition<TEntity> filter,
		CancellationToken cancellationToken = default);

	#endregion
}
