using Microsoft.EntityFrameworkCore;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Services.UserContext;
using OnlineClothes.Domain.Common;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.Exceptions.HttpExceptionCodes;

namespace OnlineClothes.Infrastructure.Repositories;

public class OrderRepository : EfCoreRepositoryBase<Order, int>, IOrderRepository
{
	private readonly IUserContext _userContext;

	public OrderRepository(AppDbContext dbContext, IUserContext userContext) : base(dbContext)
	{
		_userContext = userContext;
	}

	public override async Task<Order> GetByIntKey(int key, CancellationToken cancellationToken = default)
	{
		var entry = await AsQueryable()
			.Include(q => q.Items)
			.ThenInclude(q => q.ProductSku)
			.ThenInclude(q => q.Product)
			.Include(q => q.Account)
			.FirstAsync(q => q.Id == key, cancellationToken: cancellationToken);

		return entry;
	}

	public override async Task<Order?> FindOneAsync(FilterBuilder<Order> filterBuilder,
		CancellationToken cancellationToken = default)
	{
		var entry = await AsQueryable()
			.Include(q => q.Items)
			.ThenInclude(q => q.ProductSku)
			.ThenInclude(q => q.Product)
			.Include(q => q.Account)
			.FirstOrDefaultAsync(filterBuilder.Statement, cancellationToken: cancellationToken);

		return entry;
	}


	public async Task<Order> GetOneByUserContext(int orderId, CancellationToken cancellationToken = default)
	{
		var order = await FindOneAsync(PrepareFilterBuilder(orderId), cancellationToken);
		if (order == null)
		{
			throw new ForbiddenException();
		}

		return order;
	}

	private FilterBuilder<Order> PrepareFilterBuilder(int orderId)
	{
		if (!Enum.TryParse(_userContext.GetRole(), true, out AccountRole role))
		{
			throw new InvalidOperationException($"Invalid role {role}");
		}

		var fb = new FilterBuilder<Order>(q => q.Id == orderId);

		if (role == AccountRole.Client)
		{
			fb.And(q => q.AccountId == _userContext.GetNameIdentifier());
		}

		return fb;
	}
}
