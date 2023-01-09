using Microsoft.EntityFrameworkCore;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Services.UserContext;

namespace OnlineClothes.Infrastructure.Repositories;

public class CartRepository : EfCoreRepositoryBase<AccountCart, int>, ICartRepository
{
	private readonly IUserContext _userContext;

	public CartRepository(AppDbContext dbContext, IUserContext userContext) : base(dbContext)
	{
		_userContext = userContext;
	}

	public async Task<AccountCart> GetCurrentCart()
	{
		var cart = await AsQueryable()
			.Include(cart => cart.Items)
			.ThenInclude(cartItem => cartItem.ProductSku)
			.ThenInclude(productSku => productSku.Product)
			.FirstAsync(cart => cart.AccountId.Equals(_userContext.GetNameIdentifier()));

		return cart;
	}
}
