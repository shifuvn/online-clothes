namespace OnlineClothes.Application.Features.Products.Commands.DeleteSku;

public class DisableSkuCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public DisableSkuCommand(string sku)
	{
		Sku = sku;
	}

	public string Sku { get; set; }
}
