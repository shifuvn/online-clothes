using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Carts.Commands.RemoveItem;

public class RemoveCartItemCommandHandler : IRequestHandler<RemoveCartItemCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly ICartRepository _cartRepository;
	private readonly IUnitOfWork _unitOfWork;

	public RemoveCartItemCommandHandler(
		ICartRepository cartRepository,
		IUnitOfWork unitOfWork)
	{
		_cartRepository = cartRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(RemoveCartItemCommand request,
		CancellationToken cancellationToken)
	{
		var cart = await _cartRepository.GetCurrentCart();

		_cartRepository.Update(cart);
		cart.UpdateItemQuantity(request.ProductSku, request.Quantity);

		return await _unitOfWork.SaveChangesAsync(cancellationToken)
			? JsonApiResponse<EmptyUnitResponse>.Success()
			: JsonApiResponse<EmptyUnitResponse>.Fail();
	}
}
