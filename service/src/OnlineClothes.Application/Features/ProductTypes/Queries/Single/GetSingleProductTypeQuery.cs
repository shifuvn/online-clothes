namespace OnlineClothes.Application.Features.ProductTypes.Queries.Single;

public class GetSingleProductTypeQuery : IRequest<JsonApiResponse<GetSingleProductTypeQueryViewModel>>
{
	public int Id { get; set; }
}
