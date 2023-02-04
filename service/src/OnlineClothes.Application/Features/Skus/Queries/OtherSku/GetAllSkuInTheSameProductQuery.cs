namespace OnlineClothes.Application.Features.Skus.Queries.OtherSku;

public class GetAllSkuInTheSameProductQuery : IRequest<JsonApiResponse<List<GetAllSkuInTheSameProductQueryViewModel>>>
{
	public int ProductId { get; set; }
}
