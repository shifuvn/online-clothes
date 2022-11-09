using MongoDB.Bson;
using MongoDB.Driver;
using OnlineClothes.Domain.Attributes;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Infrastructure.AggregateModels;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Infrastructure.Services.UserContext.Abstracts;
using OnlineClothes.Persistence.Context;
using OnlineClothes.Persistence.Repositories;

namespace OnlineClothes.Infrastructure.Repositories;

public class CartRepository : RootRepositoryBase<AccountCart, string>, ICartRepository
{
	private readonly IUserContext _userContext;

	public CartRepository(IMongoDbContext dbContext, IUserContext userContext) : base(dbContext)
	{
		_userContext = userContext;
	}

	public async Task<AggregateCartInfoModel> GetItems(CancellationToken cancellationToken = default)
	{
		var lookupStage = new BsonDocument(
			"$lookup",
			new BsonDocument("from", BsonCollectionAttribute.GetName<ProductClothe>())
				.Add("localField", "items.productId")
				.Add("foreignField", "_id")
				.Add("as", "items.details"));

		// after unwind and lookup: item List -> object

		var data = await Collection.Aggregate()
			.Match(q => q.AccountId == _userContext.GetNameIdentifier())
			.Unwind(q => q.Items)
			.AppendStage<AggregateLookupCart>(lookupStage)
			.ToListAsync(cancellationToken);

		var result = data
			.GroupBy(q => q.Id)
			.Select(q => new AggregateCartInfoModel(
				q.Key,
				q.Select(AggregateCartInfoModel.Item.Create).ToList()))
			.First();

		return result;
	}
}
