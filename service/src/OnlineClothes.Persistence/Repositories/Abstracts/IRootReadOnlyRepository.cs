using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.Entity;

namespace OnlineClothes.Persistence.Repositories.Abstracts;

public interface IRootReadOnlyRepository<TEntity, TKey> : IDisposable where TEntity : IEntity<TKey>
{
	IAggregateFluent<TEntity> Aggregate(AggregateOptions? options = null);

	#region Queryable

	IMongoQueryable<TEntity> Query(AggregateOptions? options = null);
	IMongoQueryable<TDerived> Query<TDerived>(AggregateOptions? options = null) where TDerived : TEntity;

	#endregion

	#region Get

	Task<TEntity> GetOneAsync(string id, CancellationToken cancellationToken = default);

	Task<TDerived> GetOneAsync<TDerived>(string id, CancellationToken cancellationToken = default)
		where TDerived : TEntity;

	Task<TReturnProjection> GetOneAsync<TReturnProjection>(
		string id,
		Expression<Func<TEntity, TReturnProjection>> selector,
		CancellationToken cancellationToken = default);

	IFindFluent<TEntity, TEntity> GetAll(FindOptions? options = null);

	#endregion

	#region Count

	Task<long> CountAsync(CancellationToken cancellationToken = default);
	Task<long> CountAsync(FilterBuilder<TEntity> filterBuilder, CancellationToken cancellationToken = default);
	Task<long> CountAsync(FilterDefinition<TEntity> filter, CancellationToken cancellationToken = default);

	#endregion

	#region Find fluent

	IFindFluent<TEntity, TEntity> FindFluent(
		FilterBuilder<TEntity> filterBuilder,
		FindOptions? options = null);

	IFindFluent<TDerived, TDerived> FindFluent<TDerived>(
		FilterBuilder<TDerived> filterBuilder,
		FindOptions? options = null) where TDerived : TEntity;

	IFindFluent<TEntity, TEntity> FindFluent(
		Expression<Func<TEntity, object>> property,
		string regexPattern,
		string regexOptions = "i",
		FindOptions? options = null);

	IFindFluent<TEntity, TEntity> FindFluent(
		FieldDefinition<TEntity> property,
		string regexPattern,
		string regexOptions = "i",
		FindOptions? options = null);

	IFindFluent<TEntity, TEntity> FindFluent(
		IEnumerable<Expression<Func<TEntity, object>>> properties,
		string regexPattern,
		string regexOptions = "i",
		FindOptions? options = null);

	IFindFluent<TEntity, TEntity> FindFluent(
		IEnumerable<FieldDefinition<TEntity>> properties,
		string regexPattern,
		string regexOptions = "i",
		FindOptions? options = null);

	IFindFluent<TDerived, TDerived> FindFluent<TDerived>(
		Expression<Func<TDerived, object>> property,
		string regexPattern,
		string regexOptions = "i",
		FindOptions? options = null) where TDerived : TEntity;

	IFindFluent<TDerived, TDerived> FindFluent<TDerived>(
		FieldDefinition<TDerived> property,
		string regexPattern,
		string regexOptions = "i",
		FindOptions? options = null) where TDerived : TEntity;

	IFindFluent<TDerived, TDerived> FindFluent<TDerived>(
		IEnumerable<Expression<Func<TDerived, object>>> properties,
		string regexPattern,
		string regexOptions = "i",
		FindOptions? options = null) where TDerived : TEntity;

	IFindFluent<TDerived, TDerived> FindFluent<TDerived>(
		IEnumerable<FieldDefinition<TDerived>> properties,
		string regexPattern,
		string regexOptions = "i",
		FindOptions? options = null) where TDerived : TEntity;

	Task<IAsyncCursor<TEntity>> FindAsync(
		FilterBuilder<TEntity> filterBuilder,
		FindOptions<TEntity, TEntity>? options = null,
		CancellationToken cancellationToken = default);

	Task<IAsyncCursor<TEntity>> FindAsync(
		FilterDefinition<TEntity> filter,
		FindOptions<TEntity, TEntity>? options = null,
		CancellationToken cancellationToken = default);

	Task<IAsyncCursor<TDerived>> FindAsync<TDerived>(
		FilterBuilder<TDerived> filterBuilder,
		FindOptions<TDerived, TDerived>? options = null,
		CancellationToken cancellationToken = default) where TDerived : TEntity;

	Task<IAsyncCursor<TReturnProjection>> FindAsync<TReturnProjection>(
		FilterBuilder<TEntity> filterBuilder,
		Expression<Func<TEntity, TReturnProjection>> selector,
		FindOptions<TEntity, TReturnProjection>? options = null,
		CancellationToken cancellationToken = default);

	Task<IAsyncCursor<TReturnProject>> FindAsync<TDerived, TReturnProject>(
		FilterBuilder<TDerived> filterBuilder,
		Expression<Func<TDerived, TReturnProject>> returnProjection,
		FindOptions<TDerived, TReturnProject>? options = null,
		CancellationToken cancellationToken = default) where TDerived : TEntity;

	Task<TEntity> FindOneAsync(
		FilterBuilder<TEntity> filterBuilder,
		CancellationToken cancellationToken = default);

	Task<TDerived> FindOneAsync<TDerived>(
		FilterBuilder<TDerived> filterBuilder,
		CancellationToken cancellationToken = default) where TDerived : TEntity;

	Task<TReturnProject> FindOneAsync<TReturnProject>(
		Expression<Func<TEntity, bool>> filter,
		Expression<Func<TEntity, TReturnProject>> selector,
		CancellationToken cancellationToken = default);

	Task<TReturnProject> FindOneAsync<TDerived, TReturnProject>(
		FilterBuilder<TDerived> filterBuilder,
		Expression<Func<TDerived, TReturnProject>> selector,
		CancellationToken cancellationToken = default) where TDerived : TEntity;

	IFindFluent<TEntity, TEntity> FindFluent(
		FilterDefinition<TEntity> filter,
		FindOptions? options = null);

	IFindFluent<TDerived, TDerived> FindFluent<TDerived>(
		FilterDefinition<TDerived> filter,
		FindOptions? options = null) where TDerived : TEntity;

	#endregion
}
