namespace OnlineClothes.Application.Features.Products.Queries.ProductImages;

public class GetProductImageQuery : IRequest<JsonApiResponse<List<ImageDto>>>
{
	public int Id { get; set; }
}
