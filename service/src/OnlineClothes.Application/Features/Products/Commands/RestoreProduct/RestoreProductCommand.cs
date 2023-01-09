namespace OnlineClothes.Application.Features.Products.Commands.RestoreProduct;

public class RestoreProductCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public RestoreProductCommand(int productId)
	{
		ProductId = productId;
	}

	public int ProductId { get; set; }
}
