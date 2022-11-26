using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnlineClothes.Persistence.Context;
using OnlineClothes.Persistence.Repositories.Abstracts;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.Entity;
using OnlineClothes.Support.Exceptions;

namespace OnlineClothes.Persistence.Repositories;

public abstract class RootReadOnlyRepositoryBase<T> : RootReadOnlyRepositoryBase<T, string> where T : IEntity<string>
{
	protected RootReadOnlyRepositoryBase(IMongoDbContext dbContext) : base(dbContext)
	{
	}
}

public abstract class RootReadOnlyRepositoryBase<T, TKey> : IRootReadOnlyRepository<T, TKey>
	where T : IEntity<TKey>
{
	protected IMongoCollection<T> Collection;

	protected RootReadOnlyRepositoryBase(IMongoDbContext dbContext)
	{
		Collection = dbContext.GetCollection<T, TKey>();
	}

	public virtual void Dispose()
	{
		GC.SuppressFinalize(this);
	}

	public virtual IAggregateFluent<T> Aggregate(AggregateOptions? options = null)
	{
		return Collection.Aggregate(options);
	}

	public virtual IMongoQueryable<T> Query(AggregateOptions? options = null)
	{
		return Query<T>(options);
	}

	public virtual IMongoQueryable<TDerived> Query<TDerived>(AggregateOptions? options = null) where TDerived : T
	{
		return Collection.OfType<TDerived>().AsQueryable(options);
	}

	public virtual async Task<T> GetOneAsync(string id, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(id, nameof(id));

		var filter = new BsonDocument("_id", ObjectId.Parse(id));
		var cursor = await Collection.FindAsync(filter, null, cancellationToken)
			.ConfigureAwait(false);

		var result = await cursor.FirstOrDefaultAsync(cancellationToken)
			.ConfigureAwait(false);
		NullValueReferenceException.ThrowIfNull(result, nameof(result));

		return result;
	}

	public virtual async Task<TDerived> GetOneAsync<TDerived>(string id, CancellationToken cancellationToken = default)
		where TDerived : T
	{
		ArgumentNullException.ThrowIfNull(id, nameof(id));

		var filter = new BsonDocument("_id", ObjectId.Parse(id));
		var cursor = await Collection.OfType<TDerived>()
			.FindAsync(filter, cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		return await cursor.FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
	}

	public virtual async Task<TReturnProjection> GetOneAsync<TReturnProjection>(string id,
		Expression<Func<T, TReturnProjection>> selector,
		CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(id, nameof(id));

		var filter = new BsonDocument("_id", ObjectId.Parse(id));
		var cursor = await Collection.FindAsync(
				filter,
				new FindOptions<T, TReturnProjection>
				{
					Projection = Builders<T>.Projection.Expression(selector)
				},
				cancellationToken)
			.ConfigureAwait(false);

		return await cursor.FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
	}

	public virtual IFindFluent<T, T> GetAll(FindOptions? options = null)
	{
		return Collection.Find(FilterDefinition<T>.Empty, options);
	}

	public virtual async Task<long> CountAsync(CancellationToken cancellationToken = default)
	{
		return await Collection.CountDocumentsAsync(FilterDefinition<T>.Empty, cancellationToken: cancellationToken);
	}

	public virtual Task<long> CountAsync(FilterBuilder<T> filterBuilder, CancellationToken cancellationToken = default)
	{
		return CountAsync((FilterDefinition<T>)filterBuilder.Statement, cancellationToken);
	}

	public virtual async Task<long> CountAsync(FilterDefinition<T> filter,
		CancellationToken cancellationToken = default)
	{
		return await Collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);
	}

	public virtual async Task<bool> IsExistAsync(string id, CancellationToken cancellationToken = default)
	{
		var filter = new BsonDocument("_id", ObjectId.Parse(id));

		return await Collection.Find(filter).AnyAsync(cancellationToken);
	}

	public virtual async Task<bool> IsExistAsync(FilterBuilder<T> filterBuilder,
		CancellationToken cancellationToken = default)
	{
		return await Collection.Find(filterBuilder.Statement).AnyAsync(cancellationToken);
	}

	public virtual IFindFluent<T, T> FindFluent(FilterBuilder<T> filterBuilder, FindOptions? options = null)
	{
		return Collection.Find(filterBuilder.Statement, options);
	}

	public virtual IFindFluent<TDerived, TDerived> FindFluent<TDerived>(FilterBuilder<TDerived> filterBuilder,
		FindOptions? options = null) where TDerived : T
	{
		return Collection.OfType<TDerived>()
			.Find(filterBuilder.Statement, options);
	}

	public virtual IFindFluent<T, T> FindFluent(Expression<Func<T, object>> property,
		string regexPattern,
		string regexOptions = "i",
		FindOptions? options = null)
	{
		return Collection
			.Find(
				Builders<T>.Filter.Regex(property, new BsonRegularExpression(regexPattern, regexOptions)),
				options);
	}

	public virtual IFindFluent<T, T> FindFluent(FieldDefinition<T> property, string regexPattern,
		string regexOptions = "i",
		FindOptions? options = null)
	{
		return Collection
			.Find(Builders<T>.Filter.Regex(property, new BsonRegularExpression(regexPattern, regexOptions)),
				options);
	}

	public virtual IFindFluent<T, T> FindFluent(IEnumerable<Expression<Func<T, object>>> properties,
		string regexPattern,
		string regexOptions = "i",
		FindOptions? options = null)
	{
		var filters = properties.Select(p =>
			Builders<T>.Filter.Regex(p, new BsonRegularExpression(regexPattern, regexOptions)));

		return Collection.Find(Builders<T>.Filter.Or(filters), options);
	}

	public virtual IFindFluent<T, T> FindFluent(IEnumerable<FieldDefinition<T>> properties,
		string regexPattern,
		string regexOptions = "i",
		FindOptions? options = null)
	{
		var filters = properties.Select(p =>
			Builders<T>.Filter.Regex(p, new BsonRegularExpression(regexPattern, regexOptions)));

		return Collection.Find(Builders<T>.Filter.Or(filters), options);
	}

	public virtual IFindFluent<TDerived, TDerived> FindFluent<TDerived>(Expression<Func<TDerived, object>> property,
		string regexPattern, string regexOptions = "i",
		FindOptions? options = null) where TDerived : T
	{
		return Collection.OfType<TDerived>()
			.Find(Builders<TDerived>.Filter.Regex(property, new BsonRegularExpression(regexPattern, regexOptions)),
				options);
	}

	public virtual IFindFluent<TDerived, TDerived> FindFluent<TDerived>(FieldDefinition<TDerived> property,
		string regexPattern,
		string regexOptions = "i",
		FindOptions? options = null) where TDerived : T
	{
		return Collection.OfType<TDerived>()
			.Find(Builders<TDerived>.Filter.Regex(property, new BsonRegularExpression(regexPattern, regexOptions)),
				options);
	}

	public virtual IFindFluent<TDerived, TDerived> FindFluent<TDerived>(
		IEnumerable<Expression<Func<TDerived, object>>> properties,
		string regexPattern,
		string regexOptions = "i",
		FindOptions? options = null) where TDerived : T
	{
		var filters = properties.Select(p =>
			Builders<TDerived>.Filter.Regex(p, new BsonRegularExpression(regexPattern, regexOptions)));

		return Collection.OfType<TDerived>()
			.Find(Builders<TDerived>.Filter.Or(filters), options);
	}

	public virtual IFindFluent<TDerived, TDerived> FindFluent<TDerived>(
		IEnumerable<FieldDefinition<TDerived>> properties,
		string regexPattern,
		string regexOptions = "i",
		FindOptions? options = null) where TDerived : T
	{
		var filters = properties.Select(p =>
			Builders<TDerived>.Filter.Regex(p, new BsonRegularExpression(regexPattern, regexOptions)));

		return Collection.OfType<TDerived>()
			.Find(Builders<TDerived>.Filter.Or(filters), options);
	}

	public virtual Task<IAsyncCursor<T>?> FindAsync(FilterBuilder<T> filterBuilder, FindOptions<T, T>? options = null,
		CancellationToken cancellationToken = default)
	{
		return Collection.FindAsync(filterBuilder.Statement, options, cancellationToken);
	}

	public virtual Task<IAsyncCursor<T>?> FindAsync(FilterDefinition<T> filter, FindOptions<T, T>? options = null,
		CancellationToken cancellationToken = default)
	{
		return Collection.FindAsync(filter, options, cancellationToken);
	}

	public virtual Task<IAsyncCursor<TDerived>?> FindAsync<TDerived>(FilterBuilder<TDerived> filterBuilder,
		FindOptions<TDerived, TDerived>? options = null,
		CancellationToken cancellationToken = default) where TDerived : T
	{
		return Collection.OfType<TDerived>().FindAsync(filterBuilder.Statement, options, cancellationToken);
	}

	public virtual Task<IAsyncCursor<TReturnProjection>?> FindAsync<TReturnProjection>(
		FilterBuilder<T> filterBuilder,
		Expression<Func<T, TReturnProjection>> selector,
		FindOptions<T, TReturnProjection>? options = null,
		CancellationToken cancellationToken = default)
	{
		var opt = options ?? new FindOptions<T, TReturnProjection>();
		opt.Projection = Builders<T>.Projection.Expression(selector);

		return Collection.FindAsync(filterBuilder.Statement, opt, cancellationToken);
	}

	public virtual Task<IAsyncCursor<TReturnProject>?> FindAsync<TDerived, TReturnProject>(
		FilterBuilder<TDerived> filterBuilder,
		Expression<Func<TDerived, TReturnProject>> returnProjection,
		FindOptions<TDerived, TReturnProject>? options = null, CancellationToken cancellationToken = default)
		where TDerived : T
	{
		var opt = options ?? new FindOptions<TDerived, TReturnProject>();
		opt.Projection = Builders<TDerived>.Projection.Expression(returnProjection);

		return Collection.OfType<TDerived>().FindAsync(filterBuilder.Statement, opt, cancellationToken);
	}

	public virtual async Task<T?> FindOneAsync(FilterBuilder<T> filterBuilder,
		CancellationToken cancellationToken = default)
	{
		return await (await Collection.FindAsync(filterBuilder.Statement, cancellationToken: cancellationToken))
			.FirstOrDefaultAsync(cancellationToken);
	}

	public virtual async Task<TDerived?> FindOneAsync<TDerived>(FilterBuilder<TDerived> filterBuilder,
		CancellationToken cancellationToken = default) where TDerived : T
	{
		return await (await Collection.OfType<TDerived>()
				.FindAsync(filterBuilder.Statement, cancellationToken: cancellationToken))
			.FirstOrDefaultAsync(cancellationToken);
	}

	public virtual async Task<TReturnProject?> FindOneAsync<TReturnProject>(Expression<Func<T, bool>> filter,
		Expression<Func<T, TReturnProject>> selector,
		CancellationToken cancellationToken = default)
	{
		return await (await Collection.FindAsync(
				filter,
				new FindOptions<T, TReturnProject>
					{ Projection = Builders<T>.Projection.Expression(selector) },
				cancellationToken))
			.FirstOrDefaultAsync(cancellationToken);
	}

	public virtual async Task<TReturnProject?> FindOneAsync<TDerived, TReturnProject>(
		FilterBuilder<TDerived> filterBuilder,
		Expression<Func<TDerived, TReturnProject>> selector,
		CancellationToken cancellationToken = default) where TDerived : T
	{
		return await (await Collection.OfType<TDerived>()
				.FindAsync(
					filterBuilder.Statement,
					new FindOptions<TDerived, TReturnProject>
						{ Projection = Builders<TDerived>.Projection.Expression(selector) },
					cancellationToken))
			.FirstOrDefaultAsync(cancellationToken);
	}

	public virtual IFindFluent<T, T> FindFluent(FilterDefinition<T> filter, FindOptions? options = null)
	{
		return Collection.Find(filter, options);
	}

	public virtual IFindFluent<TDerived, TDerived> FindFluent<TDerived>(FilterDefinition<TDerived> filter,
		FindOptions? options = null) where TDerived : T
	{
		return Collection.OfType<TDerived>().Find(filter, options);
	}
}
