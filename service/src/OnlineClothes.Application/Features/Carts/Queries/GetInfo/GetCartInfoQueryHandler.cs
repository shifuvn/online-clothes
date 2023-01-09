using OnlineClothes.Application.Persistence;
using OnlineClothes.Support.Utilities.Extensions;

namespace OnlineClothes.Application.Features.Carts.Queries.GetInfo;

public class GetCartInfoQueryHandler : IRequestHandler<GetCartInfoQuery, JsonApiResponse<List<CartItemDto>>>
{
	private readonly ICartRepository _cartRepository;

	public GetCartInfoQueryHandler(ICartRepository cartRepository)
	{
		_cartRepository = cartRepository;
	}

	public async Task<JsonApiResponse<List<CartItemDto>>> Handle(GetCartInfoQuery request,
		CancellationToken cancellationToken)
	{
		var cart = await _cartRepository.GetCurrentCart();

		var viewmodel = cart.Items.SelectList(CartItemDto.ToModel);

		return JsonApiResponse<List<CartItemDto>>.Success(data: viewmodel);
	}
}
