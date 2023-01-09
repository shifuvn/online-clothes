namespace OnlineClothes.Application.Features.Products.Queries.Detail;

public class GetSkuDetailQuery : IRequest<JsonApiResponse<ProductSkuDto>>
{
	public GetSkuDetailQuery(string sku)
	{
		Sku = sku;
	}

	public string Sku { get; set; }
}
