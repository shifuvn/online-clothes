namespace OnlineClothes.Application.Features.Products.Queries.Single;

public class GetSingleProductQuery : IRequest<JsonApiResponse<ProductBasicDto>>
{
	public int Id { get; set; }
}
