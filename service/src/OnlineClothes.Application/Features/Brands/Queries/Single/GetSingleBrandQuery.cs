namespace OnlineClothes.Application.Features.Brands.Queries.Single;

public class GetSingleBrandQuery : IRequest<JsonApiResponse<GetSingleBrandQueryViewModel>>
{
	public GetSingleBrandQuery(int id)
	{
		Id = id;
	}

	public int Id { get; init; }
}
