using MediatR;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Cart.Commands.RemoveItem;

public class RemoveCartItemCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public string ProductId { get; init; } = null!;
	public int Quantity { get; init; } = 1;
}
