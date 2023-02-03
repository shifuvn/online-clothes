namespace OnlineClothes.Application.Features.Skus.Queries.Detail;

public class GetSkuDetailQuery : IRequest<JsonApiResponse<ProductSkuDto>>
{
	public GetSkuDetailQuery(string sku)
	{
		Sku = sku;
	}

	public string Sku { get; set; }
}
