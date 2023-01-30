namespace OnlineClothes.Application.Features.Skus.Commands.RestoreSku;

public class RestoreSkuCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public RestoreSkuCommand(string sku)
	{
		Sku = sku;
	}

	public string Sku { get; set; }
}
