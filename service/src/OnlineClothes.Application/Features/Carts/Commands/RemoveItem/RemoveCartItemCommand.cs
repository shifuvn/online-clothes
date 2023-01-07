namespace OnlineClothes.Application.Features.Carts.Commands.RemoveItem;

public class RemoveCartItemCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public RemoveCartItemCommand()
	{
	}

	public RemoveCartItemCommand(string productSku, int quantity)
	{
		ProductSku = productSku;
		Quantity = quantity;
	}

	public string ProductSku { get; init; } = null!;
	public int Quantity { get; init; } = 1;
}
