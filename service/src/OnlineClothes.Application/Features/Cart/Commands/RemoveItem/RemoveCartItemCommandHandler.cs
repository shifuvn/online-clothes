using MediatR;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Infrastructure.Services.UserContext.Abstracts;
using OnlineClothes.Persistence.Extensions;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Cart.Commands.RemoveItem;

public class RemoveCartItemCommandHandler : IRequestHandler<RemoveCartItemCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly ICartRepository _cartRepository;
	private readonly IUserContext _userContext;

	public RemoveCartItemCommandHandler(ICartRepository cartRepository, IUserContext userContext)
	{
		_cartRepository = cartRepository;
		_userContext = userContext;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(RemoveCartItemCommand request,
		CancellationToken cancellationToken)
	{
		var cart = await _cartRepository.FindOneOrInsertAsync(
			FilterBuilder<AccountCart>.Where(q => q.AccountId == _userContext.GetNameIdentifier()),
			new AccountCart
			{
				AccountId = _userContext.GetNameIdentifier(),
				Items = new HashSet<AccountCart.CartItem> { new(request.ProductId, request.Quantity) }
			},
			selector: e => e,
			cancellationToken: cancellationToken);

		cart.RemoveItem(request.ProductId, request.Quantity);

		var updatedResult = await _cartRepository.UpdateOneAsync(
			cart.Id,
			update => update.Set(q => q.Items, cart.Items),
			cancellationToken: cancellationToken);

		return updatedResult.Any()
			? JsonApiResponse<EmptyUnitResponse>.Success()
			: JsonApiResponse<EmptyUnitResponse>.Fail();
	}
}
