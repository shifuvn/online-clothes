namespace OnlineClothes.Application.Features.Carts.Commands.AddItem;

public class AddCartItemCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public AddCartItemCommand()
	{
	}

	public AddCartItemCommand(string productSku, int quantity = 1)
	{
		ProductSku = productSku;
		Quantity = quantity;
	}

	public string ProductSku { get; init; } = null!;
	public int Quantity { get; init; } = 1;
}
