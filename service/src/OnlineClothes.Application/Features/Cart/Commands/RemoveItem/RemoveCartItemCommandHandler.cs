using MediatR;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Infrastructure.Services.UserContext.Abstracts;
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
		//var cart = await _cartRepository.FindOneAsync(
		//	FilterBuilder<AccountCart>.Where(q => q.AccountId == _userContext.GetNameIdentifier()),
		//	cancellationToken: cancellationToken);
		//NullValueReferenceException.ThrowIfNull(cart, nameof(cart));

		//cart.RemoveItem(request.ProductId, request.Quantity);

		//var updatedResult = await _cartRepository.UpdateOneAsync(
		//	cart.Id,
		//	update => update.Set(q => q.Items, cart.Items),
		//	cancellationToken: cancellationToken);

		//return updatedResult.Any()
		//	? JsonApiResponse<EmptyUnitResponse>.Success()
		//	: JsonApiResponse<EmptyUnitResponse>.Fail();

		throw new NotImplementedException();
	}
}
